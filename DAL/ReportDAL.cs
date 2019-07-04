using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

using Persistence;
namespace DAL
{
    public class ReportDAL
    {
        private static ReportDAL instance;
        public static ReportDAL Instance
        {
            get { if (instance == null) instance = new ReportDAL(); return ReportDAL.instance; }
            private set { ReportDAL.instance = value; }
        }

        public ReportDAL()
        {

        }

        public List<Report> ReportbyMonth(string monthyear)/*lấy  tổng doanh thu theo tháng */
        {
            List<Report> reportList = new List<Report>();
            string query = string.Format("select  bill.id,billinfo.id,id_table,name_food, soluong,sum(soluong) as 's',price, (price*soluong)as 'totalPrice',checkin,checkout, status_bill from billinfo inner join bill on bill.id=billinfo.idbill inner join food on food.id=billinfo.idfood  group by billinfo.id having status_bill=1 and date_format(checkin,'%c/%Y') = {0}  order by checkout;", monthyear);
            DataTable Data = DBHelper.ExecuteQuery(query);
            foreach (DataRow item in Data.Rows)
            {
                Report emp = new Report(item);
                reportList.Add(emp);
            }

            return reportList;
        }
         public List<Report> ReportCur()/*lấy  tổng doanh thu thời gian hiện tại */
        {
            List<Report> reportList = new List<Report>();
            string query = string.Format("select  bill.id,billinfo.id,id_table,name_food, soluong,sum(soluong) as 's',price, (price*soluong)as 'totalPrice',checkin,checkout, status_bill from billinfo inner join bill on bill.id=billinfo.idbill inner join food on food.id=billinfo.idfood  group by billinfo.id having status_bill=1 and date_format(checkout,'%c/%Y')=date_format(curdate(),'%c/%Y') order by checkout;");
            DataTable Data = DBHelper.ExecuteQuery(query);
            foreach (DataRow item in Data.Rows)
            {
                Report emp = new Report(item);
                reportList.Add(emp);
            }

            return reportList;
        }
        public List<Report> GetBillbyDate(string datein, string dateout)/*Lấy hóa đơn theo tháng năm */
        {
            List<Report> reportList = new List<Report>();
            string query = string.Format("select bill.id,id_table, name_food, soluong,sum(soluong)as 's',price, (price*soluong) as totalPrice, checkin,checkout,status_bill from billinfo inner join bill on bill.id=billinfo.idbill inner join food on food.id=billinfo.idfood group by billinfo.id having  date_format(checkin,'%d/%c/%Y') >= {0} and date_format(checkin,'%d/%c/%Y') <= {1}  order by checkin;", datein, dateout);
            DataTable data = DBHelper.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Report emp = new Report(item);
                reportList.Add(emp);
            }
            return reportList;
        }
        public List<Report> GetBillbyID(int idbill)/*Lấy hóa đơn theo tháng năm */
        {
            List<Report> reportList = new List<Report>();
            string query = string.Format("select bill.id,id_table, name_food, soluong,sum(soluong)as 's',price, (price*soluong) as totalPrice, checkin,checkout,status_bill from billinfo inner join bill on bill.id=billinfo.idbill inner join food on food.id=billinfo.idfood group by billinfo.id having  bill.id={0};", idbill);
            DataTable data = DBHelper.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Report emp = new Report(item);
                reportList.Add(emp);
            }
            return reportList;
        }
        public List<Report> ReportByDrink(int month)/*Lấy top 5 đồ uống bán chạy ở tháng bất kì */
        {
            List<Report> reportList = new List<Report>();
            string query = string.Format("SELECT * FROM Drink_Report WHERE s >= (Select Min(A.s) from (SELECT s  FROM Drink_Report ORDER BY s DESC limit 5) as A) and month(checkout)={0} ORDER BY Drink_Report.s DESC;", month);
            DataTable Data = DBHelper.ExecuteQuery(query);
            foreach (DataRow item in Data.Rows)
            {
                Report emp = new Report(item);
                reportList.Add(emp);
            }

            return reportList;
        }

         public List<Report> ReportByDrinkCurentDate()/*Lấy top 5 đồ uống bán nhiều nhất ở tháng hiện tại */
        {
            List<Report> reportList = new List<Report>();
            string query = string.Format("SELECT * FROM Drink_Report WHERE s >= (Select Min(A.s) from (SELECT s  FROM Drink_Report ORDER BY s DESC limit 5) as A) and month(checkout)=month(curdate()) ORDER BY Drink_Report.s DESC;");
            DataTable Data = DBHelper.ExecuteQuery(query);
            foreach (DataRow item in Data.Rows)
            {
                Report emp = new Report(item);
                reportList.Add(emp);
            }

            return reportList;
        }
    }
}