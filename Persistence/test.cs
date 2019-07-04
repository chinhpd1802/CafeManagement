using System;
using System.Data;


namespace Persistence
{
    public class test
    {
        public int id { get; set; }
        public int count { get; set; }


        public test(int id, int count)
        {
            this.id = id;
            this.count = count;

        }

        public test()
        {
        }
    }


}