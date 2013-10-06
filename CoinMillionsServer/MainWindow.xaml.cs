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

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            DoThingsNow();
        }

        private void DoThingsNow()
        {

            NumberAlgorithm numbAlgo = new NumberAlgorithm();

            BitcoinQtConnector btc = new BitcoinQtConnector();

            int[] array = numbAlgo.getTicketFromValue(0.01765493);

            string ticket = numbAlgo.getArrayToString(array);

            AddLine(ticket);
        }


        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //var timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            //timer.Tick += TimerOnTick;
            //timer.IsEnabled = true;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
        }

        public void AddLine(string text)
        {
            outputBox.AppendText(text);
            //outputBox.AppendText("\u2028"); // Linebreak, not paragraph break
            outputBox.ScrollToEnd();
        }

    }
}
