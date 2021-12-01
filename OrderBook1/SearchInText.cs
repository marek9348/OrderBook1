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
        //Search client
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
