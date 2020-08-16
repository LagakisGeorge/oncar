using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mono.Data.Sqlite;

namespace test4sql
{

    static class Globals
    {
        public static string cIP ;
        public static string cSQLSERVER;

        public static string GReadSQ(string Query)
        {
            string dbPath = Path.Combine(
                  Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                  "adodemo.db3");
            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            // Open the database connection and create table with data
            connection.Open();
            // query the database to prove data was inserted!
            var contents = connection.CreateCommand();
            contents.CommandText = Query;
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            string cc = "";
            while (r.Read())
            { cc = r[0].ToString(); }
            connection.Close();
            return cc;

        }


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
