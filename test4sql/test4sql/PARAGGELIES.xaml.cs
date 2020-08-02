using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;



using System.Data.SqlClient;





using Mono.Data.Sqlite;
using System.Data;


namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PARAGGELIES : ContentPage
    {
        public PARAGGELIES()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            AFM.Focus();
        }






        async void BRES_AFM(object sender, EventArgs e)
        {

            // determine the path for the database file
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "adodemo.db3");
            bool exists = File.Exists(dbPath);
            if (!exists)
            {
                Console.WriteLine("Creating database");
                // Need to create the database before seeding it with some data
                Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);

            }

            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            // Open the database connection and create table with data
            connection.Open();

            /* var c = connection.CreateCommand();           
            c.CommandText = "Query";
            var rowcount = c.ExecuteNonQuery(); // rowcount will be 1    */

            // query the database to prove data was inserted!
            var contents = connection.CreateCommand();
            contents.CommandText = "SELECT  * from PEL WHERE AFM like '%" + AFM.Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {
                LABPEL.Text = r["EPO"].ToString();
            }
            // r["ONO"].ToString();



            connection.Close();

            BARCODE.Focus();





        }


        async void Click_Login(object sender, EventArgs e)
         {
            // determine the path for the database file
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "adodemo.db3");
            bool exists = File.Exists(dbPath);
            if (!exists)
            {
                Console.WriteLine("Creating database");
                // Need to create the database before seeding it with some data
                Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);

            }

            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            // Open the database connection and create table with data
            connection.Open();

            /* var c = connection.CreateCommand();           
            c.CommandText = "Query";
            var rowcount = c.ExecuteNonQuery(); // rowcount will be 1    */

            // query the database to prove data was inserted!
            var contents = connection.CreateCommand();
            contents.CommandText = "SELECT  * from EID WHERE BARCODE like '%"+BARCODE.Text +"%' LIMIT 1 ; "; // +BARCODE.Text +"'";
                var r = contents.ExecuteReader();
                Console.WriteLine("Reading data");
            while (r.Read())
            {
                but10.Text = r["ONO"].ToString();
            }
               // r["ONO"].ToString();
                   
                

            connection.Close();


        /*    var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {
                i = i + 1;
                ff[i] = r["Symbol"].ToString();
                Console.WriteLine("\tKey={0}; Value={1}",
                                  r["_id"].ToString(),
                                  r["Symbol"].ToString());
            }  */






        }















    } //PARAGGELIES
} //NAMESPACE 