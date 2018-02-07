using System;

namespace ObjednavkovySystem.Models
{
    public class Car
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _isLent;

        public int IsLent
        {
            get { return _isLent; }
            set { _isLent = value; }
        }

        private DateTime _added;

        public DateTime Added
        {
            get { return _added; }
            set { _added = value; }
        }
    }
}