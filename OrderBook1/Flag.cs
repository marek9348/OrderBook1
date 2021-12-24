using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBook1
{
    public class Flag
    {
        public int FlagId { get; set; }
        public string Text { get; set; }

        //Navigation property
        public int ParamId { get; set; }
        public Order_parameter Order_Parameter { get; set; }
    }
}
