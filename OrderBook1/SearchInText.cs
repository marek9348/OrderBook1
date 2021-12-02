using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;

namespace OrderBook1
{
    public class SearchInText
    {
        /// <summary>
        /// Search client in text in RichtextBox
        /// </summary>
        /// <param name="clients">ObservableCollection of client names</param>
        /// <param name="source">Text from richtextbox</param>
        /// <returns></returns>
        public Client SearchClient(ObservableCollection<Client> clients, string source)
        {
            Client foundClient = new Client();
            foreach(Client client in clients)
            {
                if(source.ToLower().Contains(client.Name.ToLower())){
                    foundClient = client;
                    break;
                }
            }
            return foundClient;
        }
        //Search OrderNumber
        public string SearchOrderNum(List<OrderNumber> ordNums, string source)
        {
            string foundOrderNum = "";
            foreach (OrderNumber ordNum in ordNums)
            {
                //Regex rgx = new Regex(ordNum.Pattern);
                //Regex search ToDo
                Match m = Regex.Match(source, ordNum.Pattern);
                if (m.Success)
                {
                    foundOrderNum = m.Value;
                    break;
                }
                    
            }
            return foundOrderNum;
        }
                
        /// <summary>
        /// Search OrderNumber overload
        /// </summary>
        /// <param name="ordNums"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public (string, string, int) SearchOrderNumWithFlags(List<OrderNumber> ordNums, string source)
        {
            string foundOrderNum = "";
            int foundIndex = 0;
            string flag = "";
            string[] sourceWords = source.Split(' ');
            foreach (OrderNumber ordNum in ordNums)
            {
                //Regex rgx = new Regex(ordNum.Pattern);
                //Regex search ToDo
                for(int i = 0; i < sourceWords.Length; i++)
                {
                    //List of flags from db
                    List<string> dbFlags = new List<string>() { "Project", "obj.", "objednávky", "OBJEDNÁVKA", "order", "zákazky", "zakázka", "zakázky" };
                    for(int j = 0; j < dbFlags.Count; j++)
                    {
                        Match m = Regex.Match(sourceWords[i], ordNum.Pattern);
                        if (m.Success)
                        {

                            foundIndex = m.Index; //Only temporally
                            if (i > 4)
                            {
                                flag = $"{sourceWords[i - 3]} {sourceWords[i - 2]} {sourceWords[i - 1]}";
                            }
                            else if (i > 0 && i < 4)
                            {
                                flag = sourceWords[i - 1];
                            }
                            foreach (string dbfl in dbFlags)
                            {
                                if (flag.ToLower().Contains(dbfl.ToLower()))
                                {
                                    foundOrderNum = m.Value;
                                    break;
                                }
                            }

                        }
                    }
                    
                }
                

            }
            (string, string, int) foundData = (foundOrderNum, flag, foundIndex);
            return foundData;
        }
        /// <summary>
        /// Get all text from richtextbox
        /// </summary>
        /// <param name="rtb">String</param>
        /// <returns>String</returns>
        public string GetTextFromRtbx(RichTextBox rtb)
        {

            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;

        }

        /// <summary>
        /// Takes a string and convert it to a regex expression
        /// </summary>
        /// <param name="source">String to be converted</param>
        /// <returns>Regex expression</returns>
        public string GetRegexExpression(string source)
        {
            Regex rgxLetterLow = new Regex(@"[a-z]");
            Regex rgxLetterUp = new Regex(@"[A-Z]");
            Regex rgxNum = new Regex(@"[0-9]");
            Regex rgxChar = new Regex(@"[\,,\.,_,\-,\:,\s]");

            string regexExp = "";
            foreach (char ch in source)
            {
                if (rgxLetterLow.IsMatch(ch.ToString()))
                {
                    regexExp += "[a-z]";
                }
                else if (rgxLetterUp.IsMatch(ch.ToString()))
                {
                    regexExp += "[A-Z]";
                }
                else if (rgxNum.IsMatch(ch.ToString()))
                {
                    regexExp += "[0-9]";
                }
                else if (rgxChar.IsMatch(ch.ToString()))
                {
                    regexExp += @"[\,,\.,_,\-, \:]";
                }
            }
            return regexExp;
        }
    }

    
}
