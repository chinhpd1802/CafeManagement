using System;
using System.Collections.Generic;
using ConsoleTables;
using DAL;
using Persistence;

namespace BL
{
    public class TableBL
    {
        private TableDAL tableDAL;
        public TableBL() => tableDAL = new TableDAL();
        public List<Table> GetInfo()
        {
            List<Table> table= tableDAL.LoadTableList();;
                var tab = new ConsoleTable("Mã Thẻ","Tên thẻ","Trạng thái thẻ");
          foreach(Table item in table)
          {
              tab.AddRow(item.ID,item.Name_Table,item.Status_Table);
              
          }
          tab.Write();
         // Console.WriteLine();
          return table;
        }
        public List<Table> LoadComboList()
        {
             List<Table> table= tableDAL.LoadComboList();;
                var tab = new ConsoleTable("ID","Name","Status");
          foreach(Table item in table)
          {
              tab.AddRow(item.ID,item.Name_Table,item.Status_Table);
              
          }
          tab.Write();
         // Console.WriteLine();
          return table;
        }
        public void UpdateTable(int id)
        {
            if (tableDAL.UpdateTable(id))
            {
                Console.WriteLine("Ok");
            }
        }
         public void UpdateTable1(int id)
        {
            if (tableDAL.UpdateTable1(id))
            {
                Console.WriteLine("Ok");
            }
        }
        public int CheckStatusTable(int id)
        {
            return tableDAL.checkStatus(id);
        }

       public bool CheckExistCard(int id)
       {
          int check=tableDAL.CheckTableid(id);
          if (check ==-1)
          {
              Console.WriteLine("Không có Thẻ này !");
            return false;            
          }
          else {
              return true;
          }
        
           
       }
    }
}
