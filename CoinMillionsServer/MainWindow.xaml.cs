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
        private const int transactionConfirmations = 6;
        private double balanceDrawAmount = 0;
        private int ticketDrawAmount = 4;
        private int blockDrawSpacer = 1; // TODO: check if needed

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
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += TimerOnTick;
            timer.IsEnabled = true;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            lblStatus.Content = string.Format("waiting");

            progressBar1.Value = tickCount;

            // actualize network informations for the tick
            actualInfo = btc.GetInfo();
            actualDraw = lottery.getTicketFromHash(btc.GetBlock(btc.GetBlockhash(actualInfo.Blocks)).MerkleRoot);

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

            // TODO: check for paytime

            // check for new ticket transactions
            getTicketTxs(allTransactions);

            // check if need for new change transactions
            getChangeTxs(allTransactions);

            // do new change transactions
            if (buildChangeTxFlag)
                makeChangeTxs();

            lblTicketTxsCount.Content = getTransactionOfType(Type.Ticket).Count();
            lblTicketsCount.Content = database.Tickets.Count();
            lblChangeTxsCount.Content = string.Format("{0} [{1}]", database.TransactionDetails.OfType<ChangeTx>().Where(t => t.Validation == true).Count(), database.TransactionDetails.OfType<ChangeTx>().Count());
            lblBlocksCount.Content = database.Blocks.Count();
            lblAmount.Content = btc.GetBalance();

            // do a draw
            if (drawNextRndFlag)
                doDrawNextRound();

            // validate open drawBlocks
            validateDrawBlocks();

            // process valid drawblocks
            processValidDrawBlocks();

            // TODO: add block information updater
        }

        /// <summary>
        /// 
        /// </summary>
        private void processValidDrawBlocks()
        {
            foreach (DrawBlock drawBlock in database.Blocks.OfType<DrawBlock>().Where(b => b.State == State.Valid))
            {

                // TODO handle payout and accounting!!!

                drawBlock.State = State.Process;
                database.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void validateDrawBlocks()
        {
            foreach (DrawBlock drawBlock in database.Blocks.OfType<DrawBlock>().Where(b => b.State == State.Open))
            {
                Block block = btc.GetBlock(drawBlock.Hash);
                if (block.Confirmations < transactionConfirmations)
                    continue;

                // set state to valid
                drawBlock.Confirmations = block.Confirmations;
                drawBlock.State = State.Valid;
                database.SaveChanges();

                AddLine("{0}[{1}]: State changed to {2}", drawBlock.GetType(), smallHashTag(drawBlock.Hash), drawBlock.State);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void doDrawNextRound()
        {
            Block block;
            if (!isValidDrawRound() || !getValidDrawBlock(out block))
                return;

            AddLine("We've a valid block! (Block: {0})", smallHashTag(block.Hash));
            //AddLine("BlockHeight: {0})", block.Height);
            //int[] drawTicket = lottery.getTicketFromHash(block.MerkleRoot);
            //TicketTx ticket = database.TransactionDetails.OfType<TicketTx>()
            //    .Where(t => t.DrawBlock == null)
            //    .OrderByDescending(t => t.BlockIndex).FirstOrDefault();
            //AddLine("ticket.BlockIndex: {0})", ticket.BlockIndex);

            DrawBlock drawBlock = new DrawBlock()
            {
                Bits = block.Bits,
                Confirmations = block.Confirmations,
                Difficulty = block.Difficulty,
                Hash = block.Hash,
                Height = block.Height,
                MerkleRoot = block.MerkleRoot,
                NextBlockHash = block.NextBlockHash,
                Nonce = block.Nonce,
                Pot = 0,
                PreviousBlockHash = block.PreviousBlockHash,
                Time = block.Time,
                Version = block.Version,
                State = State.Open
            };

            AddLine("{0}[{1}]: State changed to {2}", drawBlock.GetType(), smallHashTag(drawBlock.Hash), drawBlock.State);

            int[] drawTicket = lottery.getTicketFromHash(block.MerkleRoot);

            drawBlock.Tickets = new Ticket()
            {
                TicketString = lottery.getTicketStringFromTicket(drawTicket),
                State = State.Valid

            };

            // assign tickets ... to the draw block
            foreach (TicketTx ticketTx in getTransactionOfType(Type.Ticket).Where(t => t.DrawBlock == null && t.BlockIndex <= (drawBlock.Height - blockDrawSpacer)))
            {
                drawBlock.TicketTxes.Add(ticketTx);
                ticketTx.State = State.Assign;

                // assign finding ... to the ticket
                foreach (Ticket ticket in ticketTx.Tickets)
                {
                    int[] numbersAndStars = lottery.compareTicket(drawTicket, lottery.getTicketFromTicketString(ticket.TicketString));
                    int pN = numbersAndStars[0];
                    int sN = numbersAndStars[1];
                    ticket.Findings = database.Findings.Where(s => s.Numbers == pN && s.Stars == sN).First();
                    AddLine("ticket[{0}]: -> {2} N - {3} S", ticket.ID, ticket.TicketString, ticket.Findings.Numbers, ticket.Findings.Stars);
                }

                AddLine("{0}[{1}]: State changed to {2}", ticketTx.GetType(), smallHashTag(ticketTx.TxId), ticketTx.State);
            }

            database.Blocks.Add(drawBlock);
            database.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        private bool getValidDrawBlock(out Block block)
        {
            block = null;
            TicketTx ticket = getTransactionOfType(Type.Ticket)
                .Where(t => t.DrawBlock == null)
                .OrderByDescending(t => t.BlockIndex).FirstOrDefault();

            if (ticket == null || actualInfo.Blocks <= ticket.BlockIndex)
                return false;

            Block checkBlock = btc.GetBlock(btc.GetBlockhash(ticket.BlockIndex + blockDrawSpacer));

            if (checkBlock.Confirmations < 6)
                return false;

            block = checkBlock;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool isValidDrawRound()
        {
            IQueryable<TicketTx> validTickets = getTransactionOfType(Type.Ticket).Where(t => t.State == State.Valid);

            // TODO: check if this validation is correct approach
            if (ticketDrawAmount > 0 && validTickets.Count() < ticketDrawAmount)
                return false;

            double balanceAmount = validTickets.Any() ? validTickets.Sum(t => t.Amount) : 0;
            if (balanceDrawAmount > 0 && balanceAmount < balanceDrawAmount)
                return false;

            AddLine("We've a valid draw round incoming now! (Balance: {0}) (Tickets: {1})!", balanceAmount, validTickets.Count());
            return true;
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
                            Validation = false,
                            State = State.Open
                        };

                        RawTransaction rawtransaction = btc.GetRawTransactionObject(transaction.TxId);
                        string rawtransactionTxId = rawtransaction.Vin[0].TxId;
                        TicketTx ticketTx = getTransactionOfType(Type.Ticket).Where(t => t.TxId == rawtransactionTxId).First();

                        ticketTx.ChangeTx = changeTx;
                        database.SaveChanges();

                        AddLine("{0}[{1}]: State changed to {2}", changeTx.GetType(), smallHashTag(changeTx.TxId), changeTx.State);
                    }
                    else if (!changeTx.Validation && transaction.Confirmations >= transactionConfirmations)
                    {
                        // fill the missing values ...
                        Block block = btc.GetBlock(transaction.BlockHash);
                        changeTx.Blocks = block;
                        changeTx.Confirmations = transaction.Confirmations;
                        changeTx.Validation = true; //TODO: probably obsolet
                        changeTx.State = State.Valid;
                        database.SaveChanges();

                        AddLine("{0}[{1}]: State changed to {2}", changeTx.GetType(), smallHashTag(changeTx.TxId), changeTx.State);
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
            if (!getTransactionOfType(Type.Ticket).Any(t => t.TxId == rawtransactionTxId))
                return false;

            return true;
        }

        private IQueryable<TicketTx> getTransactionOfType(Type type)
        {
            return database.TransactionDetails.OfType<TicketTx>().Where(t => t.Type == type);
        }

        /// <summary>
        /// 
        /// </summary>
        private void makeChangeTxs()
        {
            lblStatus.Content = string.Format("makeChangeTxs");

            //foreach (TicketTx ticketTx in database.TransactionDetails.OfType<TicketTx>().Where(t => t.ChangeTx == null))
            //{

            TicketTx ticketTx = getTransactionOfType(Type.Ticket).Where(t => t.ChangeTx == null).FirstOrDefault();

            if (ticketTx == null)
                return;

            double networkFeeTx = networkFee;
            double amountToKeepTx = ticketCost;
            if (ticketTx.Amount == ticketCost)
            {
                AddLine("We've a single ticket transaction ....");
                networkFeeTx = networkFee / 2;
                amountToKeepTx = ticketCost - networkFeeTx;
            }

            string rawTransaction;
            if (btc.CreateZeroConfirmationTransaction(ticketTx.TxId, amountToKeepTx, networkFeeTx, out rawTransaction))
            {
                AddLine("rawTransaction[{0}]: {1}", smallHashTag(ticketTx.TxId), true);
                SignedRawTransaction signrawtransaction = btc.SignRawTransaction(rawTransaction);
                AddLine("signedRawTransaction[{0}]: {1}", smallHashTag(ticketTx.TxId), signrawtransaction.Complete);

                if (!signrawtransaction.Complete)
                    AddLine("signedRawTransaction[{0}]: {1}", smallHashTag(ticketTx.TxId), "not completed!");

                // TODO ... need to check if it was successful !!!
                string changeTxId = btc.SendRawTransaction(signrawtransaction.Hex);

                AddLine("changeTxId[{0}]: {1}", smallHashTag(ticketTx.TxId), smallHashTag(changeTxId));
            }
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

                TicketTx duplicate = getTransactionOfType(Type.Ticket).FirstOrDefault(t => t.TxId == transaction.TxId);

                if (isValidTicketTx(transaction) && duplicate == null && btc.DeepTransactionInfo(transaction.TxId, out changeVoutAdress, out payeeVoutAdress, out payeeVoutN, out payeeVoutValue))
                {
                    Block block = null;
                    if (transaction.BlockHash != null)
                        block = btc.GetBlock(transaction.BlockHash);

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
                        TxId = transaction.TxId,
                        State = State.Open,
                        Type = Type.Ticket
                    };

                    AddLine("{0}[{1}]: State changed to {2}", ticketTx.GetType(), smallHashTag(ticketTx.TxId), ticketTx.State);

                    // TODO: add multiple ticket transactions

                    // random ticket from transaction hash
                    ticketTx.Tickets.Add(new Ticket()
                    {
                        TicketString = lottery.getTicketStringFromTicket(lottery.getTicketFromHash(ticketTx.TxId)),
                        State = State.None
                    });

                    // personal ticket from transaction value
                    int[] personalTicket;
                    if (lottery.getTicketFromAmount(transaction.Amount, out personalTicket))
                    {
                        ticketTx.Tickets.Add(new Ticket()
                        {
                            TicketString = lottery.getTicketStringFromTicket(personalTicket),
                            State = State.None
                        });
                    }

                    database.TransactionDetails.Add(ticketTx);
                    database.SaveChanges();
                }
                else if (duplicate != null && duplicate.State == State.Open && transaction.Confirmations >= transactionConfirmations)
                {
                    duplicate.Blocks = btc.GetBlock(transaction.BlockHash);
                    duplicate.State = State.Valid;
                    database.SaveChanges();

                    AddLine("{0}[{1}]: State changed to {2}", duplicate.GetType(), smallHashTag(duplicate.TxId), duplicate.State);
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
            if (!lottery.getTicketFromAmount(transaction.Amount, out ticket) && transaction.Amount != ticketCost)
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
            foreach (TicketTx ticketTx in getTransactionOfType(Type.Ticket))
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

            foreach (TicketTx ticketTx in getTransactionOfType(Type.Ticket))
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

        private string smallHashTag(string hash)
        {
            if (hash.Length < 10)
                return hash;
            return hash.Substring(0, 5) + hash.Substring(hash.Length - 5, 5);
        }

    }
}
