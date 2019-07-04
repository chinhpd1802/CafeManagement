using System;
using System.Collections.Generic;
using BL;
using Persistence;
using System.Threading;
using System.IO;
using System.Text;
using System.Diagnostics;



namespace PL_Console
{
    class Program
    {
        private static Support sub = new Support();
        private static MenuBL menubl = new MenuBL();
        private static BillInforBL billinfo = new BillInforBL();
        private static BillBL billbl = new BillBL();
        private static Bill bill = new Bill();
        private static FoodBL foobl = new FoodBL();
        private static CataroryBL catabl = new CataroryBL();
        private static ReportBL reportbl = new ReportBL();
        private static UserBL empbl = new UserBL();
        private static User emp = new User();
        private static TableBL tablebl = new TableBL();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("======+[ Đăng Nhập ]+======");
                Console.Write("Tài khoản: ");
                string user = "'" + Console.ReadLine() + "'";
                Console.Write("Mật khẩu: ");

                string pass = "'" + sub.ReadPassword() + "'";
                Console.Write("Xin đợi trong giây lát..");
                for (int i = 0; i < 8; i++)
                {
                    Thread.Sleep(150);
                    Console.Write(".");
                }

                emp = empbl.GetUsers(user, pass);

                Console.Clear();



                if (emp != null)
                {
                    switch (emp.Id_TypeAcc)
                    {
                        case "r01"://Quản lý
                            Console.Clear();
                            sub.PrintColorMessage(ConsoleColor.Green, "Đăng nhập thành công ! ");
                            Console.Write("Xin Chào Sếp ");
                            sub.PrintColorMessage(ConsoleColor.Magenta, emp.Full_Name + "[" + emp.IdUser + "]");
                            Console.WriteLine("Chúc Sếp có một ngày làm việc hiệu quả ! ");
                            Thread.Sleep(300);
                            Manager();
                            // MenuOrder();
                            break;
                        case "r02"://Nhân viên
                            Console.Clear();

                            sub.PrintColorMessage(ConsoleColor.Green, "Đăng nhập thành công ! ");
                            Console.Write("Xin chào nhân viên ");
                            sub.PrintColorMessage(ConsoleColor.Cyan, emp.Full_Name + "[" + emp.IdUser + "]");
                            Console.WriteLine("Chúc bạn có một ngày làm việc hiệu quả ! (^.^)");
                            Thread.Sleep(300);
                            MenuOrder();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    sub.PrintColorMessage(ConsoleColor.Red, "ACC or Pass not true ! Plesae, enter again");
                    Thread.Sleep(500);

                }
            }

        }
        private static int Menu(string menuTitle, string[] menuItem)
        {

            Console.WriteLine("+=====================================+");
            Console.WriteLine("  \t{0}", menuTitle);
            Console.WriteLine("+=====================================+");

            int index = 1;
            foreach (string item in menuItem)
            {
                Console.WriteLine(index++ + ". " + item);
            }
            Console.WriteLine("+--------------------------------------+");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("#Chọn: ");
            Console.ResetColor();

            string sChoice = Console.ReadLine();

            int choice = -1;
            if (!sub.IsANumber(sChoice))
            {
                return choice;
            }

            choice = int.Parse(sChoice);
            return choice;
        }

