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
using System.Threading;
using oncar;
using Android.Views.Animations;
using System.Net;
using System.Net.Sockets;
using SharpCifs.Smb;
//  using SharpCifs.Util.Sharpen;

namespace test4sql

 

{
    // Learn more about making custom code visible in the Xamarin. Forms  previewer
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

            try
            {




                if (Globals.cFORTHGO == "99")  // PELATES
                {
                    but1.IsVisible = false;
                    but121.IsVisible = false;
                    SUPER2.IsVisible = false;
                    but1TIMOL.IsVisible = false;
                    APOTHIKI.IsVisible = false;
                    // param.IsVisible = false;
                    trapezia.IsVisible = false;
                    but1fort.IsVisible = false;
                    but1EPIST.IsVisible = false;




                }

                if (Globals.cFORTHGO.Substring(0, 1) == "8")  // ΤΡΑΠΕΖΙΑ
                {
                    but1.IsVisible = true;
                    but121.IsVisible = false;
                    SUPER2.IsVisible = false;
                    but1TIMOL.IsVisible = false;
                    APOTHIKI.IsVisible = false;
                    // param.IsVisible = false;
                    trapezia.IsVisible = true;
                    but1fort.IsVisible = false;
                    but1EPIST.IsVisible = false;
                    kinpelath.IsVisible = false;
                    reppelath.IsVisible = false;
                    PELATHS.IsVisible = false;
                    test.IsVisible = true;
                    APOTHIKI2.IsVisible = false;
                    Globals.gUserWaiter = Globals.cFORTHGO.Substring(1, 1);

                }

                if (Globals.cFORTHGO.Substring(0, 1) == "7")  // ΑΠΟΘΗΚΗ-ΤΙΜΟΛΟΓΗΣΗ-ΚΑΡΤΕΛΑ 
                {
                    but1.IsVisible = true;
                    but121.IsVisible = true;
                    SUPER2.IsVisible = true;
                    but1TIMOL.IsVisible = true;
                    APOTHIKI.IsVisible = true;
                    // param.IsVisible = false;
                    trapezia.IsVisible = false;
                    but1fort.IsVisible = true;
                    but1EPIST.IsVisible = true;
                    //  Globals.gUserWaiter = Globals.cFORTHGO.Substring(1, 1);
                    kinpelath.IsVisible = false;
                    reppelath.IsVisible = false;
                    PELATHS.IsVisible = false;
                    vardia.IsVisible = false;
                    test.IsVisible = false;



                }

                var image = new Image();
                

                    image.Source = Device.RuntimePlatform == Device.Android
                ? ImageSource.FromFile("dessert.jpg")
                : ImageSource.FromFile("Images/dessert.jpg");
                






                // DESKTOP-MPGU8SB\SQL17
                string[] lines = Globals.cSQLSERVER.Split(';');
                string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

                //     string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";
                // ok fine string constring = @"Data Source=DESKTOP-MPGU8SB\SQL17,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";
                // ok works fine string constring = @"Data Source=192.168.1.10,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

                Globals.gPWD = "3921";


               con = new SqlConnection(constring);


                con.Open();

                     DataTable dt2 = new DataTable();

                    string ccg = "";
                    ccg = "SELECT isnull(QUERY,'') AS QUERY FROM LOGGING " ;
                    dt2 = trapparagg.ReadSQLServer(ccg);
                    if (dt2.Rows.Count > 0)
                    {
                    Globals.gPWD = dt2.Rows[0]["QUERY"].ToString();
                    }
                    
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

            Query = "CREATE TABLE IF NOT EXISTS MEM( ID  INTEGER PRIMARY KEY,IP [nvarchar](45)," +
                   "[EPO] [nvarchar](255) ," +
                    "[DIE] [nvarchar](35) ," +
                      "[POL] [nvarchar](35) ," +
                        "[THL] [nvarchar](35) ," +
                      "[AFM] [nvarchar](15) )";






            c.CommandText = Query;

            var rowcount = c.ExecuteNonQuery(); // rowcount will be 1

            string c2 = PARAGGELIES.ReadSQL("select CAST(count(*) AS VARCHAR(10) )  AS NN from MEM ");
            int n = Int32.Parse(c2);
            if (n == 0)
            {
                c.CommandText = "INSERT INTO MEM (IP) VALUES ('*')";

                rowcount = c.ExecuteNonQuery(); // rowcount will be 1
                rowcount = c.ExecuteNonQuery(); // rowcount will be 1


            }


            Globals.cIP = PARAGGELIES.ReadSQL("select IP from MEM  where ID=1");
            Globals.cSQLSERVER = PARAGGELIES.ReadSQL("select EPO from MEM  where ID=1");
            Globals.useBarcodes = PARAGGELIES.ReadSQL("select DIE from MEM  where ID=1");
            Globals.cFORTHGO = PARAGGELIES.ReadSQL("select THL from MEM  where ID=1");

            Globals.cIPPR1 = PARAGGELIES.ReadSQL("select IP from MEM  where ID=2");
            Globals.cIPPR2 = PARAGGELIES.ReadSQL("select AFM from MEM  where ID=2");
            Globals.cIPPR3 = PARAGGELIES.ReadSQL("select DIE from MEM  where ID=2");
            Globals.gIPKleis  = PARAGGELIES.ReadSQL("select POL from MEM  where ID=2");

            Globals.gTITLOS = PARAGGELIES.ReadSQL("select ifnull(EPO,' ') as EPO from MEM  where ID=2");




            // l = MainPage.ExecuteSqlite("");  CAST(ARITMISI AS VARCHAR(10) )


            return rowcount;
        }






        public static SqliteConnection connection;


        // demo sqlite ========================
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


