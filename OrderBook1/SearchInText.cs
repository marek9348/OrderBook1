using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
    }

    
}
