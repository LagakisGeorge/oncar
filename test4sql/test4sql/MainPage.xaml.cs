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
using System.Data;

namespace test4sql

 

{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]



    public partial class MainPage : ContentPage
    {
        public IList<Monkey> Monkeys { get; private set; }
        public SqlConnection con;

        public MainPage()
        {
            InitializeComponent();
            // MainPage = new NavigationPage(new FirstContentPage());
            try
            {
               int i = StartSqlite("");
            }
            catch (Exception ex)
            {
                // await DisplayAlert("Error", ex.ToString(), "OK");
            }


            // DESKTOP-MPGU8SB\SQL17
            string constring = @"Data Source="+Globals.cSQLSERVER +";Initial Catalog=EMP;Uid=sa;Pwd=12345678";
            // ok fine string constring = @"Data Source=DESKTOP-MPGU8SB\SQL17,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";
            // ok works fine string constring = @"Data Source=192.168.1.10,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            con = new SqlConnection(constring);

            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                // await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }


        public static int StartSqlite(string Query)
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
            connection = new SqliteConnection("Data Source=" + dbPath);
            // Open the database connection and create table with data
            connection.Open();
            var c = connection.CreateCommand();

            Query  = "CREATE TABLE IF NOT EXISTS MEM( ID  INTEGER PRIMARY KEY,IP [nvarchar](45)," +
                   "[EPO] [nvarchar](255) ," +
                    "[DIE] [nvarchar](35) ," +
                      "[POL] [nvarchar](35) ," +
                        "[THL] [nvarchar](35) ," +
                      "[AFM] [nvarchar](15) )";
          





            c.CommandText = Query;

            var rowcount = c.ExecuteNonQuery(); // rowcount will be 1

            string c2 = PARAGGELIES.ReadSQL("select CAST(count(*) AS VARCHAR(10) )  AS NN from MEM ");
            int n = Int32.Parse(c2);
            if (n==0)
            {
                c.CommandText = "INSERT INTO MEM (IP) VALUES ('*')";

                rowcount = c.ExecuteNonQuery(); // rowcount will be 1



            }


            Globals.cIP= PARAGGELIES.ReadSQL("select IP from MEM  where ID=1");

            Globals.cSQLSERVER = PARAGGELIES.ReadSQL("select EPO from MEM  where ID=1");

            // l = MainPage.ExecuteSqlite("");  CAST(ARITMISI AS VARCHAR(10) )


            return rowcount;
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


        public async void PARAGG(object sender, EventArgs e)
        {


            //  MainPage = new NavigationPage(new FirstContentPage());

           // await NavigationPage (new Page2()  );
            await Navigation.PushAsync(new PARAGGELIES());


        }



        public void Runsql(object sender, EventArgs e)
        {
            DoSomeDataAccess();

            connection.Open();
            using (var c = connection.CreateCommand())
            {
                c.CommandText = "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('5', 'γιωργοσ')";
                var rowcount = c.ExecuteNonQuery(); // rowcount will be 1
            }

            String[] ff = new String[50];
            int i = 0;

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

            // list1.ItemsSource = ff;
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

        void on2(object sender, ItemTappedEventArgs e)
        {
            but1.Text = "==" + e.Item.ToString();

        }


        //        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        //      {
        //        Monkey selectedItem = e.SelectedItem as Monkey;
        //  }

        // ΔΙΑΛΕΓΩ ΤΟΝ ΠΕΛΑΤΗ ΚΑΙ ΤΟΝ ΕΝΗΜΕΡΩΝΩ ΤΟ ARTIM=ARTIM+1
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Monkey selectedItem = e.SelectedItem as Monkey;

            //




            try
            {
                //  con.Open();
                // await DisplayAlert("OK", "OK i am Connected", "OK");

                SqlCommand cmd = new SqlCommand("UPDATE ARITMISI SET ARITMISI=ARITMISI+1 WHERE ID = 1");
                cmd.Connection = con;
                cmd.ExecuteNonQuery();


                cmd = new SqlCommand("UPDATE PEL  SET ARTIM=ISNULL(ARTIM,0)+1 WHERE ID = " + selectedItem.idPEL);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                /*
                  Monkeys = new List<Monkey>();


                  DataTable dt = new DataTable();

                  SqlCommand cmd3 = new SqlCommand("select top 100 * FROM PEL", con);

                  var adapter2 = new SqlDataAdapter(cmd3);
                  adapter2.Fill(dt);
                  List<string> MyList = new List<string>();
                  for (int k = 0; k <= 99; k++)
                  {
                      String mF = dt.Rows[k]["EPO"].ToString();
                      MyList.Add(mF);

                      Monkeys.Add(new Monkey
                      {
                          Name = mF,

                          Location = dt.Rows[k]["DIE"].ToString(),
                          ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                          idPEL = dt.Rows[k]["ID"].ToString()
                      });
                  }
                  BindingContext = this;

                */
            }
            catch (Exception ex)
            {
                //  await DisplayAlert("Error", ex.ToString(), "OK");
            }



        }
        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Monkey tappedItem = e.Item as Monkey;
        }
        void OnListsel(object sender, SelectedItemChangedEventArgs e)
        {
            but1.Text = e.SelectedItem.ToString();

        }
        private async void ToPage1(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Page1());
            // await Navigation.PushAsync(new Page2 { });
            // await NavigationPage (new Page2());
        }

        private async void toPage2(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Page2());  //imports
           
        }

        private async void fparam(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new param1());  //imports

        }




        public DataSet Execute(String query)
        {
            DataSet ds = new DataSet();

            SqlConnectionStringBuilder dbConString = new SqlConnectionStringBuilder();
            dbConString.UserID = "My Username";
            dbConString.Password = "My Password";
            dbConString.DataSource = "My Server Address";

            using (SqlConnection con = new SqlConnection(dbConString.ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

            }
            return ds;
        }

        // ΑΝΟΙΓΕΙ ΑΠΟ SQLSERVER ΤΟ ΑΡΧΕΙΟ ΤΩΝ ΠΕΛΑΤΩΝ ΚΑΙ ΤΟ ΒΑΖΕΙ ΣΕ LISTVIEW
        private async void SqlserverTableToListview (object sender, EventArgs e)
        {

            MainPage.ExecuteSqlite("delete from PEL;");



            but11.Text = "sss";
            //  string constring = @"Data Source=192.168.1.3,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // using (SqlConnection con = new SqlConnection(constring))
            // {
            try
            {
                //  con.Open();
                //  await DisplayAlert("OK", "OK i am Connected", "OK");



                // ***************  demo πως τρεχω εντολη στον sqlserver ********************************
               // SqlCommand cmd = new SqlCommand("UPDATE ARITMISI SET ARITMISI=9009 WHERE ID = 1");
               // cmd.Connection = con;
               // cmd.ExecuteNonQuery();

                
                //  DataSet ds = new DataSet();

                //  SqlCommand cmd2 = new SqlCommand("select * FROM PEL", con);

                //   var adapter = new SqlDataAdapter(cmd2);
                //   adapter.Fill(ds);
                //  string cc = ds.Tables[0].Rows[1]["EPO"];
                Monkeys = new List<Monkey>();


                DataTable dt = new DataTable();

                SqlCommand cmd3 = new SqlCommand("select * FROM PEL WHERE EIDOS='e' and TYP<>0 order by TYP DESC", con);

                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                List<string> MyList = new List<string>();
                int k = 0;
                for ( k = 0; k <= dt.Rows.Count -1; k++)
                {
                    String mF = dt.Rows[k]["EPO"].ToString();
                    MyList.Add(mF);
                    string mTYP = dt.Rows[k]["TYP"].ToString();


                    Monkeys.Add(new Monkey
                    {
                        Name = mF,

                        Location = dt.Rows[k]["DIE"].ToString(),
                        ImageUrl = mTYP , // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    }); ;


                    string mKOD = dt.Rows[k]["kod"].ToString();
                    string mDIE = dt.Rows[k]["die"].ToString();
                    string mAFM = dt.Rows[k]["afm"].ToString();
                  
                    mTYP = mTYP.Replace(",", ".");
                    int n2 = MainPage.ExecuteSqlite("insert into PEL (TYP,KOD,EPO,DIE,AFM) VALUES ("+mTYP+",'" + mKOD + "','" + mF + "','" +mDIE + "','"+mAFM+"');");





                } // FOR
                await DisplayAlert("ΠΕΛΑΤΕΣ",""+dt.Rows.Count, "OK");

                BindingContext = this;


                // list1.ItemsSource  = MyList;




            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
            // }
        }

        public static int ExecuteSqlite(string Query)
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
                connection = new SqliteConnection("Data Source=" + dbPath);               
                // Open the database connection and create table with data
                connection.Open();
            var c = connection.CreateCommand() ;
            c.CommandText  = Query;

            var rowcount = c.ExecuteNonQuery(); // rowcount will be 1
            return rowcount;
        }

    }  // public partial class MainPage : ContentPage

}  // namespace
