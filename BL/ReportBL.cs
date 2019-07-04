using System;
using System.Collections.Generic;
using System.Globalization;
using ConsoleTables;
using DAL;
using Persistence;

namespace BL
{
    public class ReportBL
    {
        public float total { get; set; }
        public float totalcur { get; set; }
        private Support sup = new Support();
        private ReportDAL reportDAL;
        public ReportBL() => reportDAL = new ReportDAL();
        public List<Report> ReportbyMonthh(string monthyear)/*lấy doanh thu và liệt kê hóa đơn theo tháng */
        {
            List<Report> report = reportDAL.ReportbyMonth(monthyear);
            total = 0;
            var tab = new ConsoleTable("Mã thẻ", "Tên Đồ Uống", "Số lượng", "Tổng tiền", "Ngày tạo", "Ngày thanh toán", "StatusBill");
            foreach (Report item in report)
            {
                tab.AddRow(item.Id_Table, item.foodName, item.count, String.Format("{0:0,0 VND}", item.totalPrice), String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", item.CheckIn), String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", item.CheckOut), item.Status_Bill.ToString("Đã thanh toán"));
                total += item.totalPrice;


            }


            tab.Write();

            Console.Write("-($$) Tổng doanh thu của tháng ");
            sup.PrintColorMessage(ConsoleColor.Yellow, String.Format(": {0:0,0 VND}", total));


            return report;
        }
        public List<Report> ReportbyCurr()/*lấy doanh thu và liệt kê hóa đơn theo tháng hiện tại*/
        {
            List<Report> report = reportDAL.ReportCur();
            total = 0;
            var tab = new ConsoleTable("Mã thẻ", "Tên Đồ Uống", "Số lượng", "Tổng tiền", "Ngày tạo", "Ngày thanh toán", "StatusBill");
            foreach (Report item in report)
            {
                tab.AddRow(item.Id_Table, item.foodName, item.count, String.Format("{0:0,0 VND}", item.totalPrice), String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", item.CheckIn), String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", item.CheckOut), item.Status_Bill.ToString("Đã thanh toán"));
                total += item.totalPrice;


            }




            Console.Write("-($$) Tổng doanh thu của tháng " + String.Format("{0:MM/yyyy }", DateTime.Now));
            sup.PrintColorMessage(ConsoleColor.Yellow, String.Format(": {0:0,0 VND}", total));


            return report;
        }
        public List<Report> Top5Drink(int month)/*Hiển thị top 5 đồ uống bán nhiều nhất theo tháng bất kì */
        {
            List<Report> report = reportDAL.ReportByDrink(month);
            total = 0;
            var tab = new ConsoleTable("Tên Đồ Uống", "Số lượng", "Tổng tiền");
            foreach (Report item in report)
            {
                tab.AddRow(item.foodName, item.Sum, String.Format("{0:0,0 VND}", item.totalPrice));
                total += item.totalPrice;


            }


            tab.Write();
            // CultureInfo cultrue = new CultureInfo("VN");
            // Console.WriteLine("Total:{0} ", total.ToString("c", cultrue));
            //  Console.WriteLine(String.Format("Tổng doanh thu của tháng này : {0:0,0 VND}", total));


            return report;
        }
        public List<Report> Top5DrinkCur() /*Hiển thị top 5 đồ uống bán nhiều nhất theo tháng hiện tại */
        {
            List<Report> report = reportDAL.ReportByDrinkCurentDate();
            total = 0;
            var tab = new ConsoleTable("Tên Đồ Uống", "Số lượng", "Tổng tiền");
            foreach (Report item in report)
            {


                tab.AddRow(item.foodName, item.Sum, String.Format("{0:0,0 VND}", item.totalPrice));



            }


            tab.Write();



            return report;
        }
        public List<Report> GetBillbyDate(string datein, string dateout)/*Hiển thị tất cả các bill theo tháng năm nhập vào */
        {
            List<Report> report = reportDAL.GetBillbyDate(datein, dateout);
            string desStatus = null;
            var tab = new ConsoleTable("Mã hóa đơn", "Mã thẻ", "Tên đồ uống", "Số lượng", "Tổng tiền", "Ngày tạo", "Ngày Thanh Toán", "Trạng Thái hóa đơn");
            foreach (Report item in report)
            {
                switch (item.Status_Bill)
                {
                    case 0:
                        desStatus = item.Status_Bill.ToString("Chưa thanh Toán");
                        break;
                    case 1:
                        desStatus = item.Status_Bill.ToString("Đã Thanh Toán");
                        break;
                    case 2:
                        desStatus = item.Status_Bill.ToString("Chờ thanh toán");
                        break;

                }
                tab.AddRow(item.ID, item.Id_Table, item.foodName, item.count, String.Format("{0:0,0 VND}", item.totalPrice), String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", item.CheckIn), String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", item.CheckOut), desStatus);



            }


            tab.Write();
            // CultureInfo cultrue = new CultureInfo("VN");
            // Console.WriteLine("Total:{0} ", total.ToString("c", cultrue));
            //   Console.WriteLine(String.Format("Revenue of This Month : {0:0,0 VND}", total));


            return report;
        }
         public List<Report> GetBillbyIdBill(int idbill)/*Hiển thị tất cả các bill theo tháng năm nhập vào */
        {
            List<Report> report = reportDAL.GetBillbyID(idbill);
            string desStatus = null;
            var tab = new ConsoleTable("Mã hóa đơn", "Mã thẻ", "Tên đồ uống", "Số lượng", "Tổng tiền", "Ngày tạo", "Ngày Thanh Toán", "Trạng Thái hóa đơn");
            foreach (Report item in report)
            {
                switch (item.Status_Bill)
                {
                    case 0:
                        desStatus = item.Status_Bill.ToString("Chưa thanh Toán");
                        break;
                    case 1:
                        desStatus = item.Status_Bill.ToString("Đã Thanh Toán");
                        break;
                    case 2:
                        desStatus = item.Status_Bill.ToString("Chờ thanh toán");
                        break;

                }
                tab.AddRow(item.ID, item.Id_Table, item.foodName, item.count, String.Format("{0:0,0 VND}", item.totalPrice), String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", item.CheckIn), String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", item.CheckOut), desStatus);



            }


            tab.Write();
            // CultureInfo cultrue = new CultureInfo("VN");
            // Console.WriteLine("Total:{0} ", total.ToString("c", cultrue));
            //   Console.WriteLine(String.Format("Revenue of This Month : {0:0,0 VND}", total));


            return report;
        }


    }
}
