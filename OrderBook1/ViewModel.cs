using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace OrderBook1
{
    public class ViewModel : INotifyPropertyChanged
    {
        //Current new order or edited order
        private Order _currentOrder = new Order();
        //Current order selected in datagrid
        private Order _currentSelectedOrder = new Order();
        //Current selected client in combobox
        private Client _currentClient = new Client();
        private ObservableCollection<Order> _orders = new ObservableCollection<Order>();
        public ObservableCollection<Order> Orders {
            get { return _orders; }
            set
            {
                _orders = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("Orders");
            }
        }

        //Properties
        public ObservableCollection<Client> Clients { get; set; }
        //Current order to show in richtextbox for edit-> for beginning should be blank order
        public Order CurrentOrder {
            get { return _currentOrder; }
            set
            {
                _currentOrder = value;
                OnPropertyChanged("CurrentOrder");
            }
        }
        //Current client to show in combobox and use for order
        public Client CurrentClient
        {
            get { return _currentClient; }
            set
            {
                _currentClient = value;
                OnPropertyChanged("CurrentClient");
            }
        }
        public Order CurrentSelectedOrder
        {
            get { return _currentSelectedOrder; }
            set { 
                _currentSelectedOrder = value;
                OnPropertyChanged("CurrentSelectedOrder");
            }
        }        
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            //throw new NotImplementedException();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
            //MessageBox.Show("Changed to " + this.Client);
        }

        public ViewModel()
        {
            //Orders = new List<Order>();
            Clients = new ObservableCollection<Client>();
            CurrentOrder = new Order();
            CurrentClient = new Client();

        }

        /// <summary>
        /// Add new client to Clients collection
        /// </summary>
        /// <param name="clientName"></param>
        public void AddClient(string clientName)
        {
            if (!ClientExist(clientName) && clientName != "")
            {
                Client cl = new Client();
                cl.Name = clientName;
                cl.ListId = Clients.Count;
                Clients.Add(cl);
            }
            
        }
        /// <summary>
        /// Check if new client is in clients
        /// </summary>
        /// <param name="clientName">string name of the client</param>
        /// <returns>Bool</returns>
        private bool ClientExist(string clientName)
        {
            bool exists = false;
            //List to store client names
            //List<string> clientNames = new List<string>();
            //Get client names
            foreach(Client client in Clients)
            {
                if(client.Name.ToLower() == clientName.ToLower())
                {
                    exists = true;
                }
                else
                {
                    exists = false;
                }
            }

            return exists;
        }
        /// <summary>
        /// Set the current client
        /// </summary>
        /// <param name="id">Id of client object</param>
        /// <returns></returns>
        public Client SetCurrentClient(int id)
        {
            Client currentClient = new Client();
            foreach(Client cl in Clients)
            {
                if(id == cl.ListId)
                {
                    currentClient = cl;
                }
            }

            return currentClient;
        }
        public ObservableCollection<Order> SetModified(ObservableCollection<Order> orders)
        {
            foreach(Order ord in orders)
            {
                ord.Modified = false;
            }
            return orders;
        }
                

        public ObservableCollection<Order> ReverseOrders(ObservableCollection<Order> orders)
        {
            ObservableCollection<Order> result = new ObservableCollection<Order>();
            for(int i = orders.Count-1; i >= 0; i--)
            {
                result.Add(orders[i]);
            }
            //MessageBox.Show(orders.Count.ToString());
            //result.Add(orders[0]);
            return result;
        }

        
    }
}
