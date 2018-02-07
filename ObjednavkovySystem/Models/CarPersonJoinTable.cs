using System;

namespace ObjednavkovySystem.Models
{
    public class CarPersonJoinTable
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _userID;

        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private int _carID;

        public int CarID
        {
            get { return _carID; }
            set { _carID = value; }
        }

        private DateTime _rented;

        public DateTime Rented
        {
            get { return _rented; }
            set { _rented = value; }
        }

        private int _employeeID;

        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }

        private int _isDone;

        public int IsDone
        {
            get { return _isDone; }
            set { _isDone = value; }
        }
    }
}