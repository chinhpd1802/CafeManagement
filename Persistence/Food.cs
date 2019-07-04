using System;
using System.Data;


namespace Persistence
{
    public class Food
    {
        public int id { get; set; }
        public string name_food { get; set; }
        public int idcatarory { get; set; }
        public string name_catarory { get; set; }
        public float price { get; set; }

        public Food()
        {
        }
        public Food(int id, string name_food, int idcatarory, string name_catarory, float price)
        {
            this.id = id;
            this.name_food = name_food;
            this.idcatarory = idcatarory;
            this.name_catarory = name_catarory;
            this.price = price;
        }
        public Food(DataRow row)
        {
            this.id = (int)row["id"];
            this.name_food = row["name_food"].ToString();
            this.idcatarory = (int)row["idcatarory"];
            this.name_catarory = row["name_catarory"].ToString();
           
            this.price = (float)row["price"];
        }

    }
}