using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

using Persistence;
namespace DAL
{
    public class FoodDAL
    {
        private static FoodDAL instance;
        public static FoodDAL Instance
        {
            get { if (instance == null) instance = new FoodDAL(); return FoodDAL.instance; }
            private set { FoodDAL.instance = value; }
        }

        public FoodDAL()
        {

        }

      
        public List<Food> LoadFoodList()
        {
            List<Food> foodList = new List<Food>();
            string query = string.Format("select food.id, name_food,idcatarory,name_catarory,price from food inner join foodcatarory on food.idcatarory=foodcatarory.id;");

            DataTable Data = DBHelper.ExecuteQuery(query);

            foreach (DataRow item in Data.Rows)
            {
                Food food = new Food(item);
                foodList.Add(food);

            }

            return foodList;
        }
      /*   public int Count(int id)
        {
            
            string query = "select * from food inner join foodcatarory on food.idcatarory=foodcatarory.id where food.id=" + id + ";";
            DataTable Data = DBHelper.ExecuteQuery(query);

            foreach (DataRow item in Data.Rows)
            {
                Food food = new Food(item);
                return food.quantity;

            }
            
            return -1;
        }*/
        public bool InsertFood(string name_food, int idcatarory, float price)
        {
            string query = string.Format("insert into food (name_food,idcatarory,price) value (N'{0}',{1},{2},{3})", name_food, idcatarory, price);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool InsertCatagoryFood(string name_catarory)
        {
            string query =string.Format("insert into foodcatarory (name_catarory) value ('{0}')",name_catarory);
            int result= DBHelper.ExecuteNonQuery(query);
            return result>0;
        }
        public bool UpdateFood(int id, string name_food, int idcatarory, float price)
        {
            string query = string.Format("update food set name_catarory = '{0}',idcatarory={1}, price={2} where id= {3}", name_food, idcatarory, price, id);
            int result = DBHelper.ExecuteNonQuery(query);
            return result > 0;
        }
        public List<Food> SearchFood(string name)
        {
            List<Food> catarorylist = new List<Food>();
            string query = string.Format("select * from food inner join foodcatarory on food.idcatarory=foodcatarory.id where name_food like '%{0}%';", name);
            DataTable Data = DBHelper.ExecuteQuery(query);

            foreach (DataRow item in Data.Rows)
            {
                Food food = new Food(item);
                catarorylist.Add(food);

            }

            return catarorylist;
        }
        public int GetfoodbyID(int id)
        {

            string query = string.Format("select * from food  inner join foodcatarory on food.idcatarory=foodcatarory.id where food.id={0}", id);
            DataTable data = DBHelper.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                 Food food = new Food(data.Rows[0]);
               
                return food.id;
            }
          
            return -1;
        }
        public Food GetfoodInfobyID(int id)
        {
            string name_food = null;
            int idcatarory =0;
            string name_catarory = null;
            float price = 0;
            
            Food food= new Food (id, name_food, idcatarory, name_catarory, price);

            string query = string.Format("select * from food  inner join foodcatarory on food.idcatarory=foodcatarory.id where food.id={0}", id);
            DataTable data = DBHelper.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                 food = new Food(data.Rows[0]);

                return food;
            }

            return null;
        }
   /*     public bool UpdateQuantityFood(int id,int quantity)
        {
            string query = string.Format("update food set soluongton= soluongton- {0} where id={1}",quantity, id);
            int resul = DBHelper.ExecuteNonQuery(query);
            return resul > 0;
        }*/
    }
}