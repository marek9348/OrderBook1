using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace OrderBook1
{
    public class TextMng
    {
        public string Text { get; set; }

        /// <summary>
        /// Removes redundant new lines
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds text to richtbx
        /// </summary>
        /// <param name="rtb"></param>
        public void AddText(RichTextBox rtb)
        {
            string delimiter = "==============Pdf order==============";
            string inputText = delimiter + Environment.NewLine + Text;
            rtb.AppendText(inputText);
        }

        public void Clear(RichTextBox rtb)
        {
            rtb.SelectAll();

            rtb.Selection.Text = "";
        }
    }
}
