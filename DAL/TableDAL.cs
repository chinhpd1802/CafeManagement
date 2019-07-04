using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

using Persistence;
namespace DAL
{
    public class TableDAL
    {
        private static TableDAL instance;
        public static TableDAL Instance
        {
            get { if (instance == null) instance = new TableDAL(); return TableDAL.instance; }
            private set { TableDAL.instance = value; }
        }

        public TableDAL()
        {

        }

        public List<Table> LoadTableList()
        {
            List<Table> tablelist = new List<Table>();
            string query = "select * from tablefood;";
            try
            {
                DataTable Data = DBHelper.ExecuteQuery(query);
                foreach (DataRow item in Data.Rows)
                {
                    Table emp = new Table(item);
                    tablelist.Add(emp);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return tablelist;
        }
        public bool UpdateTable(int id)
        {
            string query = string.Format("update tablefood set status_table='Có người' where id={0}", id);
            int resul = DBHelper.ExecuteNonQuery(query);
            return resul > 0;
        }
        public bool UpdateTable1(int id)
        {
            string query = string.Format("update tablefood set status_table='Trống' where id={0}", id);
            int resul = DBHelper.ExecuteNonQuery(query);
            return resul > 0;
        }
        public int checkStatus(int id)
        {
            string query = string.Format("select * from tablefood where status_table='co nguoi';", id);
            DataTable data = DBHelper.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                Table table = new Table(data.Rows[0]);
                return table.ID;
            }
            return -1;
        }
         public List<Table> LoadComboList()
        {
            List<Table> tablelist = new List<Table>();
            string query = "select * from tablefood where name_table like '%Combo%';";
            try
            {
                DataTable Data = DBHelper.ExecuteQuery(query);
                foreach (DataRow item in Data.Rows)
                {
                    Table emp = new Table(item);
                    tablelist.Add(emp);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return tablelist;
        }
        public int CheckTableid(int id)
        {
            
         
            
            string query = string.Format("select * from tablefood where id={0}", id);
            DataTable data = DBHelper.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                 Table table = new Table(data.Rows[0]);
               
                return table.ID;
            }
          
            return -1;
            
        
        }
    }
}