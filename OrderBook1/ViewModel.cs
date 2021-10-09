using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBook1
{
    public class ViewModel
    {
        public List<Order> Orders { get; set; }
        public List<Client> Clients { get; set; }
        //Current order to show -> for beginning should be blank order
        public Order CurrentOrder { get; set; }
        //Current client to bind to combo
        public Client CurrentClient { get; set; }

        public ViewModel()
        {
            Orders = new List<Order>();
            Clients = new List<Client>();
            CurrentOrder = new Order();
            CurrentClient = new Client();

        }

        public void AddClient(string clientName)
        {
            if (!ClientExist(clientName))
            {
                Client cl = new Client();
                cl.Name = clientName;
                cl.ListId = Clients.Count;
                Clients.Add(cl);
            }
        }

        private bool ClientExist(string clientName)
        {
            bool result = false;
            //List to store client names
            List<string> clientNames = new List<string>();
            //Get client names
            foreach(Client client in Clients)
            {
                if(client.Name == clientName)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }
        public Client SetCurrentClient(int id)
        {
            Client result = new Client();
            foreach(Client cl in Clients)
            {
                if(id == cl.ListId)
                {
                    result = cl;
                }
            }

            return result;
        }
    }
}
