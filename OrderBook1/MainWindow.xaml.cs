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
            MainWnd.Width = 850;
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
            Console.AppendText(vm.CurrentClient.ListId.ToString());

        }

        private void ClientsCmx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.CurrentClient = vm.SetCurrentClient(ClientsCmx.SelectedIndex);
            vm.CurrentOrder.ClientName = vm.CurrentClient.Name;
        }
    }
}