      public static  void SaveFile(List<string> text,string typosekt)
        {
            if (Globals.cIP.Length < 5) return;
            //Get the SmbFile specifying the file name to be created.
            var file = new SmbFile("smb://"+ Globals.cIP.TrimStart()  + "/" + typosekt + Globals.gIDPARAGG+ DateTime.Now.ToString("HH-mm") + ".txt");
            // fine var file = new SmbFile("smb://User:1@192.168.1.5/backpel/New2FileName.txt");
            try
            {
                //Create file.
                file.CreateNewFile();
            }
            catch
            {
                // await DisplayAlert("Υπαρχει ηδη το αρχειο", "....", "OK");
                return;
            }


            //Get writable stream.
            var writeStream = file.GetOutputStream();
           

            //Write bytes.
            for (int k = 1; k<text.Count ; k++)
            {
                string cc = text[k]+ Convert.ToChar(13).ToString();
                writeStream.Write(Encoding.UTF8.GetBytes(cc));
            }

            //Dispose writable stream.
            writeStream.Dispose();
        }







        public async void PARAGG(object sender, EventArgs e)
        {


            //  MainPage = new NavigationPage(new FirstContentPage());

            // await NavigationPage (new Page2()  );
            await Navigation.PushAsync(new PARAGGELIES());


        }


        // demo sqlite  ========================================
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

        public async void Fortosh(object sender, EventArgs e) {

            await Navigation.PushAsync(new techn1());  //imports


        }


        public async void xtisimo(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new View1());  //imports


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


        private async void FSEARCH(object sender, EventArgs e)
        {
            // but11.IsVisible = false;
            await Navigation.PushAsync(new SUPER());  //imports

        }

        private async void FSEARCH2(object sender, EventArgs e)
        {
            // but11.IsVisible = false;
            await Navigation.PushAsync(new SUPER2());  //imports

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
        private async void SqlserverTableToListview(object sender, EventArgs e)
        {

            MainPage.ExecuteSqlite("delete from PEL;");



            //but11.Text = "sss";
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
                //  string cc = ds.Tables[0].Rows[1]["EPO"];  201712552030 0217
                Monkeys = new List<Monkey>();


                DataTable dt = new DataTable();

                SqlCommand cmd3 = new SqlCommand("select * FROM PEL WHERE EIDOS='e' and TYP<>0 order by TYP DESC", con);

                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String mF = dt.Rows[k]["EPO"].ToString();
                    MyList.Add(mF);
                    string mTYP = dt.Rows[k]["TYP"].ToString();


                    Monkeys.Add(new Monkey
                    {
                        Name = mF,

                        Location = dt.Rows[k]["DIE"].ToString(),
                        ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    }); ;


                    string mKOD = dt.Rows[k]["kod"].ToString();
                    string mDIE = dt.Rows[k]["die"].ToString();
                    string mAFM = dt.Rows[k]["afm"].ToString();

                    mTYP = mTYP.Replace(",", ".");
                    int n2 = MainPage.ExecuteSqlite("insert into PEL (TYP,KOD,EPO,DIE,AFM) VALUES (" + mTYP + ",'" + mKOD + "','" + mF + "','" + mDIE + "','" + mAFM + "');");





                } // FOR
                await DisplayAlert("ΠΕΛΑΤΕΣ", "" + dt.Rows.Count, "OK");

                BindingContext = this;


                // list1.ItemsSource  = MyList;




            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
            // }
        }



        private async void printing(object sender, EventArgs e)
        {


            // Ethernet or WiFi
            //    var printer = new NetworkPrinter(ipAddress: "192.168.1.80", port: 9000, reconnectOnTimeout: true);

            /*
            var e = new EPSON();
            printer.Write(
              ByteSplicer.Combine(
                e.CenterAlign(),
                e.PrintImage(File.ReadAllBytes("images/pd-logo-300.png"), true),
NewMethod(e),
                e.SetBarcodeHeightInDots(360),
                e.SetBarWidth(BarWidth.Default),
                e.SetBarLabelPosition(BarLabelPrintPosition.None),
                e.PrintBarcode(BarcodeType.ITF, "0123456789"),
                e.PrintLine(),
                e.PrintLine("B&H PHOTO & VIDEO"),
                e.PrintLine("420 NINTH AVE."),
                e.PrintLine("NEW YORK, NY 10001"),
                e.PrintLine("(212) 502-6380 - (800)947-9975"),
                e.SetStyles(PrintStyle.Underline),
                e.PrintLine("www.bhphotovideo.com"),
                e.SetStyles(PrintStyle.None),
                e.PrintLine(),
                e.LeftAlign(),
                e.PrintLine("Order: 123456789        Date: 02/01/19"),
                e.PrintLine(),
                e.PrintLine(),
                e.SetStyles(PrintStyle.FontB),
                e.PrintLine("1   TRITON LOW-NOISE IN-LINE MICROPHONE PREAMP"),
                e.PrintLine("    TRFETHEAD/FETHEAD                        89.95         89.95"),
                e.PrintLine("----------------------------------------------------------------"),
                e.RightAlign(),
                e.PrintLine("SUBTOTAL         89.95"),
                e.PrintLine("Total Order:         89.95"),
                e.PrintLine("Total Payment:         89.95"),
                e.PrintLine(),
                e.LeftAlign(),
                e.SetStyles(PrintStyle.Bold | PrintStyle.FontB),
                e.PrintLine("SOLD TO:                        SHIP TO:"),
                e.SetStyles(PrintStyle.FontB),
                e.PrintLine("  FIRSTN LASTNAME                 FIRSTN LASTNAME"),
                e.PrintLine("  123 FAKE ST.                    123 FAKE ST."),
                e.PrintLine("  DECATUR, IL 12345               DECATUR, IL 12345"),
                e.PrintLine("  (123)456-7890                   (123)456-7890"),
                e.PrintLine("  CUST: 87654321"),
                e.PrintLine(),
                e.PrintLine()
              )
            );

            */


        }








