using System;
using System.Data;


namespace Persistence
{
    public class Cataroryfood
    {
        public int id { get; set; }
        public string name_catarory { get; set; }
        
        public Cataroryfood(int id, string name_catarory)
        {
            this.id = id;
            this.name_catarory = name_catarory;
    


        }
        public Cataroryfood(DataRow row)
        {
            this.id = (int)row["id"];
            this.name_catarory = row["name_catarory"].ToString();
          





        }

        public Cataroryfood()
        {
        }
    }


}