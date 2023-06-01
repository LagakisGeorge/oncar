using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Mono.Data.Sqlite;
using SharpCifs.Util.Sharpen;

namespace test4sql
{

    static class Globals
    {

       
        public static string cIP ;
        public static string cSQLSERVER;
        public static string cFORTHGO;
        public static string gIDPARAGG;
        public static string gtIDPARAGG;
        public static string gIDBARDIA;
        public static string gKathg;
        public static string gIDEIDOS;
        public static string gTrapezi;
        public static string gUserWaiter;
        public static string gPWD;



        public static string gIPKleis;
        public static string cIPPR1;
        public static string cIPPR2;
        public static string cIPPR3;
        public static string gTITLOS;

        public static string gLocal;


        public static void AllExecute(string Query)
        {
            if (Globals.gLocal == "1")
            {
                Query = Query.ReplaceAll("ISNULL", "ifnull");
                Query = Query.ReplaceAll("GETDATE", "DATE");
                MainPage.ExecuteSqlite(Query);
            }
            else
            {
                Globals.ExecuteSQLServer(Query);
            }

        }
        // AllRead
        public static string AllRead(string Query)
        {
            if (Globals.gLocal == "1")
            {
                Query = Query.ReplaceAll("ISNULL", "ifnull");
                Query = Query.ReplaceAll("GETDATE", "DATE");
                return PARAGGELIES.ReadSQL(Query); ;
            }
            else
            {
                return Globals.ReadSQLServer(Query);
            }

        }




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
        public static string useBarcodes="0";
        public static string[,] PARAGGlines = new string[100, 6];
        public static int indexParaggLine;

        public static float FReadSQLServer(string sql)
        {



            if (Globals.cSQLSERVER.Length < 2)
            {
                // await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return 0;
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                // await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }


            string SYNT = "";

            try
            {


                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand(sql, con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();


                float ret;
                string cc;
                cc = dt.Rows[0][0].ToString();
                if (cc.Length == 0) { ret = 0; }
                else
                {
                    ret = float.Parse(cc);
                }
                    con.Close();
                return ret;


            }
            catch (Exception ex)
            {
                return 0;
                // await DisplayAlert("Error", ex.ToString(), "OK");
            }



        }

        public static string ReadSQLServer(string sql)
        {

       

          if (Globals.cSQLSERVER.Length< 2)
            {
                // await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return "";
            }
           string[] lines = Globals.cSQLSERVER.Split(';');
           string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                  // await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }


               string SYNT = "";

          try
          {

   
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand(sql, con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();


                string ret;
                ret= dt.Rows[0][0].ToString();
                con.Close();
                return ret;
        
    
          }
          catch (Exception ex)
          {
                return "";
                // await DisplayAlert("Error", ex.ToString(), "OK");
          }

               
            
        }

        public static string ReadSQLServerWithError(string sql)
        {



            if (Globals.cSQLSERVER.Length < 2)
            {
                // await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return "";
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                // await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }


            string SYNT = "";

            try
            {


                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand(sql, con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();


                string ret;
                ret = dt.Rows[0][0].ToString();
                con.Close();
                return ret;


            }
            catch (Exception ex)
            {
                return "ERROR ΜΗ ΣΥΝΔΕΣΗ";
                // await DisplayAlert("Error", ex.ToString(), "OK");
            }



        }
        public static void ExecuteSQLServer(string sql)
        {


            // public static SqlConnection con;
        // DESKTOP-MPGU8SB\SQL17
        string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            //            string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            // ok fine string constring = @"Data Source=DESKTOP-MPGU8SB\SQL17,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";
            // ok works fine string constring = @"Data Source=192.168.1.10,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            SqlConnection  con = new SqlConnection(constring);

            try
            {
                con.Open();

                // await DisplayAlert("Συνδεθηκε", "οκ", "OK");

                // ***************  demo πως τρεχω εντολη στον sqlserver ********************************
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                string cv = "";
               // DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }

        }

    }

    public class Monkey2
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string idPEL { get; set; }

        public string Prood { get; set; }

        public string ID  { get; set; }

        public override string ToString()
        {
            return Name;
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
