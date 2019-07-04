using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

using Persistence;
namespace DAL
{
    public class BillDAL
    {
        private static BillDAL instance;
        public static BillDAL Instance
        {
            get { if (instance == null) instance = new BillDAL(); return BillDAL.instance; }
            private set { BillDAL.instance = value; }
        }

        public BillDAL()
        {

        }

        public List<Bill> GetBillbyStatus()
        {
            List<Bill> bill = new List<Bill>();
            string query = string.Format("select * from bill where status_bill=0 or status_bill=2 ;");
            DataTable Data = DBHelper.ExecuteQuery(query);

            foreach (DataRow item in Data.Rows)
            {
                Bill billa = new Bill(item);
                bill.Add(billa);


            }
            if (Data.Rows.Count != 0)
            {
                
                return bill;
            }
            else
            {
                return null;
            }

        }
        public int GetUncheckBillIdbyTableID(int id, int status)
        {
            string query = string.Format("select * from bill where id_table= {0} and status_bill={1};", id, status);
            DataTable data = DBHelper.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }
        public DateTime? GetcheckoutBillIdbyTableID(int id)
        {
            try
            {
                string query = string.Format("select max(checkout) from bill where id_table= {0} and status_bill=1;", id);

                return (DateTime?)DBHelper.ExecuteScalar(query);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public DateTime? GetcheckintBillIdbyTableID(int id, int status)
        {
            try
            {
                string query = string.Format("select max(checkin) from bill where id_table= {0} and status_bill={1};", id, status);

                return (DateTime?)DBHelper.ExecuteScalar(query);
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public bool InsertBill(int idtable)
        {
            string query = string.Format("insert into bill(id_table) value ({0}) ;", idtable);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool Checkout(int id)
        {
            string query = string.Format("update bill set status_bill=1,checkout=now() where id={0};", id);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool WaitCheckOut(int id)
        {
            string query = string.Format("update bill set status_bill=2 where id={0};", id);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool CheckCombo(int id)
        {
            string query = string.Format("update bill set status_bill=3 where id={0};", id);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteBill()
        {
            string query = string.Format("delete from bill where month(checkin) = 6;");
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}