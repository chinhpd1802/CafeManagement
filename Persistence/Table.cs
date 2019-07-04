using System;
using System.Data;


namespace Persistence
{
    public class Table
    {
        private int id { get; set; }
        private string name_table { get; set; }
        private string status_table{get;set;}
        public Table(int id, string name_table,string status_table)
        {
            this.ID=id;
            this.Name_Table=name_table;
            this.Status_Table=status_table;
           
        }
        public Table()
        {
        }
        public Table(DataRow row)
        {
            this.id=(int)row["id"];
            this.name_table=row["name_table"].ToString();
            this.status_table=row["status_table"].ToString();
            



        }

        
        public int ID 
        {
            get{ return id;}
            set{ value=id;}
        }
         public string Name_Table 
        {
            get{ return name_table;}
            set{ value=name_table;}
        }
         public string Status_Table
        {
            get{ return status_table;}
            set{ value=status_table;}
        }
    }


}