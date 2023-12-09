using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Mono.Data.Sqlite;
using SharpCifs.Util.Sharpen;
using static Android.Provider.ContactsContract.CommonDataKinds;
using static Java.Text.Normalizer;
using Xamarin.Forms;

namespace test4sql
{

    static class Globals
    {
        public static string[] psw = new string[100];


        public static string cIP;
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
        //'SELECT STR(ISNULL(SHOWOLATRAP,0))+';'+STR(ISNULL(LOGPRINPLIR,0))+';'+STR(ISNULL(EKTPRINPLIR,0))+';'+STR(ISNULL(RESERVEDBYONE,0)) FROM [BARELL].[dbo].[MEM]


        public static int gSHOWOLATRAP;
        public static int gLOGPRINPLIR;
        public static int gEKTPRINPLIR;
        public static int gRESERVEDBYONE;
        public static int gCANOPENBARDIA;





        public static string gIPKleis;
        public static string cIPPR1;
        public static string cIPPR2;
        public static string cIPPR3;
        public static string gTITLOS;

        public static string gLocal;
        // public connection για SQLITE
        public static SqliteConnection gconnection = new SqliteConnection("Data Source=" + Path.Combine(
                  Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                  "adodemo.db3"));


        public static string ReadSQL(string Query)
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




        public static int NReadSQL(string Query)
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

            int cc = 0;
            while (r.Read())
            { cc = Convert.ToInt32(r[0].ToString()); }
            connection.Close();
            return cc;

        }





        public static void AllExecute(string Query)
        {
            if (Globals.gLocal == "0")
            {
                Globals.ExecuteSQLServer(Query);
            }
            else
            {
                Query = Query.ReplaceAll("ISNULL", "ifnull");
                Query = Query.ReplaceAll("GETDATE", "DATE");
                MainPage.ExecuteSqlite(Query);
            }

        }
        // AllRead
        public static string AllRead(string Query)
        {
            if (Globals.gLocal == "0")
            {

                return Globals.ReadSQLServer(Query);



            }
            else
            {
                Query = Query.ReplaceAll("ISNULL", "ifnull");
                Query = Query.ReplaceAll("GETDATE", "DATE");
                return ReadSQL(Query); ;
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
        public static string useBarcodes = "0";
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

            SqlConnection con = new SqlConnection(constring);

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

        public static int ExecuteSQLServer(string sql, int ok)
        {


            // public static SqlConnection con;
            // DESKTOP-MPGU8SB\SQL17
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            //            string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            // ok fine string constring = @"Data Source=DESKTOP-MPGU8SB\SQL17,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";
            // ok works fine string constring = @"Data Source=192.168.1.10,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            SqlConnection con = new SqlConnection(constring);

            try
            {
                con.Open();

                // await DisplayAlert("Συνδεθηκε", "οκ", "OK");

                // ***************  demo πως τρεχω εντολη στον sqlserver ********************************
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (Exception ex)
            {
                string cv = "";
                // DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
                return 0;
            }

        }

        public static string SameLetters(string ST)
        {
            int l, k;
            string s;
            int N;
            string C;

            //     If gCapitals = 1 Then
            ST = ST.ToUpper();

            ST = ST.Replace("'", "~");


            if (ST.Length > 1)
            {
                ST = ST.Replace("*", "%");
            }


            l = ST.Length;
            s = "";
            string alpha = "QWERTYUIOPASDFGHJKLZXCVBNMςΕΡΤΥΘΙΟΠΑΣΔΦΓΗΞΚΛΖΧΨΩΒΝΜςερτυθιοπασδφγηξκλζχψωβνμqwertyuiopasdfghjklzxcvbnmάέύίόήώ";
            for (k = 0; k < l; k++)
            {

                C = ST.Substring(k, 1);

                N = alpha.IndexOf(C);

                if (N <= 0)
                {
                    s = s + C;
                }
                else

                {
                    string v = "aAαΑά"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "βΒbB"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "γΓgG"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "δΔdD"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "εΕeEέ"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "ζΖzZ"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "ηήΗhH"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "θΘuU"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "ιίΙiI"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "κΚkK"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "λΛlL"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "μΜmM"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }

                    v = "νΝnN"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "ξΞjJ"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "όοΟoO"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "πΠpP"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "ρΡrR"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "σςΣsS"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }

                    v = "τΤtT"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "ύυΥyY"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "φΦfF"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "χΧxX"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "ψΨcC"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "ώωΩvV"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }

                    v = "qQ"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }
                    v = "wW"; if (v.IndexOf(C) > 0) { s = s + "[" + v + "]"; continue; }

                }

            } //next

            return s;
            // returns index of substring cream
            //  string str = "Ice cream";
            // int result = str.IndexOf("cream");
            //  4

            //  string searchString = "\u00ADm";
            //   string s1 = "ani\u00ADmal";
            //   string s2 = "animal";

            //   Console.WriteLine(s1.IndexOf(searchString, 2, 4, StringComparison.CurrentCulture));
            //   Console.WriteLine(s1.IndexOf(searchString, 2, 4, StringComparison.Ordinal));
            //   Console.WriteLine(s2.IndexOf(searchString, 2, 4, StringComparison.CurrentCulture));
            //   Console.WriteLine(s2.IndexOf(searchString, 2, 4, StringComparison.Ordinal));

