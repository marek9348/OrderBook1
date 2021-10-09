using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderBook1
{
    public class Client : INotifyPropertyChanged
    {
        private int _listId = 500;
        private string _name = "";

        public int Id { get; set; }
        [NotMapped]
        public int ListId {
            get { return _listId; }
            set
            {
                _listId = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("ListId");
            }
        }
        public string Name {
            get { return _name; }
            set
            {
                _name = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("Name");
            }
        }

        //Navigations properties
        public List<Order> Orders { get; } = new List<Order>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            //throw new NotImplementedException();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
            //MessageBox.Show("Changed to " + this.Client);
        }
    }
}
