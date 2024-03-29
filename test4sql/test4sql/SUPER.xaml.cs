﻿using Mono.Data.Sqlite;
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

namespace test4sql
{
    public  class NumericValidationBehavior : Behavior<Entry>
    {

        protected override  void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override  void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x)); //Make sure all characters are numbers  

                ((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }
    }



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SUPER : ContentPage
    {
        public IList<Monkey> Monkeys { get; private set; }
        int f_man_barcode = 0;
        string cc = "";




        public SUPER()
        {
            InitializeComponent();
            lab1.Text = "ll";
            //BARCODE.Completed += txtUserName_Completed;

        }
       // public void txtUserName_Completed(object sender, EventArgs e)
      //  {
      //      find_eid(BARCODE.Text);
      //  }


       


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
                butbarcode.Text = "BARCODE ΜΕ ΦΩΤΟ";
            }
            else
            {
                f_man_barcode = 1;
                butbarcode.Text = "ΧΕΙΡ.BARCODE";
            }


        }
        

        async void barcfoc(object sender, EventArgs e)
        {
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
                    find_eid("0");
                });
            };

           

        }


        async void savecodes(object sender, EventArgs e)
        
        {

         string   text = cc;
            //Get the SmbFile specifying the file name to be created.
            var file = new SmbFile("smb://" + Globals.cIP + "/eggtim2.csv");
            // fine var file = new SmbFile("smb://User:1@192.168.2.7/backpel/New2FileName.txt");







            try { 

            if (file.Exists()) {
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
                DisplayAlert("δεν υπαρχει ο φακελος", "....", "OK");
                return;
            }







            try
            {
                //Create file.
                file.CreateNewFile();
            }
            catch
            {
                DisplayAlert("Αδυναμία δημιουργίας αρχείου ", "....", "OK");
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
            DisplayAlert("Εγινε η δημιουργία του αρχείου ", "....", "OK");

            cc = "";
        }

       




        async void BresEidos(object sender, EventArgs e)
        {

            Show_list_Eidon(mono.Text);

        }

        void find_eid(string fromTap) {



            

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





            string cc4 = "";

            // query the database to prove data was inserted!
            var contents = connection.CreateCommand();

            if (fromTap == "0")
            {

            
              if (Globals.useBarcodes=="1") {
                //contents.CommandText = "SELECT  E.ONO,E.XONDR,E.YPOL,E.BARCODE,E.KOD from EID E inner JOIN BARCODES B ON E.KOD=B.KOD   WHERE B.BARCODE like '%" + BARCODE.Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
                contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD,XTI from EID WHERE KOD IN (SELECT KOD FROM BARCODES WHERE BARCODE like '%" + BARCODE.Text + "%' LIMIT 1)  ; "; // +BARCODE.Text +"'";
              }
              else {             
                  contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD,XTI from EID WHERE ONO like '%" + mono.Text + "%'  ; "; // +BARCODE.Text +"'";
                 }
            }
            else
            {
                contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD,XTI from EID WHERE ID=" + fromTap + "; ";

            }
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {
                lper.Text = r["ONO"].ToString();  // ****
              //  ltimh.Text = r["XONDR"].ToString();
             //   string ccc = r["XONDR"].ToString();
               
                lkode.Text = r["KOD"].ToString();
                lbarcode.Text = r["BARCODE"].ToString();  // ***
               // cc4=r["XTI"].ToString();
               // cc = cc + lkode.Text+";"+posotita.Text+ "\n";  // +lper.Text+";"+ltimh.Text+";"+ 
               // xtimh.Text = "20-"+r["XTI"].ToString();

            }
            // r["ONO"].ToString();

           

            connection.Close();

            // System.Threading.Thread.Sleep(1000);

           

            posotita.Focus();




        }


        void Show_list_Eidon(string ono)
        {
            Monkeys = new List<Monkey>();
            BindingContext = null;

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
            contents.CommandText = "SELECT  ifnull(KOD,'') as KODI,ifnull(ONO,'') AS PER,ifnull(XONDR,0) as timh,ID from EID where KOD LIKE '%" + ono + "%' OR ONO LIKE '%" + ono + "%' order by ONO ; "; // +BARCODE.Text +"'";
                                                                                                                                                                                                          // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {


                Monkeys.Add(new Monkey
                {
                    Name = (r["PER"].ToString() + "                                     ").Substring(0, 30),

                    Location = (r["KODI"].ToString() + "      ").Substring(0, 5),
                    ImageUrl = (r["timh"].ToString() + "      ").Substring(0, 5),
                    idPEL = r["ID"].ToString()
                });



            }

            listview.ItemsSource = Monkeys;
            BindingContext = this;


            connection.Close();

            BindingContext = this;
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

            Monkey tappedItem = e.Item as Monkey;
            // tappedItem.Location=>'00182'
            //tappedItem.Name=>"ΜΙΖΑΜΤΣΙΔΟΥ ΔΕΣΠΟΙΝΑ"
            //if (fisEIDH == 0)
            {
                //  BRESafm.IsEnabled = false;
                mono.Text = tappedItem.Name;
                string mmid =tappedItem.idPEL;
                find_eid(mmid);

            }





            }

        private void okscann(object sender, FocusEventArgs e)
        {
            mono.Focus();

            if (BARCODE.Text.Length >0)
            {
                find_eid("0");
            }
            
        }

        private void lista(object sender, EventArgs e)
        {
            Show_list_Eidon("");
        }

        private void savepalioKaineo(object sender, FocusEventArgs e)
        {
            cc = cc + lkode.Text + ";" + posotita.Text + "\n";
            BARCODE.Text = "";
            posotita.Text = "";
            BARCODE.Focus ();
        }
    }
}