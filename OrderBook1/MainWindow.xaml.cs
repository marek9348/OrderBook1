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
            /*try
            {
                var row_list = (Order)OrdersDtg.SelectedItem;

                vm.CurrentOrder = row_list;
            }
            catch { }*/
            //Console.AppendText(vm.CurrentOrder.Name + " # ");
        }

        private void SetOrderTabItm_LostFocus(object sender, RoutedEventArgs e)
        {
            //MainWnd.Width = 850;
            //Find selected row
            
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

        //Add new order to list and save it to db
        private void AddOrdBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.AppendText("Počet: " + vm.Orders.Count.ToString() + " # ");
            vm.Orders.Insert(0, vm.CurrentOrder);
            Console.AppendText(vm.Orders[0].Name + " # ");
            Console.AppendText(vm.Orders.Count.ToString() + " # ");
            Console.AppendText("Počet2: " + vm.Orders.Count.ToString() + " # ");
            //Reset source for orders dtg
            OrdersDtg.ItemsSource = "";
            OrdersDtg.ItemsSource = vm.Orders;
            Console.AppendText("CurrOrder: " + vm.CurrentOrder.Name + " # ");
            DbMng dbmng = new DbMng();
            dbmng.SaveOrder(vm.CurrentOrder);
            //Changes were saved
            vm.CurrentOrder.Modified = false;

        }

        private void MainWnd_Loaded(object sender, RoutedEventArgs e)
        {
            DbMng dbmng = new DbMng();
            vm.Orders = dbmng.ReadAllOrders();
            //Set modified to false -> no changes
            vm.Orders = vm.SetModified(vm.Orders);
            //Reverse the order of Orders
            vm.Orders.Reverse();
            Console.AppendText("Loaded: " + vm.Orders.Count.ToString() + " # ");
            //Reset source for orders dtg
            OrdersDtg.ItemsSource = "";
            OrdersDtg.ItemsSource = vm.Orders;
        }

        private void StatusBtn_Click(object sender, RoutedEventArgs e)
        {
            //Find selected row
            try
            {
                var row_list = (Order)OrdersDtg.SelectedItem;

                row_list.SetStatus();
                row_list.Modified = true;
            }
            catch { }
        }

        private void OrdersDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Find selected row
            try
            {
                var row_list = (Order)OrdersDtg.SelectedItem;

                vm.CurrentSelectedOrder = row_list;
                if (vm.CurrentSelectedOrder != null)
                {
                    InfoLb.Content = vm.CurrentSelectedOrder.ClientName;
                }
            }
            catch { }
        }

        private void AddOrdFileBtn_Click(object sender, RoutedEventArgs e)
        {
            //Read text from pdf
            FileMng fmng = new FileMng();
            //Init Text mng for contain the text
            TextMng tmng = new TextMng();
            tmng.Text = fmng.OpenFile();
            //Set current file path
            vm.CurrentOrder.OrderFilePath = fmng.CurrentFilePath;
            //Add text to rtbx
            tmng.AddText(OrdRTxb);
            //No more changes
            vm.CurrentOrder.Modified = false;
        }

        private void NewOrdBtn_Click(object sender, RoutedEventArgs e)
        {
            //Button in SetOrderTab
            //Alert about saving new order!!! ToDo
            vm.CurrentOrder = new Order();
            vm.CurrentOrder.Modified = true;
            TextMng tmng = new TextMng();
            tmng.Clear(OrdRTxb);

        }

        private void NewOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            //Button on menu bar
            //Alert about saving new order!!! ToDo
            vm.CurrentOrder = new Order();
            vm.CurrentOrder.Modified = true;
            TextMng tmng = new TextMng();
            tmng.Clear(OrdRTxb);

        }

        private void EditOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            SetOrderTabItm.Focus();
            //If nothing is processing yet???
            //If is selected an Order
            vm.CurrentOrder = vm.CurrentSelectedOrder;
            //Changed
            vm.CurrentOrder.Modified = true;
            //Reade attached pdf order if exists
            if (vm.CurrentOrder.OrderFilePath!= null)
            {
                FileMng fmng = new FileMng();
                try { OrdRTxb.AppendText(fmng.ExtractTextFromPdf(vm.CurrentOrder.OrderFilePath)); }
                catch { }
                
            }
        }

        private void OrderPathBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveEditedOrdBtn_Click(object sender, RoutedEventArgs e)
        {
            //Update changes in DB
            DbMng dbmng = new DbMng();
            dbmng.UpdateOrder(vm.CurrentOrder);
            //No more changes
            vm.CurrentOrder.Modified = false;
            //Reset source for orders dtg
            OrdersDtg.ItemsSource = "";
            OrdersDtg.ItemsSource = vm.Orders;
        }

        private void UrgentChb_Checked(object sender, RoutedEventArgs e)
        {
            vm.CurrentOrder.Status = "Urg";
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            SetOrderTabItm.Focus();
            //If nothing is processing yet???
            //If is selected an Order
            vm.CurrentOrder = vm.CurrentSelectedOrder;
            //Changed
            vm.CurrentOrder.Modified = true;
            //Reade attached pdf order if exists
            if (vm.CurrentOrder.OrderFilePath != null)
            {
                FileMng fmng = new FileMng();
                try { OrdRTxb.AppendText(fmng.ExtractTextFromPdf(vm.CurrentOrder.OrderFilePath)); }
                catch { }

            }
        }

        private void MainWnd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DbMng dbmng = new DbMng();
            dbmng.SaveUpdates(vm.Orders);
        }
    }
}
