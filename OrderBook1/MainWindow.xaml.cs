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

namespace OrderBook1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Set viewmodel
        ViewModel vm = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            //Set datacontext
            DataContext = vm;
            //Clients for clients cmbox binding itemssource
            ClientsCmx.ItemsSource = vm.Clients;
            //And for orders dtg
            OrdersDtg.ItemsSource = vm.Orders;
        }
              

        

        private void SetOrderTabItm_GotFocus(object sender, RoutedEventArgs e)
        {
            MainWnd.Width = 1280;
        }

        private void SetOrderTabItm_LostFocus(object sender, RoutedEventArgs e)
        {
            //MainWnd.Width = 850;
        }

        private void OrdersTabItm_GotFocus(object sender, RoutedEventArgs e)
        {
            MainWnd.Width = 1050;
        }

        private void OrdRTxb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Remove text formatting from clipboard 
            if ((e.Key == Key.V) && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // remove text formatting in the text in clipboard
                if (Clipboard.ContainsText(TextDataFormat.Html) || Clipboard.ContainsText(TextDataFormat.Rtf))
                {
                    string plainText = Clipboard.GetText();
                    Clipboard.Clear();
                    TextMng txmng = new TextMng();

                    plainText = txmng.RemoveSecondNewLine(plainText); //plainText.Replace("\r\n", " ==== ");
                    Clipboard.SetText(plainText);
                }
            }
        }

        private void ClientBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.CurrentOrder.ClientName = OrdRTxb.Selection.Text.Trim();
            vm.AddClient(vm.CurrentOrder.ClientName);
            vm.CurrentClient = vm.Clients.Last();
            ClientsCmx.SelectedIndex = vm.CurrentClient.ListId;
            Console.AppendText(": " + vm.CurrentClient.ListId.ToString()+" # ");
            OrdRTxb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aqua);
            //OrdRTxb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aqua);

        }

        private void ClientsCmx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.CurrentClient = vm.SetCurrentClient(ClientsCmx.SelectedIndex);
            vm.CurrentOrder.ClientName = vm.CurrentClient.Name;
        }       

        private void OrdNameBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.CurrentOrder.Name = OrdRTxb.Selection.Text.Trim();
            OrdRTxb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aqua);
            Console.AppendText(vm.CurrentOrder.Name + " # ");
        }

        private void OrdNumBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.CurrentOrder.Num = OrdRTxb.Selection.Text.Trim();
            OrdRTxb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aqua);
            Console.AppendText(vm.CurrentOrder.Num + " # ");
        }

        private void OrdDeadlineBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.CurrentOrder.Deadline = OrdRTxb.Selection.Text.Trim();
            OrdRTxb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aqua);
            Console.AppendText(vm.CurrentOrder.Deadline + " # ");
        }

        private void OrdWordcountBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.CurrentOrder.WordCount = OrdRTxb.Selection.Text.Trim();
            OrdRTxb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aqua);
            Console.AppendText(vm.CurrentOrder.WordCount + " # ");
        }

        private void OrdRawWordcountBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.CurrentOrder.QtNs = OrdRTxb.Selection.Text.Trim();
            OrdRTxb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aqua);
            Console.AppendText(vm.CurrentOrder.QtNs + " # ");
        }

        private void MngNameBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.CurrentOrder.PMName = OrdRTxb.Selection.Text.Trim();
            OrdRTxb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aqua);
            Console.AppendText(vm.CurrentOrder.PMName + " # ");
        }

        private void MngLastNameBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.CurrentOrder.PMLastName = OrdRTxb.Selection.Text.Trim();
            OrdRTxb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aqua);
            Console.AppendText(vm.CurrentOrder.PMLastName + " # ");
        }

        private void TotalPriceBtn_Click(object sender, RoutedEventArgs e)
        {
            string input = OrdRTxb.Selection.Text.Trim();
            input = input.Replace('.', ',');
            //Console.AppendText("Replaced: " + input + " # ");
            try { vm.CurrentOrder.TotalPrice = Convert.ToDouble(input); }
            catch(FormatException)
            {
                MessageBox.Show("Neplatný formát." + Environment.NewLine + "Zadajte cenu ručne.");
            }


            OrdRTxb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aqua);
            Console.AppendText(vm.CurrentOrder.TotalPrice.ToString() + " # ");
        }

        private void AddOrdBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.AppendText("Počet: " + vm.Orders.Count.ToString() + " # ");
            vm.Orders.Add(vm.CurrentOrder);
            Console.AppendText(vm.Orders[0].Name + " # ");
            Console.AppendText(vm.Orders.Count.ToString() + " # ");
            Console.AppendText("Počet2: " + vm.Orders.Count.ToString() + " # ");
            //Reset source for orders dtg
            OrdersDtg.ItemsSource = "";
            OrdersDtg.ItemsSource = vm.Orders;
            Console.AppendText("CurrOrder: " + vm.CurrentOrder.Name + " # ");
            DbMng dbmng = new DbMng();
            dbmng.SaveOrder(vm.CurrentOrder);

        }

        private void MainWnd_Loaded(object sender, RoutedEventArgs e)
        {
            DbMng dbmng = new DbMng();
            vm.Orders = dbmng.ReadAllOrders();
            Console.AppendText("Loaded: " + vm.Orders.Count.ToString() + " # ");
            //Reset source for orders dtg
            OrdersDtg.ItemsSource = "";
            OrdersDtg.ItemsSource = vm.Orders;
        }
    }
}
