using System;
using System.Collections.Generic;
using System.Data;

using MySql.Data.MySqlClient;

using Persistence;
namespace DAL
{
    public class BillInforDAL
    {
        private static BillInforDAL instance;
        public static BillInforDAL Instance
        {
            get { if (instance == null) instance = new BillInforDAL(); return BillInforDAL.instance; }
            private set { BillInforDAL.instance = value; }
        }

        public BillInforDAL()
        {

        }

        public bool InsertBillInfor(int idbill, int idfood, int count)
        {
            string query = string.Format("call UPS_InsertBillInfo({0},{1},{2});", idbill, idfood, count);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
        public List<BillInfor> GetListBillInfo(int id)
        {
            List<BillInfor> listBillInfo = new List<BillInfor>();
            DataTable data = DBHelper.ExecuteQuery("select * from billinfo where idbill= " + id);
            foreach (BillInfor item in listBillInfo)
            {
                BillInfor billinfo = new BillInfor();
                listBillInfo.Add(billinfo);
            }
            return listBillInfo;
        }

        public bool DeleteBillInfo()
        {
            string query = string.Format("delete from billinfo where idbill in (select id from bill where month(checkin) =6);");
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
         public bool DeleteBillInfoByIdBill(int idbill)
        {
            string query = string.Format("delete from billinfo where idbill={0};",idbill);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }


    }
}