            // The example displays the following output:
            //       4
            //       3
            //       3
            //       -1
        }




        public static string Idia(string ST)

        {
            int l, k;
            string s;
            int N;
            string C;

            //     If gCapitals = 1 Then
            ST = ST.ToUpper();

            ST = ST.Replace("'", "~");


            if (ST.Length > 1)
            {
                ST = ST.Replace("*", "%");
            }


            l = ST.Length;
            s = "";
            string alpha = "QWERTYUIOPASDFGHJKLZXCVBNMςΕΡΤΥΘΙΟΠΑΣΔΦΓΗΞΚΛΖΧΨΩΒΝΜςερτυθιοπασδφγηξκλζχψωβνμqwertyuiopasdfghjklzxcvbnmάέύίόήώ";
            for (k = 0; k < l; k++)
            {

                C = ST.Substring(k, 1);

                N = alpha.IndexOf(C);

                if (N <= 0)
                {
                    s =  s + "('"+C+"')"; continue;
                }
                else

                {
                    string v = "aAαΑά"; if (v.IndexOf(C) > 0) { s = s + "('Α','A')"; continue; }
                    v = "βΒbB"; if (v.IndexOf(C) > 0) { s = s + "('Β','B')"; continue; }
                    v = "γΓgG"; if (v.IndexOf(C) > 0) { s = s + "('G','Γ')"; continue; }
                    v = "δΔdD"; if (v.IndexOf(C) > 0) { s = s + "('Δ','D')"; continue; }
                    v = "εΕeEέ"; if (v.IndexOf(C) > 0) { s = s + "('Ε','E')"; continue; }
                    v = "ζΖzZ"; if (v.IndexOf(C) > 0) { s = s + "('Ζ','Z')"; continue; }
                    v = "ηήΗhH"; if (v.IndexOf(C) > 0) { s = s + "('Η','H')"; continue; }
                    v = "θΘuU"; if (v.IndexOf(C) > 0) { s = s + "('Θ','U')"; continue; }
                    v = "ιίΙiI"; if (v.IndexOf(C) > 0) { s = s + "('Ι','I')"; continue; }
                    v = "κΚkK"; if (v.IndexOf(C) > 0) { s = s + "('Κ','K')"; continue; }
                    v = "λΛlL"; if (v.IndexOf(C) > 0) { s = s + "('Λ','L')"; continue; }
                    v = "μΜmM"; if (v.IndexOf(C) > 0) { s = s + "('Μ','M')"; continue; }

                    v = "νΝnN"; if (v.IndexOf(C) > 0) { s = s + "('Ν','N')"; continue; }
                    v = "ξΞjJ"; if (v.IndexOf(C) > 0) { s = s + "('Ξ','J')"; continue; }
                    v = "όοΟoO"; if (v.IndexOf(C) > 0) { s = s + "('Ο','O')"; continue; }
                    v = "πΠpP"; if (v.IndexOf(C) > 0) { s = s + "('Π','P')"; continue; }
                    v = "ρΡrR"; if (v.IndexOf(C) > 0) { s = s + "('Ρ','R')"; continue; }
                    v = "σςΣsS"; if (v.IndexOf(C) > 0) { s = s + "('Σ','S')"; continue; }

                    v = "τΤtT"; if (v.IndexOf(C) > 0) { s = s + "('Τ','T')"; continue; }
                    v = "ύυΥyY"; if (v.IndexOf(C) > 0) { s = s + "('Υ','Y')"; continue; }
                    v = "φΦfF"; if (v.IndexOf(C) > 0) { s = s + "('Φ','F')"; continue; }
                    v = "χΧxX"; if (v.IndexOf(C) > 0) { s = s + "('Χ','X')"; continue; }
                    v = "ψΨcC"; if (v.IndexOf(C) > 0) { s = s + "('Ψ','C')"; continue; }
                    v = "ώωΩvV"; if (v.IndexOf(C) > 0) { s = s + "('Ω','V')"; continue; }

                    v = "qQ"; if (v.IndexOf(C) > 0) { s = s + "('Q')"; continue; }
                    v = "wW"; if (v.IndexOf(C) > 0) { s = s + "('W')"; continue; }

                }

            } //next

            return s;
            // returns index of substring cream
            //  string str = "Ice cream";
            // int result = str.IndexOf("cream");
            //  4

            //  string searchString = "\u00ADm";
            //   string s1 = "ani\u00ADmal";
            //   string s2 = "animal";

            //   Console.WriteLine(s1.IndexOf(searchString, 2, 4, StringComparison.CurrentCulture));
            //   Console.WriteLine(s1.IndexOf(searchString, 2, 4, StringComparison.Ordinal));
            //   Console.WriteLine(s2.IndexOf(searchString, 2, 4, StringComparison.CurrentCulture));
            //   Console.WriteLine(s2.IndexOf(searchString, 2, 4, StringComparison.Ordinal));

            // The example displays the following output:
            //       4
            //       3
            //       3
            //       -1
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