        // ΑΝΟΙΓΕΙ ασυγχρονα  ΑΠΟ SQLSERVER ΤΟ ΑΡΧΕΙΟ ΤΩΝ EIDΩΝ ΚΑΙ ΤΟ ΒΑΖΕΙ ΣΕ LISTVIEW
        private async void asSqlserverTοSQLITE(object sender, EventArgs e)
        {

            await DisplayAlert("--Εναρξη μεταφοράς ειδών", "Είδη  ", "OK");
            Task<int> task = new Task<int>(Imp_EIDH);
            // IMPORTEID.Text = "*** ΠΑΡΑΚΑΛΩ ΠΕΡΙΜΕΝΕΤΕ 3λεπτά*****";
            task.Start();
            int count = await task;

            // IMPORTEID.Text = count.ToString();
            await DisplayAlert("--Εναρξη μεταφοράς ειδών", "Είδη που περάσ " + count.ToString(), "OK");
            BindingContext = this;








            Task<int> task2 = new Task<int>(Imp_BARCODES);
            task2.Start();

            int count2 = await task2;

            // IMPORTEID.Text = count.ToString();
            await DisplayAlert("--Εναρξη μεταφοράς BARCODES", "    barcodes που περάστηκαν " + count2.ToString(), "OK");

        }


        // ΑΝΟΙΓΕΙ ασυγχρονα  ΑΠΟ SQLSERVER ΤΟ ΑΡΧΕΙΟ ΤΩΝ EIDΩΝ ΚΑΙ ΤΟ ΒΑΖΕΙ ΣΕ LISTVIEW
        private async void SqlserverTοBARCODES(object sender, EventArgs e)
        {




            //  int count2= Imp_BARCODES();



            Task<int> task = new Task<int>(Imp_BARCODES);
            task.Start();

            int count2 = await task;

            // IMPORTEID.Text = count.ToString();
            await DisplayAlert("--Εναρξη μεταφοράς BARCODES", "    barcodes που περάστηκαν " + count2.ToString(), "OK");

        }








        int Imp_EIDH()
        {


            // me to parakato loop δουλεευει το ασυγχρονο
            /*
            int k2;
            for (k2 = 0; k2 <= 50 ; k2++)
            {
                Thread.Sleep(500);
            }
            return k2;
            */
            // ta epano einai debugging

            Thread.Sleep(500);
            int nc = 0;
            MainPage.ExecuteSqlite("delete from EID;");

            //  return 1; //debug

            //   but11.Text = "sss";
            //  string constring = @"Data Source=192.168.1.3,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // using (SqlConnection con = new SqlConnection(constring))
            // {
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select   ID,isnull(KOD,'') AS MKOD,ISNULL(ONO,'') AS MONO,ISNULL(ERG,'') AS MERG,ISNULL(LTI5,0) AS MLTI5  FROM EID ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    // Thread.Sleep(500);
                    String mF = dt.Rows[k]["MONO"].ToString();
                    mF = mF.Replace("'", "`");
                    MyList.Add(mF);
                    string mTYP = dt.Rows[k]["MLTI5"].ToString();
                    mTYP = mTYP.Replace(",", ".");
                    Monkeys.Add(new Monkey
                    {
                        Name = mF,
                        Location = dt.Rows[k]["MKOD"].ToString(),
                        ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    }); ;


                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();
                    string mERG = dt.Rows[k]["MERG"].ToString();
                    mERG = mERG.Replace("'", "`");

                    mTYP = mTYP.Replace(",", ".");
                    int n2 = MainPage.ExecuteSqlite("insert into EID (XONDR,KOD,ONO,BARCODE) VALUES (" + mTYP + ",'" + mKOD + "','" + mF + "','" + mERG + "');");





                } // FOR
                nc = dt.Rows.Count;

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;

                // await DisplayAlert("EIΔΗ", " Eιδη:" + dt.Rows.Count, "OK");

