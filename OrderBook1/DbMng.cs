using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderBook1
{
    public class DbMng
    {
        public List<Order> ReadAllOrders()
        {
            //Result variable
            List<Order> result = new List<Order>();
            using (var db = new OrderBookContext())
            {
                // Read                
                var orders = db.Orders.ToList();
                result = orders;
            }

            return result;
        }

        public void SaveOrder(Order order)
        {
            using (var db = new OrderBookContext())
            {
                db.Orders.Add(order);
                Order ord = new Order();
                //ord.Name = "Marek";
                //db.Orders.Add(ord);
                db.SaveChanges();
            }

        }

        public void UpdateOrder(Order order)
        {
            using (var db = new OrderBookContext())
            {
                db.Update(order);
                db.SaveChanges();
            }
        }

        public void SaveUpdates(List<Order> orders)
        {
            foreach(Order ord in orders)
            {
                if(ord.Modified == true)
                {
                    UpdateOrder(ord);
                }
            }
        }
    }
}
