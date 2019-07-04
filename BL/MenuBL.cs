using System;
using System.Collections.Generic;
using System.Globalization;
using ConsoleTables;
using DAL;
using Persistence;

namespace BL
{
    public class MenuBL
    {
        public float total { get; set; }
        private MenuDAL menuDAL;
        public MenuBL() => menuDAL = new MenuDAL();
        public List<Menu> GetMenuListByTableID(int id, int status)
        {
            List<Menu> menu = menuDAL.LoadListMenuByTable(id, status);
            total = 0;
            var tab = new ConsoleTable("Tên Đồ Uống", "Số lượng", "Giá", "Thành Tiền");
            foreach (Menu item in menu)
            {
                tab.AddRow(item.foodName, item.count, String.Format("{0:0,0 VND}", item.price), String.Format("{0:0,0 VND}", item.totalPrice));
                total += item.totalPrice;


            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            tab.Write();



            // CultureInfo cultrue=new CultureInfo("vi-VN");
            // Console.WriteLine("Total:{0} ", total.ToString("c",cultrue));
            Console.WriteLine(String.Format("Tổng tiền(+VAT):  \t\t\t {0:0,0 VND}", total));
            Console.ResetColor();

            return menu;
        }


    }
}
