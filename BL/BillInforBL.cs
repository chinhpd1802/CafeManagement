using System;
using System.Collections.Generic;
using ConsoleTables;
using DAL;
using Persistence;
using System.Threading;

namespace BL
{
    public class BillInforBL
    {
        private BillInforDAL billinforDAL;
        private FoodBL foodbl = new FoodBL();
        private Support sup = new Support();
        private FoodBL foobl = new FoodBL();
        public BillInforBL() => billinforDAL = new BillInforDAL();


        public void InsertBillInfor(int idbill, int idfood, int count)/*Lưu vào CSDL */
        {

            try
            {

                if (billinforDAL.InsertBillInfor(idbill, idfood, count))
                {
                    sup.PrintColorMessage(ConsoleColor.Green, "Thành Công!");

                }
            }
            catch (Exception e)
            {

                Console.Write(e.Message);

            }
        }
        public void InsertBillInfo(int idbill)/*Thêm món vào Hóa đơn */
        {
            foodbl.LoadFoodList();
            Console.WriteLine("Nhấn F để tìm kiếm đồ uống");
            Console.WriteLine("Nhấn phím bất kì đề nhập đồ uống");
            var fFs = Console.ReadKey();
            Console.Clear();
            switch (fFs.Key)
            {
                case ConsoleKey.F:
                    while (true)
                    {
                        Console.Clear();
                        foodbl.LoadFoodList();
                        Console.Write("Nhập tên món cần tìm: ");
                        string nameFood = Console.ReadLine();
                        foobl.SearchFood(nameFood);
                        var kj1 = Console.ReadKey();
                        if (kj1.Key == ConsoleKey.Escape)
                        {

                            break;
                        }
                    }

                    break;
                default:
                    foodbl.LoadFoodList();
                    break;

            }
            // foodbl.LoadFoodList();
            Console.Write("Sau khi nhập xong một món, Nhấn ");
            sup.PrintColorMessage1(ConsoleColor.Cyan, "Enter");
            Console.Write(" để tiếp tục thêm món, Nhấn ");
            sup.PrintColorMessage1(ConsoleColor.Red, "ESC ");
            Console.Write("để dừng thêm món,Nhấn ");
            sup.PrintColorMessage1(ConsoleColor.DarkYellow, "F ");
            Console.WriteLine("để tìm kiếm món ! ");

           
            int dem = 0;
            string id = null;
            string sl = null;
            List<test> a = new List<test>();
            Console.WriteLine("Bạn không nên nhập sai quá 5 lần");
            while (true && dem < 5)
            {
                test t = new test();
                Console.Write("-Mã Đồ Uống: ");
                id = Console.ReadLine();

                if (sup.IsANumber(id) != true)
                {
                    dem++;
                    continue;

                }

                int s = Convert.ToInt32(id);
                if (foodbl.CheckExistFood(s) != true)
                {
                    dem++;
                    continue;
                }
                Console.Write("-Số lượng: ");
                sl = Console.ReadLine();

                if (sup.IsANumber(sl) != true)
                {
                    dem++;
                    continue;

                }

                int b = Convert.ToInt32(sl);
                a.Add(new test(s, b));
              
                try
                {
                   
                        InsertBillInfor(idbill, s, b);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {

                    break;
                }
                if (key.Key == ConsoleKey.F)
                {
                    while (true)
                    {
                        Console.Clear();
                        foodbl.LoadFoodList();
                        Console.Write("Nhập tên món cần tìm: ");
                        string nameFood = Console.ReadLine();
                        foobl.SearchFood(nameFood);
                        var kj1 = Console.ReadKey();
                        if (kj1.Key == ConsoleKey.Escape)
                        {

                            break;
                        }
                    }
                }

            }


        }

        public bool DeleteBillInfo()
        {
            return billinforDAL.DeleteBillInfo();
        }
         public void DeleteBillInfoByIdBill(int idbill)
         {
            if (billinforDAL.DeleteBillInfoByIdBill(idbill))
            {
                Console.WriteLine("Hóa đơn chi tiết có mã " + idbill+ " đã được xóa");
            }
         }
    }
}
