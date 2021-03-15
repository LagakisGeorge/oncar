using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SharpCifs.Smb;  // http://sharpcifsstd.dobes.jp/

using Plugin.Toast;
using System.Data.SqlClient;
//<<<<<<< HEAD
// ' using ZXing.Net.Mobile.Forms;
//'=======
using ZXing.Net.Mobile.Forms;
//>>>>>>> 846966aea3c06d66db1f7d414f0bbcdce34bb4a5




using Mono.Data.Sqlite;
using System.Data;


namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PARAGGELIES : ContentPage
    {

        private int nn = 1;
        private string EIDOSPAR = "";
       public List<string> MyList = new List<string>();
        public IList<Monkey> Monkeys { get; private set; }
       
        public PARAGGELIES()
        {
            InitializeComponent();
            ATIM.Text = ReadSQL("select  ARITMISI+1  FROM PARASTAT where ID=1");
            PAR2.Text = ReadSQL("select TITLOS FROM PARASTAT where ID=1");
           // Monkeys = new List<Monkey>();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            AFM.Focus();
        }

        // 



        async void  kataxorisi2(object sender, EventArgs e)
        {

           

        }


        async void FBRESPREV(object sender, EventArgs e)
        {

            if (nn >1)
            {
                nn = nn - 1;
            }
            PAR2.Text = PARAGGELIES.ReadSQL("select ifnull(TITLOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());


            ATIM.Text = PARAGGELIES.ReadSQL("select  ARITMISI+1  FROM PARASTAT WHERE  ID=" + nn.ToString());


            EIDOSPAR = PARAGGELIES.ReadSQL("select ifnull(EIDOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());





        }

        async void FBRESNEXT(object sender, EventArgs e)
        {

            if (nn < PARAGGELIES.NReadSQL("select count(*) from PARASTAT"))
            {
                nn = nn + 1;
            }
            PAR2.Text = PARAGGELIES.ReadSQL("select ifnull(TITLOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());


            ATIM .Text = PARAGGELIES.ReadSQL("select  ARITMISI+1  FROM PARASTAT WHERE  ID=" + nn.ToString());


            EIDOSPAR = PARAGGELIES.ReadSQL("select ifnull(EIDOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());


        }


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

        async void KODECompleted(object sender, EventArgs e)
        {
            BresEidos(CKODE.Text);
         //  listview.ItemsSource = null;
         //   listview.ItemsSource = Monkeys;

        }










        async void posothtaCompleted(object sender, EventArgs e)
        {

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



            // query the database to prove data was inserted!
            var contents = connection.CreateCommand();
            contents.CommandText = "SELECT  * from PEL WHERE KOD like '%" + AFM.Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {
               EPO.Text = r["EPO"].ToString();
            }
            // r["ONO"].ToString();



            connection.Close();

          //  BARCODE.Focus();

        }



        async void BresEidos(string CCC)
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








            // query the database to prove data was inserted!
            var contents = connection.CreateCommand();
            if (Globals.useBarcodes == "1")
            {
                contents.CommandText = "SELECT  ONO,XONDR,ANAM,DESM,YPOL,BARCODE,KOD from EID WHERE KODE like '%" + CKODE .Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
            }
            else
            {
                contents.CommandText = "SELECT  ONO,XONDR,ANAM,DESM,YPOL,BARCODE,KOD from EID WHERE KOD like '%" + CKODE.Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
            }
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {
                LPER.Text = r["ONO"].ToString();  // ****
                CTIMH.Text = r["XONDR"].ToString();
                string ccc = r["XONDR"].ToString();
                //lanam.Text = r["ANAM"].ToString(); // ***
                //ldesm.Text = r["DESM"].ToString(); // ****
                //lypol.Text = r["YPOL"].ToString();
                //lkode.Text = r["KOD"].ToString();
                //lbarcode.Text = r["BARCODE"].ToString();  // ***

            }
            // r["ONO"].ToString();
            connection.Close();

        }







        async void kataxorisi(object sender, EventArgs e)
        {

            Monkeys = new List<Monkey>();

            listview.ItemsSource = null;
           


            //string cposo = POSOTHTA.Text;
            //if (cposo.Length == 0) { cposo = "0"; };
            //cposo = cposo.Replace(",", ".");

            //string ctimh = ltimh.Text;
            //if (ctimh.Length == 0) { ctimh = "0"; };
            //ctimh = ctimh.Replace(",", ".");

            //BindingContext = null;


    
           



            //string cc = "INSERT INTO EGGTIM (ATIM,KODE,POSO,TIMH) VALUES ('" + laritmisi.Text + "','" + lkode.Text + "'," + cposo + "," + ctimh + ")";

            // int n2 = MainPage.ExecuteSqlite(cc);

            string fff = CKODE.Text + "__" + LPER.Text+";" + CPOSO.Text + ";" + CTIMH.Text + ";" + CEKPT.Text;
            MyList.Add(fff);

            int k0 = MyList.Count ;
            for (int k = 0; k<k0; k++)
            {
                string[] lines = MyList[k].Split(';');

                CKODE.Text = lines[0];
                CPOSO.Text = lines[1];
                CTIMH.Text = lines[2];
                CEKPT.Text = lines[3];

                Monkeys.Add(new Monkey
                {
                    Name = CKODE.Text,
                    Location = CPOSO.Text,
                    ImageUrl = CTIMH.Text,
                    idPEL = CEKPT.Text
                });

            }

  
            listview.ItemsSource = Monkeys;
            BindingContext = this;

            //BARCODE.Text = "";
            //BARCODE.Focus();


        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Monkey selectedItem = e.SelectedItem as Monkey;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Monkey tappedItem = e.Item as Monkey;
        }


        /*   
           async void CloseOrder(object sender, EventArgs e)
           {
               int n2 = MainPage.ExecuteSqlite("update ARITMISI SET ARITMISI=ARITMISI+1 WHERE ID=1;");

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



               // query the database to prove data was inserted!
               var contents = connection.CreateCommand();
               contents.CommandText = "SELECT  * from PEL WHERE KOD like '%" + AFM.Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
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

           // barcfoc

           async void barcfoc(object sender, EventArgs e)
           {
               if (f_man_barcode == 1)
               {
                   return;
               }

               var scanPage = new ZXingScannerPage();
               // Navigate to our scanner page
               await Navigation.PushAsync(scanPage);

               scanPage.OnScanResult += (result) =>
               {
                   // Stop scanning
                   scanPage.IsScanning = false;

                   // Pop the page and show the result
                   Device.BeginInvokeOnMainThread(async () =>
                   {
                       await Navigation.PopAsync();
                       // await DisplayAlert("Scanned Barcode", result.Text, "OK");
                       BARCODE.Text = result.Text;
                   });
               };

           }






          

           async void WriteFile(object sender, EventArgs e)
           {


               string dbPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        "adodemo.db3");
               SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
               // Open the database connection and create table with data
               connection.Open();
               // query the database to prove data was inserted!
               var contents = connection.CreateCommand();
               contents.CommandText = "SELECT ATIM,KODE,POSO,TIMH FROM EGGTIM";
               var r = contents.ExecuteReader();
               // Console.WriteLine("Reading data");
               string cc = "";
               while (r.Read())
               {
                   cc = cc + r["ATIM"].ToString() + ";";
                   cc = cc + r["KODE"].ToString() + ";";
                   cc = cc + r["POSO"].ToString() + ";";
                   cc = cc + r["TIMH"].ToString() + "\n";

               }
               connection.Close();
               SaveFile(cc);
               CrossToastPopUp.Current.ShowToastMessage("Αποθηκεύτηκε");

           }


















          

          void SaveFile(string text)
           {
               //Get the SmbFile specifying the file name to be created.
               var file = new SmbFile("smb://" + Globals.cIP + "/eggtim2.txt");
               // fine var file = new SmbFile("smb://User:1@192.168.1.5/backpel/New2FileName.txt");
               try
               {
                   //Create file.
                   file.CreateNewFile();
               }
               catch
               {
                   DisplayAlert("Υπαρχει ηδη το αρχειο", "....", "OK");
                   return;
               }


               //Get writable stream.
               var writeStream = file.GetOutputStream();
              // string c = "1;2;3;4;5;6;7;8;\n";
             //  c = c + "8;8;9;9;9;9;9;9\n";
             //  c = c + "18;18;19;19;19;19;19;19\n";

               //Write bytes.
               writeStream.Write(Encoding.UTF8.GetBytes(text));

               //Dispose writable stream.
               writeStream.Dispose();
           }

   */



    } //PARAGGELIES
} //NAMESPACE 