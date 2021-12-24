using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBook1
{
    public class Order_parameter
    {
        public int ParamId { get; set; }
        public string Name { get; set; }

        //Navigation properties
        public List<Flag> Flags { get; set; }
        public List<RegExp> RegExps { get; set; }
    }
}
