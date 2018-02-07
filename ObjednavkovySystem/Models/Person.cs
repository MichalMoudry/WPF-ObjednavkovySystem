using System;

namespace ObjednavkovySystem.Models
{
    public abstract class Person
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

        private DateTime _added;

        public DateTime Added
        {
            get { return _added; }
            set { _added = value; }
        }
    }
}