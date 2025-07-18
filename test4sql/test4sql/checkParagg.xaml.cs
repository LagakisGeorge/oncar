using Android.Content;
using Android.Widget;
using Java.Lang;
using Mono.Data.Sqlite;
using SharpCifs.Smb;  // http://sharpcifsstd.dobes.jp/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using static Android.Resource;
using static Java.Util.ResourceBundle;
using Exception = System.Exception;

namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class checkParagg : ContentPage
    {
        public IList<Monkey> Monkeys { get; private set; }
        int f_man_barcode = 1;
        string cc = "";
        int firstTime = 0;
        //  Globals.PARAGGlines[k, 0] = mOno;
        //             Globals.PARAGGlines[k, 1] = mPoso;
        // Globals.ExecuteSQLServer(cc);
        string f_ID_NUM = "0";

        public checkParagg()
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
          //  Show_list_Eidon(ONO.Text);         
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
                  //  find_eid();
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


        public async void find2_eid()  // αναζητηση με barcode
        {
            string SQL = "";
            string CC = "";
            string C = BARCODE.Text.Trim();
            if (C.Length < 8)
            {
                SQL = "SELECT TOP 1 KOD+';'+ONO AS KODONO FROM EID  WHERE KOD='" + C + "'";
            }
            //'bohu kodikos  ' SQL = "SELECT TOP 1 HENAME+';'+HECODE AS CC FROM [HEITEMS] WHERE HEAUXILIARYCODE = '" + C + "'";
            else
            {
                SQL = "SELECT TOP 1 BARCODES.KOD+';'+ONO AS KODONO FROM BARCODES INNER JOIN EID ON BARCODES.KOD=EID.KOD WHERE BARCODES.ERG='" + C + "'";
            }
            try
            {         
            CC = Globals.ReadSQLServerWithError(SQL);
            ONO.Text = CC;
            if (ONO.Text.Substring(0, 5) == "ERROR")
            {
                    await DisplayAlert("δεν υπαρχει το BARCODE", "ΑΓΝΩΣΤΟ BARCODE", "OK");
                    BARCODE.Text = "";
                BARCODE.Focus();
            }
            else
            {
                //ΒΡΕΘΗΚΕ ΝΑ ΑΦΑΙΡΕΘΕΙ'
                // ΕΙΝΑΙ ΜΕΣΑ ΣΤΑ ΕΙΔΗ ΤΟΥ ΤΙΜΟΛΟΓΙΟΥ?
                // ΝΑΙ ΝΑ ΑΦΑΙΡΕΘΕΙ
                // ΟΧΙ
                //ΒΓΑΖΕΙ ΜΗΝΥΜΑ

                string posothtascan = "1";
                    string rr = RPOSO.Text;
//if (rr.Length > 0)
                   // {
                        posothtascan = rr;
                  //  }

                string[] lines = CC.Split(';');
                BARCODE.Text = lines[0];
                ONO.Text = lines[1];
                if (f_ID_NUM == "0") { }
                else
                {
                     int n3 = MainPage.ExecuteSqlite("update EGGTIM set NUM1=NUM1+"+ posothtascan + " WHERE IDPARAGG="+f_ID_NUM +" AND KODE='" + lines[0]+"'");
                    if (n3 == 0)
                        // δεν βρεθηκε ο κωδικος μεσα στο τιμολόγιο
                        // ή δεν βγαζω τιποτα ή
                        // ή υπάρχει ο κωδικός αλλα ειναι άλλο είδος οπότε το προσθέτω
                        // ή δεν βρίσκω το είδος με αυτό το barcode και βγαζω msgbox
                    {
                        int n4  = MainPage.ExecuteSqlite("insert into EGGTIM (IDPARAGG,NUM1,ONO,KODE,POSO) VALUES (" + f_ID_NUM + ","+posothtascan+",'" +"**"+ lines[1] + "','" + lines[0] + "'," + posothtascan + ");");
                            ONO.BackgroundColor = Xamarin.Forms.Color.Red;
                    }
                        else
                    {
                            ONO.BackgroundColor = Xamarin.Forms.Color.Green;
                    }               
                }

                Show_listNew(f_ID_NUM);
                    RPOSO.Text = "1";
                    BARCODE.Text = "";
                    BARCODE.Focus();
            }

            }
            catch (Exception)
            {

                throw;
            }





        }

        public void find3_eid(string CID) // ekana klik sto listview
        {
            //string SQL = "";

            //string C = BARCODE.Text;
            //SQL = "SELECT TOP 1 LEFT(HENAME,49)+';'+HECODE AS CC FROM [HEITEMS] WHERE HECODE = '" + C + "'";
            //ONO.Text = Globals.ReadSQLServerWithError(SQL);
            //if (ONO.Text.Contains("ERROR"))
            //{
            //    ONO.Text = "λαθος";
            //    return;
            //}
            //SQL = "SELECT SUM(HEATOTIMPQTY) -SUM(HEATOTEXPQTY) AS XX   from [HEITEMACCUMULATORS] " +
            //     "WHERE YEAR(HEDATE)=2023 AND  HEITEMID IN ( SELECT HEID FROM  [HEITEMS] WHERE HECODE='" + C + "' )";
            //float N = Globals.FReadSQLServer(SQL);
            //// FPA.Text = N.ToString();

            //SQL = "SELECT TOP 1  HEITEMCOST FROM HEITEMCOSTPRICES WHERE HEITEMID='" + CID + "'  AND HEITEMCOST>0   ORDER BY HEITEMID,HEDATE DESC";

            //N = Globals.FReadSQLServer(SQL);
            //string c3 = N.ToString();
            //TIMH.Text = c3.Replace(",", ".");
            //TIMH.Text = c3;

            ////'ReadSQL(string Query) LITE








            //SQL = "SELECT (SELECT TOP 1  HENAME FROM HEMEASUREMENTUNITS  WHERE HEID=HEITEMS.HEAMSNTID ) AS DD  FROM HEITEMS WHERE HEID='" + CID + "';";
            //string c4 = Globals.ReadSQLServer(SQL);

            //MON.Text = c4;

            //LTI5.Focus();







        }


        //void find_eid()
        //{





        //    // determine the path for the database file
        //    string dbPath = Path.Combine(
        //        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        //        "adodemo.db3");
        //    bool exists = File.Exists(dbPath);
        //    if (!exists)
        //    {
        //        Console.WriteLine("Creating database");
        //        // Need to create the database before seeding it with some data
        //        Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);

        //    }

        //    SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
        //    // Open the database connection and create table with data
        //    connection.Open();







        //    // query the database to prove data was inserted!
        //    var contents = connection.CreateCommand();
        //    if (Globals.useBarcodes == "1")
        //    {
        //        //contents.CommandText = "SELECT  E.ONO,E.XONDR,E.YPOL,E.BARCODE,E.KOD from EID E inner JOIN BARCODES B ON E.KOD=B.KOD   WHERE B.BARCODE like '%" + BARCODE.Text + "%' LIMIT 1 ; "; // +BARCODE.Text +"'";
        //        contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD from EID WHERE KOD IN (SELECT KOD FROM BARCODES WHERE BARCODE like '%" + BARCODE.Text + "%' LIMIT 1)  ; "; // +BARCODE.Text +"'";
        //    }
        //    else
        //    {
        //        contents.CommandText = "SELECT  ONO,XONDR,YPOL,BARCODE,KOD from EID WHERE ONO like '%" + ONO.Text + "%'  ; "; // +BARCODE.Text +"'";
        //    }

        //    var r = contents.ExecuteReader();
        //    Console.WriteLine("Reading data");
        //    while (r.Read())
        //    {
        //        //lper.Text = r["ONO"].ToString();  // ****
        //        // ltimh.Text = r["XONDR"].ToString();
        //        string ccc = r["XONDR"].ToString();

        //        //  lkode.Text = r["KOD"].ToString();
        //        //  lbarcode.Text = r["BARCODE"].ToString();  // ***

        //        //  cc = cc + lbarcode.Text+";";  // +lper.Text+";"+ltimh.Text+";"+ 

        //    }
        //    // r["ONO"].ToString();



        //    connection.Close();

        //    // System.Threading.Thread.Sleep(1000);







        //}


        void Show_list_Eidon(string ono)  // αναζητηση με ονομα 
        {
            Monkeys = new List<Monkey>();
            BindingContext = null;
            string sql = "select top 50 ATIM,ATIM+PEL.EPO AS EPO,CONVERT(CHAR(10),HME,103) AS HMER,STR(ID_NUM) AS ID_NUM  from TIM INNER JOIN PEL ON TIM.KPE=PEL.KOD AND TIM.EIDOS=PEL.EIDOS WHERE TIM.EIDOS='e' AND   CHARINDEX( LEFT(ATIM,1) , '" + Globals.gTITLOS+"')>0  ORDER BY ID_NUM DESC ";
            // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";

            int lathos = 0;
            DataTable dt = LReadSQLServer(sql, lathos, 0);
            //   for (k = 0; k <= dt.Rows.Count - 1; k++)   String mF = dt.Rows[k]["MONO"].ToString();
            int k;
            for (k = 0; k <= dt.Rows.Count - 1; k++)
            {
                Monkeys.Add(new Monkey
                {
                    Name = (dt.Rows[k]["EPO"].ToString() + "                                ").Substring(0, 25),

                    Location = (dt.Rows[k]["HMER"].ToString() + "                  ").Substring(0, 12),
                    ImageUrl = (dt.Rows[k]["ATIM"].ToString() + "                                         ").Substring(0, 40),
                    idPEL = (dt.Rows[k]["ID_NUM"].ToString() + "                  ").Substring(0, 12),  // dt.Rows[k]["HEID"].ToString()
                });
            }
            listview.ItemsSource = Monkeys;
            listview.BackgroundColor = Xamarin.Forms.Color.Bisque;
            BindingContext = this;
            BindingContext = this;
        }


        //void Show_Last()
        //{
        //    Monkeys = new List<Monkey>();
        //    BindingContext = null;
        //    string sql = "select * from EGGTIMINP  order by ID DESC ; ";
        //    int lathos = 0;
        //    DataTable dt = LReadSQLServer(sql, lathos, 1);
        //    //   for (k = 0; k <= dt.Rows.Count - 1; k++)   String mF = dt.Rows[k]["MONO"].ToString();   dt.Rows[k]["POSO"].ToString() + "                  ").Substring(0, 3)
        //    int k;
        //    for (k = 0; k <= dt.Rows.Count - 1; k++)
        //    {
        //        Monkeys.Add(new Monkey
        //        {
        //            Name = (dt.Rows[k]["POSO"].ToString() + "                  ").Substring(0, 3) + "-" + (dt.Rows[k]["ONO"].ToString() + "                                ").Substring(0, 25),
        //            Location = "  ",
        //            ImageUrl = (dt.Rows[k]["KODE"].ToString() + "                       ").Substring(0, 15),
        //            idPEL = ("      ").Substring(0, 1)      // dt.Rows[k]["HEID"].ToString()
        //        });
        //    }
        //    listview.ItemsSource = Monkeys;
        //    listview.BackgroundColor = Xamarin.Forms.Color.Green;
        //    BindingContext = this;
        //    BindingContext = this;
        //}



        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

            Monkey tappedItem = e.Item as Monkey;
            // tappedItem.Location=>'00182'
            //tappedItem.Name=>"ΜΙΖΑΜΤΣΙΔΟΥ ΔΕΣΠΟΙΝΑ"
            //if (fisEIDH == 0)
            {
                //  BRESafm.IsEnabled = false;


                if (firstTime == 0) // kanv klik sto tim
                {
                    ONO.Text = tappedItem.Name + ";" + tappedItem.Location;
                    BARCODE.Text = "";// tappedItem.Location;
                    string CID = (tappedItem.ImageUrl.ToString() + "                                             ").Substring(0, 40);
                    //  find3_eid(CID);
                    BARCODE.Text = "";// tappedItem.idPEL;
                    string id = tappedItem.idPEL;
                    f_ID_NUM = id;
                    firstTime = 1;
                    try
                    {
                        Show_list_DET(id);
                    }
                    catch
                    {

                    }

                   
                }
                else
                {
                    BARCODE.Text = tappedItem.idPEL;




                }





            }





        }

        void Show_list_DET(string id)  // αναζητηση με id_num
        {



            int n2 = MainPage.ExecuteSqlite("delete from EGGTIM where IDPARAGG=" + id );

            Monkeys = new List<Monkey>();
            BindingContext = null;
            string sql = "select top 150 ONOMA,STR(POSO) AS POSO,MONA,STR(TIMM) +MONA AS TIMH,KODE  from EGGTIM  WHERE ID_NUM="+id+" ORDER BY ONOMA ";
            // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";

            int lathos = 0;
            DataTable dt = LReadSQLServer(sql, lathos, 0);
            //   for (k = 0; k <= dt.Rows.Count - 1; k++)   String mF = dt.Rows[k]["MONO"].ToString();
            int k;
            for (k = 0; k <= dt.Rows.Count - 1; k++)
            {
                string MONO, MKOD, MPOSO;
                MONO = dt.Rows[k]["ONOMA"].ToString();
                MPOSO = dt.Rows[k]["POSO"].ToString();
                MKOD = dt.Rows[k]["KODE"].ToString();
                int n3 = MainPage.ExecuteSqlite("insert into EGGTIM (IDPARAGG,NUM1,ONO,KODE,POSO) VALUES ("+id+",0,'"+MONO+"','" + MKOD + "'," + MPOSO  + ");"   );

                Monkeys.Add(new Monkey
                {
                    Name = (MONO + "                                ").Substring(0, 25),

                    Location = (MPOSO + "                  ").Substring(0, 12),
                    ImageUrl = (dt.Rows[k]["TIMH"].ToString() + "                                         ").Substring(0, 40),
                    idPEL = (MKOD + "                  ").Substring(0, 12),  // dt.Rows[k]["HEID"].ToString()
                });
            }
            listview.ItemsSource = Monkeys;
            listview.BackgroundColor = Xamarin.Forms.Color.Bisque;
            BindingContext = this;
            BindingContext = this;
            BARCODE.Focus();
        }


        void Show_listNew(string id)  // αναζητηση με id_num
        {



           // int n2 = MainPage.ExecuteSqlite("delete from EGGTIM where IDPARAGG=" + id);

            Monkeys = new List<Monkey>();
            BindingContext = null;




            string dbPath = Path.Combine(  Environment.GetFolderPath(Environment.SpecialFolder.Personal),  "adodemo.db3");          
            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();
            var contents = connection.CreateCommand();
            contents.CommandText = "SELECT  ifnull(NUM1,0) as NUM1,ONO,KODE,POSO from EGGTIM WHERE IDPARAGG=" + id+"  ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {        
                

                //    lab1.Text = "ειδη : " + r["d"].ToString() + " αναζ. ειδη";  // ****
                                                                                // 
                string MONO, MKOD, MPOSO,MNUM1;
                MONO = r["ONO"].ToString();
                MPOSO = r["POSO"].ToString();
                MKOD = r["KODE"].ToString();
                MNUM1 = r["NUM1"].ToString();
                //int n3 = MainPage.ExecuteSqlite("insert into EGGTIM (IDPARAGG,NUM1,ONO,KODE,POSO) VALUES (" + id + ",0,'" + MONO + "','" + MKOD + "'," + MPOSO + ");");

                // double retNum;
               if (val(MPOSO) > val(MNUM1))
                {
                    MNUM1 = MNUM1 + " ---";
                }else
                {
                    if (val(MPOSO) == val(MNUM1))
                    { MNUM1 = MNUM1 + " OK OK  "; }
                    else {  MNUM1 = MNUM1 + "********"; }                        
                }
                    try
                    {


                        Monkeys.Add(new Monkey
                        {
                            Name = (MONO + "                                ").Substring(0, 25),

                            Location = (MPOSO + "                  ").Substring(0, 12),
                            ImageUrl = (MKOD + "                                         ").Substring(0, 40),
                            idPEL = MNUM1 //(MKOD + "                  ").Substring(0, 12)"",  // dt.Rows[k]["HEID"].ToString()
                        });

                    }
                    catch (System.Exception)
                    {

                        throw;
                    }








            }
            connection.Close();

            listview.ItemsSource = Monkeys;
            listview.BackgroundColor = Xamarin.Forms.Color.Bisque;
            BindingContext = this;
            BindingContext = this;
        }



        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static float val(string cc)
        {
            float ret;
            ret = float.Parse(cc);
            //'retNum = double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return ret;
        }




      //  private async void UPDATEKOD(object sender, EventArgs e)
     //   {

            //if (IsNumeric(LTI5.Text) == false)

            //{
            //    butbarcode.BackgroundColor = Xamarin.Forms.Color.Yellow;
            //    LTI5.Text = "";
            //    await DisplayAlert("ΒΑΛΤΕ ΠΟΣΟΤΗΤΑ", "ΒΑΛΤΕ ΠΟΣΟΤΗΤΑ", "OK");
            //    LTI5.Focus();
            //    return;
            //}



            //if (ONO.Text.Length > 0)
            //{




            //    try
            //    {


            //        string tt = TIMH.Text;
            //        tt = tt.Replace(",", ".");
            //        string PRAGM = LTI5.Text;
            //        PRAGM = PRAGM.Replace(",", ".");


            //        string[] lines = ONO.Text.Split(';');
            //        //  string sql = "insert into EGGTIMINP (KODE,POSO,TIMH) VALUES('" + lines[1] + "'," + LTI5.Text + "-" + FPA.Text + "," + tt + ")";

            //        string sql = "insert into EGGTIMINP (ONO,KODE,POSO,TIMH) VALUES('" + lines[0] + "','" + lines[1] + "'," + PRAGM + "," + tt + ")";
            //        if (LExecuteSQLServer(sql) == 1)
            //        {  //'LExecuteSQLServer(sql);
            //            ONO.Text = "";
            //            butbarcode.BackgroundColor = Xamarin.Forms.Color.Green;
            //        }
            //        else
            //        {
            //            await DisplayAlert("δεν αποθηκευτηκε", "δεν αποθηκευτηκε", "OK");

            //            string LAT = "ΚΩΔΙΚΟΣ:" + lines[1] + " ONOMA:" + lines[0] + " ΠΟΣΟΤΗΤΑ:" + PRAGM + " TIMH:" + tt;
            //            await DisplayAlert(LAT, " λαθος", "OK");
            //        }
            //    }
            //    catch
            //    {
            //        ONO.Text = "ΛΑΘΟΣ";
            //        butbarcode.BackgroundColor = Xamarin.Forms.Color.Yellow;
            //    }
            //    BARCODE.Text = "";
            //    lab1.Text = ONO.Text;
            //    //  ONO.Text = "";
            //    FPA.Text = "";
            //    LTI5.Text = "";
            //    try
            //    {
            //        Show_Last();
            //    }
            //    catch
            //    {

            //    }
            //    BARCODE.Focus();

            //}

     //   }

        private int LExecuteSQLServer(string sql)
        {
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

           // string constring = @"Data Source=" + lines[0] + ";Initial Catalog=MERCURY;Uid=sa;Pwd=" + lines[2];

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
                return 1;
            }
            catch (System.Exception ex)
            {
                string cv = "";
                // DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
                return 0;
            }

        }

        private DataTable LReadSQLServer(string sql, int latos, int ISMERCURY)
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
                if (LExecuteSQLServer(sql) == 1)
                    await DisplayAlert("Διαγράφηκαν", ".", "OK");
            }
        }

        private void barcfoc2(object sender, EventArgs e)
        {
            try
            {
               find2_eid();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void SHOWTIMP(object sender, EventArgs e)
        {
            RPOSO.Text = "1";
            Show_list_Eidon("");
        }

        private void UPDATEKOD(object sender, EventArgs e)
        {

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "adodemo.db3");
            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();
            var contents = connection.CreateCommand();
            contents.CommandText = "SELECT  ifnull(NUM1,0) as CNUM1,ONO,KODE,POSO from EGGTIM WHERE IDPARAGG=" + f_ID_NUM  + "  ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {
                string q2 = "update EGGTIM SET KOLA=" + r["CNUM1"].ToString() + " WHERE ID_NUM=" + f_ID_NUM + " AND KODE='" + r["KODE"].ToString() + "'";
                int n = LExecuteSQLServer(q2);
                if (r["ONO"].ToString().Substring(0, 2) == "**")
                {
                   string q4 = "insert into  EGGTIM (EIDOS,ID_NUM,KODE,ONOMA,POSO,KOLA) VALUES('e',"+f_ID_NUM+",'"+r["KODE"].ToString()+"','" + r["ONO"].ToString()+"',"+ r["CNUM1"].ToString()+","+ r["CNUM1"].ToString()+")";
                    n = LExecuteSQLServer(q4);
                    // CC = Globals.ReadSQLServerWithError(SQL);
                    // q= " string q = \"update EGGTIM SET KOLA=\" + r[\"CNUM1\"].ToString() + \" WHERE ID_NUM=\" + f_ID_NUM + \" AND KODE='\" + r[\"KODE\"].ToString() + \"'\";"
                     string q3 = "update EGGTIM SET ATIM=(SELECT ATIM FROM TIM WHERE ID_NUM="+f_ID_NUM+") WHERE ID_NUM=" + f_ID_NUM + " AND KODE='" + r["KODE"].ToString() + "'";
                     n = LExecuteSQLServer(q3);

                    q3 = "update EGGTIM SET HME=(SELECT HME FROM TIM WHERE ID_NUM=" + f_ID_NUM + ") WHERE ID_NUM=" + f_ID_NUM + " AND KODE='" + r["KODE"].ToString() + "'";
                    n = LExecuteSQLServer(q3);











                }

            }


            connection.Close();

           string q = "update TIM SET PARAT='"+PARAT.Text+"' WHERE ID_NUM=" + f_ID_NUM ;
            int n2 = LExecuteSQLServer(q);

            KATAX.IsEnabled = false;

        }
    }



}

