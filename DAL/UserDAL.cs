using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

using Persistence;
namespace DAL
{
    public class UserDAL
    {
        private static UserDAL instance;
        public static UserDAL Instance
        {
            get { if (instance == null) instance = new UserDAL(); return UserDAL.instance; }
            private set { UserDAL.instance = value; }
        }

        public UserDAL()
        {

        }

        public User GetUsers(string userN, string pass, string role)
        {
            string id = null;
            string fullName = null;
            //  string usersName=null;
            //  string passwords=null;
            DateTime Birthday = DateTime.Now;
            string Cmt = null;
            //   string id_typeacc=null;

            User bill = new User(id, fullName, userN, pass, Birthday, Cmt, role);


            string query = string.Format("select * from users where user_name= {0} and passwords=MD5({1}) and id_typeacc={2};", userN, pass, role);
            DataTable data = DBHelper.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                bill = new User(data.Rows[0]);

                return bill;
            }
            else
            {
                return null;
            }
        }
        public string GetUsersById(string id)
        {




            string query = string.Format("select * from users where id='{0}';", id);
            DataTable data = DBHelper.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                User user = new User(data.Rows[0]);

                return user.IdUser;
            }
            else
            {
                return null;
            }
        }
        public string GetUsersByUserN(string userN)
        {




            string query = string.Format("select * from users where user_name='{0}';", userN);
            DataTable data = DBHelper.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                User user = new User(data.Rows[0]);

                return user.User_Name;
            }
            else
            {
                return null;
            }
        }

        public bool InsertUsers(string id, string fullname, string userN, string pass, string birthday, string cmt, string role)
        {
            string query = string.Format("insert into users (id,full_name,user_name,passwords,birth,so_cmt,id_typeacc) value ('{0}','{1}','{2}',MD5('{3}'),'{4}','{5}','{6}');", id, fullname, userN, pass, birthday, cmt, role);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}