using System;
using System.Data;


namespace Persistence
{
    public class User
    {
        private string id { get; set; }
        private string fullName { get; set; }
        private string usersName { get; set; }
        private string passwords { get; set; }
        private DateTime birthday { get; set; }
        private string cmt { get; set; }
        private string id_typeacc { get; set; }



        public User()
        {
        }
        public User(string id, string fullName, string usersName, string passwords, DateTime birthday, string cmt, string id_typeacc)
        {
            this.IdUser = id;
            this.Full_Name = fullName;
            this.User_Name = usersName;
            this.Passwords = passwords;
            this.Birthday = birthday;
            this.Cmt = cmt;
            this.Id_TypeAcc = id_typeacc;


        }
        public User(DataRow row)
        {
            this.id = row["id"].ToString();
            this.fullName = row["Full_Name"].ToString();
            this.usersName = row["user_name"].ToString();
            this.passwords = row["passwords"].ToString();
            this.birthday = (DateTime)row["birth"];
            this.cmt = row["so_cmt"].ToString();
            this.id_typeacc = row["id_typeacc"].ToString();
        }
        public string IdUser
        {
            get { return id; }
            set { value = id; }
        }
        public string Full_Name
        {
            get { return fullName; }
            set { value = fullName; }
        }
        public string User_Name
        {
            get { return usersName; }
            set { value = usersName; }
        }
        public string Passwords
        {
            get { return passwords; }
            set { value = passwords; }
        }
        public DateTime Birthday
        {
            get { return birthday; }
            set { value = birthday; }
        }
        public string Cmt
        {
            get { return cmt; }
            set { value = cmt; }
        }
        public string Id_TypeAcc
        {
            get { return id_typeacc; }
            set { value = id_typeacc; }
        }


    }
}