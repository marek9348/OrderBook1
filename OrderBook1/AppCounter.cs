using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBook1
{
    public class AppCounter
    {
        //One or multiple orders in one rtbx
        public string EditMode { get; set; }
        //Status of edited document in rtbx
        public bool IsOrderEdited { get; set; }
        

        public AppCounter()
        {
            EditMode = "single";
            IsOrderEdited = false;
        }

    }
}
