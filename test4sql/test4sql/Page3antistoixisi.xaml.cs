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
using test4sql;

namespace oncar 
{ 
   



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage



    {

        public IList<Monkey> Monkeys { get; private set; }
        int f_man_barcode = 1;
        string cc = "";






        public Page3()
        {
            InitializeComponent();
            lab1.Text = "ll";
        }



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
                }
                else
                {
                    lab1.Text = "ειδη : " + r["d"].ToString() + " αναζ. ειδη";  // ****
                }
            }

            connection.Close();





        }



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

        private void lista(object sender, EventArgs e)
        {
            Show_list_Eidon("");
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
            TOSQLSERVER("","");
        }

        private async void TOSQLSERVER(string KOD,string BARCODE)
        {

            string text = cc;
            try
            {
                Globals.ExecuteSQLServer(cc);
                cc = "";
                MainPage.ExecuteSqlite("UPDATE EID SET BARCODE='"+BARCODE+"' WHERE KOD='"+KOD+"'");

            }
            catch
            {
                await DisplayAlert("δεν εγινε συνδεση", "....", "OK");
                return;
            }

           // await Navigation.PopAsync(); // new PelReports());
            return;

            //Get the SmbFile specifying the file name to be created.
            var file = new SmbFile("smb://" + Globals.cIP + "/eggtim2.csv");
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
               await DisplayAlert("δεν υπαρχει ο φακελος", "....", "OK");
                return;
            }







            try
            {
                //Create file.
                file.CreateNewFile();
            }
            catch
            {
               await DisplayAlert("Αδυναμία δημιουργίας αρχείου ", "....", "OK");
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
           await DisplayAlert("Εγινε η δημιουργία του αρχείου ", "....", "OK");

            cc = "";

           await Navigation.PopAsync(); // new PelReports());

        }






        private void savepalioKaineo(object sender, FocusEventArgs e)
        {
            cc = cc + "delete from BARCODES WHERE ERG='"+BARCODE.Text+"';insert into BARCODES(ERG,KOD) VALUES('"+BARCODE.Text+"','" + KODIKOS.Text +"');";
            TOSQLSERVER(KODIKOS.Text,BARCODE.Text );
            BARCODE.Text = "";
            posotita.Text = "";
            mono.Text = "";
            lper.Text = "";
            
            BARCODE.Focus();
        }


        async void BresEidos(object sender, EventArgs e)
        {

            Show_list_Eidon(mono.Text);

        }

        void find_eid(string fromTap)
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





            string cc4 = "";

            // query the database to prove data was inserted!
            var contents = connection.CreateCommand();

            if (fromTap == "0")
            {


                if (Globals.useBarcodes == "1")
                {
                    //contents.CommandText = "SELECT  E.ONO,E.XONDR,E.YPOL,E.BARCODE,E.KOD from EID E inner JOIN BARCODES B ON E.KOD=B.KOD   WHERE B.BARCODE like '%" + BARCODE.Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
                    contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD,XTI from EID WHERE KOD IN (SELECT KOD FROM BARCODES WHERE BARCODE like '%" + BARCODE.Text + "%' LIMIT 1)  ; "; // +BARCODE.Text +"'";
                }
                else
                {
                    contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD,XTI from EID WHERE KOD like '%" + BARCODE.Text + "%'  ; "; // +BARCODE.Text +"'";
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
            ono = ono.ToUpper();
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
            contents.CommandText = "SELECT  ifnull(KOD,'') as KOD,ifnull(ONO,'') AS PER,ifnull(BARCODE,'') as BARCODE,IFNULL(XONDR,'0') AS XONDR,ID from EID where KOD LIKE '%" + ono + "%' OR ONO LIKE '%" + ono + "%' order by ONO ; "; // +BARCODE.Text +"'";
                                                                                                                                                                                                          // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {


                Monkeys.Add(new Monkey
                {
                    Name = (r["PER"].ToString() + "                                               ").Substring(0, 40),

                    Location = (r["KOD"].ToString() + "                      ").Substring(0, 20),
                    ImageUrl = r["XONDR"].ToString()+"--"+(r["BARCODE"].ToString() + "                 ").Substring(0, 13),
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
                KODIKOS.Text = tappedItem.Location;
                string mmid = tappedItem.idPEL;
                find_eid(mmid);

            }





        }

        private void okscann(object sender, FocusEventArgs e)
        {
            mono.Focus();

           

        }

        private void breseidh(object sender, FocusEventArgs e)
        {
            Show_list_Eidon(mono.Text);
        }

        private void AKYROEidos(object sender, EventArgs e)
        {
            lper .Text = "";
        }
    }
}