using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleTables;
using DAL;
using Persistence;

namespace BL
{
    public class BillBL
    {
        private BillDAL billDAL;

        private Support sup = new Support();

        private TableBL tablebl = new TableBL();
        private MenuBL menubl = new MenuBL();
        private FoodBL foobl = new FoodBL();
        private BillInforBL billinfo = new BillInforBL();
        public BillBL() => billDAL = new BillDAL();


        public int GetBillByTableID(int id, int status)/*Lấy bill bằng Mã thẻ */
        {
            return billDAL.GetUncheckBillIdbyTableID(id, status);
        }

        public DateTime? GetcheckoutBillByTableID(int id)/*Lấy thời gian checkout bằng mã thẻ */
        {
            return billDAL.GetcheckoutBillIdbyTableID(id);
        }
        public DateTime? GetcheckinBillByTableID(int id, int status)/*Lấy thời gian check in bằng mã thẻ */
        {
            return billDAL.GetcheckintBillIdbyTableID(id, status);
        }
        public void InsertBill(int idtable)/*Thêm hóa đơn */
        {
            if (billDAL.InsertBill(idtable))
            {
                sup.PrintColorMessage(ConsoleColor.Green, "Tạo thành công!");
            }
        }
        public string CheckOut(int id) /* Cập nhật check out cho hóa đơn */
        {

            if (billDAL.Checkout(id))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write("Ngày thanh toán: ");
                Console.ResetColor();

            }
            return null;
        }

        public void WaitCheckOut(int id) /* Chờ thanh toán, cập nhật trạng thái hóa đơn = 2 */
        {
            if (billDAL.WaitCheckOut(id))
            {
                Console.WriteLine("Chờ thanh toán thành công !");
            }
        }
        /*Get bill by status =2 and status =0 */
        public List<Bill> GetBillbyStatus()/*Lấy hóa đơn theo trạng thái */
        {
            List<Bill> bill = billDAL.GetBillbyStatus();
            string desStatus = null;
            var tab = new ConsoleTable("Mã thẻ", "Ngày tạo", "Ngày thanh toán", "Trạng thái");
            if (bill != null)
            {


                foreach (Bill item in bill)
                {
                    if (item.Status_Bill == 2)
                    {
                        desStatus = item.Status_Bill.ToString("Chờ thanh toán");



                    }
                    if (item.Status_Bill == 0)
                    {
                        desStatus = item.Status_Bill.ToString("Chưa thanh toán");

                    }



                    tab.AddRow(item.Id_Table, item.CheckIn, item.CheckOut, desStatus);

                }


                tab.Write();

                return bill;
            }
            else
            {
                Console.WriteLine("Không có hóa đơn cần thanh toán !");
                return null;
            }
        }


        public void CreatBill(int tableid)/*Tạo hóa đơn */
        {

            //GET BILL ID BY TABLE ID (Lấy hóa đơn bằng mã thẻ)
            int billid = GetBillByTableID(tableid, 0);
            //BILL NOT DOES EXITS --> CREATE BILL (hóa đơn không tồn tại thì tạo hóa đơn)
            if (billid == -1)
            {
                Console.WriteLine("Thẻ này chưa có hóa đơn !");
                Console.WriteLine("Đang tạo....");
                InsertBill(tableid);
                //UPDATE STATUS TABLE SET = 'PEPOLE'
                tablebl.UpdateTable(tableid);
                Thread.Sleep(500);
                billid = GetBillByTableID(tableid, 0);
                //AFTER CREATE BILL--> INSERT ITEMS (BILL INFO)
                billinfo.InsertBillInfo(billid);



            }
            else
            {

                Console.WriteLine("Thẻ này hiện tại không ở trạng thái trống");
                sup.PrintColorMessage1(ConsoleColor.Magenta, "Nhấn N ");
                Console.Write("bạn muốn tiếp tục thêm thực đơn cho thẻ này ");
                sup.PrintColorMessage1(ConsoleColor.Blue, "Nhấn ESC sau đó nhấn EnTer");
                Console.WriteLine(" Để nhập lại Thẻ mong muốn");


                var ky = Console.ReadKey();
                Console.Clear();
                switch (ky.Key)
                {
                    case ConsoleKey.N:
                        // BILL EXITED --> ADD ITEMS (BILL INFO )
                        billinfo.InsertBillInfo(billid);
                        //UPDATE STATUS TABLE SET = 'PEPOLE'
                        tablebl.UpdateTable(tableid);
                        break;
                    default:
                        Console.Clear();
                        break;
                }

            }
        }

