using System;
using System.Data;


namespace Persistence
{
    public class Menu
    {
        public int count { get; set; }
        public string foodName { get; set; }
        public float price { get; set; }
        public float totalPrice { get; set; }

        public Menu(string foodName, int count, float price, float totalPrice)
        {
            this.foodName = foodName;
            this.count = count;

            this.price = price;
            this.totalPrice = totalPrice;
        }
        public Menu(DataRow row)
        {
            this.foodName = row["name_food"].ToString();
            this.count = (int)row["soluong"];
            this.price = (float)Convert.ToDouble(row["price"].ToString());
            this.totalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
        }

        public Menu()
        {
        }


    }


}