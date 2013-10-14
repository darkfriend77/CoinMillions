using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using BitcoinWrapper.Data;
using BitcoinWrapper.Wrapper;
using CoinMillions.Core;
using Newtonsoft.Json.Linq;

namespace CoinMillionsServer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const double ticketCost = 0.01;
        private const double networkFee = 0.0001;
        private const int updateFrequency = 100;


        private int tickCount;
        private double startTime;

        private DispatcherTimer timer;

        private BitcoinQtConnector btc;
        private NumberAlgorithm numbAlgo;

        private CoinMillionsModelContainer database;



        public MainWindow()
        {
            InitializeComponent();

            database = new CoinMillionsModelContainer();

            this.startTime = ConvertToTimestamp(new DateTime(2013, 10, 1, 23, 0, 0, 0));

            // initialize
            this.numbAlgo = new NumberAlgorithm();
            this.btc = new BitcoinQtConnector();


            Loaded += OnLoaded;


            DoThingsNow();
        }

        private void DoThingsNow()
        {
            //int[] array = numbAlgo.getTicketFromValue(0.01765493);
            //string ticket = numbAlgo.getArrayToString(array);
            //AddLine(ticket);
        }


        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            progressBar1.Foreground = Brushes.Red;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            tickCount = 0;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += TimerOnTick;
            timer.IsEnabled = true;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            lblStatus.Content = string.Format("waiting");

            progressBar1.Value = tickCount;

            Info info = btc.GetInfo();
            lblActBlock.Content = info.Blocks;
            lblDifficulty.Content = info.Difficulty;
            lblGenTicket.Content = numbAlgo.getArrayToString(numbAlgo.getTicketFromHash(btc.GetBlockhash(info.Blocks)));

            if (tickCount++ % (updateFrequency + 1) > 0)
                return;

            progressBar1.Foreground = Brushes.Green;

            tickCount %= updateFrequency;
            progressBar1.Value = 0;

            keepAnEyeOnWallet();

            progressBar1.Foreground = Brushes.Red;
        }

        private void keepAnEyeOnWallet()
        {
            List<Transaction> allTransactions = btc.ListTransactionsByCategory();
            //AddLine("alltransactions: {0}", btc.ListTransactions());

            //timer.IsEnabled = false;

            // check for paytime

            // check for new ticket transactions
            getTicketTxs(allTransactions);

            // check if need for new change transactions
            getChangeTxs(allTransactions);

            // do new change transactions
            makeChangeTxs();

            lblTicketCount.Content = database.TicketTxes.Count();
            lblAmount.Content = btc.GetBalance();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allTransactions"></param>
        private void getChangeTxs(List<Transaction> allTransactions)
        {
            lblStatus.Content = string.Format("getChangeTxs[{0}]", allTransactions.Count);

            foreach (Transaction transaction in allTransactions)
            {
                progressBar1.Value += 1;

                if (isValidChangeTx(transaction))
                {
                    ChangeTx changeTx = database.ChangeTxes.Where(t => t.TxId == transaction.TxId).FirstOrDefault();
                    if (changeTx == null)
                    {
                        RawTransaction rawtransaction = btc.GetRawTransactionObject(transaction.TxId);
                        string rawtransactionTxId = rawtransaction.Vin[0].TxId;
                        TicketTx ticketTx = database.TicketTxes.Where(t => t.TxId == rawtransactionTxId).First();
                        ticketTx.ChangeTx = new ChangeTx()
                        {
                            TxId = transaction.TxId,
                            Validation = false,
                            TicketTx = ticketTx
                        };
                        database.SaveChanges();
                        AddLine("ChangeTx[{0}]: Found new changeTx!", transaction.TxId);

                    }
                    else if (!changeTx.Validation)
                    {
                        changeTx.Validation = true;
                        database.SaveChanges();
                        AddLine("ChangeTx[{0}]: Validation okay!", transaction.TxId);
                    }
                }

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private bool isValidChangeTx(Transaction transaction)
        {
            // amount check
            if (transaction.Amount >= ticketCost)
                return false;

            // category check
            if (transaction.Details[0].Category != "send")
                return false;

            // check if it's linked to a ticket transaction
            RawTransaction rawtransaction = btc.GetRawTransactionObject(transaction.TxId);
            string rawtransactionTxId = rawtransaction.Vin[0].TxId;
            if (!database.TicketTxes.Any(t => t.TxId == rawtransactionTxId))
                return false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void makeChangeTxs()
        {
            lblStatus.Content = string.Format("makeChangeTxs");

            foreach (TicketTx ticketTx in database.TicketTxes.Where(t => t.ChangeTx == null))
            {
                string rawTransaction;
                if (btc.CreateZeroConfirmationTransaction(ticketTx.TxId, ticketCost, networkFee, out rawTransaction))
                {
                    AddLine("rawTransaction[{0}]: {1}", ticketTx.TxId, true);
                    SignedRawTransaction signrawtransaction = btc.SignRawTransaction(rawTransaction);
                    AddLine("signedRawTransaction[{0}]: {1}", ticketTx.TxId, signrawtransaction.Complete);
                    if (signrawtransaction.Complete)
                    {
                        string changeTxId = btc.SendRawTransaction(signrawtransaction.Hex);
                        AddLine("changeTxId[{0}]: {1}", ticketTx.TxId, changeTxId);
                        ticketTx.ChangeTx = new ChangeTx()
                        {
                            TxId = changeTxId,
                            Validation = false,
                            TicketTx = ticketTx
                        };
                        database.SaveChanges();
                    }
                    else
                        AddLine("signedRawTransaction[{0}]: {1}", "not completed!");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allTransactions"></param>
        private void getTicketTxs(List<Transaction> allTransactions)
        {
            lblStatus.Content = string.Format("getTicketTxs[{0}]", allTransactions.Count);

            foreach (Transaction transaction in allTransactions)
            {
                progressBar1.Value += 1;

                string changeVoutAdress, payeeVoutAdress;
                int payeeVoutN;
                double payeeVoutValue;

                bool duplicate = database.TicketTxes.Any(t => t.TxId == transaction.TxId);

                if (isValidTicketTx(transaction) && !duplicate && btc.DeepTransactionInfo(transaction.TxId, out changeVoutAdress, out payeeVoutAdress, out payeeVoutN, out payeeVoutValue))
                {
                    AddLine("Valid Ticket: {0}", transaction.TxId);

                    database.TicketTxes.Add(new TicketTx()
                    {
                        TxId = transaction.TxId,
                        Amount = transaction.Amount,
                        Sender = changeVoutAdress
                    });
                    database.SaveChanges();
                }
                //else
                //    AddLine("Invalid Ticket: {0}, Duplicate: {1}", transaction.TxId, duplicate);
            }
        }

        private bool isValidTicketTx(Transaction transaction)
        {
            // amount check
            if (transaction.Amount < ticketCost || transaction.Amount >= (ticketCost * 2))
                return false;

            // category check
            if (transaction.Details[0].Category != "receive")
                return false;

            return true;
        }

        public void AddLine(string text, params object[] args)
        {
            outputBox.AppendText(DateTime.Now.ToLongTimeString() + " # " + string.Format(text, args));
            outputBox.AppendText("\u2028"); // Linebreak, not paragraph break
            outputBox.ScrollToEnd();
        }

        private double ConvertToTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return span.TotalSeconds;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(TicketTx ticketTx in database.TicketTxes)
            {
                int[] personalTicket = numbAlgo.getTicketFromValue(ticketTx.Amount);
                int[] randomTicket = numbAlgo.getTicketFromHash(ticketTx.TxId);
                int splitHashLenght = numbAlgo.getSplitHashLenght(ticketTx.TxId);
                AddLine("TicketTx[{0}] Pers.: {1} Rand.: {2} [{3}]", ticketTx.ID, numbAlgo.getArrayToString(personalTicket), numbAlgo.getArrayToString(randomTicket), splitHashLenght);
            }
        }

    }
}
