using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly ViewModel vm = new ViewModel();
        private readonly AppCounter apc = new AppCounter();
        public MainWindow()
        {
            InitializeComponent();
            //Set datacontext
            DataContext = vm;
            //Reads orders from DB
            DbMng dbmng = new DbMng();
            vm.Orders = dbmng.ReadAllOrders();
            //Set modified to false -> no changes
            vm.Orders = vm.SetModified(vm.Orders);
            //Reverse the order of Orders
            vm.Orders = vm.ReverseOrders(vm.Orders);
            //Read clients from db
            vm.Clients = dbmng.ReadAllClients();
            //Clients for clients cmbox binding itemssource
            ClientsCmx.ItemsSource = vm.Clients;
            //And for orders dtg
            OrdersDtg.ItemsSource = vm.Orders;
            string dateTime = DateTime.Now.ToString();
            //OrdRTxb.AppendText(dateTime);
            vm.ordNums.Add(new OrderNumber() { Id = 200, Pattern = @"\b[0-9][0-9][A-Z][A-Z][A-Z][0-9][0-9][0-9][0-9]", Client = "" });
            vm.ordNums.Add(new OrderNumber() { Id = 200, Pattern = @"\b[0-9][0-9][0-9][0-9][0-9][0-9]", Client = "" });
            vm.ordNums.Add(new OrderNumber() { Id = 200, Pattern = @"\b[A-Z][A-Z][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]", Client = "" });
            vm.ordNums.Add(new OrderNumber() { Id = 200, Pattern = @"\b[A-Z][A-Z][A-Z][0 - 9][0 - 9][0 - 9][0 - 9][0 - 9][0 - 9][0 - 9][0 - 9][\,,\., _,\-, \:][a-z][a-z][a-z][\,,\., _,\-, \:][a-z][a-z][a-z]", Client = "" });

            

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
            apc.IsOrderEdited = true;
            SaveEditedOrdBtn.Background = Brushes.Orange;
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
            DbMng dbmng = new DbMng();
            dbmng.AddClient(vm.CurrentOrder.ClientName, Console);
            dbmng.ReadAllClients();

        }

        private void ClientsCmx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.CurrentClient = vm.SetCurrentClient(ClientsCmx.SelectedIndex);
            vm.CurrentOrder.ClientName = vm.CurrentClient.Name;
            //Console.Clear();
            Console.AppendText($"Selected index {ClientsCmx.SelectedIndex} ");
            Console.AppendText($"Selected clientId {vm.CurrentClient.ListId} ");
            Console.AppendText($"Client name from order: {vm.CurrentOrder.ClientName} ");
            //ClientTb.Text = vm.CurrentClient.Name;
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
            SearchInText srtext = new SearchInText();
            string searchPattern = srtext.GetRegexExpression(vm.CurrentOrder.Num);
            //Gets the previous text and then flag            
            SearchInText inText = new SearchInText();
            //string[] words = inText.GetPreviousText(OrdRTxb);
            string flag = inText.GetFlag(OrdRTxb);
            Console.Clear();
            //Print flag
            Console.AppendText($"Flag: {flag}");
            
            
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
            
            Console.AppendText("CurrOrder: " + vm.CurrentOrder.Name + " # ");
            DbMng dbmng = new DbMng();
            try
            {
                dbmng.SaveOrder(vm.CurrentOrder);
            }
            catch
            {
                Console.Clear();
                Console.AppendText("Už bolo uložené, pomocou tlačidla 'Uložiť zmeny'");
            }
            //Reads orders from DB            
            vm.Orders = dbmng.ReadAllOrders();
            //Reverse the order of Orders
            vm.Orders = vm.ReverseOrders(vm.Orders);
            //Reset source for orders dtg
            OrdersDtg.ItemsSource = "";
            OrdersDtg.ItemsSource = vm.Orders;

            //Changes were saved
            vm.CurrentOrder.Modified = false;
            //Order was saved
            apc.IsOrderEdited = false;
            SaveEditedOrdBtn.Background = Brushes.LightGray;

        }

        private void MainWnd_Loaded(object sender, RoutedEventArgs e)
        {
            
            //vm.Orders = new ObservableCollection<Order>(vm.Orders.OrderBy(i => i));
            InfoLb.Content = "Loaded: " + vm.Orders.Count.ToString() + " # ";
            //Reset source for orders dtg
            //OrdersDtg.ItemsSource = "";
            //OrdersDtg.ItemsSource = vm.Orders;
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
            //There are changes??
            vm.CurrentOrder.Modified = true;
            //Order was not saved??
            apc.IsOrderEdited = true;
        }

        private void NewOrdBtn_Click(object sender, RoutedEventArgs e)
        {
            //Button in SetOrderTab
            if(apc.IsOrderEdited == true)
            {
                //Alert to save edited order
                MessageBoxResult result = MessageBox.Show("Objednávka bola zmenená.\n\nChcete ju uložiť?", "Pozor!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if(vm.CurrentOrder.Id < 0)
                        {                            
                            //Saves order to db and add to collection
                            DbMng dbmng = new DbMng();
                            dbmng.SaveOrder(vm.CurrentOrder);
                            vm.Orders.Add(vm.CurrentOrder);
                            vm.CurrentOrder = new Order();
                            vm.CurrentOrder.Modified = true;
                            TextMng yesTmng = new TextMng();
                            yesTmng.Clear(OrdRTxb);
                            //Sets IsInDb to true -> is saved to db                            
                            //No more changes
                            vm.CurrentOrder.Modified = false;
                            apc.IsOrderEdited = false;
                            SaveEditedOrdBtn.Background = Brushes.Gray;
                        }
                        else
                        {
                            //Updates edited order
                            DbMng dbmng = new DbMng();
                            dbmng.UpdateOrder(vm.CurrentOrder);
                            vm.CurrentOrder = new Order();
                            vm.CurrentOrder.Modified = true;
                            TextMng elseTmng = new TextMng();
                            elseTmng.Clear(OrdRTxb);
                            //No more changes
                            vm.CurrentOrder.Modified = false;
                            apc.IsOrderEdited = false;
                            SaveEditedOrdBtn.Background = Brushes.Gray;
                        }
                        
                        break;
                    case MessageBoxResult.No:
                        vm.CurrentOrder = new Order();
                        vm.CurrentOrder.Modified = true;
                        TextMng tmng = new TextMng();
                        tmng.Clear(OrdRTxb);
                        apc.IsOrderEdited = false;
                        SaveEditedOrdBtn.Background = Brushes.Gray;
                        break;
                }
            }
            else
            {
                vm.CurrentOrder = new Order();
                vm.CurrentOrder.Modified = true;
                TextMng tmng = new TextMng();
                tmng.Clear(OrdRTxb);
                apc.IsOrderEdited = false;
                SaveEditedOrdBtn.Background = Brushes.Gray;
            }
            

        }

        private void NewOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            //Button on menu bar
            if (apc.IsOrderEdited == true)
            {
                //Alert to save edited order
                MessageBoxResult result = MessageBox.Show("Objednávka bola zmenená.\n\nChcete ju uložiť?", "Pozor!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (vm.CurrentOrder.Id < 0)
                        {
                            //Saves order to db and add to collection
                            DbMng dbmng = new DbMng();
                            dbmng.SaveOrder(vm.CurrentOrder);
                            vm.Orders.Add(vm.CurrentOrder);
                            //Sets IsInDb to true -> is saved to db                            
                            //No more changes
                            vm.CurrentOrder.Modified = false;
                            apc.IsOrderEdited = false;
                            SaveEditedOrdBtn.Background = Brushes.Gray;
                        }
                        else
                        {
                            //Updates edited order
                            DbMng dbmng = new DbMng();
                            dbmng.UpdateOrder(vm.CurrentOrder);
                            //No more changes
                            vm.CurrentOrder.Modified = false;
                            apc.IsOrderEdited = false;
                            SaveEditedOrdBtn.Background = Brushes.Gray;
                        }

                        break;
                    case MessageBoxResult.No:
                        vm.CurrentOrder = new Order();
                        vm.CurrentOrder.Modified = true;
                        //Clear the richtextbox
                        TextMng tmng = new TextMng();
                        tmng.Clear(OrdRTxb);
                        apc.IsOrderEdited = false;
                        SaveEditedOrdBtn.Background = Brushes.Gray;
                        break;
                    
                }
            }
            else
            {
                vm.CurrentOrder = new Order();
                vm.CurrentOrder.Modified = true;
                TextMng tmng = new TextMng();
                tmng.Clear(OrdRTxb);
                apc.IsOrderEdited = false;
                SaveEditedOrdBtn.Background = Brushes.Gray;
            }



        }

        private void EditOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            SetOrderTabItm.Focus();
            //If nothing is processing yet???
            if(apc.IsOrderEdited == true)
            {
                //Alert to save edited order
                MessageBoxResult result = MessageBox.Show("Objednávka bola zmenená.\n\nChcete ju uložiť?", "Pozor!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (vm.CurrentOrder.Id < 0)
                        {
                            //Saves order to db and add to collection
                            DbMng dbmng = new DbMng();
                            dbmng.SaveOrder(vm.CurrentOrder);
                            vm.Orders.Add(vm.CurrentOrder);
                            //Sets IsInDb to true -> is saved to db                            
                            //No more changes
                            //vm.CurrentOrder.Modified = false;
                            //If is selected an Order
                            vm.CurrentOrder = vm.CurrentSelectedOrder;
                            //Changed
                            vm.CurrentOrder.Modified = true;
                            //apc.IsOrderEdited = true;
                            //Clear the richtextbox
                            TextMng yesTmng = new TextMng();
                            yesTmng.Clear(OrdRTxb);
                            //Reade attached pdf order if exists
                            if (vm.CurrentOrder.OrderFilePath != null)
                            {
                                FileMng fmng = new FileMng();
                                try { OrdRTxb.AppendText(fmng.ExtractTextFromPdf(vm.CurrentOrder.OrderFilePath)); }
                                catch { }

                            }
                            apc.IsOrderEdited = false;
                            SaveEditedOrdBtn.Background = Brushes.Gray;
                        }
                        else
                        {
                            //Updates edited order
                            DbMng dbmng = new DbMng();
                            dbmng.UpdateOrder(vm.CurrentOrder);
                            //No more changes
                            vm.CurrentOrder.Modified = false;
                            //If is selected an Order
                            vm.CurrentOrder = vm.CurrentSelectedOrder;
                            //Changed
                            vm.CurrentOrder.Modified = true;
                            //apc.IsOrderEdited = true;
                            //Clear the richtextbox
                            TextMng elseTmng = new TextMng();
                            elseTmng.Clear(OrdRTxb);
                            //Reade attached pdf order if exists
                            if (vm.CurrentOrder.OrderFilePath != null)
                            {
                                FileMng fmng = new FileMng();
                                try { OrdRTxb.AppendText(fmng.ExtractTextFromPdf(vm.CurrentOrder.OrderFilePath)); }
                                catch { }

                            }
                            apc.IsOrderEdited = false;
                            SaveEditedOrdBtn.Background = Brushes.Gray;
                        }

                        break;
                    case MessageBoxResult.No:
                        //If is selected an Order
                        vm.CurrentOrder = vm.CurrentSelectedOrder;
                        //Changed
                        vm.CurrentOrder.Modified = true;
                        apc.IsOrderEdited = false;
                        //Clear the richtextbox
                        TextMng tmng = new TextMng();
                        tmng.Clear(OrdRTxb);

                        //Reade attached pdf order if exists
                        if (vm.CurrentOrder.OrderFilePath != null)
                        {
                            FileMng fmng = new FileMng();
                            try { OrdRTxb.AppendText(fmng.ExtractTextFromPdf(vm.CurrentOrder.OrderFilePath)); }
                            catch { }

                        }
                        apc.IsOrderEdited = false;
                        SaveEditedOrdBtn.Background = Brushes.Gray;
                        break;
                    
                }
            }
            else
            {
                //If is selected an Order
                vm.CurrentOrder = vm.CurrentSelectedOrder;
                //Changed
                vm.CurrentOrder.Modified = true;
                //apc.IsOrderEdited = true;
                //Clear the richtextbox
                TextMng tmng = new TextMng();
                tmng.Clear(OrdRTxb);

                //Reade attached pdf order if exists
                if (vm.CurrentOrder.OrderFilePath != null)
                {
                    FileMng fmng = new FileMng();
                    try { OrdRTxb.AppendText(fmng.ExtractTextFromPdf(vm.CurrentOrder.OrderFilePath)); }
                    catch { }

                }
                apc.IsOrderEdited = false;
                SaveEditedOrdBtn.Background = Brushes.Gray;

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
            //Reads orders from DB            
            vm.Orders = dbmng.ReadAllOrders();
            //No more changes
            vm.CurrentOrder.Modified = false;
            //Reset source for orders dtg
            OrdersDtg.ItemsSource = "";
            OrdersDtg.ItemsSource = vm.Orders;
            apc.IsOrderEdited = false;
            SaveEditedOrdBtn.ClearValue(Button.BackgroundProperty);
        }

        private void UrgentChb_Checked(object sender, RoutedEventArgs e)
        {
            vm.CurrentOrder.Status = "Urg";
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            SetOrderTabItm.Focus();
            if (apc.IsOrderEdited == true)
            {
                //Alert to save edited order
                MessageBoxResult result = MessageBox.Show("Objednávka bola zmenená.\n\nChcete ju uložiť?", "Pozor!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (vm.CurrentOrder.Id < 0)
                        {
                            //Saves order to db and add to collection
                            DbMng dbmng = new DbMng();
                            dbmng.SaveOrder(vm.CurrentOrder);
                            vm.Orders.Add(vm.CurrentOrder);
                            //Sets IsInDb to true -> is saved to db                            
                            //No more changes
                            //vm.CurrentOrder.Modified = false;
                            //If is selected an Order
                            vm.CurrentOrder = vm.CurrentSelectedOrder;
                            //Changed
                            vm.CurrentOrder.Modified = true;
                            //apc.IsOrderEdited = true;
                            //Clear the richtextbox
                            TextMng yesTmng = new TextMng();
                            yesTmng.Clear(OrdRTxb);
                            //Reade attached pdf order if exists
                            if (vm.CurrentOrder.OrderFilePath != null)
                            {
                                FileMng fmng = new FileMng();
                                try { OrdRTxb.AppendText(fmng.ExtractTextFromPdf(vm.CurrentOrder.OrderFilePath)); }
                                catch { }

                            }
                            apc.IsOrderEdited = false;
                            SaveEditedOrdBtn.Background = Brushes.Gray;
                        }
                        else
                        {
                            //Updates edited order
                            DbMng dbmng = new DbMng();
                            dbmng.UpdateOrder(vm.CurrentOrder);
                            //No more changes
                            vm.CurrentOrder.Modified = false;
                            //If is selected an Order
                            vm.CurrentOrder = vm.CurrentSelectedOrder;
                            //Changed
                            vm.CurrentOrder.Modified = true;
                            //apc.IsOrderEdited = true;
                            //Clear the richtextbox
                            TextMng elseTmng = new TextMng();
                            elseTmng.Clear(OrdRTxb);
                            //Reade attached pdf order if exists
                            if (vm.CurrentOrder.OrderFilePath != null)
                            {
                                FileMng fmng = new FileMng();
                                try { OrdRTxb.AppendText(fmng.ExtractTextFromPdf(vm.CurrentOrder.OrderFilePath)); }
                                catch { }

                            }
                            apc.IsOrderEdited = false;
                            SaveEditedOrdBtn.Background = Brushes.Gray;
                        }

                        break;
                    case MessageBoxResult.No:
                        //If is selected an Order
                        vm.CurrentOrder = vm.CurrentSelectedOrder;
                        //Changed
                        vm.CurrentOrder.Modified = true;
                        //apc.IsOrderEdited = true;
                        //Clear the richtextbox
                        TextMng tmng = new TextMng();
                        tmng.Clear(OrdRTxb);
                        //Reade attached pdf order if exists
                        if (vm.CurrentOrder.OrderFilePath != null)
                        {
                            FileMng fmng = new FileMng();
                            try { OrdRTxb.AppendText(fmng.ExtractTextFromPdf(vm.CurrentOrder.OrderFilePath)); }
                            catch { }

                        }
                        apc.IsOrderEdited = false;
                        SaveEditedOrdBtn.Background = Brushes.Gray;
                        break;

                }
            }
            else
            {
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
                apc.IsOrderEdited = false;
                SaveEditedOrdBtn.Background = Brushes.Gray;
            }
        }

        private void MainWnd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DbMng dbmng = new DbMng();
            if (apc.IsOrderEdited == true && vm.CurrentOrder.Id < 0) //If new edited order
            {
                //Alert to save edited order
                MessageBoxResult result = MessageBox.Show("Objednávka bola zmenená.\n\nChcete ju uložiť?", "Pozor!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (vm.CurrentOrder.Id < 0)
                        {
                            //Saves order to db and add to collection
                            //DbMng dbmng = new DbMng();
                            dbmng.SaveOrder(vm.CurrentOrder);
                            vm.Orders.Add(vm.CurrentOrder);
                        }
                        else
                        {
                            //Updates edited order
                            //DbMng dbmng = new DbMng();
                            dbmng.UpdateOrder(vm.CurrentOrder);
                        }

                        break;
                    case MessageBoxResult.No:
                        
                        break;

                }
            }
            else
            {
                dbmng.SaveUpdates(vm.Orders);
            }
            
        }

        private void CurrencyTb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SaveEditedOrdBtn.Background = Brushes.Orange;
            apc.IsOrderEdited = true;
            
        }

        private void SearchInOrder_Click(object sender, RoutedEventArgs e)
        {
            SearchInText srtx = new SearchInText();
            string source = srtx.GetTextFromRtbx(OrdRTxb);
            (string, string, int) found = srtx.SearchOrderNumWithFlags(vm.ordNums, source);
            //SearchInText srchText = new SearchInText();
            //Gets text from rtbx
            //string rtbText = srchText.GetTextFromRtbx(OrdRTxb);

            //Searches for client in text
            //vm.CurrentClient = srchText.SearchClient(vm.Clients, Clipboard.GetText());
            vm.CurrentClient = srtx.SearchClient(vm.Clients, source);
            //string regexExpression = srchText.GetRegexExpression("OBJ0073-ac_236");
            Console.Clear();
            //Console.AppendText($"{ vm.CurrentClient.Name} id: {vm.CurrentClient.ListId}");
            //Console.AppendText($"Regex výraz: {regexExpression}");
            ClientTb.Text = vm.CurrentClient.Name;
            ClientsCmx.SelectedIndex = vm.CurrentClient.ListId;
            
            Console.AppendText($"Číslo objednávky: {found.Item1}, flag: {found.Item2}, index: {found.Item3}");
            ProjNumTb.Text = found.Item1;
        }

        private void OrdRTxb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            

        }
    }
}
