using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace OrderBook1
{
    public class DbMng
    {
        public ObservableCollection<Order> ReadAllOrders()
        {
            //Result variable
            List<Order> result = new List<Order>();
            using (var db = new OrderBookContext())
            {
                // Read                
                var orders = db.Orders.ToList();
                result = orders;
            }
            //Convert to ObservableCollection
            ObservableCollection<Order> observResult = new ObservableCollection<Order>();
            foreach(Order ord in result)
            {
                observResult.Add(ord);
            }
            

            return observResult;
        }

        public void SaveOrder(Order order)
        {
            using var db = new OrderBookContext();
            
                db.Orders.Add(order);            
                db.SaveChanges();            

        }

        public void UpdateOrder(Order order)
        {
            using (var db = new OrderBookContext())
            {
                db.Update(order);
                db.SaveChanges();
            }
        }

        public void SaveUpdates(ObservableCollection<Order> orders)
        {
            foreach(Order ord in orders)
            {
                if(ord.Modified == true)
                {
                    UpdateOrder(ord);
                }
            }
        }
        /// <summary>
        /// Read all clients from db
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Client> ReadAllClients()
        {
            //Result variable
            //List<Client> clientsList = new List<Client>();
            using var db = new OrderBookContext();
            // Read                
            var clientsList = db.Clients.ToList();
             
            
            //Convert to ObservableCollection
            ObservableCollection<Client> clientsObserv = new ObservableCollection<Client>();
            foreach (Client cl in clientsList)
            {
                clientsObserv.Add(cl);
            }
            //Set ListId
            int i = 0;
            foreach (Client cl in clientsObserv)
            {
                cl.ListId = i;
                i++;
            }
            //Delete first empty item
            //clientsObserv.RemoveAt(0);
            return clientsObserv;
        }
        /// <summary>
        /// Add new client to db
        /// </summary>
        /// <param name="name">string name of the client</param>
        public void AddClient(string name, TextBox tbx)
        {
            using var db = new OrderBookContext();
            if (name != "")
            {
                Client cl = new Client
                {

                    Name = name,
                    CompareName = name.ToLower()


                };

                try
                {
                    db.Clients.Add(cl);
                    db.SaveChanges();
                }
                catch
                {
                    tbx.AppendText("Klient už existuje");
                }
            }
        }
    }
}
