using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBook1
{
    public class DateTimeMng
    {
        //List of regex expressions from db to parse the date string
        public List<string> RegexExpressions { get; set; }
        //Regex expression to be saved in db
        public string RegexString { get; set; }
        //Year
        public string Year { get; set; }
        //Month
        public string Month { get; set; }
        //day
        public string Day { get; set; }


        /// <summary>
        /// Parses the string and get DateTime
        /// </summary>
        /// <param name="dateString">String to be parsed</param>
        /// <returns></returns>
        public DateTime ParseDate(string dateString)
        {
            DateTime result = new DateTime();


            return result;
        }

        /// <summary>
        /// Analyses the string to get a regex expression
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public string GetDateStringMatrix(string dateString)
        {
            

            string dateOnly = "yyyy-MM-dd";
            string result = dateOnly;

            return result;
        }
    }
}
