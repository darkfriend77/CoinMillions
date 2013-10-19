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
using CoinMillionsServer.Common;
using CoinMillionsServer.Core;
using CoinMillionsServer.Data;
using CoinMillionsServer.Wrapper;
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
        private Lottery lottery;

        private CoinMillionsModelContainer database;

        private bool buildChangeTxFlag;
        private bool drawNextRndFlag;

        private Info actualInfo;
        private int[] actualDraw;

        public MainWindow()
        {
            InitializeComponent();

            database = new CoinMillionsModelContainer();

            this.startTime = ConvertToTimestamp(new DateTime(2013, 10, 1, 23, 0, 0, 0));

            // initialize
            this.btc = new BitcoinQtConnector();
            this.lottery = new Lottery();

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
            foreach (Finding finding in lottery.getFindings())
                database.Findings.Add(finding);
            database.SaveChanges();

            AddLine("We have added {0} findings to the db.", database.Findings.Count());

            buildChangeTxFlag = (bool)chBxChangeTx.IsChecked;
            drawNextRndFlag = (bool)chBxDrawRnd.IsChecked;

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

            // actualize network informations for the tick
            actualInfo = btc.GetInfo();
            actualDraw = lottery.getTicketFromHash(btc.GetBlockhash(actualInfo.Blocks));

            lblActBlock.Content = actualInfo.Blocks.ToString("N0") + " [" + (2016 - (actualInfo.Blocks % 2016)) + "]";
            lblDifficulty.Content = actualInfo.Difficulty.ToString("N0");
            lblGenTicket.Content = lottery.getArrayToString(actualDraw);

            if (tickCount++ % (updateFrequency + 1) > 0)
                return;

            progressBar1.Foreground = Brushes.Green;

            tickCount %= updateFrequency;

            // check wallet for new tickets and stuff
            keepAnEyeOnWallet();

        }

        /// <summary>
        /// 
        /// </summary>
        private void keepAnEyeOnWallet()
        {
            List<TransactionDetail> allTransactions = btc.ListTransactions("", 999999, 0);
            //AddLine("alltransactions: {0}", btc.ListTransactions());

            //timer.IsEnabled = false;

            // check for paytime

            // check for new ticket transactions
            getTicketTxs(allTransactions);

            // check if need for new change transactions
            getChangeTxs(allTransactions);

            // do new change transactions
            if (buildChangeTxFlag)
                makeChangeTxs();

            lblTicketsCount.Content = database.TransactionDetails.OfType<TicketTx>().Count();
            lblChangesCount.Content = string.Format("{0} [{1}]", database.TransactionDetails.OfType<ChangeTx>().Where(t => t.Validation == true).Count(), database.TransactionDetails.OfType<ChangeTx>().Count());
            lblBlocksCount.Content = database.Blocks.Count();
            lblAmount.Content = btc.GetBalance();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allTransactions"></param>
        private void getChangeTxs(List<TransactionDetail> allTransactions)
        {
            lblStatus.Content = string.Format("getChangeTxs[{0}]", allTransactions.Count);

            foreach (TransactionDetail transaction in allTransactions)
            {
                if (isValidChangeTx(transaction))
                {
                    ChangeTx changeTx = database.TransactionDetails.OfType<ChangeTx>().Where(t => t.TxId == transaction.TxId).FirstOrDefault();

                    if (changeTx == null)
                    {
                        changeTx = new ChangeTx()
                        {
                            Account = transaction.Account,
                            Address = transaction.Address,
                            Amount = transaction.Amount,
                            BlockHash = transaction.BlockHash,
                            BlockIndex = transaction.BlockIndex,
                            BlockTime = transaction.BlockTime,
                            Category = transaction.Category,
                            Confirmations = 0,
                            Fee = transaction.Fee,
                            Time = transaction.Time,
                            TimeReceived = transaction.TimeReceived,
                            TxId = transaction.TxId,
                            Validation = false
                        };

                        RawTransaction rawtransaction = btc.GetRawTransactionObject(transaction.TxId);
                        string rawtransactionTxId = rawtransaction.Vin[0].TxId;
                        TicketTx ticketTx = database.TransactionDetails.OfType<TicketTx>().Where(t => t.TxId == rawtransactionTxId).First();

                        ticketTx.ChangeTx = changeTx;
                        database.SaveChanges();
                        AddLine("ChangeTx[{0}]: Found new changeTx!", transaction.TxId);


                    }
                    else if (!changeTx.Validation && transaction.Confirmations > 0)
                    {
                        // fill the missing values ...
                        Block block = btc.GetBlock(transaction.BlockHash);
                        changeTx.Blocks = block;
                        changeTx.Confirmations = transaction.Confirmations;
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
        private bool isValidChangeTx(TransactionDetail transaction)
        {
            // amount check
            if (transaction.Amount >= ticketCost)
                return false;

            // category check
            if (transaction.Category != "send")
                return false;

            // adress check
            if (btc.ValidateAddress(transaction.Address).IsMine)
                return false;

            // check if it's linked to a ticket transaction
            RawTransaction rawtransaction = btc.GetRawTransactionObject(transaction.TxId);
            string rawtransactionTxId = rawtransaction.Vin[0].TxId;
            if (!database.TransactionDetails.OfType<TicketTx>().Any(t => t.TxId == rawtransactionTxId))
                return false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void makeChangeTxs()
        {
            lblStatus.Content = string.Format("makeChangeTxs");

            //foreach (TicketTx ticketTx in database.TransactionDetails.OfType<TicketTx>().Where(t => t.ChangeTx == null))
            //{

            TicketTx ticketTx = database.TransactionDetails.OfType<TicketTx>().Where(t => t.ChangeTx == null).FirstOrDefault();
            if (ticketTx == null)
                return;

            string rawTransaction;
            if (btc.CreateZeroConfirmationTransaction(ticketTx.TxId, ticketCost, networkFee, out rawTransaction))
            {
                AddLine("rawTransaction[{0}]: {1}", ticketTx.TxId, true);
                SignedRawTransaction signrawtransaction = btc.SignRawTransaction(rawTransaction);
                AddLine("signedRawTransaction[{0}]: {1}", ticketTx.TxId, signrawtransaction.Complete);
                if (signrawtransaction.Complete)
                {

                    // TODO ... need to check if it was successful !!!
                    string changeTxId = btc.SendRawTransaction(signrawtransaction.Hex);

                    //ChangeTx changeTx = new ChangeTx()
                    //{
                    //    Account = "",
                    //    Address = "",
                    //    Amount = 0.0,
                    //    BlockHash = "",
                    //    BlockIndex = 0,
                    //    BlockTime = 0,
                    //    Category = "",
                    //    Confirmations = 0,
                    //    Fee = 0,
                    //    Time = 0,
                    //    TimeReceived = 0,
                    //    TxId = changeTxId,
                    //    Validation = false
                    //};

                    AddLine("changeTxId[{0}]: {1}", ticketTx.TxId, changeTxId);
                    //ticketTx.ChangeTx = changeTx;
                    //database.SaveChanges();
                }
                else
                    AddLine("signedRawTransaction[{0}]: {1}", "not completed!");
            }

            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allTransactions"></param>
        private void getTicketTxs(List<TransactionDetail> allTransactions)
        {
            lblStatus.Content = string.Format("getTicketTxs[{0}]", allTransactions.Count);

            foreach (TransactionDetail transaction in allTransactions)
            {
                string changeVoutAdress, payeeVoutAdress;
                int payeeVoutN;
                double payeeVoutValue;

                bool duplicate = database.TransactionDetails.OfType<TicketTx>().Any(t => t.TxId == transaction.TxId);

                if (isValidTicketTx(transaction) && !duplicate && btc.DeepTransactionInfo(transaction.TxId, out changeVoutAdress, out payeeVoutAdress, out payeeVoutN, out payeeVoutValue))
                {
                    AddLine("Valid Ticket: {0}", transaction.TxId);

                    Block block = btc.GetBlock(transaction.BlockHash);

                    TicketTx ticketTx = new TicketTx()
                    {
                        Account = transaction.Account,
                        Address = transaction.Address,
                        Amount = transaction.Amount,
                        BlockHash = transaction.BlockHash,
                        BlockIndex = transaction.BlockIndex,
                        Blocks = block,
                        BlockTime = transaction.BlockTime,
                        Category = transaction.Category,
                        Confirmations = transaction.Confirmations,
                        Fee = transaction.Fee,
                        Sender = changeVoutAdress,
                        Time = transaction.Time,
                        TimeReceived = transaction.TimeReceived,
                        TxId = transaction.TxId
                    };
                    database.TransactionDetails.Add(ticketTx);
                    database.SaveChanges();
                }
                //else
                //    AddLine("Invalid Ticket: {0}, Duplicate: {1}", transaction.TxId, duplicate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private bool isValidTicketTx(TransactionDetail transaction)
        {
            // amount check
            if (transaction.Amount < ticketCost || transaction.Amount >= (ticketCost * 2))
                return false;

            // category check
            if (transaction.Category != "receive")
                return false;

            // adress check
            if (!btc.ValidateAddress(transaction.Address).IsMine)
                return false;

            int[] ticket = null;
            if (!lottery.getTicketFromAmount(transaction.Amount, out ticket))
                return false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
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

        private void CheckBox_Clicked_BuildChangeTx(object sender, RoutedEventArgs e)
        {
            buildChangeTxFlag = (bool)chBxChangeTx.IsChecked;
            AddLine("Building ChangeTx? {0}", buildChangeTxFlag);
        }

        private void Button_Click_Finding(object sender, RoutedEventArgs e)
        {
            foreach (Finding finding in database.Findings)
            {
                AddLine(string.Format("N: {1}; S: {0}; P: {2}; G: {3}", finding.Numbers, finding.Stars, string.Format("{0,10:0.00000 %}", finding.Probability), string.Format("{0,8:0.000000}", finding.Gain)));
            }
            AddLine("{0} total probability", string.Format("{0,10:0.00 %}", database.Findings.Sum(s => s.Probability)));
            AddLine("{0} total gain", string.Format("{0,10:0.00 %}", database.Findings.Sum(s => s.Gain)));
        }

        private void Button_Click_Ticket(object sender, RoutedEventArgs e)
        {
            foreach (TicketTx ticketTx in database.TransactionDetails.OfType<TicketTx>())
            {
                int[] personalTicket;
                int[] randomTicket = lottery.getTicketFromHash(ticketTx.TxId);
                int splitHashLenght = lottery.getSplitHashLenght(ticketTx.TxId);
                if (lottery.getTicketFromAmount(ticketTx.Amount, out personalTicket))
                    AddLine("TicketTx[{0}] Pers.: {1} Rand.: {2} [{3}]", ticketTx.ID, lottery.getArrayToString(personalTicket), lottery.getArrayToString(randomTicket), splitHashLenght);
                else
                    AddLine("TicketTx[{0}] Pers.: {1} Rand.: {2} [{3}]", ticketTx.ID, "BAD TICKET!", lottery.getArrayToString(randomTicket), splitHashLenght);
            }
        }

        private void Button_Click_Draw(object sender, RoutedEventArgs e)
        {
            AddLine("Drawed Ticket Block[{0}] --------------------- ", actualInfo.Blocks);
            AddLine("Draw.: {0}", lottery.getArrayToString(actualDraw));

            foreach (TicketTx ticketTx in database.TransactionDetails.OfType<TicketTx>())
            {
                int pN, sN;
                AddLine("TicketTx[{0}] -------------------------- ", ticketTx.ID);

                int[] personalTicket;
                if (lottery.getTicketFromAmount(ticketTx.Amount, out personalTicket))
                {
                    int[] personalArray = lottery.compareTicket(actualDraw, personalTicket);
                    pN = personalArray[0];
                    sN = personalArray[1];
                    Finding personalFinding = database.Findings.Where(s => s.Numbers == pN && s.Stars == sN).First();
                    AddLine("Pers.: {0}", lottery.getArrayToString(personalTicket));
                    AddLine("Number: {0}, Star: {1}, Probability: {2}, Gain: {3}", personalFinding.Numbers, personalFinding.Stars, personalFinding.Probability, personalFinding.Gain);
                }

                int[] randomTicket = lottery.getTicketFromHash(ticketTx.TxId);
                int[] RandomArray = lottery.compareTicket(actualDraw, randomTicket);
                pN = RandomArray[0];
                sN = RandomArray[1];
                Finding randomFinding = database.Findings.Where(s => s.Numbers == pN && s.Stars == sN).First();

                AddLine("Rand.: {0}", lottery.getArrayToString(randomTicket));
                AddLine("Number: {0}, Star: {1}, Probability: {2}, Gain: {3}", randomFinding.Numbers, randomFinding.Stars, randomFinding.Probability, randomFinding.Gain);
            }
        }

        private void CheckBox_Clicked_DrawNextRound(object sender, RoutedEventArgs e)
        {
            drawNextRndFlag = (bool)chBxDrawRnd.IsChecked;
            AddLine("Draw next rnd.? {0}", drawNextRndFlag);
        }

    }
}
