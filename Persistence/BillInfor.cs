using System;
using System.Data;


namespace Persistence
{
    public class BillInfor
    {
        public int id { get; set; }
        public int idbill { get; set; }
        public int idfood { get; set; }
        public int count { get; set; }

        public BillInfor(int id, int idbill, int idfood, int count)
        {
            this.id = id;
            this.idbill = idbill;
            this.idfood = idfood;
            this.count = count;



        }
        public BillInfor(DataRow row)
        {
            this.id = (int)row["id"];
            this.idbill = (int)row["idbill"];
            this.idfood = (int)row["idfood"];
            this.count = (int)row["soluong"];
        }

        public BillInfor()
        {
        }
    }


}