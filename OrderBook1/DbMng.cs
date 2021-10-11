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
    }
}
