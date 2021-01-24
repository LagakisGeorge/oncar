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

namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SUPER : ContentPage
    {
        public IList<Monkey> Monkeys { get; private set; }
        int f_man_barcode = 0;





        public SUPER()
        {
            InitializeComponent();
            lab1.Text = "ll";
            
            
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
           // BARCODE.Focus();
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
            while (r.Read())
            {
                lper.Text = r["ONO"].ToString();  // ****
                ltimh.Text = r["XONDR"].ToString();
                string ccc = r["XONDR"].ToString();
               
                lkode.Text = r["KOD"].ToString();
                lbarcode.Text = r["BARCODE"].ToString();  // ***



            }
            // r["ONO"].ToString();



            connection.Close();


           





        }








    }
}