        public bool Payment(int id, int status, string empid)/*Thanh toán hóa đơn */
        {
            //Show Menu Pay: NameFood, Count,Price, Total
            menubl.GetMenuListByTableID(id, status);


            if (menubl.total > 0)
            {
                //PAY THE ORDER
                Console.WriteLine("Bạn có muốn thanh toán hóa đơn cho thẻ này không?: ");
                Console.WriteLine("1.Thanh toán ngay \t\t 2.Chờ thanh toán");
                Console.Write("#Chọn: ");
                int choose1 = Convert.ToInt32(Console.ReadLine());
                int d = 0;
                while (true && d < 5)
                {
                    switch (choose1)
                    {
                        case 1:
                            int billida = GetBillByTableID(id, status);
                            string money = null;
                            float guestMoney = 0;


                            //ENTER INFORMATION
                            Console.Write("Tiền khách trả: ");
                            money = Console.ReadLine();
                            if (sup.IsANumber(money) != true)
                            {
                                d++;
                                continue;
                            }


                            guestMoney = float.Parse(money);
                            if (menubl.total > guestMoney)
                            {
                                d++;
                                sup.PrintColorMessage(ConsoleColor.Magenta, "Số tiền không đủ");
                                continue;


                            }
                            Console.WriteLine("Tiền hoàn lại: {0}", guestMoney - menubl.total);
                            //Creating Bill
                            Console.WriteLine("Nhấn phím P để in hóa đơn");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("Đang in hóa đơn cho khách....");
                            Thread.Sleep(2000);

                            //PRINTING ORDER BILL
                            sup.PrintColorMessage(ConsoleColor.Yellow, "+-------------------------------------------------------------+");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "                       ---COFFEE QC---              ");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "       Địa chỉ: Khu 2- Xã Hà Thạch - TX Phú Thọ - Phú Thọ   ");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "    SĐT: 0847323989 - Wifi: CoffeeQC - PassWifi: CoffeeQC02PT    ");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "                       ***************              ");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "\nMã thẻ: " + id);
                            sup.PrintColorMessage(ConsoleColor.Yellow, "Nhân viên thanh toán: " + empid);
                            sup.PrintColorMessage(ConsoleColor.Yellow, String.Format("Ngày tạo: {0:dd/MM/yyyy HH:mm:ss tt}", GetcheckinBillByTableID(id, status)));
                            menubl.GetMenuListByTableID(id, status);
                            sup.PrintColorMessage(ConsoleColor.Yellow, "Tiền khách trả: \t\t\t " + String.Format("{0:0,0 VND}", guestMoney));
                            sup.PrintColorMessage(ConsoleColor.Yellow, "Tiền hoàn :     \t\t\t " + String.Format("{0:0,0 VND}", (guestMoney - menubl.total)));

                            //BILLED PAYMENT (STATUS=1,Checkout Date)
                            CheckOut(billida);
                            sup.PrintColorMessage(ConsoleColor.Yellow, String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", (GetcheckoutBillByTableID(id))));
                            sup.PrintColorMessage(ConsoleColor.Yellow, "  \n                       ***************              ");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "   CHÚC QUÝ KHÁCH CÓ KHOẢNG THỜI GIAN THƯ GIÃN VỚI COFFEE QC(^.^)");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "\t\t      ---HẸN GẶP LẠI---");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "+-----------------------------------------------------------------+");

                            sup.PrintColorMessage(ConsoleColor.Green, "Đã in thành công");
                            //UPDATE STATUS TABLE = 'EMPTY'  
                            tablebl.UpdateTable1(id);

                            break;



                        case 2:
                            Console.Clear();
                            //GIVE THE STATUS OF WAITING PAYMENT (STATUS=2);
                            int billidaa = GetBillByTableID(id, status);
                            WaitCheckOut(billidaa);
                            //UPDATE STATUS TABLE = 'EMPTY'  
                            tablebl.UpdateTable1(id);
                            break;



                    }
                    break;
                }
                return true;

            }
            else
            {

                return false;
            }


        }
        public void CheckCombo(int id)
        {
            if (billDAL.CheckCombo(id))
            {
                Console.WriteLine("OK");
            }
        }
        public void CreatCombo(int tableid)
        {

            //GET BILL ID BY TABLE ID (Lấy hóa đơn bằng mã thẻ)
            int billid = GetBillByTableID(tableid, 0);
            //BILL NOT DOES EXITS --> CREATE BILL (hóa đơn không tồn tại thì tạo hóa đơn)
            if (billid == -1)
            {
                Console.WriteLine("Thẻ này chưa có hóa đơn !");
                Console.WriteLine("Đang tạo....");
                InsertBill(tableid);
                //UPDATE STATUS TABLE SET = 'PEPOLE'
                tablebl.UpdateTable(tableid);
                Thread.Sleep(500);
                billid = GetBillByTableID(tableid, 0);
                //AFTER CREATE BILL--> INSERT ITEMS (BILL INFO)
                billinfo.InsertBillInfo(billid);



            }
            else
            {

                Console.WriteLine("Thẻ này hiện tại không ở trạng thái trống");
                sup.PrintColorMessage1(ConsoleColor.Magenta, "Nhấn N ");
                Console.Write("bạn muốn tiếp tục thêm thực đơn cho thẻ này ");
                sup.PrintColorMessage1(ConsoleColor.Blue, "Nhấn ESC sau đó nhấn EnTer");
                Console.WriteLine(" Để nhập lại Thẻ mong muốn");


                var ky = Console.ReadKey();
                Console.Clear();
                switch (ky.Key)
                {
                    case ConsoleKey.N:
                        // BILL EXITED --> ADD ITEMS (BILL INFO )
                        billinfo.InsertBillInfo(billid);
                        //UPDATE STATUS TABLE SET = 'PEPOLE'
                        tablebl.UpdateTable(tableid);
                        break;
                    default:
                        Console.Clear();
                        break;
                }

            }
        }
        public bool PaymentCombo(int id, int status)
        {
            //Show Menu Pay: NameFood, Count,Price, Total
            menubl.GetMenuListByTableID(id, status);


            if (menubl.total > 0)
            {
                //PAY THE ORDER
                Console.WriteLine("Bạn có muốn thanh toán hóa đơn cho Combo này không?: ");
                Console.WriteLine("1.Thanh toán ngay \t\t 2.Chờ thanh toán");
                Console.Write("#Chọn: ");
                int choose1 = Convert.ToInt32(Console.ReadLine());
                int d = 0;
                while (true && d < 5)
                {
                    switch (choose1)
                    {
                        case 1:
                            int billida = GetBillByTableID(id, status);
                            string money = null;
                            float guestMoney = 0;


                            //ENTER INFORMATION
                            Console.Write("Tiền khách trả: ");
                            money = Console.ReadLine();
                            if (sup.IsANumber(money) != true)
                            {
                                d++;
                                continue;
                            }


                            guestMoney = float.Parse(money);
                            if (menubl.total > guestMoney)
                            {
                                d++;
                                sup.PrintColorMessage(ConsoleColor.Magenta, "Số tiền không đủ");
                                continue;


                            }
                            Console.WriteLine("Tiền hoàn lại: {0}", guestMoney - menubl.total);
                            //Creating Bill
                            Console.WriteLine("Nhấn phím P để in hóa đơn");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("Đang in hóa đơn cho khách....");
                            Thread.Sleep(2000);

                            //PRINTING ORDER BILL
                            sup.PrintColorMessage(ConsoleColor.Yellow, "+-------------------------------------------------------------+");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "                       ---COFFEE QC---              ");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "       Địa chỉ: Khu 2- Xã Hà Thạch - TX Phú Thọ - Phú Thọ   ");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "    SĐT: 0847323989 - Wifi: CoffeeQ - PassWifi: CoffeeQC02PT    ");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "Mã thẻ: " + id);

                            sup.PrintColorMessage(ConsoleColor.Yellow, String.Format("Ngày tạo: {0:dd/MM/yyyy HH:mm:ss tt}", GetcheckinBillByTableID(id, status)));
                            menubl.GetMenuListByTableID(id, status);
                            sup.PrintColorMessage(ConsoleColor.Yellow, "Tiền khách trả: \t\t\t " + String.Format("{0:0,0 VND}", guestMoney));
                            sup.PrintColorMessage(ConsoleColor.Yellow, "Tiền hoàn :     \t\t\t " + String.Format("{0:0,0 VND}", (guestMoney - menubl.total)));

                            //BILLED PAYMENT (STATUS=1,Checkout Date)
                            CheckOut(billida);
                            sup.PrintColorMessage(ConsoleColor.Yellow, String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", (GetcheckoutBillByTableID(id))));
                            sup.PrintColorMessage(ConsoleColor.Yellow, "Quý khách xin để lại nhận xét và góp ý cho Coffee QC tại Bảng đánh giá \n");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "CHÚC QUÝ KHÁCH CÓ KHOẢNG THỜI GIAN THƯ GIÃN VỚI COFFEE QC(^.^) \n");
                            sup.PrintColorMessage(ConsoleColor.Yellow, "+-----------------------------------------------------------------+");

                            sup.PrintColorMessage(ConsoleColor.Green, "Đã in thành công");
                            //UPDATE STATUS TABLE = 'EMPTY'  
                            // tablebl.UpdateTable1(id);
                            //  CheckCombo(billida);

                            break;



                        case 2:
                            Console.Clear();
                            //GIVE THE STATUS OF WAITING PAYMENT (STATUS=2);
                            int billidaa = GetBillByTableID(id, status);
                            WaitCheckOut(billidaa);
                            //UPDATE STATUS TABLE = 'EMPTY'  
                            // tablebl.UpdateTable1(id);
                            break;



                    }
                    break;
                }
                return true;

            }
            else
            {

                return false;
            }

        }
        public bool DeleteBill()
        {
            return billDAL.DeleteBill();
        }
    }
}

