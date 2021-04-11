using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE.Abstractions.Contracts ;
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
using System.Collections.ObjectModel;
using Plugin.BLE;
using Android.Bluetooth;
using Java.Util;

// using Android.OS;

namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PARAGGELIES : ContentPage
    {

        
        //IBluetoothLE ble;
        //IAdapter adapter;
        //ObservableCollection<IDevice> devicelist;

        public BluetoothDevice mmDevice;
        private int nn = 1;
       public int fisEIDH = 0;
        private string EIDOSPAR = "";

        public string fTIMOK;
        public float fEKPTNUM1=0;
        public float faji = 0;  // SYNOLO ME FPA
        public float fkauaji = 0;  // SYNOLO ME FPA

        public float fkauajiPro = 0;  // SYNOLO ME FPA

        public float fYPOLPEL = 0;  // YPOLOIPO PELATH





        public List<string> MyList = new List<string>();
        public IList<Monkey> Monkeys { get; private set; }
       
        public PARAGGELIES()
        {
            InitializeComponent();
            //ble = CrossBluetoothLE.Current;
            //adapter = CrossBluetoothLE.Current.Adapter;
            //devicelist = new ObservableCollection<IDevice>();



            //ATIM.Text = ReadSQL("select  EIDOS+printf('%06d',  IFNULL(ARITMISI,0)+1)   FROM PARASTAT where ID=1");

            ATIM.Text = PARAGGELIES.ReadSQL("select  printf('%06d',  IFNULL(ARITMISI,0)+1)    FROM PARASTAT WHERE  ID=" + nn.ToString());


            EIDOSPAR = PARAGGELIES.ReadSQL("select ifnull(EIDOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());

            ATIM.Text = EIDOSPAR + Right("000000" + ATIM.Text, 6);





            PAR2.Text = ReadSQL("select TITLOS FROM PARASTAT where ID=1");
            // Monkeys = new List<Monkey>();
            Show_list();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            AFM.Focus();
        }

        // 

        public static string toGreek(string Q)
        {
            string t = "";
            for (int k = 1; k <= Q.Length ; k +=  1)
            {
                string m = Q.Substring(k-1, 1);
                if (instr(0, "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ", m, 1) == -1)
                {
                    t = t + m;
                }
                else
                {
                    if (m == "Α") { t +=  "\u0380"; }
                    if (m == "Β") { t +=  "\u0381"; }
                    if (m == "Γ") { t +=  "\u0382"; }
                    if (m == "Δ") { t +=  "\u0383"; }
                    if (m == "Ε") { t += "\u0384"; }
                    if (m == "Ζ") { t += "\u0385"; }
                    if (m == "Η") { t +=  "\u0386"; }
                    if (m == "Θ") { t +=  "\u0387"; }

                    if (m == "Ι") { t +=  "\u0388"; }
                    if (m == "Κ") { t +=  "\u0389"; }
                    if (m == "Λ") { t +=  "\u038a"; }
                    if (m == "Μ") { t +=  "\u038b"; }
                    if (m == "Ν") { t +=  "\u038c"; }
                    if (m == "Ξ") { t +=  "\u038d"; }
                    if (m == "Ο") { t +=  "\u038E"; }
                    if (m == "Π") { t +=  "\u038F"; }

                    if (m == "Ρ") { t +=  "\u0390"; }
                    if (m == "Σ") { t +=  "\u0391"; }
                    if (m == "Τ") { t +=  "\u0392"; }
                    if (m == "Υ") { t +=  "\u0393"; }
                    if (m == "Φ") { t +=  "\u0394"; }
                    if (m == "Χ") { t +=  "\u0395"; }
                    if (m == "Ψ") { t +=  "\u0396"; }
                    if (m == "Ω") { t +=  "\u0397"; }
                }
            }
            return t;
        }


        //Hi, The following code emulates the instr function with one exception: 
        // It is based on zero based strings.Therefore where the old VB function returned 0
        // (for no match) this returns -1 and if the mach is at the first character this method returns 0. 
        static int instr(int StartPos, String SearchString, String SearchFor, int IgnoreCaseFlag)
        {
            int result = -1;
            if (IgnoreCaseFlag == 1)
                result = SearchString.IndexOf(SearchFor, StartPos, StringComparison.OrdinalIgnoreCase);
            else
                result = SearchString.IndexOf(SearchFor, StartPos);
            return result;
        }


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

           // ATIM.Text = ReadSQL("select  EIDOS+printf('%06d',  IFNULL(ARITMISI,0)+1)   FROM PARASTAT where ID=1");
           // PAR2.Text = ReadSQL("select TITLOS FROM PARASTAT where ID=1");

           // ATIM.Text = PARAGGELIES.ReadSQL("select  EIDOS+printf('%06d',  IFNULL(ARITMISI,0)+1)    FROM PARASTAT WHERE  ID=" + nn.ToString());


        //    EIDOSPAR = PARAGGELIES.ReadSQL("select ifnull(EIDOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());


            ATIM.Text = PARAGGELIES.ReadSQL("select  printf('%06d',  IFNULL(ARITMISI,0)+1)    FROM PARASTAT WHERE  ID=" + nn.ToString());


            EIDOSPAR = PARAGGELIES.ReadSQL("select ifnull(EIDOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());

            ATIM.Text = EIDOSPAR + Right("000000" + ATIM.Text, 6);



        }

        async void FBRESNEXT(object sender, EventArgs e)
        {

            if (nn < PARAGGELIES.NReadSQL("select count(*) from PARASTAT"))
            {
                nn = nn + 1;
            }
            PAR2.Text = PARAGGELIES.ReadSQL("select ifnull(TITLOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());


            ATIM .Text = PARAGGELIES.ReadSQL("select  printf('%06d',  IFNULL(ARITMISI,0)+1)    FROM PARASTAT WHERE  ID=" + nn.ToString());


            EIDOSPAR = PARAGGELIES.ReadSQL("select ifnull(EIDOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());

            ATIM.Text = EIDOSPAR + Right("000000" + ATIM.Text, 6);
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
          //  BresEidos(CKODE.Text);
         //  listview.ItemsSource = null;
         //   listview.ItemsSource = Monkeys;

        }

        async void posothtaCompleted(object sender, EventArgs e)
        {
            CTIMH.Focus();

        }


        async void BRES_AFM(object sender, EventArgs e)
        {
            fisEIDH = 0;

            SAJIA.WidthRequest = 90;
            BRESPREV.IsVisible = false;
            BRESNEXT.IsVisible = false;

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

            Monkeys = new List<Monkey>();
            listview.ItemsSource = null;

            // query the database to prove data was inserted!
            var contents = connection.CreateCommand();
            contents.CommandText = "SELECT  * from PEL WHERE EPO LIKE '% "+ AFM.Text.ToUpper () + "%' OR KOD like '%" + AFM.Text.ToUpper() + "%' LIMIT 100 ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {
                EPO.Text = r["EPO"].ToString();
                fTIMOK = r["PEK"].ToString ();
                fEKPTNUM1 = (float)r["NUM1"];
                fYPOLPEL= (float)r["TYP"];
                Monkeys.Add(new Monkey
                {
                    Name = r["EPO"].ToString(),
                    Location = r["KOD"].ToString(),
                    ImageUrl = r["PEK"].ToString(),
                    idPEL =""
                });



            }
            // r["ONO"].ToString();

            listview.ItemsSource = Monkeys;
            BindingContext = this;

            connection.Close();

          //  BARCODE.Focus();

        }

        async void BresEidos(string CCC)
        {
            CCC=CCC.ToUpper();
            CKODE.Text = CCC;
            fisEIDH = 1; // gia to listview
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
            {               contents.CommandText = "SELECT  ONO,XONDR,ANAM,DESM,YPOL,BARCODE,KOD,IFNULL(FPA,1) AS FPA2 from EID WHERE KODE =" + CKODE .Text + "' LIMIT 2 ; "; // +BARCODE.Text +"'";
            }
            else
            {               contents.CommandText = "SELECT  ONO,XONDR,ANAM,DESM,YPOL,BARCODE,KOD,IFNULL(FPA,1) AS FPA2  from EID WHERE KOD = '" + CKODE.Text + "' LIMIT 2 ; "; // +BARCODE.Text +"'";
            }


           String DD = PARAGGELIES.ReadSQL("select IFNULL(EKPT,0) AS EKTP2 FROM TIMOKAT WHERE KOD='"+CKODE.Text +"' AND  TIMOK=" +fTIMOK );


            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            int flag = 0;
            while (r.Read())
            {
                flag++;
                LPER.Text = r["ONO"].ToString();  // ****
                CTIMH.Text = DD;  // r["XONDR"].ToString();
                //string ccc = r["XONDR"].ToString();
                CEKPT.Text  ="0" ; // ***
                //ldesm.Text = r["DESM"].ToString(); // ****
                //lypol.Text = r["YPOL"].ToString();
                CKODE.Text = r["KOD"].ToString();
                
                // IF DELTA PROIONTA

                FPA.Text = r["FPA2"].ToString();
                //lbarcode.Text = r["BARCODE"].ToString();  // ***

            }
            // r["ONO"].ToString();
            connection.Close();

            if (flag==0 || flag>1)
            {
                Show_list_Eidon(CKODE.Text);
            }
            else
            {
               CPOSO.Focus();
            }


          

        }

        async void kataxorisi(object sender, EventArgs e)
        {


            if ( CKODE.Text.Length == 0)
            {
                await DisplayAlert("ΔΕΝ ΒΑΛΑΤΕ ΚΩΔΙΚΟ", "", "OK");
                return;

            }

            if (CPOSO.Text.Length  == 0 & CKODE.Text.Length> 0)
            {
                await DisplayAlert("ΔΕΝ ΒΑΛΑΤΕ ΠΟΣΟΤΗΤΑ", "", "OK");
                return;

            }



            if (CTIMH.Text.Length == 0 & CKODE.Text.Length > 0)
            {
                await DisplayAlert("ΔΕΝ ΒΑΛΑΤΕ TIMH", "", "OK");
                return;

            }


            //string cposo = POSOTHTA.Text;
            //if (cposo.Length == 0) { cposo = "0"; };
            //cposo = cposo.Replace(",", ".");

            //string ctimh = ltimh.Text;
            //if (ctimh.Length == 0) { ctimh = "0"; };
            //ctimh = ctimh.Replace(",", ".");

            //BindingContext = null;







            //string cc = "INSERT INTO EGGTIM (ATIM,KODE,POSO,TIMH) VALUES ('" + laritmisi.Text + "','" + lkode.Text + "'," + cposo + "," + ctimh + ")";

            // int n2 = MainPage.ExecuteSqlite(cc);

            string fff = CKODE.Text + "_" + LPER.Text+";" + CPOSO.Text + ";" + CTIMH.Text + ";" + CEKPT.Text;
            MyList.Add(fff);

         

            //int k0 = MyList.Count ;
            //for (int k = 0; k<k0; k++)
            //{
             //   string[] lines = MyList[k].Split(';');

            //CKODE.Text = lines[0] + "                 ";
            //CPOSO.Text = lines[1] + "          ";
            //CTIMH.Text = lines[2] + "          ";
            //CEKPT.Text = lines[3] + "          ";

            string cpos, ctimh, cekpt,cfpa;
            cpos = CPOSO.Text.Replace(",", ".");
            ctimh = CTIMH.Text.Replace(",", ".");
            if (CEKPT.Text.Length == 0) { CEKPT.Text = "0"; };

            cekpt = CEKPT.Text.Replace(",", ".");

            if (FPA.Text.Length == 0) {
                cfpa  = "1";
            }
            else
            {
                cfpa = FPA.Text ;
            }
                


            //    Monkeys.Add(new Monkey
            //    {
            //        Name = CKODE.Text.Substring(0,20),
            //        Location = CPOSO.Text.Substring(0, 5),
            //        ImageUrl = CTIMH.Text.Substring(0, 5),
            //        idPEL = CEKPT.Text.Substring(0, 2)
            //    });
            try
            {
                string SQL1 = "insert into EGGTIM (ONO,ATIM,HME,KODE,POSO,TIMH,EKPT,FPA) VALUES ('" + LPER.Text + "','" + ATIM.Text + "', datetime('now'),'" + CKODE.Text + "'," + cpos + "," + ctimh + "," + cekpt + "," +cfpa  + ")";
            MainPage.ExecuteSqlite(SQL1);
                if (EIDOSPAR == "τ")
                {
                    MainPage.ExecuteSqlite("update EID set YPOL=YPOL+" + cpos + " WHERE KOD='" + CKODE.Text + "'");

                }
                else
                {
                    MainPage.ExecuteSqlite("update EID set DESM=DESM+" + cpos + " WHERE KOD='" + CKODE.Text + "'");
                }
                
            }
            catch
            {
                await DisplayAlert("ΔΕΝ ΑΠΟΘΗΚΕΥΤΗΚΕ", "", "OK");

            }

          //  }
            Show_list();
  
            //listview.ItemsSource = Monkeys;
            //BindingContext = this;

            //BARCODE.Text = "";
            //BARCODE.Focus();
            CKODE.Text = "";
            CPOSO.Text = "";
            CTIMH.Text = "";
            CEKPT.Text = "";
            CKODE.Focus();

        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Monkey selectedItem = e.SelectedItem as Monkey;
        }

      async  void  OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Monkey tappedItem = e.Item as Monkey;
            // tappedItem.Location=>'00182'
            //tappedItem.Name=>"ΜΙΖΑΜΤΣΙΔΟΥ ΔΕΣΠΟΙΝΑ"
            if (fisEIDH == 0) { 
              EPO.Text = tappedItem.Name;
              AFM.Text = tappedItem.Location;
              listview.ItemsSource = null;
              fisEIDH = 1; // για να βλεπει τα ειδη απο δω και περα
                fTIMOK = tappedItem.ImageUrl;
                return;
            }
            if (fisEIDH == 1)
            {

                LPER.Text = tappedItem.Name;
                CTIMH.Text = tappedItem.ImageUrl ;
                CKODE.Text = tappedItem.Location;
              
                listview.ItemsSource = null;
                fisEIDH = 2; // για να βλεπει τα ειδη απο δω και περα
                             // BRESPREV.IsVisible = false;
                             // BRESNEXT.IsVisible = false;
                CPOSO.Focus();
                return;
            }

            var action = await DisplayAlert("Να διαγραφεί?", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {
                string cid;
                
                cid = tappedItem.idPEL ;
                string[] lines =  cid.Split(';');
                //  Navigate to first page
                MainPage.ExecuteSqlite("delete from EGGTIM WHERE ID=" + lines[1]);
                await DisplayAlert("διαγραφτηκε", "", "OK");
                Show_list();
            }











        }


        private async void BtnScan_Clicked(object sender, EventArgs e)
        {
            PRINTOUT();
        }

        private  async void PRINTOUT()
        {

        
            string af = AFM.Text;

          //  BluetoothDevice mmDevice;
            BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;

            if (mBluetoothAdapter == null)
            {
                // await MyFavHelper.InformUser("No bluetooth adapter found", "Bluetooth");
                await DisplayAlert("Scanned Barcode", "not ok", "OK");

                return;
            }
            else
            {
               // await DisplayAlert("Αdαpter  ΟΚ", " ok", "OK");
            }

            if (!mBluetoothAdapter.IsEnabled)
            {mBluetoothAdapter.Enable();}
            else
            {    //await DisplayAlert("is enabled", " not ok", "OK");
            }

            ICollection<BluetoothDevice> pairedDevices = mBluetoothAdapter.BondedDevices;

            if (pairedDevices.Count > 0)
            {
                foreach (BluetoothDevice device in pairedDevices)
                {

                   // await DisplayAlert(device.Name, " not ok", "OK");

                    if (device.Name.Contains("ADT"))
                    {
                        mmDevice = device;

                        Stream outStream;

                        Android.OS.ParcelUuid uuid = mmDevice.GetUuids().ElementAt(0);
                        BluetoothSocket socket = mmDevice.CreateInsecureRfcommSocketToServiceRecord(uuid.Uuid);
                        try
                        { socket.Connect();


                        
                        outStream = socket.OutputStream;



                            //Printing

                            // = Encoding.UTF8.GetBytes("αβγδεζηθικλμνξοπρστυφχψωΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ  MY TEXT TO PRINT");
                            //  outStream.Write(toBytes, 0, toBytes.Length);
                            // char[] ccc;

                            //  string llll = Convert.ToChar(921) + Convert.ToChar(922)+ "ελληνικα ΕΛΛΗΝΙΚΑ";

                            //= Convert.ToChar(915);// + Convert.ToChar(916) + Convert.ToChar(917);
                            //  toBytes =     Encoding.Unicode.GetBytes("lowew aα bβ cγ dδ eε zζ hη uθ iι kκ lλ mμ nν xξ oο pπ rρ sσ tτ yυ fφ cχ psψ oω lower");
                            // outStream.Write(toBytes, 0, toBytes.Length);
                            //  string b = "\u03a0";// ι
                            //  string c = "\u03a1";  // κ
                            //  string d = "\u03a3";  // μ
                            //  Ρ     Σ    Τ     Υ      β    γ      δ
                            // llll ="///"+ b + c + d+ "/\u0390\u0391\u0392\u0393\u0399\u039a\u039b //";
                            //  Encoding unicode = Encoding.Unicode;
                            // Convert the string into a byte array.


                            //  byte[] unicodeBytes = unicode.GetBytes(llll);
                            //  outStream.Write(unicodeBytes, 0, unicodeBytes.Length);
                            //DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")	05/29/2015 05:50 AM
                            string spac40 = "                                        ";
                            printt(outStream,(PAR2.Text +spac40).Substring (0,50)+(ATIM.Text+"          ").Substring(0,9)+ DateTime.Now.ToString("dd/MM/yyyy   HH:mm tt") + "\n");
                            printt(outStream, "\n");
                            printt(outStream, "\n");
                            printt(outStream, "\n");
                            printt(outStream, (" ΕΠΩΝΥΜΙΑ:"+EPO.Text+"                         ").Substring (0,30)+ "\n");



                            
                            string EPA=ReadSQL("select  IFNULL(EPA,'')  FROM PEL where KOD='"+ af +"'");
                            string DIE = ReadSQL("select  IFNULL(DIE,'')  FROM PEL where KOD='" + af + "'");
                            string AFM = ReadSQL("select  IFNULL(AFM,'')  FROM PEL where KOD='" + af + "'");
                            string POL = ReadSQL("select  IFNULL(POL,'')  FROM PEL where KOD='" + af + "'");
                            string DOY = ReadSQL("select  IFNULL(DOY,'')  FROM PEL where KOD='" + af + "'");

                            printt(outStream, ("ΕΠΑΓΓΕΛΜΑ: "+EPA + "                         ").Substring(0, 30) + "\n");
                            printt(outStream, ("ΔΙΕΥΘΥΝΣΗ: "+DIE + "                              ").Substring(0, 30) + "\n");
                            printt(outStream, ("     ΠΟΛΗ: "+POL + "                              ").Substring(0, 25) + "\n");
                            printt(outStream, ("      ΑΦΜ: "+AFM + "                              ").Substring(0, 30)+"                          "+BCASH.Text  + "\n");
                            printt(outStream, ("      ΔΟΥ: "+DOY + "                              " + "                               "+ fEKPTNUM1.ToString ()) + "\n"); //fYPOLPEL

                            string dbPath = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                            "adodemo.db3");
                            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
                            // Open the database connection and create table with data
                            connection.Open();
                            var contents = connection.CreateCommand();
                            contents.CommandText = "SELECT  KODE,ifnull(ONO,'') AS PER,POSO,TIMH,EKPT,ID from EGGTIM where ATIM ='" + ATIM.Text + "' order by ID DESC ; ";                                                                                                                                                                          // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
                            var r = contents.ExecuteReader();
                            Console.WriteLine("Reading data");
                            
                           
                            printt(outStream, "\n");
                            printt(outStream, "\n");
                            printt(outStream, "\n");
                            int seir = 13;
                            Single ssum = 0;
                            // String.Format("{0:0.0#}", 123.4567)       // "123.46"
                            while (r.Read())
                            {
                                seir++;
                                Single tt;
                                Single te;
                                te=(Single)r["TIMH"]  * (100 - (Single)r["EKPT"]) / 100;
                                tt = (Single)r["TIMH"] * (Single)r["POSO"] * (100 - (Single)r["EKPT"]) / 100;
                                ssum = ssum + tt;
                                string lin = (r["KODE"].ToString() +"          ").Substring(0,10)+ " " + (r["PER"].ToString() + spac40).Substring(0, 35)+"TEM ";
                                lin = lin + Right("     " + r["POSO"].ToString(), 5) + "  ";
                                lin = lin + Right("     " + String.Format("{0:0.00}", te), 5) + " " ;
                                lin = lin + ""+ "13"+"    "+Right("      "+ String.Format("{0:0.00}", tt), 5) +" ";

                                // lin =lin+Right("     "+String.Format("{0:0.00}", r["EKPT"]),5 )+"13";
                                printt(outStream, lin + "\n");

                            }



                            for (int kk = seir; kk < 40; kk++)
                            {
                                printt(outStream, "\n");

                            }
                            //  fkauajiPro = s;
                           //   s = s - (s * fEKPTNUM1) / 100;
                            //  fkauaji = s;

                            printt(outStream, spac40 + "             ΣΥΝ.ΑΞΙΑ      " + Right("     " + String.Format("{0:0.00}", fkauajiPro), 6) + "\n");
                            printt(outStream, spac40 + "             ΣΥΝ.ΕΚΠΤΩΣΗ   " + Right("     " + String.Format("{0:0.00}", fkauajiPro- fkauaji ), 6) + "\n");


                            printt(outStream, Right("               "+ fYPOLPEL .ToString (),15)+  spac40+"            "+ Right("     " + String.Format("{0:0.00}", fkauaji), 6) + "\n");
                            printt(outStream, spac40+"                           "  + Right("     " + String.Format("{0:0.00}", fkauaji * 0.13), 6) + "\n");
                            printt(outStream, "\n");
                            printt(outStream, spac40+"                           " + Right("     " + String.Format("{0:0.00}", fkauaji * 1.13), 6) + "\n");

                            printt(outStream, "\n");
                            printt(outStream, "\n");
                            printt(outStream, "\n");
                            for (int kk = 0; kk < 17; kk++)
                            {
                                printt(outStream, "\n");

                            }

                            //fff=toGreek ("ΠΑΜΕ ΠΟΛΥ ΚΑΛΑ ΚΑΛΗΝΥΧΤΑ ΑΒΓΔ\n");

                            //toBytes = Encoding.Unicode.GetBytes(fff);
                            //outStream.Write(toBytes, 0, toBytes.Length);
                            ////            "ΠΑΜΕ ΠΟΛΥ ΚΑΛΑ ΚΑΛΗΝΥΧΤΑ ΑΒΓΔ\n"
                            //fff = toGreek("I I I I IIIIIIIIIIIIIIIIIIII\n");
                            //toBytes = Encoding.Unicode.GetBytes(fff);
                            //outStream.Write(toBytes, 0, toBytes.Length);




                            //fff = toGreek("                          II\n");
                            //toBytes = Encoding.Unicode.GetBytes(fff);
                            //outStream.Write(toBytes, 0, toBytes.Length);


                            //string ddd = (char)920+(char)921+ (char)922 + (char)923 + "--ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ PRINT/n";
                            //toBytes = Encoding.GetEncoding("windows-1253").GetBytes(ddd);
                            //   string eee = Encoding.GetEncoding("windows-1253").GetString(toBytes);

                            //   outStream.Write( toBytes, 0, toBytes.Length);
                            // toBytes = Encoding.UTF8 .GetBytes("ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ PRINT/n");
                            //  outStream.Write(toBytes, 0, toBytes.Length);



                        }
                        catch
                        {
                            await DisplayAlert("αδυναμια εκτυπωσης", " ok", "OK");
                        };


                        //    toBytes = Encoding.Default .GetBytes("αβγδεζηθικλμνξοπρστυφχψωΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ   MY TEXT TO PRINT");
                        //   outStream.Write(toBytes, 0, toBytes.Length);
                        try
                        { 

                        socket.Close(); }
                         catch
                        {

                         }
                        //
                        

                        break;
                    }


                }
            }
            else
            {

                await DisplayAlert("Paired Devices not found", " not ok", "OK");
               // await MyFavHelper.InformUser("Paired Devices not found", "Bluetooth");
                return;
            }

            

            /*
            UUID uuid = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");

            if (mmDevice == null)
            {
                await DisplayAlert("NO DEVICE FOUND", " not ok", "OK");
              //  await MyFavHelper.InformUser("No Device Found", "Sorry");
            }

          BluetoothSocket   mmsSocket = mmDevice.CreateRfcommSocketToServiceRecord(uuid);

            mmsSocket.Connect();

            if (mmsSocket.IsConnected)
            {
                await DisplayAlert("socket ok", " not ok", "OK");
                //await MyFavHelper.InformUser("Socket Connected Successfully", "Success");
            }
            else
            {
                await DisplayAlert("NO DEVICE FOUND", " not ok", "OK");
              //  await MyFavHelper.InformUser("Socket Not Connected Successfully", "Sorry");
            }

            System.IO.Stream datastream = mmsSocket.OutputStream;

            byte[] byteArray = Encoding.ASCII.GetBytes("Sample Text");

            datastream.Write(byteArray, 0, byteArray.Length);

            */







            /*   ParcelUuid uuid = mmDevice.GetUuids().ElementAt(0);

               if (mmDevice == null)
               {
                   await MyFavHelper.InformUser("No Device Found", "Sorry");
               }

               mmsSocket = mmDevice.CreateInsecureRfcommSocketToServiceRecord(uuid.Uuid);

               mmsSocket.Connect();


               if (mmsSocket.IsConnected)
               {
                   await DisplayAlert("Socket  Connected Successfully", "not ok", "OK");
                   //await MyFavHelper.InformUser("Socket Connected Successfully", "Success");
               }
               else
               {
                   await DisplayAlert("Socket Not Connected Successfully", "not ok", "OK");
                   // await MyFavHelper.InformUser("Socket Not Connected Successfully", "Sorry");
               }

               var datastream = mmsSocket.OutputStream;

               byte[] byteArray = Encoding.ASCII.GetBytes("Sample Text");

               datastream.Write(byteArray, 0, byteArray.Length);

               */






            //devicelist.Clear();
            //adapter.DeviceDiscovered += (s, a) =>
            //{
            //    devicelist.Add(a.Device);
            //};
            //await adapter.StartScanningForDevicesAsync();
        }

        void printt(Stream outs, string qq)
        {
            byte[] toBytes;
            string fff;
            fff = toGreek(qq);

            toBytes = Encoding.Unicode.GetBytes(fff);
            outs.Write(toBytes, 0, toBytes.Length);



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

        public static string Right(string original, int numberCharacters)
        {
            return original.Substring(original.Length - numberCharacters);
        }

        void Show_list()
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
            contents.CommandText = "SELECT  KODE,ifnull(ONO,'') AS PER,POSO,TIMH,EKPT,ID from EGGTIM where ATIM ='" + ATIM.Text + "' order by ID DESC ; "; // +BARCODE.Text +"'";
                                                                                                                    // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            Single s = 0;
            while (r.Read())
            {
                s = s + (Single)r["POSO"]* (Single)r["TIMH"]*(100- (Single)r["EKPT"])/100;   
                Monkeys.Add(new Monkey
                {
                    Name = (r["KODE"].ToString()+";"+ r["PER"].ToString()+"                ").Substring(0,18),
                    Location = (r["POSO"].ToString()+"      ").Substring(0,5),
                    ImageUrl = (r["TIMH"].ToString()+"      ").Substring(0,5),
                    idPEL = r["EKPT"].ToString() + ";" + r["ID"].ToString()
                }); 
            }

            listview.ItemsSource = Monkeys;
            BindingContext = this;
            fkauajiPro = s;
            s = s - (s * fEKPTNUM1   ) / 100;
            fkauaji = s;
            faji = s * 113/100;
            connection.Close();

            SAJIA.Text = String.Format("{0:0.00}", faji);  // s.ToString();

            BindingContext = this;
        }

        private void Adapter_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void BRESEIDOS(object sender, EventArgs e)
        {
            if (CKODE.Text.Length == 0)
            {
                await DisplayAlert("ΔΕΝ ΒΑΛΑΤΕ ΚΩΔΙΚΟ", "", "OK");
                return;

            }

            BresEidos(CKODE.Text);
        }

        private async void CloseInvoice_Clicked(object sender, EventArgs e)
        {


            MainPage.ExecuteSqlite("update PARASTAT SET ARITMISI=ifnull(ARITMISI,0)+1 WHERE   ID=" + nn.ToString());

            
            MainPage.ExecuteSqlite("INSERT INTO TIM (AJI,HME,ATIM,KPE,TRP,EPO) VALUES ("+SAJIA.Text.Replace(",",".") +",datetime('now'),'" + ATIM.Text +"','"+AFM.Text+"','"+BCASH.Text.Substring(0,5)+"','"+EPO.Text+"')");

            await Navigation.PopAsync();


        }

        private void TimhCompleted(object sender, EventArgs e)
        {
            CEKPT.Focus();
        }

        private async void Cash(object sender, EventArgs e)
        {
            if (BCASH.Text == "ΠΙΣΤΩΣΗ")
            {

                var action = await DisplayAlert("METPHTA ?", "Εισαι σίγουρος?", "Ναι", "Οχι");
                if (action)
                {
                    BCASH.Text = "ΜΕΤΡΗΤΑ";
                    BCASH.BackgroundColor = Xamarin.Forms.Color.Green;
                }
                else
                {
                  //  BCASH.Text = "ΠΙΣΤΩΣΗ";
                }
            }
            else
            {
                var action = await DisplayAlert("ΠΙΣΤΩΣΗ ?", "Εισαι σίγουρος?", "Ναι", "Οχι");
                if (action)
                {
                    BCASH.Text = "ΠΙΣΤΩΣΗ";
                    BCASH.BackgroundColor = Xamarin.Forms.Color.Red ;
                }
                else
                {
                  //  BCASH.Text = "ΠΙΣΤΩΣΗ";
                }



            }




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