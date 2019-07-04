using System;
using System.Collections.Generic;
using System.Data;

using Persistence;
namespace DAL
{
    public class MenuDAL
    {
        private static MenuDAL instance;
        public static MenuDAL Instance
        {
            get { if (instance == null) instance = new MenuDAL(); return MenuDAL.instance; }
            private set { MenuDAL.instance = value; }
        }

        public MenuDAL()
        {

        }

        public List<Menu> LoadListMenuByTable(int id,int status)
        {
            List<Menu> menuList = new List<Menu>();
           // string query =string.Format("select id_table, name_food, soluong, price, (price*soluong)as 'totalPrice', status_bill from billinfo inner join bill on bill.id=billinfo.idbill inner join food on food.id=billinfo.idfood group by billinfo.id having id_table = {0} and status_bill={1} ;",id,status);
            string query= string.Format("select * from ViReportBill where id_table = {0} and status_bill={1};",id, status);
            DataTable Data = DBHelper.ExecuteQuery(query);
            foreach (DataRow item in Data.Rows)
            {
                Menu emp = new Menu(item);
                menuList.Add(emp);
            }

            return menuList;
        }
    }
}