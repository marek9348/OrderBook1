using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OrderBook1
{
    public class Order : INotifyPropertyChanged
    {
        //Number values are for simplification as string, in later processing will be converted in int, double or datetime
        private string _clientName = "";
        private string _name = "";
        private string _num = "";
        private string _deadline = "";
        private string _wordcount = "";
        private string _qtns = "";
        private double _totalPrice = 0;
        private string _projectManagerName = "";
        private string _projectManagerLastname = "";
        private string _orderFilePath = "";
        private bool _isUrgent = false;
        private bool _completed = false;

        public int Id { get; set; }
        public string Num {
            get { return _num; }
            set
            {
                _num = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("Num");
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
        public string ClientName
        {
            get { return _clientName; }
            set
            {
                _clientName = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("ClientName");
            }
        }
        
        public string Deadline {
            get { return _deadline; }
            set
            {
                _deadline = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("Deadline");
            }
        }
        public string WordCount {
            get { return _wordcount; }
            set
            {
                _wordcount = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("WordCount");
            }
        }
        public string QtNs {
            get { return _qtns; }
            set
            {
                _qtns = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("QtNs");
            }
        }
        public string PMName {
            get { return _projectManagerName; }
            set
            {
                _projectManagerName = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("PMName");
            }
        }
        public string PMLastName {
            get { return _projectManagerLastname; }
            set
            {
                _projectManagerLastname = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("PMLastName");
            }
        }
        public double TotalPrice {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("TotalPrice");
            }
        }

        public string OrderFilePath
        {
            get { return _orderFilePath; }
            set
            {
                _orderFilePath = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("OrderFilePath");
            }
        }

        public bool Completed
        {
            get { return _completed; }
            set
            {
                _completed = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("Completed");
            }
        }

        public bool IsUrgent
        {
            get { return _isUrgent; }
            set
            {
                _isUrgent = value;
                // Call OnPropertyChanged whenever the property is updated
                //Parameter must by string "..."
                OnPropertyChanged("IsUrgent");
            }
        }

        //Navigations properties
        public string ClientId { get; set; }
        public Client Client { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            //throw new NotImplementedException();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
            //MessageBox.Show("Changed to " + this.Client);
        }

    }
}