        private static void Manager()/*Quản lý */
        {

            // Console.Clear();
            string menuTitle = "Hệ Thống quản lý";
            string[] menuItem = {
                "Thống Kê",
                "Quản lý nhân viên",
                "Quản lý Đồ Uống",
                "Đăng xuất"
            };

            bool isChoice = false;
            while (!isChoice)
            {
                int choice = Menu(menuTitle, menuItem);
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("+-----------------------+");
                        Console.WriteLine("\tThống kê");
                        Console.WriteLine("+-----------------------+");
                        Console.Write("(!)THỐNG KÊ NHANH TRONG THÁNG ");
                        sub.PrintColorMessage(ConsoleColor.Green, String.Format("{0:MM/yyyy }", DateTime.Now));
                        sub.PrintColorMessage1(ConsoleColor.Cyan, "-Top 5 ");
                        Console.WriteLine("đồ uống ưa thích, được gọi nhiều nhất:");
                        reportbl.Top5DrinkCur();
                        reportbl.ReportbyCurr();//Tổng doanh thu tháng hiện tại
                        Report();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("+-----------------------+");
                        Console.WriteLine("    Quản lý nhân viên");
                        Console.WriteLine("+-----------------------+");
                        MenuUser();

                        break;
                    case 3:
                        Console.Clear();
                        MenuFood();

                        break;
                    case 4:
                        sub.PrintColorMessage(ConsoleColor.Green, "Good bye and See you again !!");
                        isChoice = true;
                        break;
                    default:
                        Console.Clear();
                        sub.PrintColorMessage(ConsoleColor.Red, "Error, Please input from 1 to " + menuItem.Length);
                        break;
                }
            }
        }


        private static void MenuOrder()/*Hóa đơn */
        {

            string menuTitle = "Quản lý hóa đơn";
            string[] menuItem = {
                "Thanh toán",
                "Hiển thị hóa đơn",
                "Thêm hóa đơn",
                "Tạo Combo",
                "Trợ giúp",
                "Đăng xuất"
            };

            bool isChoice = false;
            while (!isChoice)
            {


                int choice = Menu(menuTitle, menuItem);
                switch (choice)
                {
                    /*THANH TOÁN HÓA ĐƠN */
                    case 1:

                        int dem = 0;





                        while (true && dem < 4)
                        {
                            Console.Clear();
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine("\tThanh Toán");
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine(emp.Full_Name + " thân mến !Bạn đang ở chức năng thanh toán hóa đơn");
                            Console.WriteLine("Lưu ý Trạng thái hóa đơn: ");
                            sub.PrintColorMessage(ConsoleColor.Green, "1- ĐÃ THANH TOÁN");
                            sub.PrintColorMessage(ConsoleColor.Magenta, "2- CHỜ THANH TOÁN");
                            sub.PrintColorMessage(ConsoleColor.Red, "0- CHƯA THANH TOÁN");
                            // billbl.GetBillbyStatus();

                            if (billbl.GetBillbyStatus() != null)
                            {

                                sub.PrintColorMessage(ConsoleColor.Blue, "Bạn không nên nhập sai quá 3 lần ! Chức năng thanh toán sẽ tự động thoát ra ");
                                //Input ID Card need Pay

                                Console.Write("Vui lòng nhập mã Thẻ: ");

                                string ID = Console.ReadLine();
                                Console.Write("Vui Lòng nhập Trạng thái bill cần thanh toán: ");

                                string statusid = Console.ReadLine();
                                if (sub.IsANumber(ID) != true)
                                {
                                    dem++;

                                    continue;

                                }
                                Console.Clear();
                                Console.WriteLine(emp.Full_Name + " thân mến !Bạn đang ở chức năng thanh toán hóa đơn");
                                int id = Convert.ToInt32(ID);

                                if (sub.IsANumber(statusid) != true)
                                {
                                    dem++;

                                    continue;
                                }
                                int status = Convert.ToInt32(statusid);
                                if (status != 1 && status < 4)
                                {
                                    billbl.Payment(id, status, string.Concat(emp.Full_Name, "[", emp.IdUser, "]"));
                                }
                                else
                                {
                                    Console.WriteLine("Trạng thái nhập 1- ĐÃ THANH TOÁN !");
                                    Console.WriteLine("Xin Mời nhập lại: 0- CHƯA THANH TOÁN, 2- CHỜ THANH TOÁN");
                                }

                            }
                            var keyy = Console.ReadKey();

                            if (keyy.Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                break;
                            }

                        }

                        break;
                    case 2:/*HIỂN THỊ HÓA ĐƠN */
                        Console.Clear();
                        //SHOW BILL LIST
                        while (true)
                        {
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine("    Hiển thị hóa đơn");
                            Console.WriteLine("+-----------------------+");

                            Console.WriteLine(emp.Full_Name + "Bạn đang trong Ở Hiển thị hóa đơn");
                            Console.WriteLine("Nhấn F để tìm kiếm và hiển thị hóa đơn theo ngày");
                            var kei = Console.ReadKey();
                            Console.Clear();
                            switch (kei.Key)
                            {
                                case ConsoleKey.F:
                                    while (true)
                                    {
                                        Console.Write("Bạn cần nhập ngày theo định dạng: ");
                                        sub.PrintColorMessage(ConsoleColor.Magenta, "dd/m/yyyy (VD:18/2/2000)");
                                        Console.Write("Bạn muốn hiển thị Từ ngày: ");
                                        string datein = "'" + Console.ReadLine() + "'";
                                        Console.Write("Đến ngày: ");
                                        string dateout = "'" + Console.ReadLine() + "'";
                                        Console.Clear();
                                        reportbl.GetBillbyDate(datein, dateout);

                                        var kj = Console.ReadKey();
                                        if (kj.Key == ConsoleKey.Delete)
                                        {
                                            Console.Write("Nhập mã hóa đơn cần xóa: ");
                                            int id = Convert.ToInt32(Console.ReadLine());
                                            reportbl.GetBillbyIdBill(id);
                                            Console.WriteLine("Bạn có chắc chắn muốn xóa hóa đơn này?? (Y/N)");
                                            string choice1 = Console.ReadLine();
                                            if (choice1 == "Y")
                                            {
                                                billinfo.DeleteBillInfoByIdBill(id);
                                                tablebl.UpdateTable1(id);
                                            }

                                            break;
                                        }
                                        if (kj.Key == ConsoleKey.Escape)
                                        {
                                            Console.Clear();
                                            break;
                                        }
                                    }
                                    break;
                                case ConsoleKey.Delete:
                                    while (true)
                                    {
                                        if (billinfo.DeleteBillInfo())
                                        {
                                            Console.WriteLine("Xóa thành công !");
                                        }
                                        if (billbl.DeleteBill())
                                        {
                                            Console.WriteLine("Xóa hóa đơn thành công !");

                                        }
                                        var kj = Console.ReadKey();
                                        if (kj.Key == ConsoleKey.Escape)
                                        {
                                            Console.Clear();
                                            break;
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                            if (kei.Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                break;
                            }
                        }
                        break;
                    case 3:/*TẠO HÓA ĐƠN */
                        Console.Clear();
                        int tableid = 0;
                        int dm = 0;
                        Console.WriteLine("Bạn không nên nhập sai quá 3 lần");
                        while (true && dm < 3)
                        {
                            Console.Clear();
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine("\tThêm hóa đơn");
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine(emp.Full_Name + " thân mến !Bạn đang ở chức năng thêm hóa đơn");
                            tablebl.GetInfo();// Hiển thị thông tin các thẻ
                            Console.Write("*Mã thẻ: ");
                            string iD = Console.ReadLine();
                            if (sub.IsANumber(iD) != true)
                            {
                                dm++;
                                continue;
                            }

                            tableid = Convert.ToInt32(iD);
                            if (tablebl.CheckExistCard(tableid) != true)
                            {
                                dm++;
                                continue;
                            }
                            billbl.CreatBill(tableid);
                            menubl.GetMenuListByTableID(tableid, 0);
                            ConsoleKeyInfo key = Console.ReadKey();
                            if (key.Key == ConsoleKey.Escape)
                            {
                                Console.WriteLine("Đang Thoát...");
                                Console.Clear();
                                break;
                            }


                        }

                        break;
                    case 4://Tạo Combo//


                        break;
                    case 5://Mở File trợ giúp//
                        Console.Clear();

                        try
                        {

                            var p = new Process();

                            p.StartInfo = new ProcessStartInfo("file:///F:/HelpAPP/helpemp.htm")
                            {
                                UseShellExecute = true
                            };
                            p.Start();

                            //  Process.Start(@"F:\Menu các món trong quán cà phê.docx");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;

                    case 6:
                        Console.Clear();
                        isChoice = true;
                        break;



                    default:
                        Console.Clear();
                        sub.PrintColorMessage(ConsoleColor.Red, "Error, Input from 1 to " + menuItem.Length);

                        break;
                }
            }
        }

        private static void Report()/*Thống kê */
        {


            string menuTitle = "Thống kê quán QC Coffee";
            string[] menuItem = {
                "Thống kê doanh thu theo tháng",
                "Top Đồ uống theo tháng",
                "Thống kê khách hàng ",
                "Quay lại"
            };

            bool isChoice = false;
            while (!isChoice)
            {
                int choice = Menu(menuTitle, menuItem);
                switch (choice)
                {
                    case 1:


                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine("  Doanh thu theo tháng");
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine(emp.Full_Name + " Sếp đang trong Thống kê doanh thu theo tháng");


                            Console.Write("Bạn cần nhập ngày theo định dạng: ");
                            sub.PrintColorMessage(ConsoleColor.Magenta, "m/yyyy (VD:2/2000)");
                            Console.Write("Nhập thời gian cần thống kê: ");
                            string montYear = "'" + Console.ReadLine() + "'";
                            Console.Clear();
                            reportbl.ReportbyMonthh(montYear);

                            var kj = Console.ReadKey();
                            if (kj.Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                break;
                            }


                        }
                        break;


                    case 2:
                        Console.Clear();
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("+------------------------+");
                            Console.WriteLine("  Top đồ uống theo tháng");
                            Console.WriteLine("+------------------------+");
                            Console.WriteLine(emp.Full_Name + " Sếp đang trong Thống kê top đồ uống bán nhiều nhất theo tháng");
                            Console.Write("Nhập tháng: ");
                            int mont = Convert.ToInt32(Console.ReadLine());
                            reportbl.Top5Drink(mont);

                            var kj = Console.ReadKey();
                            if (kj.Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                break;
                            }
                        }

                        break;
                    case 3:
                        Console.Clear();

                        break;

                    case 4:
                        Console.Clear();
                        isChoice = true;
                        break;

                    default:
                        Console.Clear();
                        sub.PrintColorMessage(ConsoleColor.Red, "Error, Input from 1 to " + menuItem.Length);
                        break;
                }
            }
        }
        private static void MenuFood()/*Đồ uống */
        {


            string menuTitle = "Quản lý đồ uống";
            string[] menuItem = {
                "Thêm Đồ Uống",
                "Cập nhật đồ uống",
                "Xóa đồ uống",
                "Quay lại"
            };

            bool isChoice = false;
            while (!isChoice)
            {
                int choice = Menu(menuTitle, menuItem);
                switch (choice)
                {
                    case 1: /*Thêm đồ uống */


                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine("   Thêm đồ uống");
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine(emp.Full_Name + " Sếp đang ở Thêm đồ uống");
                            Console.WriteLine("Nhấn C để thêm Loại đồ uống");
                            Console.WriteLine("Nhấn D để thêm đồ uống ");



                            var kj = Console.ReadKey();
                            switch (kj.Key)
                            {
                                case ConsoleKey.C:
                                    Console.Write("Nhập tên loại món: ");
                                    string name = Console.ReadLine();
                                    foobl.InsertCatagoryFood(name);
                                    break;
                                case ConsoleKey.D:

                                    // Console.InputEncoding = Encoding.UTF8;
                                    //  Console.OutputEncoding = Encoding.UTF8;
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.Write("Name Food: ");
                                        string nameFood = Console.ReadLine();
                                        Console.WriteLine(nameFood);
                                        string NameFood = sub.FormatProperCase(nameFood);
                                        Console.Write("Loại: ");
                                        string iD = Console.ReadLine();
                                        if (sub.IsANumber(iD) != true)
                                        {
                                            continue;
                                        }
                                        int id = Convert.ToInt32(iD);
                                        Console.Write("Giá: ");
                                        float price = float.Parse(Console.ReadLine());
                                        foobl.InsertFood(NameFood, id, price);

                                        var ke = Console.ReadKey();
                                        if (ke.Key == ConsoleKey.Escape)
                                        {
                                            Console.Clear();
                                            break;
                                        }
                                    }

                                    break;
                                default:
                                    break;
                            }

                            var kj1 = Console.ReadKey();
                            if (kj1.Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                break;
                            }
                        }
                        break;


                    case 2: /*Cập nhật đồ uống */

                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine("    Cập nhật đồ uống");
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine(emp.Full_Name + " Sếp đang ở Cập nhật đồ uống");
                            Console.WriteLine("Nhấn C để thêm Loại đồ uống");
                            Console.WriteLine("Nhấn D để thêm đồ uống ");



                            var kj = Console.ReadKey();
                            switch (kj.Key)
                            {
                                case ConsoleKey.C:
                                    Console.Write("Nhập tên loại món: ");
                                    string name = Console.ReadLine();
                                    foobl.InsertCatagoryFood(name);
                                    break;
                                case ConsoleKey.D:

                                    while (true)
                                    {
                                        Console.Clear();
                                        // Người dùng khống nhớ mã đồ uống ???
                                        //--> cần tìm kiếm --> hiển thị
                                        Console.Write("Mã đồ uống cần cập nhật: ");
                                        string idn = Console.ReadLine();
                                        // Kiểm tra mã có tồn tại không?
                                        //Nếu có tồn tại đưa ra thông tin của mã đồ uống đó
                                        int ID = Convert.ToInt32(idn);
                                        if (foobl.CheckExistFood(ID) != true)
                                        {
                                            continue;
                                        }
                                        foobl.GetFoodInfoByID(ID);
                                        Console.Write("Tên mới: ");
                                        string nameFood = Console.ReadLine();
                                        Console.WriteLine(nameFood);
                                        string NameFood = sub.FormatProperCase(nameFood);
                                        //Người dùng không nhớ mã loại đồ uống ???
                                        // --> Cần phải tìm kiếm 
                                        Console.Write("Mã Loại: ");
                                        string iD = Console.ReadLine();
                                        if (sub.IsANumber(iD) != true)
                                        {
                                            continue;
                                        }
                                        int id = Convert.ToInt32(iD);
                                        //Kiểm tra mã loại đồ uống có tồn tại không?
                                        if (catabl.CheckExitsCatagoryFood(id) != true)
                                        {
                                            continue;
                                        }
                                        Console.Write("Giá mới: ");
                                        string prices = Console.ReadLine();
                                        if (sub.IsANumber(prices) != true)
                                        {
                                            continue;
                                        }
                                        float price = float.Parse(prices);
                                        foobl.UpdateFood(ID, NameFood, id, price);
                                        //Hiển thị thông tin mới cập nhật của mã đồ uống

                                        var ke = Console.ReadKey();
                                        if (ke.Key == ConsoleKey.Escape)
                                        {
                                            Console.Clear();
                                            break;
                                        }
                                    }

                                    break;
                                default:
                                    break;
                            }

                            var kj1 = Console.ReadKey();
                            if (kj1.Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                break;
                            }
                        }
                        break;

                    case 3: /*Xóa đồ uống */
                        Console.Clear();
                        //Nếu nuốn xóa một loại đồ uống thì phải xóa hết các đồ uống thuộc loại đấy
                        //
                        break;

                    case 4:
                        Console.Clear();
                        isChoice = true;
                        break;

                    default:
                        Console.Clear();
                        sub.PrintColorMessage(ConsoleColor.Red, "Error, Input from 1 to " + menuItem.Length);
                        break;
                }
            }
        }
        private static void MenuUser()/*Nhân viên */
        {


            string menuTitle = "Quản lý Nhân viên";
            string[] menuItem = {
                "Thêm nhân viên",
                "Cập nhật thông tin Nhân viên",
                "Xóa Nhân viên",
                "Quay lại"
            };

            bool isChoice = false;
            while (!isChoice)
            {
                int choice = Menu(menuTitle, menuItem);
                switch (choice)
                {
                    case 1: /*Thêm nhân viên  */


                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine("   Thêm nhân viên");
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine(emp.Full_Name + " Sếp đang ở Thêm nhân viên");
                            Console.WriteLine("Xin mời nhập những thông tin sau để thêm nhân viên : ");

                            Console.Write("Mã Nhân viên: ");
                            string idEmp = Console.ReadLine();

                            if (empbl.CheckExitsIDUser(idEmp) != true)
                            {
                                Thread.Sleep(300);
                                continue;
                            }
                            string fullname = null;
                            while (true)
                            {
                                Console.Write("Họ Tên : ");
                                fullname = Console.ReadLine();
                                if (sub.IsANotNumber(fullname) != true)
                                {
                                    continue;
                                }
                                break;
                            }
                            string Full_Name = sub.FormatProperCase(fullname);
                            Console.WriteLine(Full_Name);
                            string userN = null;
                            while (true)
                            {
                                Console.Write("Tên đăng nhập : ");
                                userN = Console.ReadLine();
                                if (empbl.GetUsersByUserN(userN) != true)
                                {
                                    continue;
                                }
                                break;
                            }
                            string pass = null;
                            while (true)
                            {
                                Console.Write("Mật Khẩu : ");
                                pass = sub.ReadPassword();
                                if (sub.ValidatePassword(pass, out string mess) != true)
                                {
                                    sub.PrintColorMessage(ConsoleColor.Red, mess);

                                    continue;
                                }
                                break;
                            }
                            string birth = null;
                            while (true)
                            {
                                Console.Write("Ngày sinh (yyyy-MM-dd) : ");
                                birth = Console.ReadLine();


                                if (sub.ValidateTime(birth) != true)
                                {
                                    continue;
                                }
                                if (sub.IsLegalAge(birth) != true)
                                {
                                    continue;
                                }
                                break;
                            }
                            string cmt = null;
                            while (true)
                            {
                                Console.Write("Số CMND : ");
                                cmt = Console.ReadLine();
                                if (sub.ValidateIdentityCardNumber(cmt) != true)
                                {
                                    continue;
                                }
                                break;
                            }
                            Console.Write("Chức vụ : ");
                            string role = Console.ReadLine();

                            empbl.InsertUsers(idEmp, Full_Name, userN, pass, birth, cmt, role);

                            var kj1 = Console.ReadKey();
                            if (kj1.Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                break;
                            }
                        }
                        break;


                    case 2: /*Cập nhật Thông tin Nhân viên */

                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("+--------------------------+");
                            Console.WriteLine("Cập nhật thông tin nhân viên");
                            Console.WriteLine("+--------------------------+");
                            Console.WriteLine(emp.Full_Name + " ! Sếp đang ở Cập nhật Thông tin nhân viên");
                          



                            var kj = Console.ReadKey();
                            switch (kj.Key)
                            {
                                case ConsoleKey.C:
                                    Console.Write("Nhập tên loại món: ");
                                    string name = Console.ReadLine();
                                    foobl.InsertCatagoryFood(name);
                                    break;
                                case ConsoleKey.D:

                                    // Console.InputEncoding = Encoding.UTF8;
                                    //  Console.OutputEncoding = Encoding.UTF8;
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.Write("Mã đồ uống cần cập nhật: ");
                                        string idn = Console.ReadLine();
                                        // Kiểm tra mã có tồn tại không?
                                        //Nếu có tồn tại đưa ra thông tin của mã đồ uống đó
                                        int ID = Convert.ToInt32(idn);
                                        Console.Write("Tên mới: ");
                                        string nameFood = Console.ReadLine();
                                        Console.WriteLine(nameFood);
                                        string NameFood = sub.FormatProperCase(nameFood);
                                        Console.Write("Mã Loại: ");
                                        string iD = Console.ReadLine();
                                        if (sub.IsANumber(iD) != true)
                                        {
                                            continue;
                                        }
                                        int id = Convert.ToInt32(iD);
                                        //Kiểm tra mã loại đồ uống có tồn tại không?
                                        Console.Write("Giá mới: ");
                                        float price = float.Parse(Console.ReadLine());
                                        foobl.UpdateFood(ID, NameFood, id, price);
                                        //Hiển thị thông tin mới đcập nhật của mã đồ uống

                                        var ke = Console.ReadKey();
                                        if (ke.Key == ConsoleKey.Escape)
                                        {
                                            Console.Clear();
                                            break;
                                        }
                                    }

                                    break;
                                default:
                                    break;
                            }

                            var kj1 = Console.ReadKey();
                            if (kj1.Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                break;
                            }
                        }
                        break;

                    case 3: /*Xóa Nhân viên */
                        Console.Clear();
                        //Nếu nuốn xóa một loại đồ uống thì phải xóa hết các đồ uống thuộc loại đấy
                        //
                        break;

                    case 4:
                        Console.Clear();
                        isChoice = true;
                        break;

                    default:
                        Console.Clear();
                        sub.PrintColorMessage(ConsoleColor.Red, "Error, Input from 1 to " + menuItem.Length);
                        break;
                }
            }
        }



    }
}






