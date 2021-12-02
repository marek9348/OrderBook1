using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBook1
{
    public class OrderNumber
    {
        public int Id { get; set; }
        public string Pattern { get; set; }
        public string Client { get; set; }

        List<string> Flags { get; set; } = new List<string>();

    }
}
