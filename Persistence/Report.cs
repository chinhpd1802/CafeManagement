using System;
using System.Data;


namespace Persistence
{
    public class Report : Menu
    {
        private int id { get; set; }
        private int id_table { get; set; }
        private DateTime? checkin { get; set; }
        private DateTime? checkout { get; set; }
       private int status_bill { get; set; }
        private decimal s { get; set; }


        public Report()
        {

        }
        public Report(int id, int id_table, string foodName, int count, float price, float totalPrice, DateTime? checkin, DateTime? checkout, int status_bill, decimal s)
        : base(foodName, count, price, totalPrice)
        {
            this.Id_Table = id_table;
            this.CheckIn = checkin;
            this.CheckOut = checkout;
            this.Sum = s;
        }
        public Report(DataRow row)
        : base(row["name_food"].ToString(), (int)row["soluong"], (float)Convert.ToDouble(row["price"].ToString()), (float)Convert.ToDouble(row["totalPrice"].ToString()))
        {
            this.id = (int)row["id"];
            this.id_table = (int)row["id_table"];

            this.checkin = (DateTime?)row["checkin"];

            var dateCheckOutTemp = row["checkout"];
            if (dateCheckOutTemp.ToString() != "")
            {
                this.checkout = (DateTime?)dateCheckOutTemp;
            }
            this.status_bill = (int)row["status_bill"];
            this.s = (decimal)row["s"];
        }
        public int ID 
        {
            get{return id;}
            set{value =id;}
        }
        public int Id_Table
        {
            get{return id_table;}
            set{value =id_table;}
        }
        public DateTime? CheckIn 
        {
            get{return checkin;}
            set{value =checkin;}
        }
        public DateTime? CheckOut
        {
            get{return checkout;}
            set{value =checkout;}
        }
        public int Status_Bill
        {
            get{return status_bill;}
            set{value =status_bill;}
        }
        public decimal Sum
        {
            get{return s;}
            set{value =s;}
        }
    }


}