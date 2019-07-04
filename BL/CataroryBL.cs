using System;
using System.Collections.Generic;
using ConsoleTables;
using DAL;
using Persistence;

namespace BL
{
    public class CataroryBL
    {
        private CataroryDAL cataroryDAL;
        public CataroryBL() => cataroryDAL = new CataroryDAL();
        public List<Cataroryfood> GetInfo(int id)
        {

            List<Cataroryfood> catalist = cataroryDAL.LoadCataroryList(id);
            var tab = new ConsoleTable("ID", "Name");

            foreach (Cataroryfood item in catalist)
            {
                tab.AddRow(item.id, item.name_catarory);
                tab.Write();


            }

            // Console.WriteLine();
            return catalist;
        }
        public List<Cataroryfood> LoadList()
        {

            List<Cataroryfood> catalist = cataroryDAL.LoadCataroryLis();
            var tab = new ConsoleTable("ID", "Name");

            foreach (Cataroryfood item in catalist)
            {
                tab.AddRow(item.id, item.name_catarory);



            }
            tab.Write();

            // Console.WriteLine();
            return catalist;
        }
        public int check(int id)
        {
            return cataroryDAL.GetbyID(id);
        }

        public void InsertFoodcatarory(string name_catarory)
        {
            if (cataroryDAL.Insertcataroty(name_catarory))
            {
                Console.WriteLine("Successfull!");
                LoadList();
            }
            else
            {
                Console.Write("Error");
            }
        }
        public void UpdateFoodCatarory(int id, string name_catarory)
        {
            if (cataroryDAL.Updatecataroty(id, name_catarory))

            {

                Console.WriteLine("Successfull!");
                LoadList();
            }
            else
            {
                Console.Write("Error");
            }
        }
        public List<Cataroryfood> Search(string name)
        {
            List<Cataroryfood> catalist = cataroryDAL.SearchCatarory(name);
            var tab = new ConsoleTable("ID", "Name");

            foreach (Cataroryfood item in catalist)
            {
                tab.AddRow(item.id, item.name_catarory);



            }
            tab.Write();

            // Console.WriteLine();
            return catalist;
        }
        public bool CheckExitsCatagoryFood(int idCatagory)
        {
             int check = cataroryDAL.GetIdCatagoryfood(idCatagory);
            if (check == -1)
            {
                Console.WriteLine("Không có Loại đồ uống này !");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
