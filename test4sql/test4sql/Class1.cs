using System;
using System.Collections.Generic;
using System.Text;

namespace test4sql
{

    static class Globals
    {
        public static string cIP ;
        public static string cSQLSERVER;


    } 



    public class Monkey
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string idPEL { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
