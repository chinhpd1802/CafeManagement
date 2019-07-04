using System;
using System.Collections.Generic;
using ConsoleTables;
using DAL;
using Persistence;

namespace BL
{
    public class FoodBL
    {
        private CataroryBL cata = new CataroryBL();
        private FoodDAL foodDAL;
      
        public FoodBL() => foodDAL = new FoodDAL();

        public bool CheckExistFood(int id)
        {
            int check = foodDAL.GetfoodbyID(id);
            if (check == -1)
            {
                Console.WriteLine("Không có đồ uống này !");
                return false;
            }
            else
            {
                return true;
            }
        }
        public Food GetFoodInfoByID(int id)
        {
            Food food=foodDAL.GetfoodInfobyID(id);
            var tab = new ConsoleTable("Mã Đồ Uống", "Tên đồ uống", "Tên Loại đồ uống", "Đơn giá");
         
            tab.AddRow(food.id,food.name_food,food.name_catarory,String.Format("{0:0,0 VND}",food.price));


            
            
            tab.Write();
            return food;
        }
        public List<Food> LoadFoodList()
        {

            List<Food> foodList = foodDAL.LoadFoodList();
            var tab = new ConsoleTable("Mã đồ uống", "Tên", "Loại", "Giá");
            foreach (Food item in foodList)
            {
                tab.AddRow(item.id, item.name_food, item.name_catarory, String.Format("{0:0,0 VND}", item.price));
            }
            tab.Write();

          
            return foodList;
        }
        
        public void InsertFood(string name_food, int idcatarory,  float price)
        {

            if (foodDAL.InsertFood(name_food, idcatarory,  price))
            {
                Console.WriteLine("Thành Công!");
                LoadFoodList();
            }
            else
            {
                Console.Write("Có lỗi xảy ra");
            }
        }

        public void UpdateFood(int id, string name_food, int idcatarory, float price)
        {
            if (foodDAL.UpdateFood( id, name_food, idcatarory, price))
            {
                Console.WriteLine("Thành Công");
            }
        }
        public List<Food> SearchFood(string name)
        {
            List<Food> listFood = foodDAL.SearchFood(name);
            var tab = new ConsoleTable("Mã đồ uống", "Tên", "Đơn giá");
            foreach (Food item in listFood)
            {
                tab.AddRow(item.id, item.name_food, String.Format("{0:0,0 VND}",item.price));



            }
            tab.Write();
            return listFood;

        }
        public void InsertCatagoryFood(string name_catarory)
        {
             if (foodDAL.InsertCatagoryFood(name_catarory))
            {
                Console.WriteLine("Successfull!");
                cata.LoadList();
            }
            else
            {
                Console.Write("Error");
            }
        }
   /*      public bool CheckQuantity(int amount, int id)
        {
            int count = foodDAL.Count(id);
            try
            {
                if (count != -1)
                {

                    if (count >= amount)
                    {
                        return true;
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return false;
        }
        /*  public void UpdateFoodCatarory(int id, string name_catarory)
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
         public List<Cataroryfood> Search(string name)*/

    }
}
