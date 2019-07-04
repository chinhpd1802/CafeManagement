using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

using Persistence;
namespace DAL
{
    public class CataroryDAL
    {
        private static CataroryDAL instance;
        public static CataroryDAL Instance
        {
            get { if (instance == null) instance = new CataroryDAL(); return CataroryDAL.instance; }
            private set { CataroryDAL.instance = value; }
        }

        public CataroryDAL()
        {

        }

        public List<Cataroryfood> LoadCataroryList(int id)
        {
            List<Cataroryfood> catarorylist = new List<Cataroryfood>();
            string query = "select * from foodcatarory where id=" + id + ";";
            DataTable Data = DBHelper.ExecuteQuery(query);

            foreach (DataRow item in Data.Rows)
            {
                Cataroryfood emp = new Cataroryfood(item);
                catarorylist.Add(emp);

            }
            if (Data.Rows.Count == 0)
            {
                Console.WriteLine("Not Have in Database");
            }
            return catarorylist;
        }
        public List<Cataroryfood> LoadCataroryLis()
        {
            List<Cataroryfood> catarorylist = new List<Cataroryfood>();
            string query = "select * from foodcatarory;";
            DataTable Data = DBHelper.ExecuteQuery(query);

            foreach (DataRow item in Data.Rows)
            {
                Cataroryfood emp = new Cataroryfood(item);
                catarorylist.Add(emp);

            }

            return catarorylist;
        }
        public int GetbyID(int id)
        {
            List<Cataroryfood> catarorylist = new List<Cataroryfood>();
            string query = "select * from foodcatarory where id=" + id + ";";
            DataTable Data = DBHelper.ExecuteQuery(query);

            foreach (DataRow item in Data.Rows)
            {
                Cataroryfood emp = new Cataroryfood(item);
                catarorylist.Add(emp);

            }
            if (Data.Rows.Count == 0)
            {
                return -1;
            }
            return 1;
        }
        public bool Insertcataroty(string name_catarory)
        {
            string query = string.Format("insert into foodcatarory (name_catarory) value ('{0}')", name_catarory);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool Updatecataroty(int id, string name_catarory)
        {
            string query = string.Format("update foodcatarory set name_catarory = '{0}' where id= {1}", name_catarory, id);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
    
        public int GetIdCatagoryfood(int idcatagory)
        {
            string query = string.Format("select * from  foodcatarory  where fid={0}", idcatagory);
            DataTable data = DBHelper.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                 Cataroryfood food = new Cataroryfood(data.Rows[0]);
               
                return food.id;
            }
          
            return -1;
        }
        public List<Cataroryfood> SearchCatarory( string name)
        {
            List<Cataroryfood> catarorylist = new List<Cataroryfood>();
            string query =string.Format( "select * from foodcatarory where name_catarory like '%{0}%';",name);
            DataTable Data = DBHelper.ExecuteQuery(query);

            foreach (DataRow item in Data.Rows)
            {
                Cataroryfood emp = new Cataroryfood(item);
                catarorylist.Add(emp);

            }

            return catarorylist;
        }
    }

}