using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBook1
{
    public class RegExp
    {
        public int RegExpId { get; set; }
        public string RegText { get; set; }

        //Navigation property
        public int ParamId { get; set; }
        public Order_parameter Order_Parameter { get; set; }
    }
}
