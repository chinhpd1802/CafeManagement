using System;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;
namespace BL
{
    public class Support
    {

        List<string> Address = new List<string>(){"An Giang","Ba Ria Vung Tau","Bac Giang","Bac Kan","Bac Lieu","Bac Ninh","Ben Tre",
         "Binh Dinh","Binh Duong","Binh Phuoc","Binh Thuan","Ca Mau","Cao Bang","Dak Lak","Dak Nong","Dien Bien","Dong Nai","Dong Thap",
         "Gia Lai","Ha Giang","Ha Nam","Ha Tinh","Hai Duong","Hau Giang","Hoa Binh","Hung Yen","Khanh Hoa","Kien Giang","Kon Tum","Lai Chau",
         "Lam Dong","Lang Son","Lao Cai","Long An","Nam Dinh","Nghe An","Ninh Binh","Ninh Thuan","Phu Tho","Quang Binh","Quang Nam",
         "Quang Ngai","Quang Ninh","Quang Tri","Soc Trang","Son La","Tay Ninh","Thai Binh","Thai Nguyen","Thanh Hoa","Thua Thien Hue",
         "Tien Giang","Tra Vinh","Tuyen Quang","Vinh Long","Vinh Phuc","Yen Bai","Phu Yen","Can Tho","Da Nang","Hai Phong","Ha Noi","TP HCM"};

        public Support()
        {
        }

        public bool IsANumber(string number)/* Kiểm tra có pahir giá trị số */
        {
            try
            {
                int.Parse(number);
                return true;
            }
            catch (Exception)
            {
                PrintColorMessage(ConsoleColor.Red, number + " is not Number !");
                return false;
            }
        }

        public bool IsANotNumber(string character)/* Kiểm tra có pahir giá trị số */
        {
            try
            {
                var hasNumber = new Regex(@"[0-9]+");
                if (!hasNumber.IsMatch(character))
                {
                    PrintColorMessage(ConsoleColor.Green, "Hợp lệ");
                }
                return true;
            }
            catch (Exception)
            {
                PrintColorMessage(ConsoleColor.Red, character + " Có chứa ký tự số !");
                return false;
            }
        }



        public bool IsRealAddress(string a) /*Kiểm tra địa chỉ */
        {
            try
            {
                if (Address.Contains(a) != true)
                {
                    return false;
                }
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }


        public void PrintColorMessage(ConsoleColor color, string message)/*Thay đổi màu chữ */
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void PrintColorMessage1(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;

            Console.Write(message);
            Console.ResetColor();
        }
        public string PrintColorMessage2(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            return message;

        }
        public bool ValidatePassword(string password, out string ErrorMessage)/*Kiểm tra điều kiện Mật khẩu */
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Mật khẩu không được để trống");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái viết thường";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái viết hoa";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Mật khẩu không được nhỏ hơn 8 kí tự";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Mật khẩu phải chứa ít nhất một giá trị số";
                return false;
            }

            else if (hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Mật khẩu không chứa  ký tự trường hợp đặc biệt";
                return false;
            }
            else
            {
                PrintColorMessage(ConsoleColor.Green, "Hợp lệ");
                return true;
            }
        }
        public string FormatProperCase(string str)/*Chuẩn hóa tên */
        {
            CultureInfo cultureInfo = new CultureInfo("vi-VN");
            TextInfo textInfo = cultureInfo.TextInfo;
            str = textInfo.ToLower(str);
            // Replace multiple white space to 1 white  space
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s{2,}", " ");
            //Upcase like Title
            return textInfo.ToTitleCase(str);
        }
        public string ReadPassword()/*Ẩn mật khẩu */
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }

            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }
        public bool ValidateTime(string date)/*Kiểm tra thời gian nhập vào */
        {

            try
            {
                DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d);
                return true;
            }
            catch
            {
                PrintColorMessage(ConsoleColor.Red, "Thời gian nhập không đúng !");

                return false;
            }
        }

        public bool IsLegalAge(string inputDate)/* Kiểm tra độ tuổi */

        {
            try
            {
                var bday = Convert.ToDateTime(inputDate);
                var ts = DateTime.Today - bday;
                var year = DateTime.MinValue.Add(ts).Year - 1;
                if (year >= 18)
                {
                    PrintColorMessage(ConsoleColor.Green, "Độ tuổi phù hợp");

                }
                return true;
            }
            catch (Exception)
            {

                return false;

            }
        }
        public bool ValidateIdentityCardNumber(string cmt)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{9}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (hasUpperChar.IsMatch(cmt))
            {
                PrintColorMessage(ConsoleColor.Red, "Không được chứa chữ cái");
                return false;
            }
            else if (hasLowerChar.IsMatch(cmt))
            {
                PrintColorMessage(ConsoleColor.Red, "Không được chứa chữ cái");
                return false;
            }
            else if (hasSymbols.IsMatch(cmt))
            {
                PrintColorMessage(ConsoleColor.Red, "Không được chứa ký tự đặc biệt");
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(cmt))
            {
                PrintColorMessage(ConsoleColor.Red, "CMND chưa đủ độ dài");
                return false;
            }
            else
            {

                PrintColorMessage(ConsoleColor.Green, "Hợp Lệ !");
                return true;
            }

        }
    }
}
