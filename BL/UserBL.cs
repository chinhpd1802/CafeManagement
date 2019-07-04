using System;
using System.Collections.Generic;
using System.Threading;
using DAL;
using Persistence;

namespace BL
{
    public class UserBL
    {
        private UserDAL empdal;
        private Support sub = new Support();
        public UserBL() => empdal = new UserDAL();
        public User GetUsers(string userN, string pass)
        {
            string[] roles = { "'r01'", "'r02'" };
            User emp = null;
            for (int i = 0; i < roles.Length; i++)
            {
                emp = empdal.GetUsers(userN, pass, roles[i]);
                if (emp != null)
                {
                    return emp;
                }
            }
            return null;
        }
        public void InsertUsers(string id, string fullname, string userN, string pass, string birthday, string cmt, string role)
        {
            if (empdal.InsertUsers(id,fullname,userN,pass,birthday,cmt,role))
            {
                Console.WriteLine("Thêm thành công thành viên mới "+ fullname +"[" +id +"]");
            }
        }
        public bool CheckExitsIDUser(string id)
        {
           
            if (empdal.GetUsersById(id) != null)
            {
                sub.PrintColorMessage(ConsoleColor.Red,"Mã Nhân viên đã tồn tại !");
                return false;
            }
            else 
            {
                return true;
            }
        }
          public bool GetUsersByUserN(string userN)
         {
             
            if (empdal.GetUsersByUserN(userN) != null)
            {
                sub.PrintColorMessage(ConsoleColor.Red,"Tài khoản đã tồn tại !");
                return false;
            }
            else 
            {
                 sub.PrintColorMessage(ConsoleColor.Green,"Hợp Lệ !");
                return true;
            }
         }

    }
}
