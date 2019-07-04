using System;
using System.Data;


namespace Persistence
{
    public class Bill
    {
        private int id;
        private int id_table;
        private int status_bill;
        private DateTime? checkindate =null;
        private DateTime? checkoutdate = null;

        public Bill(int id, int id_table, DateTime? checkindate , DateTime? checkoutdate, int status_bill)
        {
            this.Id = id;
            this.Id_Table = id_table;
            this.CheckIn = checkindate;
            this.CheckOut = checkoutdate;
            this.Status_Bill = status_bill;



        }
        public Bill(DataRow row)
        {
            this.id = (int)row["id"];
            this.id_table = (int)row["id_table"];
            this.CheckIn = (DateTime?)row["checkin"];

            var dateCheckOutTemp = row["checkout"];
            if (dateCheckOutTemp.ToString() != "")
            {
                this.CheckOut = (DateTime?)dateCheckOutTemp;
            }
            this.status_bill = (int)row["status_bill"];
        }

        public Bill()
        {
        }
        public int Id
        {
            get { return id; }
            set { value = id; }
        }
        public int Id_Table
        {
            get { return id_table; }
            set { value = id_table; }
        }
        public DateTime? CheckIn
        {
            get { return checkindate; }
            set {  checkindate =value; }
        }
         public DateTime? CheckOut
        {
            get { return checkoutdate; }
            set { checkoutdate=value; }
        }
        public int Status_Bill
        {
            get { return status_bill; }
            set { value = status_bill; }
        }
       
    }


}