                //  BindingContext = this;





            }
            catch (Exception ex)
            {
                // await DisplayAlert("Error", ex.ToString(), "OK");
            }



            return nc;
        }


        int Imp_BARCODES()
        {


            // me to parakato loop δουλεευει το ασυγχρονο
            /*
            int k2;
            for (k2 = 0; k2 <= 50 ; k2++)
            {
                Thread.Sleep(500);
            }
            return k2;
            */
            // ta epano einai debugging

            Thread.Sleep(500);
            int nc = 0;
            MainPage.ExecuteSqlite("delete from BARCODES;");

            //  return 1; //debug

            //   but11.Text = "sss";
            //  string constring = @"Data Source=192.168.1.3,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // using (SqlConnection con = new SqlConnection(constring))
            // {
            try
            {

                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select   isnull(KOD,'') AS MKOD,ISNULL(ERG,'') AS MERG  FROM BARCODES ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                //   List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {



                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();
                    string mERG = dt.Rows[k]["MERG"].ToString();
                    mERG = mERG.Replace("'", "`");


                    int n2 = MainPage.ExecuteSqlite("insert into BARCODES (KOD,BARCODE) VALUES ('" + mKOD + "','" + mERG + "');");





                } // FOR
                nc = dt.Rows.Count;






            }
            catch (Exception ex)
            {
                // await DisplayAlert("Error", ex.ToString(), "OK");
            }



            return nc;
        }




        // ΑΝΟΙΓΕΙ ΑΠΟ SQLSERVER ΤΟ ΑΡΧΕΙΟ ΤΩΝ EIDΩΝ ΚΑΙ ΤΟ ΒΑΖΕΙ ΣΕ LISTVIEW
        private async void SqlserverTοSQLITE(object sender, EventArgs e)
        {
            await DisplayAlert("Εκκίνηση..", "Περιμενετε..", "OK");
            // await ShowAlert("Εκκινηση");

            // var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here

            // Device.BeginInvokeOnMainThread(async () =>
            //  {
            //       await DisplayAlert("Alert", "No internet connection", "Ok");
            //   });




            MainPage.ExecuteSqlite("delete from EID;");



            //  but11.Text = "sss";
            //  string constring = @"Data Source=192.168.1.3,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // using (SqlConnection con = new SqlConnection(constring))
            // {
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select  ID,isnull(KOD,'') AS MKOD,ISNULL(ONO,'') AS MONO,ISNULL(ERG,'') AS MERG,ISNULL(LTI5,0) AS MLTI5  FROM EID ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String mF = dt.Rows[k]["MONO"].ToString();
                    mF = mF.Replace("'", "`");
                    MyList.Add(mF);
                    string mTYP = dt.Rows[k]["MLTI5"].ToString();
                    mTYP = mTYP.Replace(",", ".");
                    Monkeys.Add(new Monkey
                    {
                        Name = mF,
                        Location = dt.Rows[k]["MKOD"].ToString(),
                        ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    }); ;


                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();
                    string mERG = dt.Rows[k]["MERG"].ToString();
                    mERG = mERG.Replace("'", "`");

                    mTYP = mTYP.Replace(",", ".");
                    int n2 = MainPage.ExecuteSqlite("insert into EID (XONDR,KOD,ONO,BARCODE) VALUES (" + mTYP + ",'" + mKOD + "','" + mF + "','" + mERG + "');");





                } // FOR

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;

                await DisplayAlert("EIΔΗ", " Eιδη:" + dt.Rows.Count, "OK");

                BindingContext = this;





            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }

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
            var c = connection.CreateCommand();
            c.CommandText = Query;

            var rowcount = c.ExecuteNonQuery(); // rowcount will be 1
            return rowcount;
        }

        static int instr(int StartPos, String SearchString, String SearchFor, int IgnoreCaseFlag)
        {
            int result = -1;
            if (IgnoreCaseFlag == 1)
                result = SearchString.IndexOf(SearchFor, StartPos, StringComparison.OrdinalIgnoreCase);
            else
                result = SearchString.IndexOf(SearchFor, StartPos);
            return result;
        }

        public static string ToGreek737(string Q)
        {
            // Q = Q.ToUpper();
            string t = "";
            for (int k = 1; k <= Q.Length; k += 1)
            {
                string m = Q.Substring(k - 1, 1);
                if (instr(0, "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψωάέήίόύώς", m, 1) == -1)
                {
                    t = t + m;
                }
                else
                {
                    if (m == "Α") { t += Convert.ToChar(128).ToString(); }
                    if (m == "Ά") { t += Convert.ToChar(234).ToString(); }


                    if (m == "Β") { t += Convert.ToChar(129).ToString(); }
                    if (m == "Γ") { t += Convert.ToChar(130).ToString(); }
                    if (m == "Δ") { t += Convert.ToChar(131).ToString(); }
                    if (m == "Ε") { t += Convert.ToChar(132).ToString(); }
                    if (m == "Έ") { t += Convert.ToChar(235).ToString(); }
                    if (m == "Ζ") { t += Convert.ToChar(133).ToString(); }
                    if (m == "Η") { t += Convert.ToChar(134).ToString(); }
                    if (m == "Ή") { t += Convert.ToChar(236).ToString(); }
                    if (m == "Θ") { t += Convert.ToChar(135).ToString(); }

                    if (m == "Ι") { t += Convert.ToChar(136).ToString(); }
                    if (m == "Ί") { t += Convert.ToChar(237).ToString(); }

                    if (m == "Κ") { t += Convert.ToChar(137).ToString(); }
                    if (m == "Λ") { t += Convert.ToChar(138).ToString(); }
                    if (m == "Μ") { t += Convert.ToChar(139).ToString(); }
                    if (m == "Ν") { t += Convert.ToChar(140).ToString(); }
                    if (m == "Ξ") { t += Convert.ToChar(141).ToString(); }
                    if (m == "Ο") { t += Convert.ToChar(142).ToString(); }
                    if (m == "Ό") { t += Convert.ToChar(238).ToString(); }
                    if (m == "Π") { t += Convert.ToChar(143).ToString(); }

                    if (m == "Ρ") { t += Convert.ToChar(144).ToString(); }
                    if (m == "Σ") { t += Convert.ToChar(145).ToString(); }
                    if (m == "Τ") { t += Convert.ToChar(146).ToString(); }
                    if (m == "Υ") { t += Convert.ToChar(147).ToString(); }
                    if (m == "Ύ") { t += Convert.ToChar(239).ToString(); }

                    if (m == "Φ") { t += Convert.ToChar(148).ToString(); }
                    if (m == "Χ") { t += Convert.ToChar(149).ToString(); }

                    if (m == "Ψ") { t += Convert.ToChar(150).ToString(); }
                    if (m == "Ω") { t += Convert.ToChar(151).ToString(); }
                    if (m == "Ώ") { t += Convert.ToChar(240).ToString(); }


                    if (m == "α") { t += Convert.ToChar(152).ToString(); }
                    if (m == "ά") { t += Convert.ToChar(225).ToString(); }


                    if (m == "β") { t += Convert.ToChar(153).ToString(); }
                    if (m == "γ") { t += Convert.ToChar(154).ToString(); }
                    if (m == "δ") { t += Convert.ToChar(155).ToString(); }
                    if (m == "ε") { t += Convert.ToChar(156).ToString(); }
                    if (m == "έ") { t += Convert.ToChar(226).ToString(); }
                    if (m == "ζ") { t += Convert.ToChar(157).ToString(); }
                    if (m == "η") { t += Convert.ToChar(158).ToString(); }
                    if (m == "ή") { t += Convert.ToChar(227).ToString(); }
                    if (m == "θ") { t += Convert.ToChar(159).ToString(); }

                    if (m == "ι") { t += Convert.ToChar(160).ToString(); }
                    if (m == "ί") { t += Convert.ToChar(229).ToString(); }
                    if (m == "ΐ") { t += Convert.ToChar(229).ToString(); }
                    if (m == "ϊ") { t += Convert.ToChar(160).ToString(); }

                    if (m == "κ") { t += Convert.ToChar(161).ToString(); }
                    if (m == "λ") { t += Convert.ToChar(162).ToString(); }
                    if (m == "μ") { t += Convert.ToChar(163).ToString(); }
                    if (m == "ν") { t += Convert.ToChar(164).ToString(); }
                    if (m == "ξ") { t += Convert.ToChar(165).ToString(); }
                    if (m == "ο") { t += Convert.ToChar(166).ToString(); }
                    if (m == "ό") { t += Convert.ToChar(230).ToString(); }
                    if (m == "π") { t += Convert.ToChar(167).ToString(); }

                    if (m == "ρ") { t += Convert.ToChar(168).ToString(); }
                    if (m == "σ") { t += Convert.ToChar(169).ToString(); }
                    if (m == "ς") { t += Convert.ToChar(170).ToString(); }

                    if (m == "τ") { t += Convert.ToChar(171).ToString(); }
                    if (m == "υ") { t += Convert.ToChar(172).ToString(); }
                    if (m == "ύ") { t += Convert.ToChar(231).ToString(); }

                    if (m == "φ") { t += Convert.ToChar(173).ToString(); }
                    if (m == "χ") { t += Convert.ToChar(174).ToString(); }

                    if (m == "ψ") { t += Convert.ToChar(175).ToString(); }
                    if (m == "ω") { t += Convert.ToChar(224).ToString(); }
                    if (m == "ώ") { t += Convert.ToChar(233).ToString(); }




















                }



            }
            return t;

        }


        private async void fAPOTHIKI(object sender, EventArgs e)
        {

            //  private async void fparam(object sender, EventArgs e)


            await Navigation.PushAsync(new SUPER());  //imports


        }

        private async void fPELATHS(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new PELATES());  //imports

        }

        private async void fPELkin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pelkin());
        }







        private async void ftrapezia(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            string ERR = Globals.ReadSQLServerWithError("select top 1 ONO from TABLES");
            ERR = ERR + ".....";
            if (ERR.Substring(0, 5) == "ERROR") { 
                await DisplayAlert("Αδυναμία Σύνδεσης", "", "οκ");
                return;
            }

            try
            {
                int n;
                string ccg = "";
                string cc = "0";
                try
                {
                   
                    ccg = "SELECT isnull(STR(MAX(  ISNULL(ID,0)   )),'0')   as aa FROM BARDIA where NUM1=" + Globals.gUserWaiter.ToString();
                    dt2 = trapparagg.ReadSQLServer(ccg);

                    
                   
                    if (dt2 == null || dt2.Rows.Count == 0)
                    {
                        // Throw the error or retrun the code 
                        Globals.ExecuteSQLServer("INSERT INTO BARDIA(OPENH, ISOPEN, HME, IDERGAZ, NUM1) VALUES(substring(  convert(char(16),CURRENT_TIMESTAMP,121) ,1,16), 1, getdate(), " + Globals.gUserWaiter.ToString() + ", " + Globals.gUserWaiter.ToString() + ")");
                        return;
                        n = 0;
                    }
                    else
                    {

                        cc = dt2.Rows[0]["aa"].ToString();
                        n = Int32.Parse(cc);
                    }

                }
                catch
                {
                    await DisplayAlert("λαθος", "οκ2", "οκ");
                    return;
                }



                if (n == 0)
                {
                    Globals.ExecuteSQLServer("INSERT INTO BARDIA(OPENH, ISOPEN, HME, IDERGAZ, NUM1) VALUES(substring(  convert(char(16),CURRENT_TIMESTAMP,121) ,1,16), 1, getdate(), " + Globals.gUserWaiter.ToString() + ", " + Globals.gUserWaiter.ToString() + ")");
                }
                else
                {


                    DataTable dt3 = trapparagg.ReadSQLServer("SELECT * FROM BARDIA WHERE ID= " + cc);
                    if (dt3.Rows[0]["ISOPEN"].ToString() == "1")
                    {
                        Globals.gIDBARDIA = dt3.Rows[0]["ID"].ToString();

                    }
                    else
                    {
                        Globals.gIDBARDIA = "0";
                        await DisplayAlert("ΑΝΟΙΞΤΕ ΒΑΡΔΙΑ", "   ", "OK");
                        return;
                    }



                }


                await Navigation.PushAsync(new trapezia2());

            }
            catch
            {
                await DisplayAlert("λαθος", "οκ1", "οκ");
                    return;
            }


        }

        private async void fPELREP(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PelReports());

        }

        private async void fCloseVardia(object sender, EventArgs e)
        {

            string kodikos = await DisplayPromptAsync("Κωδικός", "");
            if (kodikos==Globals.gPWD)
            {

            }else
            {
                return;
            }




            DataTable dt3 = new DataTable();
            // ΒΡΕΣ AN EXV ANOIXTA TRAPEZIA
            dt3 = trapparagg.ReadSQLServer("SELECT * FROM TABLES where KATEILHMENO=1 AND  NUM1=" + Globals.gUserWaiter.ToString());


            if (dt3.Rows.Count > 0)
            {
                await DisplayAlert("ΚΛΕΙΣΤΕ ΤΑ ΑΝΟΙΧΤΑ ΤΡΑΠΕΖΙΑ", dt3.Rows.Count.ToString() + " ανοικτα", "OK");
                return;
            }







            DataTable dt2 = new DataTable();
            // ΒΡΕΣ ΤΗΝ ΤΕΛΕΥΤΑΙΑ ΒΑΡΔΙΑ ΤΟΥ ΣΕΡΒΙΤΟΡΟΥ ν
            dt2 = trapparagg.ReadSQLServer("SELECT TOP 1 * FROM BARDIA where NUM1=" + Globals.gUserWaiter.ToString()+" ORDER BY ID DESC" );


            if (dt2.Rows.Count == 0)   // EINAI NEOS SERBITOROS ΠΑΡΘΕΝΙΚΗ ΕΜΦΑΝΙΣΗ
            {
                Globals.ExecuteSQLServer("INSERT INTO BARDIA(OPENH, ISOPEN, HME, IDERGAZ, NUM1) VALUES(substring(  convert(char(16),CURRENT_TIMESTAMP,121) ,1,16), 1, getdate(), " + "0" + ", " + Globals.gUserWaiter.ToString() + ")");
                //   Globals.gIDBARDIA = cc;
                await DisplayAlert("ΔΕΝ ΥΠΗΡΧΕ ΑΝΟΙΧΤΗ ΒΑΡΔΙΑ.ΤΩΡΑ ΜΟΛΙΣ ΑΝΟΙΞΕ ΝΕΑ", "   ", "OK");
                return;
            }
            else // βρηκε βαρδια να δω αν ειναι ανοιχτη
            {

                string cc = dt2.Rows[0]["ISOPEN"].ToString();
                int n = Int32.Parse(cc);
                if (n == 0)  // ΕΙΝΑΙ ΚΛΕΙΣΤΗ Η ΒΑΡΔΙΑ
                {
                    await DisplayAlert("ΔΕΝ ΥΠΗΡΧΕ ΑΝΟΙΧΤΗ ΒΑΡΔΙΑ ΓΙΑ ΝΑ ΚΛΕΙΣΕΙ.", "   ", "OK");

                    var action2 = await DisplayAlert("ΕΝΑΡΞΗ ΝΕΑΣ ΒΑΡΔΙΑΣ;", "Εισαι σίγουρος?", "Ναι", "Οχι");
                    if (action2)
                    {
                        Globals.ExecuteSQLServer("INSERT INTO BARDIA(OPENH, ISOPEN, HME, IDERGAZ, NUM1) VALUES(substring(  convert(char(16),CURRENT_TIMESTAMP,121) ,1,16), 1, getdate(), " + Globals.gUserWaiter.ToString() + ", " + Globals.gUserWaiter.ToString() + ")");
                        //   Globals.gIDBARDIA = cc;
                        await DisplayAlert("ΜΟΛΙΣ ΑΝΟΙΞΕ ΝΕΑ", "   ", "OK");
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                Globals.gIDBARDIA = dt2.Rows[0]["ID"].ToString();
            }

            // ΟΚ Η ΒΑΡΔΙΑ ΕΙΝΑΙ ΑΝΟΙΧΤΗ


            Single CASHTOT = 0;


            if (Globals.gIDBARDIA == "0")
            {
                await DisplayAlert("ΑΝΟΙΞΤΕ ΒΑΡΔΙΑ", "   ", "OK");
                return;

            }
            else
            {
                DataTable DT;
                // DT = trapparagg.ReadSQLServer("SELECT Sum(isnull(AJIA,0)) AS JJ , isnull(TROPOS,2) as TROPOS  FROM PARAGGMASTER GROUP BY TROPOS,IDBARDIA  HAVING  IDBARDIA=" + Globals.gIDBARDIA);
                DT = trapparagg.ReadSQLServer("SELECT Sum(isnull(CASH,0)) AS CASH ,  Sum(isnull(PIS1,0)) AS PIS1 , Sum(isnull(PIS2,0)) AS PIS2 ,  Sum(isnull(KERA,0)) AS KERA  FROM PARAGGMASTER WHERE  IDBARDIA=" + Globals.gIDBARDIA);
                string mm = "-------" + "\r\n";  // myText.Add( "\r\n");
                // string[] CASH;
                List<string> myText = new List<string>();



                if (DT.Rows.Count == 0)   // ΔΕΝ ΕΧΕΙ ΚΙΝΗΣΕΙΣ ΤΟ PARAGGMASTER
                {
                    Globals.ExecuteSQLServer("INSERT INTO BARDIA(OPENH, ISOPEN, HME, IDERGAZ, NUM1) VALUES(substring(  convert(char(16),CURRENT_TIMESTAMP,121) ,1,16), 1, getdate(), " + "0" + ", " + Globals.gUserWaiter.ToString() + ")");
                    //   Globals.gIDBARDIA = cc;
                    await DisplayAlert("ΔΕΝ ΥΠΗΡΧΕ ΑΝΟΙΧΤΗ ΒΑΡΔΙΑ.ΤΩΡΑ ΜΟΛΙΣ ΑΝΟΙΞΕ ΝΕΑ", "   ", "OK");
                    return;
                }


                string Tropos = "Με";
                Globals.ExecuteSQLServer("UPDATE BARDIA SET CASH1=" + DT.Rows[0]["CASH"].ToString().Replace(",", ".") + " WHERE ID=" + Globals.gIDBARDIA);
                mm = mm + Tropos + "=> " + DT.Rows[0]["CASH"].ToString();// + "\r\n";
                Tropos = "κ1";
                Globals.ExecuteSQLServer("UPDATE BARDIA SET CASH2=" + DT.Rows[0]["PIS1"].ToString().Replace(",", ".") + " WHERE ID=" + Globals.gIDBARDIA);
                mm = mm + Tropos + "=> " + DT.Rows[0]["PIS1"].ToString();// + "\r\n";


              //  if (Int32.Parse(DT.Rows[0]["PIS2"].ToString().Replace(",", ".")) > 0) {
                    Tropos = "κ2";
                    Globals.ExecuteSQLServer("UPDATE BARDIA SET CASH3=" + DT.Rows[0]["PIS2"].ToString().Replace(",", ".") + " WHERE ID=" + Globals.gIDBARDIA);
                    mm = mm + Tropos + "=> " + DT.Rows[0]["PIS2"].ToString();// + "\r\n";
               // }
                Tropos = "Κερ";
                Globals.ExecuteSQLServer("UPDATE BARDIA SET CASH4=" + DT.Rows[0]["KERA"].ToString().Replace(",", ".") + " WHERE ID=" + Globals.gIDBARDIA);
                mm = mm + Tropos + "=> " + DT.Rows[0]["KERA"].ToString();// + "\r\n";


                //  List<string> myText = new List<string>();
              //  myText.Add("=================================" + "\r\n");
                myText.Add(PARAGGELIES.toGreek(mm));
                await DisplayAlert(mm, "---   ", "OK");
                // printing(myText);

             //   printthis(myText);



            }

            //-------------------------  αναλυση ανα τραπεζι ----------------------------------- // String.Format("{0:0.0#}", 123.4567)       // "123.46"------ String.Format("{0:0.00}", te)
            DataTable DT2;
            DT2 = trapparagg.ReadSQLServer("SELECT TRAPEZI,HME,AJIA,isnull(CASH,0) as CASH,ISNULL(PIS1,0) AS PIS1,ISNULL(PIS2,0) AS PIS2,ISNULL(KERA,0) AS KERA  FROM PARAGGMASTER   where  IDBARDIA=" + Globals.gIDBARDIA);


            // string[] CASH;
            List<string> myText2 = new List<string>();
            string mm2 = "";
            myText2.Add("---");
            myText2.Add(ToGreek737("TΡΑΠΕΖΙ ΗΜΕΡΟΜ         ΜΕΤΡ ΚΑΡ1  EKΠΤ ΚΕΡΑΣΜ"));
            //  Right("      " + r["POSO"].ToString(), 6)
            float s1, s2, s3, s4;
            s1 = 0;
            s2 = 0;
            s3 = 0;
            s4 = 0;

            for (int K = 0; K <= DT2.Rows.Count - 1; K++)
            {
                string v = (DT2.Rows[K]["TRAPEZI"].ToString() + "     ").Substring(0, 5) + " " + DT2.Rows[K]["HME"].ToString().Substring(0, 14) + " " + Right("     "+String.Format("{0:0.00}",float.Parse(DT2.Rows[K]["CASH"].ToString()) ), 7) + "" + Right("       "+ String.Format("{0:0.00}", DT2.Rows[K]["PIS1"]), 7) + " " + Right("      "+ String.Format("{0:0.00}", DT2.Rows[K]["PIS2"]), 6) + "" + Right("      "+ String.Format("{0:0.00}", DT2.Rows[K]["KERA"]), 6) + "\r\n";
                mm2 = mm2 + v;
                myText2.Add(v);
                s1 =s1+ float.Parse(DT2.Rows[K]["CASH"].ToString());
                s2 =s2+ float.Parse(DT2.Rows[K]["PIS1"].ToString());
                s3 =s3+ float.Parse(DT2.Rows[K]["PIS2"].ToString());
                s4 =s4+ float.Parse(DT2.Rows[K]["KERA"].ToString());

            }
            //  σουμες
            myText2.Add(("          ").Substring(0, 5) + " " + "              "  + Right("      "+ String.Format("{0:0.00}", s1), 6) + "  " + Right("      "+ String.Format("{0:0.00}", s2), 6) + " " + Right("      "+ String.Format("{0:0.00}", s3), 6)+" " + Right("       " + String.Format("{0:0.00}", s4), 6)+"\r\n");

            await DisplayAlert(mm2, "   ", "OK");
      //      printing(myText2);

            string ss1, ss2, ss3, ss4;
            ss1 = s1.ToString().Replace(",", ".");
            ss2 = s2.ToString().Replace(",", ".");
            ss3 = s3.ToString().Replace(",", ".");
            ss4 = s4.ToString().Replace(",", ".");



            myText2.Add("METP      "+ String.Format("{0:0.00}", s1) + "\r\n");
            myText2.Add("PIST      "+ String.Format("{0:0.00}", s2) + "\r\n");
            myText2.Add("EKPT      " + String.Format("{0:0.00}", s3) + "\r\n");
            myText2.Add("KEPA      " + String.Format("{0:0.00}", s4) + "\r\n");
            myText2.Add("");
            myText2.Add("");
            myText2.Add("");
            myText2.Add("");


            var action = await DisplayAlert("ΝΑ ΚΛΕΙΣΕΙ ΟΡΙΣΤΙΚΑ Η ΒΑΡΔΙΑ;", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {
                try
                {

                    printthis(myText2);
                    Globals.ExecuteSQLServer("UPDATE BARDIA SET CASHTOT=" + (s1 + s2 + s3 + s4).ToString().Replace(",", ".") + ",CASH1=" + ss1 + ",CASH2=" + ss2 + ",CASH3=" + ss3 + ",CASH4 = " + ss4 + ",CLOSEH=substring(  convert(char(16),CURRENT_TIMESTAMP,121) ,1,16) , ISOPEN=0 WHERE ID=" + Globals.gIDBARDIA);
                    
                }
                catch 
                {
                    await DisplayAlert("αδυναμια εκτυπωσης ", "δεν εκλεισε η βαρδια", "OK");
                    // await DisplayAlert("error2", "", "");
                }
            }
            
        }


        private  int  printthis(List<string>mytext)
        {


            string ipAddress = Globals.cIPPR1; // "192.168.1.120";
            int portNumber = 9100;
            var printer = DependencyService.Get<test4sql.iPrinter>();
            if (printer == null)
            {
                //await DisplayAlert("Error", "δεν υπαρχει συνδεση", "");
                return 0;
            }
            try
            {
                PrintSmall(ipAddress);
                LF(ipAddress);
                LF(ipAddress);
                printer.Print(ipAddress, portNumber, mytext);
                CutPaper(ipAddress);
                return 1;
            }

            catch (Exception ex)
            {
                //await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
                return 0;
                // await DisplayAlert("error2", "", "");
            }
            
        }




        public static string Right(string original, int numberCharacters)
        {
            if (original.Length - numberCharacters > 0)
            {
                return original.Substring(original.Length - numberCharacters);
            }

            else
            {

                return original;
            }
        }


        private void PrintSmall(string ipAddress)
        {

            try
            {

            
           List<byte> outputList1 = new List<byte>();
            outputList1.Add(0x1B);
            outputList1.Add(0x40);

            //outputList1.Add(0x1D);
            //outputList1.Add(0x21);
            //outputList1.Add(0x00);
            Socket pSocket1 = new Socket(SocketType.Stream, ProtocolType.IP);
            // Connect to the printer
            pSocket1.Connect(ipAddress, 9100);
            pSocket1.Send(outputList1.ToArray());
            pSocket1.Close();
            }
            catch (Exception ex)
            {
                //await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
                // await DisplayAlert("error2", "", "");
            }

        }

 public static void CutPaper(string ipAddress)
        {
            try
            {


                List<byte> outputList1 = new List<byte>();

                outputList1.Add(0x1B);
                outputList1.Add(0x69);


                Socket pSocket1 = new Socket(SocketType.Stream, ProtocolType.IP);
                // Connect to the printer
                pSocket1.Connect(ipAddress, 9100);
                pSocket1.Send(outputList1.ToArray());
                pSocket1.Close();
            }
            catch (Exception ex)
            {
                //await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
                // await DisplayAlert("error2", "", "");
            }

        }


        

public static void BigLetters(string ipAddress)
        {
            List<byte> outputList1 = new List<byte>();

            outputList1.Add(0x1D);
            outputList1.Add(0x21);
            outputList1.Add(0x11);


            Socket pSocket1 = new Socket(SocketType.Stream, ProtocolType.IP);
            // Connect to the printer
            pSocket1.Connect(ipAddress, 9100);
            pSocket1.Send(outputList1.ToArray());
            pSocket1.Close();


        }











        public static void LF(string ipAddress)
        {
            try
            {


                List<byte> outputList1 = new List<byte>();

                outputList1.Add(0x0A);

                outputList1.Add(0x0D);

                Socket pSocket1 = new Socket(SocketType.Stream, ProtocolType.IP);
                // Connect to the printer
                pSocket1.Connect(ipAddress, 9100);
                pSocket1.Send(outputList1.ToArray());
                pSocket1.Close();
            }
            catch (Exception ex)
            {
                //await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
                // await DisplayAlert("error2", "", "");
            }


        }




        private async void printing(List<string> myText) //  object sender, EventArgs e)
        {





            string ipAddress = Globals.cIPPR1;// "192.168.1.120";
            int portNumber = 9100;
           // List<string> myText = new List<string>();
            //  {PARAGGELIES.toGreek( "ΓΕΙΑ ΣΟΥ ΜΕΓΑΛΕ ΜΟΥ"),"From","Replace","MrNashad","Please Like"};
            //DataTable dt = ReadSQLServer("SELECT  ISNULL(ONO,'') AS ONO, POSO, TIMH,ID,ISNULL(PROSUETA,'') AS PROSUETA  FROM PARAGG where IDPARAGG = " + Globals.gIDPARAGG + "  order by ID ; ");
            // Monkeys.Add(new Monkey
            //myText.Add(PARAGGELIES.toGreek("*******  TΡΑΠΕΖΙ " + Globals.gTrapezi) + " *********");

            //for (int k = 0; k <= dt.Rows.Count - 1; k++)
            //{
            //    myText.Add(dt.Rows[k]["POSO"].ToString() + " " + PARAGGELIES.toGreek(dt.Rows[k]["ONO"].ToString()));
            //    myText.Add(PARAGGELIES.toGreek(dt.Rows[k]["PROSUETA"].ToString()));
            //}


            var printer = DependencyService.Get<test4sql.iPrinter>();
            if (printer == null)
            {
                await DisplayAlert("Error", "δεν υπαρχει συνδεση", "");
                return;

            }
            try
            {
                printer.Print(ipAddress, portNumber, myText);
            }
            catch
            {
                await DisplayAlert("error2", "", "");
            }

        }

        private async void testprint(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page11());

            return;







            string ipAddress = Globals.cIPPR1; // "192.168.1.120";
            int portNumber = 9100;
            List<string> myText = new List<string>();
            DataTable dt;

            for (int k = 0; k <= 800; k++)
            {

            }

            string ss = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠ";
            ss = PARAGGELIES.toGreek(ss);
            // 'myText.Add (Encoding.Unicode.GetBytes(ss));  Convert.ToChar(921) + Convert.ToChar(922)+
            myText.Add(ToGreek737("ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ-ABCDEαβγδεζηθικλμ")) ;

            myText.Add("------------------"+Convert.ToChar (129).ToString()+Convert.ToChar(131).ToString()+Convert.ToChar(130).ToString() )  ;
            myText.Add("A" );
            myText.Add("Β");
            myText.Add("Γ");
            myText.Add("Δ");
            myText.Add("Ε");
            myText.Add("Ζ");
            myText.Add("Η");
            myText.Add("Θ");
            myText.Add("Ι");
            myText.Add("==========================");
            myText.Add("Λ");
            myText.Add("Μ");
            myText.Add("Ν");
            myText.Add("Ξ");
            myText.Add("Ο");
            myText.Add("////////////////////");






            var printer = DependencyService.Get<test4sql.iPrinter>();
            if (printer == null)
            {

                await DisplayAlert("Error", "δεν υπαρχει συνδεση", "");
                return;

            }
            try
            {
                printer.Print(ipAddress, portNumber, myText);
            }

            catch (Exception ex)
            {
                await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
                // await DisplayAlert("error2", "", "");
            }






        }

        private async void fANTIST(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page3 ());

            return;


        }
    }  // public partial class MainPage : ContentPage











}  // namespace
