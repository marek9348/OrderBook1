using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBook1
{
    public class TextMng
    {

        public string RemoveSecondNewLine(string text)
        {
            string result = "";
            string[] lines = text.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            foreach(string line in lines)
            {
                result += line + Environment.NewLine;
            }
            //result = lines[0];

            return result;
        }
    }
}
