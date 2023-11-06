using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using SharpCifs.Smb;  // http://sharpcifsstd.dobes.jp/





namespace test4sql
{ 
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class APOTtsism : ContentPage
{
    public IList<Monkey> Monkeys { get; private set; }
    int f_man_barcode = 1;
    string cc = "";

    // Globals.ExecuteSQLServer(cc);


    public APOTtsism()
    {

        InitializeComponent();
        lab1.Text = "ll";
            BARCODE.Focus();


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

    // 
    async void CHANGEBARCODE(object sender, EventArgs e)
    {
        //if (f_man_barcode == 0)
        //{
        //    f_man_barcode = 0;
        //    butbarcode.Text = "BARCODE ΦΩΤΟ";
        //}
        //else
        //{
        //    f_man_barcode = 1;
        //    butbarcode.Text = "Χ.BARCODE";
        //}
            find2_eid();


        }


    async void barcfoc(object sender, EventArgs e)
    {
        if (f_man_barcode == 1)
        {
                find2_eid();
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

        string text = cc;
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

        Show_list_Eidon(ONO.Text);

    }


     public   void find2_eid()
        {
            string SQL ="" ;
           
            string C = BARCODE.Text;
            SQL = "SELECT TOP 1 HENAME+';'+HECODE AS CC FROM [HEITEMS] WHERE HEAUXILIARYCODE = '" + C + "'";
            ONO.Text = Globals.ReadSQLServerWithError(SQL);
            SQL = "SELECT SUM(HEATOTIMPQTY) -SUM(HEATOTEXPQTY) AS XX   from [HEITEMACCUMULATORS] " +
                 "WHERE YEAR(HEDATE)=2023 AND  HEITEMID IN ( SELECT HEID FROM  [HEITEMS] WHERE HEAUXILIARYCODE='"+C+"' )";
            float N = Globals.FReadSQLServer(SQL);
            FPA.Text = N.ToString();
        }




        void find_eid()
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
            //contents.CommandText = "SELECT  E.ONO,E.XONDR,E.YPOL,E.BARCODE,E.KOD from EID E inner JOIN BARCODES B ON E.KOD=B.KOD   WHERE B.BARCODE like '%" + BARCODE.Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
            contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD from EID WHERE KOD IN (SELECT KOD FROM BARCODES WHERE BARCODE like '%" + BARCODE.Text + "%' LIMIT 1)  ; "; // +BARCODE.Text +"'";
        }
        else
        {
            contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD from EID WHERE ONO like '%" + ONO.Text + "%'  ; "; // +BARCODE.Text +"'";
        }

        var r = contents.ExecuteReader();
        Console.WriteLine("Reading data");
        while (r.Read())
        {
            //lper.Text = r["ONO"].ToString();  // ****
            // ltimh.Text = r["XONDR"].ToString();
            string ccc = r["XONDR"].ToString();

            //  lkode.Text = r["KOD"].ToString();
            //  lbarcode.Text = r["BARCODE"].ToString();  // ***

            //  cc = cc + lbarcode.Text+";";  // +lper.Text+";"+ltimh.Text+";"+ 

        }
        // r["ONO"].ToString();



        connection.Close();

        // System.Threading.Thread.Sleep(1000);







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
                Name = (r["PER"].ToString() + "                         ").Substring(0, 18),

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
            ONO.Text = tappedItem.Name;
            find_eid();

        }





    }

    private void UPDATEKOD(object sender, EventArgs e)
    {
            string[] lines = ONO.Text.Split(';');
            string sql="insert into EGGTIMINP (KODE,POSO) VALUES('" + lines[1]+"',"+LTI5.Text+"-"+FPA.Text+")";
            LExecuteSQLServer(sql);
            BARCODE.Text = "";
            labold.Text = ONO.Text;
            ONO.Text = "";
            FPA.Text = "";
            LTI5.Text = "";
            BARCODE.Focus();



    }
        private void LExecuteSQLServer(string sql)
        {
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=MERCURY;Uid=sa;Pwd=" + lines[2];

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


        private void barcfoc(object sender, TextChangedEventArgs e)
        {
            find2_eid();
        }

        private async void DIAGROLD(object sender, EventArgs e)
        {
           // string[] lines = ONO.Text.Split(';');
            string sql = "DELETE FROM EGGTIMINP";
            LExecuteSQLServer(sql);
            await DisplayAlert("Διαγράφηκαν", ".", "OK");
        }
    }
}

