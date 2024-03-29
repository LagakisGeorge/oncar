﻿using Mono.Data.Sqlite;
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
using System.Data;
using Android.Content;
using Android.Widget;
using static Java.Util.ResourceBundle;

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
            //need this line to init effect in android
           // Xamarin.KeyboardHelper.Platform.Droid.Effects.Init(this);

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

            //  if (BARCODE.Text.Length == 0)
            //  {
           
                Show_list_Eidon(ONO.Text);
                // return;
          //  }



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
          //  find2_eid();


        }


    async void barcfoc()  // object sender, EventArgs e)
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


        public void find2_eid()  // αναζητηση με barcode
        {
            string SQL = "";
            String CC = "";

            string C = BARCODE.Text;
            if (C.Length < 8)
            {
                SQL = "SELECT TOP 1 HENAME+';'+HECODE+';'+CONVERT(NVARCHAR(50),HEID)  AS CC FROM [HEITEMS] WHERE HECODE='" + C + "'";
            }
            //'bohu kodikos  ' SQL = "SELECT TOP 1 HENAME+';'+HECODE AS CC FROM [HEITEMS] WHERE HEAUXILIARYCODE = '" + C + "'";
            else
            {
                 SQL = "SELECT TOP 1 HENAME+';'+HECODE+';'+CONVERT(NVARCHAR(50),HEID)  AS CC FROM [HEITEMS] WHERE HEID=(select HEITEMID from [HEITEMALTERCODES] WHERE HECODE='" + C + "')";
            }

             
             
            
            CC = Globals.ReadSQLServerWithError(SQL);
            ONO.Text = CC;
            if (ONO.Text.Substring(0, 5) == "ERROR")
            {
                BARCODE.Text = "";
                BARCODE.Focus();
            }
            else
            {

                string[] lines = ONO.Text.Split(';');
                String CID = lines[2];
                SQL = "SELECT SUM(HEATOTIMPQTY) -SUM(HEATOTEXPQTY) AS XX   from [HEITEMACCUMULATORS] " +

                //   "WHERE YEAR(HEDATE)=2023 AND  HEITEMID IN ( SELECT HEID FROM  [HEITEMS] WHERE HEAUXILIARYCODE='"+C+"' )";
                " WHERE YEAR(HEDATE) = 2023 AND HEITEMID ='" + CID + "'";  // (select HEITEMID from[HEITEMALTERCODES] WHERE HECODE = '"+C+"')";



                float N = Globals.FReadSQLServer(SQL);
                FPA.Text = N.ToString();

                SQL = "SELECT TOP 1  HEITEMCOST FROM HEITEMCOSTPRICES WHERE HEITEMID='" + CID + "'  AND HEITEMCOST>0   ORDER BY HEITEMID,HEDATE DESC";
                N = Globals.FReadSQLServer(SQL);
                string c3 = N.ToString();
                TIMH.Text = c3.Replace(",", ".");
                TIMH.Text = c3;

                SQL = "SELECT (SELECT TOP 1  HENAME FROM HEMEASUREMENTUNITS  WHERE HEID=HEITEMS.HEAMSNTID ) AS DD  FROM HEITEMS WHERE HEID='"+CID+"';";
                string c4=  Globals.ReadSQLServer(SQL);
                
                MON.Text = c4;
                





                LTI5.Focus();




            }




            //if (ONO.Text.Substring(0, 5) == "ERROR")
          //  {
              //  BARCODE.Text = "";
              //  BARCODE.Focus();
           // }
          //  else
          //  {
           //     LTI5.Focus();
          //  }




        }

        public void find3_eid(string CID) // ekana klik sto listview
        {
            string SQL = "";

            string C = BARCODE.Text;
            SQL = "SELECT TOP 1 HENAME+';'+HECODE AS CC FROM [HEITEMS] WHERE HECODE = '" + C + "'";
            ONO.Text = Globals.ReadSQLServerWithError(SQL);
            if (ONO.Text.Contains("ERROR"))
            {
                ONO.Text = "λαθος";
                return;
            }
            SQL = "SELECT SUM(HEATOTIMPQTY) -SUM(HEATOTEXPQTY) AS XX   from [HEITEMACCUMULATORS] " +
                 "WHERE YEAR(HEDATE)=2023 AND  HEITEMID IN ( SELECT HEID FROM  [HEITEMS] WHERE HECODE='" + C + "' )";
            float N = Globals.FReadSQLServer(SQL);
            FPA.Text = N.ToString();

            SQL = "SELECT TOP 1  HEITEMCOST FROM HEITEMCOSTPRICES WHERE HEITEMID='" + CID + "'  AND HEITEMCOST>0   ORDER BY HEITEMID,HEDATE DESC";

            N = Globals.FReadSQLServer(SQL);
            string c3 = N.ToString();
            TIMH.Text = c3.Replace(",", ".");
            TIMH.Text = c3;










            SQL = "SELECT (SELECT TOP 1  HENAME FROM HEMEASUREMENTUNITS  WHERE HEID=HEITEMS.HEAMSNTID ) AS DD  FROM HEITEMS WHERE HEID='" + CID + "';";
            string c4 = Globals.ReadSQLServer(SQL);

            MON.Text = c4;

            LTI5.Focus();







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


    void Show_list_Eidon(string ono)  // αναζητηση με ονομα 
    {
        Monkeys = new List<Monkey>();
        BindingContext = null;

         
        
       string sql = "select top 50  HENAME,HECODE,HEID from HEITEMS where HECODE LIKE '"+ONO+"%' OR HENAME LIKE '"+ ono + "%'  order by HENAME ; ";
            // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";

            int lathos = 0;
            DataTable dt= LReadSQLServer(sql,lathos,0);
            //   for (k = 0; k <= dt.Rows.Count - 1; k++)   String mF = dt.Rows[k]["MONO"].ToString();
            int k;
            for ( k=0; k<=dt.Rows.Count - 1; k++)
        {


                //Monkeys.Add(new Monkey
                //{
                //    Name = (dt.Rows[k]["HENAME"].ToString() ,

                //    Location = "",
                //    ImageUrl = "",
                //    idPEL = ""
                //});


                Monkeys.Add(new Monkey
                {
                    Name = (dt.Rows[k]["HENAME"].ToString() + "                                ").Substring(0, 25),

                    Location =  (dt.Rows[k]["HECODE"].ToString() + "                  ").Substring(0, 12),
                    ImageUrl = (dt.Rows[k]["HEID"].ToString() +"                                         ").Substring(0, 40),
                    idPEL = ("      ").Substring(0, 1)      // dt.Rows[k]["HEID"].ToString()
                });






            }

            listview.ItemsSource = Monkeys;
            listview.BackgroundColor = Color.Bisque ;
            BindingContext = this;


        

        BindingContext = this;
    }


        void Show_Last()
        {
            Monkeys = new List<Monkey>();
            BindingContext = null;
            string sql = "select * from EGGTIMINP  order by ID DESC ; ";           
            int lathos = 0;
            DataTable dt = LReadSQLServer(sql, lathos,1);
            //   for (k = 0; k <= dt.Rows.Count - 1; k++)   String mF = dt.Rows[k]["MONO"].ToString();   dt.Rows[k]["POSO"].ToString() + "                  ").Substring(0, 3)
            int k;
            for (k = 0; k <= dt.Rows.Count - 1; k++)
            {
                Monkeys.Add(new Monkey
                {
                    Name = (dt.Rows[k]["POSO"].ToString() + "                  ").Substring(0, 3)+"-"+(dt.Rows[k]["ONO"].ToString() + "                                ").Substring(0, 25),
                    Location = "  ",
                    ImageUrl = (dt.Rows[k]["KODE"].ToString() + "                       ").Substring(0, 15),
                    idPEL = ("      ").Substring(0, 1)      // dt.Rows[k]["HEID"].ToString()
                });
            }
            listview.ItemsSource = Monkeys;
            listview.BackgroundColor =Color.Green ; 
            BindingContext = this;
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
            ONO.Text = tappedItem.Name+";" + tappedItem.Location;
                BARCODE.Text = tappedItem.Location;
                string CID = (tappedItem.ImageUrl.ToString() + "                                             ").Substring(0, 40);
            find3_eid(CID);

        }





    }


        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        private async void UPDATEKOD(object sender, EventArgs e)
    {

            if (IsNumeric(LTI5.Text )==false )

            { 
                 butbarcode.BackgroundColor = Color.Yellow ;
                LTI5.Text = "";
                await DisplayAlert("ΒΑΛΤΕ ΠΟΣΟΤΗΤΑ", "ΒΑΛΤΕ ΠΟΣΟΤΗΤΑ", "OK");
                LTI5.Focus ();
                return; } 



            if (ONO.Text.Length > 0)
            {
               



                try
                {


                    string tt = TIMH.Text;
                    tt = tt.Replace(",", ".");
                    string PRAGM = LTI5.Text;
                    PRAGM = PRAGM.Replace(",", ".");


                    string[] lines = ONO.Text.Split(';');
                    //  string sql = "insert into EGGTIMINP (KODE,POSO,TIMH) VALUES('" + lines[1] + "'," + LTI5.Text + "-" + FPA.Text + "," + tt + ")";

                    string sql = "insert into EGGTIMINP (ONO,KODE,POSO,TIMH) VALUES('" + lines[0] + "','" + lines[1] + "'," + PRAGM + "," + tt + ")";
                    LExecuteSQLServer(sql);
                    ONO.Text = "";
                    butbarcode.BackgroundColor= Color.Green;
                }
                catch
                {
                    ONO.Text = "ΛΑΘΟΣ";
                    butbarcode.BackgroundColor = Color.Yellow ;
                }
                BARCODE.Text = "";
                lab1.Text = ONO.Text;
                //  ONO.Text = "";
                FPA.Text = "";
                LTI5.Text = "";
                try
                {
                  Show_Last();
                }
                catch
                {

                }
                BARCODE.Focus();

            }

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

       private DataTable LReadSQLServer(string sql,int latos,int ISMERCURY)
        {



            if (Globals.cSQLSERVER.Length < 2)
            {
                // await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return null; ;
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            if (ISMERCURY == 1)
            {
                
                 constring = @"Data Source=" + lines[0] + ";Initial Catalog=MERCURY;Uid=sa;Pwd=" + lines[2];

            }



            // string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
                latos = 0;
            }
            catch (Exception ex)
            {
                // await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
                latos = 1;
            }




            string SYNT = "";

            try
            {


                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand(sql, con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);               
                con.Close();
                return dt;


            }
            catch (Exception ex)
            {
                return null;
                // await DisplayAlert("Error", ex.ToString(), "OK");
            }


        }





        private async void DIAGROLD(object sender, EventArgs e)
        {

            var choice = await DisplayAlert("Title", "Να διαγραφουν;", "Ναι", "Οχι");
            if (choice) //yes was clicked
            {
                //do something
           
            // string[] lines = ONO.Text.Split(';');
            string sql = "DELETE FROM EGGTIMINP";
            LExecuteSQLServer(sql);
            await DisplayAlert("Διαγράφηκαν", ".", "OK");
            }
        }

        private void barcfoc2(object sender, EventArgs e)
        {
           find2_eid();
        }
    }

   


}

