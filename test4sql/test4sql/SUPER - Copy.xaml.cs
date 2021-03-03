using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using SharpCifs.Smb;  // http://sharpcifsstd.dobes.jp/
using Plugin.SimpleAudioPlayer;

namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SUPER2 : ContentPage
    {
        private ISimpleAudioPlayer _simpleAudioPlayer;

        public IList<Monkey> Monkeys { get; private set; }
        int f_man_barcode = 1;
        string cc = "";




        public SUPER2()
        {
            InitializeComponent();
            lab1.Text = "ll";
            //  BARCODE.TextChanged += BARCODE_TextChanged;
            _simpleAudioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            Stream beepStream = GetType().Assembly.GetManifestResourceStream("test4sql.beep.mp3");
            bool isSuccess = _simpleAudioPlayer.Load(beepStream);
            BARCODE.Focus();




        }

        

            public void BarcodeComplete(object sender, EventArgs e)
        {
            // ObjectCommandIsOn.LoginCommand.Invoke();
            // OR
            find_eid();
            // Login Command Logic Can go here, but use a ViewModel like a normal Person at least.
        }

        

        //void BARCODE_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        //{
        //    BARCODE.TextChanged -= BARCODE_TextChanged;
        //    char key = e.NewTextValue?.Last() ?? ' ';

        //    if (key ==  'a' )   //(char)13)   
        //    {
        //        //do something 
        //        find_eid();
        //    }


        //    BARCODE.TextChanged += BARCODE_TextChanged;
        //}






        protected override void OnAppearing()
        {
            base.OnAppearing();
            // BARCODE.Focus();



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




            var contents = connection.CreateCommand();
           
                contents.CommandText = "SELECT  count(*) as d from EID  ; "; // +BARCODE.Text +"'";
            

            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {
                if (Globals.useBarcodes == "1")
                {
                    lab1.Text = "ειδη : " + r["d"].ToString() + " αναζ. BARCODES";  // ****
                } else
                {
                    lab1.Text = "ειδη : " + r["d"].ToString() + " αναζ. ειδη";  // ****
                }
            }
           
            connection.Close();





        }

        // 
        async void CHANGEBARCODE(object sender, EventArgs e)
        {
            if (f_man_barcode == 1)
            {
                f_man_barcode = 0;
                //butbarcode.Text = "BARCODE ΜΕ ΦΩΤΟ";
            }
            else
            {
                f_man_barcode = 1;
               // butbarcode.Text = "ΧΕΙΡ.BARCODE";
            }


        }
        

        async void barcfoc(object sender, EventArgs e)
        {  // me fotografiki 
            if (f_man_barcode == 1)
            {
                return;
            }
            System.Threading.Thread.Sleep(3000);
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
                   // Completed = "posothtaCompleted"
                    find_eid();
                });
            };

           

        }


        async void savecodes(object sender, EventArgs e)
        
        {

            string cc = "";
            string dbPath = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.Personal),
               "adodemo.db3");
            bool exists = File.Exists(dbPath);
            if (!exists)
            {
                Console.WriteLine("Creating database");
                Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);
            }
            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();
            var contents = connection.CreateCommand();
            contents.CommandText = "SELECT  KODE,ONO from EGGTIM ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
             while (r.Read())
            {
                int ll = r["KODE"].ToString().Length;
                string ff = r["KODE"].ToString().Substring(0, ll - 1);

               // cc = cc + r["KODE"].ToString() +";"+ ";;;;;\n";  // +lper.Text+";"+ltimh.Text+";"+ 
                cc = cc + ";;"+ff + ";;;;;" + r["ONO"].ToString() + ";;;;;\n";  // +lper.Text+";"+ltimh.Text+";"+ 
            }

             connection.Close();











           // string   text = cc;
            //Get the SmbFile specifying the file name to be created.
            var file = new SmbFile("smb://" + Globals.cIP + "/eggtim2.txt");
            // fine var file = new SmbFile("smb://User:1@192.168.2.7/backpel/New2FileName.txt");







            try 
          { 

            if (file.Exists()) 
              {
                var answer = await DisplayAlert("Το αρχείο υπάρχει", "Να διαγραφεί;", "Ναι", "Οχι");
                if (answer)
                {
                    file.Delete();
                }
                else
                {
                    return;
                }
              }



           
               
             
          }
            catch
            {
               await  DisplayAlert("δεν υπαρχει ο φακελος", "....", "OK");
                return;
            }







            try
            {
                //Create file.
                file.CreateNewFile();
            }
            catch
            {
               await  DisplayAlert("Αδυναμία δημιουργίας αρχείου ", "....", "OK");
                return;
            }


            //Get writable stream.
            var writeStream = file.GetOutputStream();
            // string c = "1;2;3;4;5;6;7;8;\n";
            //  c = c + "8;8;9;9;9;9;9;9\n";
            //  c = c + "18;18;19;19;19;19;19;19\n";

            //Write bytes.
            writeStream.Write(Encoding.UTF8.GetBytes(cc));

            //Dispose writable stream.
            writeStream.Dispose();
           await DisplayAlert("Εγινε η δημιουργία του αρχείου ", "....", "OK");

            cc = "";
        }






        async void BresEidos(object sender, EventArgs e)
        {
            find_eid();

                }

        void find_eid() { 
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
            if (Globals.useBarcodes=="1") {
                //contents.CommandText = "SELECT  E.ONO,E.XONDR,E.YPOL,E.BARCODE,E.KOD from EID E inner JOIN BARCODES B ON E.KOD=B.KOD   WHERE B.BARCODE like '%" + BARCODE.Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
                contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD from EID WHERE KOD IN (SELECT KOD FROM BARCODES WHERE BARCODE like '%" + BARCODE.Text + "%' LIMIT 1)  ; "; // +BARCODE.Text +"'";
            }
            else
            {             
            contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD from EID WHERE KOD like '%" + BARCODE.Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
            }
            
            var r = contents.ExecuteReader();



            Console.WriteLine("Reading data");
                lper.Text = "";
            lbarcode.Text = "";


            while (r.Read())
               
            {
                lper.Text = r["ONO"].ToString();  // ****
                int ll = lper.Text.Length;
                if (ll > 45) {
                    ll = 45;
                }
                lper.Text = lper.Text.Substring(0, ll-1);

                ltimh.Text = r["XONDR"].ToString();
                string ccc = r["XONDR"].ToString();
                but10.BackgroundColor = Color.Green;
                _simpleAudioPlayer.Play();

                lkode.Text = r["KOD"].ToString();
                lbarcode.Text = r["BARCODE"].ToString();  // ***

                 cc = cc + lbarcode.Text+";";  // +lper.Text+";"+ltimh.Text+";"+ 
                connection.Close();


               // string cc2 = "INSERT INTO PARALABES (BARCODE,ATIM) VALUES ('" + lbarcode.Text + "','" + lper.Text + "')";
                string cc2 = "INSERT INTO EGGTIM (KODE,ONO) VALUES ('" + lbarcode.Text + "','"+lper.Text+"')";
              //  var contents2 = connection.CreateCommand();
              //  contents2.CommandText = cc2;
               // var r2 = contents2.ExecuteReader();



                int n2 = MainPage.ExecuteSqlite(cc2);
                break;
            }
            // r["ONO"].ToString();
            if (lper.Text == "")
            {
                but10.BackgroundColor = Color.Red;
            }
           

            connection.Close();

            // System.Threading.Thread.Sleep(1000);

            BARCODE.Text = "";
            BARCODE.Focus();

           


        }








    }
}