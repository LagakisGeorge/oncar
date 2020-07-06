using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data.SqlClient;
using System.IO;
using Mono.Data.Sqlite;
namespace test4sql

 

{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public static SqliteConnection connection;
        public static void DoSomeDataAccess()
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
                connection = new SqliteConnection("Data Source=" + dbPath);

                var commands = new[] {
            "CREATE TABLE [Items] (_id ntext, Symbol ntext);",
            "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('1', 'AAPL')",
            "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('2', 'GOOG')",
            "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('3', 'MSFT')"
        };
                // Open the database connection and create table with data
                connection.Open();



                using (var c = connection.CreateCommand())
                {
                    c.CommandText = "CREATE TABLE IF NOT EXISTS ARITMISI (ID  INTEGER PRIMARY KEY  ," +
                    "[ARITMISI] [int] ," +
                    "[ONO] [nvarchar](55)  );";
                    var rowcount = c.ExecuteNonQuery(); // rowcount will be 1
                }

                





                foreach (var command in commands)
                {
                    using (var c = connection.CreateCommand())
                    {
                        c.CommandText = command;
                        var rowcount = c.ExecuteNonQuery();
                        Console.WriteLine("\tExecuted " + command);
                    }
                }
            }
            else
            {
                Console.WriteLine("Database already exists");
                // Open connection to existing database file
                connection = new SqliteConnection("Data Source=" + dbPath);
                connection.Open();
            }

            // query the database to prove data was inserted!
            using (var contents = connection.CreateCommand())
            {
                contents.CommandText = "SELECT [_id], [Symbol] from [Items]";
                var r = contents.ExecuteReader();
                Console.WriteLine("Reading data");
                while (r.Read())
                    Console.WriteLine("\tKey={0}; Value={1}",
                                      r["_id"].ToString(),
                                      r["Symbol"].ToString());
            }
            connection.Close();
        }



        public void Runsql(object sender,EventArgs e)
        {
            DoSomeDataAccess();

            connection.Open();
            using (var c = connection.CreateCommand())
            {
                c.CommandText = "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('5', 'γιωργοσ')";
                var rowcount = c.ExecuteNonQuery(); // rowcount will be 1
            }

            String[] ff=new String[50];
            int i=0;

            // query the database to prove data was inserted!
            using (var contents = connection.CreateCommand())
            {
                contents.CommandText = "SELECT [_id], [Symbol] from [Items]";
                var r = contents.ExecuteReader();
                Console.WriteLine("Reading data");
                while (r.Read())
                {
                    i = i + 1;
                    ff[i] = r["Symbol"].ToString();
                    Console.WriteLine("\tKey={0}; Value={1}",
                                      r["_id"].ToString(),
                                      r["Symbol"].ToString());
                }
            }
         //   var listview = new ListView();

         //   listview.ItemsSource  = ff;
            
            


            connection.Close();
            but1.Text = "TEST1";

            list1.ItemsSource = ff;
 /* new string[]
   {
  "mono",
  "monodroid",
  "monotouch",
  "monorail",
  "monodevelop",
  "monotone",
  "monopoly",
  "monomodal",
  "mononucleosis"
   };
*/
            // String c3 = morecomplex();
        }




        public static string morecomplex() // Opensql(object sender, EventArgs e)
        {
            var output = "";
            output += "\nComplex query example: ";
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");

            connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();
            using (var contents = connection.CreateCommand())
            {
                contents.CommandText = "SELECT * FROM [Items] WHERE Symbol = 'MSFT'";
                var r = contents.ExecuteReader();
                output += "\nReading data";
                while (r.Read())
                    output += String.Format("\n\tKey={0}; Value={1}",
                            r["_id"].ToString(),
                            r["Symbol"].ToString());
            }
            connection.Close();

            return output;
        }


        public void Execute()
        {
            SqlConnectionStringBuilder dbConString = new SqlConnectionStringBuilder();
            dbConString.UserID = "My Username";
            dbConString.Password = "My Password";
            dbConString.DataSource = "My Server Address";

            using (SqlConnection con = new SqlConnection(returnConnectionString().ConnectionString))
            {
                con.Open();
                for (int i = 0; i < commands.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE MyTable SET Name = 'New Name' WHERE ID = 1");
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }








        }
}
