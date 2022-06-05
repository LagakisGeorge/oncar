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
// ' using ZXing.Net.Mobile.Forms;....
//'=======
using ZXing.Net.Mobile.Forms;
//>>>>>>> 846966aea3c06d66db1f7d414f0bbcdce34bb4a5




using Mono.Data.Sqlite;
using System.Data;
using System.Collections.ObjectModel;
using Plugin.BLE;
using Android.Bluetooth;
//using Java.Util;

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
        private int fISDiortosi = 0;
        public string fTIMOK;  // timokatalogos pelath
        public float fEKPTNUM1=0; // εχτρα εκπτωση πελατη
        public float faji = 0;  // SYNOLO ME FPA
        public float fkauaji = 0;  // SYNOLO ME FPA
       // public string fIDTimDior = "0";
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

            string lasttim = ReadSQL("select ATIM FROM TIM WHERE NUM1=-1 ORDER BY ID DESC");
        if (lasttim.Length == 0)  // den yparxei miso teleiomeno parastatiko
            {
                fISDiortosi = 0;
            ATIM.Text = PARAGGELIES.ReadSQL("select  printf('%06d',  IFNULL(ARITMISI,0)+1)    FROM PARASTAT WHERE  ID=" + nn.ToString());
            EIDOSPAR = PARAGGELIES.ReadSQL("select ifnull(EIDOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());
            ATIM.Text = EIDOSPAR + Right("000000" + ATIM.Text, 6);
            PAR2.Text = ReadSQL("select TITLOS FROM PARASTAT where ID=1");
                CKODE.IsEnabled = true;

            }
        else
            {

                
              try
              {

                
                fISDiortosi = 1;
                    BRESPREV.IsEnabled = false;
                    BRESNEXT.IsEnabled = false;
                    // fIDTimDior = ReadSQL("select ID FROM TIM WHERE ATIM='" + ATIM.Text + "'");
                    ATIM.Text = lasttim;
                EIDOSPAR = lasttim.Substring(0, 1);
                PAR2.Text = ReadSQL("select TITLOS FROM PARASTAT where EIDOS='"+EIDOSPAR+"'");
                AFM.Text = ReadSQL("select KPE FROM TIM WHERE ATIM='" + ATIM.Text + "'");
                EPO.Text = ReadSQL("select EPO FROM TIM WHERE ATIM='" + ATIM.Text + "'");
                  nn =(int) NReadSQL("select ID FROM PARASTAT where EIDOS='" + EIDOSPAR + "'");

                    CKODE.Focus();
                    bresPelath_NoList();  //  bresPelath(); // giana parei ypoloipo kai katalogo kai ekptosi
                   
                  CKODE.IsEnabled = true;
                
                }
                catch
                {                }                
                //  fTIMOK = tappedItem.ImageUrl;
            }
        if (EIDOSPAR == "τ")
            {
                BSYGKEPIS.IsVisible = true;
            }
        else
            {
                BSYGKEPIS.IsVisible = false;
            }          
            Show_list();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            AFM.Focus();
        }

         void bresPelath_NoList()
         {
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "adodemo.db3");
               SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();
            string ff = "";
            try
            {


                var contents = connection.CreateCommand();
                contents.CommandText = "SELECT  EPO,IFNULL(PEK,0) AS PEK2,IFNULL(NUM1,0) AS NUM12,IFNULL(TYP,0) AS TYP2 from PEL WHERE EPO LIKE '% " + AFM.Text.ToUpper() + "%' OR KOD like '%" + AFM.Text.ToUpper() + "%' LIMIT 100 ; "; // +BARCODE.Text +"'";
                var r = contents.ExecuteReader();
                while (r.Read())
                {
                    // EPO.Text = r["EPO"].ToString();  gia na kanei klik sto listview
                    fTIMOK = r["PEK2"].ToString();
                    fEKPTNUM1 = float.Parse(r["NUM12"].ToString());
                    fYPOLPEL = float.Parse(r["TYP2"].ToString());
                    LPLIR.Text = r["ID"].ToString() + " Εκπ:" + r["NUM12"].ToString();
                }
            }
            catch
            {
                ff = "error";
            }
            connection.Close();
         }

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

            if (EIDOSPAR == "τ")
            {
                BSYGKEPIS.IsVisible = true ;
            }
            else
            {
                BSYGKEPIS.IsVisible = false;
            }

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

            if (EIDOSPAR == "τ")
            {
                BSYGKEPIS.IsVisible = true;
            }
            else
            {
                BSYGKEPIS.IsVisible = false;
            }


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
            contents.CommandText = "SELECT  * from PEL WHERE EPO LIKE '%" + AFM.Text.ToUpper() + "%' OR KOD like '%" + AFM.Text.ToUpper() + "%' LIMIT 100 ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {
               // EPO.Text = r["EPO"].ToString();
                fTIMOK = r["PEK"].ToString();
                if (nn == 1)
                {
                    fEKPTNUM1 = (float)r["NUM1"];
                }
                else  // στην σειρα Β δεν εχει εκπτωση
                {
                    fEKPTNUM1 = 0;
                }
                fYPOLPEL = (float)r["TYP"];
                Monkeys.Add(new Monkey
                {
                    Name = r["EPO"].ToString(),
                    Location = r["KOD"].ToString(),
                    ImageUrl = r["PEK"].ToString(),
                    idPEL = r["ID"].ToString()
                });



            }
            // r["ONO"].ToString();

            listview.ItemsSource = Monkeys;
            BindingContext = this;

            connection.Close();

            //  BARCODE.Focus();

        }






        /*
        async void BRES_AFM(object sender, EventArgs e)
        {
           
            fisEIDH = 0;
           // CKODE.IsEnabled = true;
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
            contents.CommandText = "SELECT  EPO,IFNULL(PEK,0) AS PEK2,IFNULL(NUM1,0) AS NUM12,IFNULL(TYP,0) AS TYP2 from PEL WHERE EPO LIKE '% "+ AFM.Text.ToUpper () + "%' OR KOD like '%" + AFM.Text.ToUpper() + "%' LIMIT 100 ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {
                // EPO.Text = r["EPO"].ToString();  gia na kanei klik sto listview
                fTIMOK = r["PEK2"].ToString ();
                fEKPTNUM1 = float.Parse(r["NUM12"].ToString ());
                fYPOLPEL= float.Parse(r["TYP2"].ToString());  // (float)r["TYP2"];
                LPLIR.Text = "Yπ:" + r["TYP2"].ToString () + " Εκπ:" + r["NUM12"].ToString ();


                Monkeys.Add(new Monkey
                {
                    Name = r["EPO"].ToString(),
                    Location = r["KOD"].ToString(),
                    ImageUrl = r["PEK2"].ToString(),
                    idPEL = ""  //  r["TYP2"].ToString() + ";" + r["NUM12"].ToString()
                }); ;



            }
            // r["ONO"].ToString();

            listview.ItemsSource = Monkeys;
            BindingContext = this;

            connection.Close();

          //  BARCODE.Focus();

        }

        */


        async void BresEidos(string CCC)
        {
            CCC=CCC.ToUpper();
            CKODE.Text = CCC;
           
           // fisEIDH = 1; // gia to listview
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
            {              
                contents.CommandText = "SELECT  ONO,XONDR,ANAM,DESM,YPOL,BARCODE,KOD,IFNULL(FPA,1) AS FPA2  from EID WHERE KOD = '" + CKODE.Text + "' LIMIT 2 ; "; // +BARCODE.Text +"'";
            }


           String DD = PARAGGELIES.ReadSQL("select IFNULL(EKPT,0) AS EKTP2 FROM TIMOKAT WHERE KOD='"+CKODE.Text +"' AND  TIMOK=" +fTIMOK );


            var r = contents.ExecuteReader();
           // Console.WriteLine("Reading data");
            int flag = 0;
            while (r.Read())
            {
                flag++;
                LPER.Text = r["ONO"].ToString();  // ****
                LPER.TextColor = Color.Blue;

                if (DD  == "" || DD == "0")
                {
                    //  /string cdd = r["XONDR"];
                    // CTIMH.Text = r["XONDR"].ToString() ;
                    CTIMH.Text = String.Format("{0:0.0#}", r["XONDR"]);
                   // TEST   :  LTIMH.Text = ReadSQL("select XONDR FROM EID WHERE KOD='" + CKODE.Text + "'");

                }
                else
                { CTIMH.Text = DD; }
                
                
                //CTIMH.Text = DD;  // r["XONDR"].ToString();
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



            string cck = CKODE.Text;
            if (flag == 0 )
            {
                LPER.Text = cck+" ΔΕΝ ΒΡΕΘΗΚΕ ";  // ****
                LPER.TextColor = Color.Red;
                CKODE.Text = "";
                CKODE.Focus();

            }



            if   (flag==0 || flag>1)
            {
                Show_list_Eidon(cck);
                fisEIDH = 1; // για να tsimpaei  τα ειδη 
            }
            else
            {
               CPOSO.Focus();
                fisEIDH = 2; // για να βλεπει τα ειδη timol  απο δω και περα
                Show_list();
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
                
               

            }


            if (EIDOSPAR == "τ" )
            {
                CTIMH.Text = "0";
            }
            else
            {

                try
                {

                    //if (IsNumeric(   CPOSO.Text))
                    //{
                    //    await DisplayAlert("η ποσότητα δεν ειναι αριθμός", "", "OK");
                    //    return;
                    //}
                    

                string mpos = ReadSQL("select IFNULL(YPOL,0)-IFNULL(DESM,0) AS TR FROM EID WHERE KOD='" + CKODE.Text + "'");
               // string value = "246246.246";
               // Convert.ToInt64(Convert.ToDouble(value));
                if (Convert.ToInt64(Convert.ToDouble(CPOSO.Text.Replace(",","."))) > Convert.ToInt64(Convert.ToDouble(mpos)))
                {

                await DisplayAlert("ΕΧΩ ΥΠΟΛΟΙΠΟ "+mpos, "", "OK");
                return;
                }
              }catch
                {

              }

            }


            if (CTIMH.Text.Length == 0 & CKODE.Text.Length > 0)
            {


                //  αν ειναι δελτιο συγ η απλο δεν θελω τιμη  CKODE.Text
                if (EIDOSPAR == "τ" || EIDOSPAR == "A")
                {
                    CTIMH.Text = "0";
                }
                else
                {
                   await DisplayAlert("ΔΕΝ ΒΑΛΑΤΕ TIMH", "", "OK");
                   return;
                   
                }

                

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


            BRESafm.IsEnabled = false;
            // KLEIDONO TON PELATH GIATI ΘΑ ΒΓΟΥΝ ΛΑΘΟΣ ΟΙ ΤΙΜΕΣ
            // ΑΝ ΠΕΡΑΣΩ ΕΝΑ ΕΙΔΟΣ ΜΕ ΠΕΛΑΤΗ Α
            // ΚΑΙ ΤΟ ΔΕΥΤΕΡΟ ΜΕ ΠΕΛΑΤΗ Β (ΘΑ ΠΑΡΕΙ ΑΛΛΟ ΤΙΜΟΚΑΤΑΛΟΓΟ)



            try
            {
                string SQL1 = "insert into EGGTIM (CH1,ONO,ATIM,HME,KODE,POSO,TIMH,EKPT,FPA) VALUES ('"+AFM.Text +"','" + LPER.Text + "','" + ATIM.Text + "', datetime('now'),'" + CKODE.Text + "'," + cpos + "," + ctimh + "," + cekpt + "," +cfpa  + ")";
            MainPage.ExecuteSqlite(SQL1);
                if (EIDOSPAR == "τ")
                {
                    MainPage.ExecuteSqlite("update EID set YPOL=IFNULL(YPOL,0)+" + cpos + " WHERE KOD='" + CKODE.Text + "'");

                }
                else
                {
                    MainPage.ExecuteSqlite("update EID set DESM=IFNULL(DESM,0)+" + cpos + " WHERE KOD='" + CKODE.Text + "'");
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


        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
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
                //  BRESafm.IsEnabled = false;
              EPO.Text = tappedItem.Name;
              AFM.Text = tappedItem.Location;
              listview.ItemsSource = null;
                CKODE.IsEnabled = true;
              fisEIDH = 1; // για να βλεπει τα ειδη απο δω και περα

                // BAZO NUM1=-1 ΟΤΙ ΕΙΝΑΙ ΕΚΚΡΕΜΕΣ
                if (fISDiortosi == 1){}
                else
                {
                    MainPage.ExecuteSqlite("delete from TIM WHERE ATIM='" + ATIM.Text + "'");


                   MainPage.ExecuteSqlite("INSERT INTO TIM (NUM1,HME,ATIM,KPE,TRP,EPO) VALUES (-1,datetime('now'),'" + ATIM.Text + "','" + AFM.Text + "','" + BCASH.Text.Substring(0, 5) + "','" + EPO.Text + "')");
                }



                fTIMOK = tappedItem.ImageUrl;
                string ccv = ReadSQL("SELECT IFNULL(NUM1,0) FROM PEL WHERE ID=" + tappedItem.idPEL);
                fEKPTNUM1 = float.Parse(ccv);
                if (nn == 1)
                {
                    //fEKPTNUM1 = (float)r["NUM1"];
                }
                else  // στην σειρα Β δεν εχει εκπτωση
                {
                    fEKPTNUM1 = 0;
                }


                string ccv2 = ReadSQL("SELECT IFNULL(TYP,0) FROM PEL WHERE ID=" + tappedItem.idPEL);
                fYPOLPEL = float.Parse(ccv2);
               
                // string[] lines = tappedItem.idPEL.Split(';');
                // fEKPTNUM1 = float.Parse (lines[1]);
                //  fYPOLPEL = float.Parse(lines[0]);
                 LPLIR.Text = "Yπ:" +ccv2 + " Εκπ:" + ccv;
                // idPEL = r["TYP2"].ToString() + ";" + r["NUM12"].ToString()
                return;
            }
            if (fisEIDH == 1)
            {

                LPER.Text = tappedItem.Name;
                CTIMH.Text = tappedItem.ImageUrl ;
                CKODE.Text = tappedItem.Location;
              
                listview.ItemsSource = null;
                fisEIDH = 2; // για να βλεπει τα ειδη timol  απο δω και περα
                             // BRESPREV.IsVisible = false;
                             // BRESNEXT.IsVisible = false;
                CPOSO.Focus();
                Show_list();
                return;
            }

            var action = await DisplayAlert("Να διαγραφεί?", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {
                string cKOD;
                
                cKOD = tappedItem.Name;
                string[] lines1 = cKOD.Split(';');
                cKOD = lines1[0];

                string cid;
                
                cid = tappedItem.idPEL ;
                string[] lines =  cid.Split(';');
                //  Navigate to first page
                string mposo=ReadSQL("select POSO FROM EGGTIM WHERE  ID=" + lines[1]);
                if (EIDOSPAR == "τ")
                {
                    MainPage.ExecuteSqlite("update EID set YPOL=IFNULL(YPOL,0)-" +mposo   + " WHERE KOD='" + cKOD + "'");

                }
                else
                {
                    MainPage.ExecuteSqlite("update EID set DESM=IFNULL(DESM,0)-" + mposo  +" WHERE KOD='" + cKOD + "'");
                }

                MainPage.ExecuteSqlite("delete from EGGTIM WHERE ID=" + lines[1]);
                await DisplayAlert("διαγραφτηκε", "", "OK");
                Show_list();
            }











        }


        private async void BtnScan_Clicked(object sender, EventArgs e)
        {



            try
            {
                if (ATIM.Text.Substring(0, 1) == "τ")  // συγκεντρωτικο
                {
                    PRINTOUT(1);
                }
                else
                {
                  PRINTOUT(0);
                }
               
            }
            catch
            {

            }
           
        }

        private async void PRINTOUT( int IsSygkEpistr) // 0=timologio 1=sygkentrotiko epistrofis
        {

        
            string af = AFM.Text;

            //  BluetoothDevice mmDevice;   ATIM.Text(Τ000012) ,epo.text ,BCASH.Text(ΜΕΤΡΗΤΑ), Par2.Text(ΤΙΤΛΟΣ ΠΑΡ/ΚΟΥ)  ,AFM.TEXT (0001)
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
            try
            {


                if (pairedDevices.Count > 0)
                {
                    foreach (BluetoothDevice device in pairedDevices)
                    {

                        // await DisplayAlert(device.Name, " not ok", "OK");

                        if (device.Name.Contains("ADT") || device.Name.Contains("DDA6"))
                        {
                            mmDevice = device;

                            Stream outStream;

                            Android.OS.ParcelUuid uuid = mmDevice.GetUuids().ElementAt(0);
                            BluetoothSocket socket = mmDevice.CreateInsecureRfcommSocketToServiceRecord(uuid.Uuid);
                            try
                            {
                                socket.Connect();



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



                                //int i = 3;
                                //Console.WriteLine(i);   // output: 3
                                //Console.WriteLine(i++); // output: 3
                                //Console.WriteLine(i);   // output: 4


                                //Prefix increment operator
                                //double a = 1.5;
                                //Console.WriteLine(a);   // output: 1.5
                                //Console.WriteLine(++a); // output: 2.5
                                //Console.WriteLine(a);   // output: 2.5

                                //                            int[] terms = new int[400];
                                //                            for (int runs = 0; runs < 400; runs++)
                                //                            {
                                //                                terms[runs] = value;
                                //                            }
                                //                            Alternatively, you can use Lists -the advantage with lists being, you don't need to know the array size when instantiating the list.

                                //List<int> termsList = new List<int>();
                                //                            for (int runs = 0; runs < 400; runs++)
                                //                            {
                                //                                termsList.Add(value);
                                //                            }

                                //                            // You can convert it back to an array if you would like to
                                //                            int[] terms = termsList.ToArray();



                                string spac40 = "                                        ";
                                // diabazo lines apo timologio
                                string[] cLine = new string[400];
                                string dbPath = Path.Combine(
                                 Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                 "adodemo.db3");

                                SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
                                // Open the database connection and create table with data
                                connection.Open();
                                var contents = connection.CreateCommand();
                                //   if (IsSygkEpistr ==0)
                                //   {
                                contents.CommandText = "SELECT  KODE,ifnull(ONO,'') AS PER,ifnull(POSO,0) as POSO,IFNULL(TIMH,0 ) AS TIMH ,IFNULL(EKPT,0) AS EKPT ,ID ,IFNULL(TIMH*POSO,0) AS AXIA,ifnull(POSO,0)-ifnull(TIMH,0) as EPISTRPOSO  from EGGTIM where ATIM ='" + ATIM.Text + "' order by ID DESC ; ";                                                                                                                                                                          // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";

                                //  }
                                //  else
                                //  {
                                //     contents.CommandText = "SELECT  KODE,ifnull(ONO,'') AS PER,EID.YPOL AS POSO,IFNULL(EID.DESM,0) AS TIMH,EGGTIM.EKPT from EGGTIM inner join EID ON EGGTIM.KODE=EID.KOD  where ATIM ='" + ATIM.Text + "';";   // order by EGGTIM.ID DESC ; ";                                                                                                                                                                          // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";

                                // }
                                var r = contents.ExecuteReader();
                                Console.WriteLine("Reading data");
                                int nseira = 0;
                                //   Single ssum = 0;
                                // String.Format("{0:0.0#}", 123.4567)       // "123.46"
                                while (r.Read())
                                {
                                    nseira++;
                                    Single tt;
                                    Single te;

                                    if (IsSygkEpistr == 1)
                                    {
                                        string MPOL = PARAGGELIES.ReadSQL("select IFNULL(DESM,0) AS DESM from EID where KOD='" + r["KODE"].ToString() + "'");
                                        // te = Convert.ToInt64(Convert.ToDouble(MPOL));
                                        te = float.Parse(r["TIMH"].ToString());
                                        tt = float.Parse(r["EPISTRPOSO"].ToString());
                                        //  tt = Convert.ToInt64(Convert.ToDouble(r["POSO"])) - te;        //(Single)r["POSO"] - te ;
                                    }
                                    else
                                    {
                                        te = float.Parse(r["TIMH"].ToString());  //.Replace(",",".") );  //  * (100 - (Single)r["EKPT"]) / 100;
                                        tt = float.Parse(r["AXIA"].ToString()); //.Replace(",", "."));   //   * (Single)r["POSO"] * (100 - (Single)r["EKPT"]) / 100;
                                    }
                                    //  ssum = ssum + tt;
                                    string lin = (r["KODE"].ToString() + "          ").Substring(0, 10) + " " + (r["PER"].ToString() + spac40).Substring(0, 35) + "TEM";
                                    lin = lin + Right("      " + r["POSO"].ToString(), 6) + " ";
                                    lin = lin + Right("      " + String.Format("{0:0.00}", te), 6) + " ";
                                    lin = lin + "" + "13" + "  " + Right("       " + String.Format("{0:0.00}", tt), 7) + " ";
                                    cLine[nseira] = lin;
                                }
                                // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
                                int nCount = nseira;   // posa records exei to timologio
                                int RecPerPage = 26;
                                int nPages = nCount / RecPerPage;
                                if (nCount % RecPerPage > 0) // exei ypoloipo
                                {
                                    ++nPages;
                                };

                                int quotient = 5 / 3;
                                //Console.WriteLine(13 / 5);    // output: 2
                                //Console.WriteLine(17 % 4);   // output: 1




                                //==========================================================================================================
                                int nCurRow = 0;
                                for (int nCurPage = 1; nCurPage <= nPages; nCurPage++)
                                {


                                    printt(outStream, (PAR2.Text + spac40).Substring(0, 50) + (ATIM.Text + "          ").Substring(0, 9) + DateTime.Now.ToString("dd/MM/yyyy   HH:mm tt") + "\n");
                                    printt(outStream, "\n");
                                    printt(outStream, "\n");
                                    printt(outStream, "\n");
                                    printt(outStream, (" ΕΠΩΝΥΜΙΑ:" + EPO.Text + "                                   ").Substring(0, 40) + "                              " + (PARAGGELIES.toGreek(Globals.cFORTHGO) + "          ").Substring(0, 9) + "\n");




                                    string EPA = PARAGGELIES.ReadSQL("select  IFNULL(EPA,'')  FROM PEL where KOD='" + af + "'");
                                    string DIE = PARAGGELIES.ReadSQL("select  IFNULL(DIE,'')  FROM PEL where KOD='" + af + "'");
                                    string AFM = PARAGGELIES.ReadSQL("select  IFNULL(AFM,'')  FROM PEL where KOD='" + af + "'");
                                    string POL = PARAGGELIES.ReadSQL("select  IFNULL(POL,'')  FROM PEL where KOD='" + af + "'");
                                    string DOY = PARAGGELIES.ReadSQL("select  IFNULL(DOY,'')  FROM PEL where KOD='" + af + "'");
                                    string PARAD = PARAGGELIES.ReadSQL("select  IFNULL(CH3,'ΕΔΡΑ ΠΕΛΑΤΗ')  FROM PEL where KOD='" + af + "'");


                                    printt(outStream, ("ΕΠΑΓΓΕΛΜΑ: " + EPA + "                         ").Substring(0, 30) + "\n");
                                    printt(outStream, ("ΔΙΕΥΘΥΝΣΗ: " + DIE + "                              ").Substring(0, 36) + "              " + (PARAD+"                              ").Substring(0,30)+ "\n");
                                    printt(outStream, ("     ΠΟΛΗ: " + POL + "                              ").Substring(0, 25) + "\n");
                                    printt(outStream, ("      ΑΦΜ: " + AFM + "                              ").Substring(0, 30) + "                       " + BCASH.Text + "\n");  // 3 ARISTERA PHGE APO ARXIKO
                                    printt(outStream, ("      ΔΟΥ: " + DOY + "                              " + "                            " + fEKPTNUM1.ToString()) + "\n"); //fYPOLPEL

                                    //string dbPath = Path.Combine(
                                    //Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                    //"adodemo.db3");
                                    //SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
                                    //// Open the database connection and create table with data
                                    //connection.Open();
                                    //var contents = connection.CreateCommand();
                                    //contents.CommandText = "SELECT  KODE,ifnull(ONO,'') AS PER,POSO,TIMH,EKPT,ID from EGGTIM where ATIM ='" + ATIM.Text + "' order by ID DESC ; ";                                                                                                                                                                          // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
                                    //var r = contents.ExecuteReader();
                                    //Console.WriteLine("Reading data");


                                    printt(outStream, "\n");
                                    printt(outStream, "\n");

                                    // εαν ειναι συγκεντρωτικό να τυπωνει και επικεφαλιδα αλλοιώς κενο
                                    if (IsSygkEpistr == 1)
                                    {
                                        string lin = ("ΚΩΔΙΚΟΣ          ").Substring(0, 10) + " " + ("ΠΕΡΙΓΡΑΦΗ" + spac40).Substring(0, 35) + "TEM ";
                                        lin = lin + "ΦΟΡΤΩΣΗ";
                                        lin = lin + "ΠΩΛΗΣΗ";
                                        lin = lin + "" + "  " + "    " + "ΥΠΟΛΟΙΠΟ ";
                                        printt(outStream, lin + "\n");
                                    }
                                    else
                                    {
                                        printt(outStream, "\n");
                                    }

                                    int seir = 13;
                                    // Single ssum = 0;
                                    // String.Format("{0:0.0#}", 123.4567)       // "123.46"


                                    int nSeiresSelidas = 0;
                                    if (nCurPage == nPages)
                                    {
                                        nSeiresSelidas = RecPerPage;
                                    }
                                    else
                                    {
                                        nSeiresSelidas = RecPerPage;
                                    }

                                    for (int k = 1; k <= nSeiresSelidas; k++)
                                    {

                                        nCurRow++;
                                        seir++;
                                        printt(outStream, cLine[nCurRow] + "\n");



                                    }



                                    for (int kk = seir; kk < 40; kk++)
                                    {
                                        printt(outStream, "\n");

                                    }
                                    //  fkauajiPro = s;
                                    //   s = s - (s * fEKPTNUM1) / 100;
                                    //  fkauaji = s;
                                    if (nCurPage == nPages)


                                    {

                                        float NEO;
                                        if (BCASH.Text.Substring(0, 1) == "Μ")
                                        {
                                            NEO = (float)fYPOLPEL;
                                        }
                                        else
                                        {
                                            NEO = (float)fYPOLPEL + (float)fkauaji * 113 / 100;
                                        }
                                        string cold, cneo;
                                        cold = String.Format("{0:0.00}", fYPOLPEL);
                                        cneo = String.Format("{0:0.00}", NEO);

                                        printt(outStream, spac40 + "            ΣΥΝ.ΑΞΙΑ      " + Right("     " + String.Format("{0:0.00}", fkauajiPro), 7) + "\n");
                                        printt(outStream, spac40 + "            ΣΥΝ.ΕΚΠΤΩΣΗ   " + Right("     " + String.Format("{0:0.00}", fkauajiPro - fkauaji), 7) + "\n");
                                        printt(outStream, Right("               " + cold, 15) + spac40 + "           " + Right("     " + String.Format("{0:0.00}", fkauaji), 7) + "\n");
                                        printt(outStream, Right("               " + cneo, 15) + spac40 + "           " + Right("     " + String.Format("{0:0.00}", fkauaji * 0.13), 7) + "\n");
                                        printt(outStream, "\n");
                                        printt(outStream, spac40 + "                          " + Right("     " + String.Format("{0:0.00}", fkauaji * 1.13), 7) + "\n");
                                    }
                                    else
                                    {

                                        for (int kk = 1; kk <= 5; kk++)
                                        {
                                            printt(outStream, "\n");
                                        }
                                        printt(outStream, "\n");

                                    }
                                    if (nCurPage > 1)
                                    {
                                        printt(outStream, "ΣΕΛ." + nCurPage.ToString() + "\n");
                                    }
                                    else
                                    {
                                        if (nPages > 1)
                                        {
                                            printt(outStream, "ΣΕΛ." + nCurPage.ToString() + "\n");
                                        }
                                        else
                                        {
                                            printt(outStream, "\n");
                                        }


                                    }


                                    printt(outStream, "\n");
                                    printt(outStream, "\n");
                                    for (int kk = 0; kk < 17; kk++)
                                    {
                                        printt(outStream, "\n");

                                    }
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
                            catch (SqliteException ex)
                            {

                                await DisplayAlert("αδυναμια εκτυπωσης ", ex.ErrorCode.ToString(), "OK");
                            };


                            //    toBytes = Encoding.Default .GetBytes("αβγδεζηθικλμνξοπρστυφχψωΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ   MY TEXT TO PRINT");
                            //   outStream.Write(toBytes, 0, toBytes.Length);
                            try
                            {

                                socket.Close();
                            }
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

            } catch
            {
                await DisplayAlert("ΑΝΕΠΙΤΥΧΗΣ ΕΚΤΥΠΩΣΗ", " not ok", "OK");

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
            if (EPO.Text.Substring(0,2)=="..")
            {
                await DisplayAlert("δεν διαλεξες πελάτη", " not ok", "OK");
                return;


            }


            var action = await DisplayAlert(BCASH.Text , "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {
            }
            else
            {
                return;
            }




                MainPage.ExecuteSqlite("update PARASTAT SET ARITMISI=ifnull(ARITMISI,0)+1 WHERE   ID=" + nn.ToString());
            string TYP;
            TYP = String.Format("{0:0.00}", fYPOLPEL);
            TYP = TYP.Replace(",", ".");
            MainPage.ExecuteSqlite("UPDATE TIM SET KPE='"+AFM.Text+"',TRP='"+ BCASH.Text.Substring(0, 5)+"',EPO='"+EPO.Text +"', NUM1 =0,TYP="+TYP+",AJI="+SAJIA.Text.Replace(",",".") +" WHERE ATIM='" + ATIM.Text +"'");

            // EAN EINAI PISTVSH NA ANEBEI TO YPOLOIPO
            if (BCASH.Text.Substring(0, 1) == "Μ")
            {

            }else
            {            
                MainPage.ExecuteSqlite("UPDATE PEL SET TYP=TYP+" + SAJIA.Text.Replace(",", ".") + " WHERE KOD='" + AFM.Text + "'");
             }

            btnScan.IsEnabled = true;
            AFM.IsEnabled = false;
            BRESafm.IsEnabled = false;


          //  await Navigation.PopAsync();


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

        private void SYGKEPIS(object sender, EventArgs e)
        {



            string MATIM = "";
            // ΒΡΙΣΚΩ ΤΟ ΠΡΟΓΟΥΜΕΝΟ  ΣΥΓΚΕΝΤΡΩΤΙΚΟ ΓΙΑ ΝΑ ΤΟ ΑΝΤΙΓΡΑΨΩ
            MATIM  = PARAGGELIES.ReadSQL("select  printf('%06d',  IFNULL(ARITMISI,0))    FROM PARASTAT WHERE  ID=" + nn.ToString());
            EIDOSPAR = PARAGGELIES.ReadSQL("select ifnull(EIDOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());
            MATIM = EIDOSPAR + Right("000000" + MATIM, 6);



            // σβηνω τυχον απομειναρια Συγκεντρωτικου επιστροφης απο λαθος δημιουργια 
            string SQL2 = "delete FROM EGGTIM WHERE ATIM='" + ATIM.Text + "'";
            MainPage.ExecuteSqlite(SQL2);
            string SQL = "insert into EGGTIM (ATIM,ONO,KODE,POSO) ";
            SQL = SQL + "SELECT '" + ATIM.Text + "' AS MATIM, ONO,KODE,sum(POSO) AS SPOSO FROM EGGTIM WHERE ATIM like 'τ%'  GROUP BY ONO,KODE,MATIM "; //='"+MATIM+"'";


           // SQL = SQL + "SELECT ONO,'" + ATIM.Text + "',HME,KODE,POSO,TIMH,EKPT,FPA FROM EGGTIM WHERE ATIM like 'τ%'"; //='"+MATIM+"'";
            MainPage.ExecuteSqlite(SQL);

            //αν τυχον εβγαλε 2 συγκεντρωτικα ενα κανονικο+1 συπληρωματικο και το 2ο εχει το ίδιο είδος
            // τοτε θα εμφανιζει 2 φορες το ιδιο ειδος

            MainPage.ExecuteSqlite("UPDATE EGGTIM SET HME= datetime('now'),TIMH=0,EKPT=0,FPA=13 WHERE ATIM='" + ATIM.Text + "'");


            MainPage.ExecuteSqlite("UPDATE EGGTIM SET TIMH=(SELECT IFNULL(DESM,0)  FROM EID WHERE KOD=EGGTIM.KODE) WHERE ATIM='" + ATIM.Text + "'");
          //  MainPage.ExecuteSqlite("UPDATE EGGTIM SET AXIA=POSO-TIMH  WHERE ATIM='" + ATIM.Text + "'");


            PRINTOUT(1);


            MainPage.ExecuteSqlite("update PARASTAT SET ARITMISI=ifnull(ARITMISI,0)+1 WHERE   ID=" + nn.ToString());

            MainPage.ExecuteSqlite("INSERT INTO TIM (NUM1,HME,ATIM,KPE,TRP,EPO) VALUES (-1,datetime('now'),'" + ATIM.Text + "','" + AFM.Text + "','" + BCASH.Text.Substring(0, 5) + "','" + EPO.Text + "')");
            //  }

            // CLOSE INVOICE
           // MainPage.ExecuteSqlite("update PARASTAT SET ARITMISI=ifnull(ARITMISI,0)+1 WHERE   ID=" + nn.ToString());
          //  string TYP;
          //  TYP = String.Format("{0:0.00}", fYPOLPEL);
         //   TYP = TYP.Replace(",", ".");
           // MainPage.ExecuteSqlite("UPDATE TIM SET KPE='" + AFM.Text + "',TRP='" + BCASH.Text.Substring(0, 5) + "',EPO='" + EPO.Text + "', NUM1 =0,TYP=" + TYP + ",AJI=" + SAJIA.Text.Replace(",", ".") + " WHERE ATIM='" + ATIM.Text + "'");

            // EAN EINAI PISTVSH NA ANEBEI TO YPOLOIPO
      //      if (BCASH.Text.Substring(0, 1) == "Μ")
     //       {

      //      }
       //     else
       //     {
          //      MainPage.ExecuteSqlite("UPDATE PEL SET TYP=TYP+" + SAJIA.Text.Replace(",", ".") + " WHERE KOD='" + AFM.Text + "'");
         //   }

            btnScan.IsEnabled = true;
            AFM.IsEnabled = false;
            BRESafm.IsEnabled = false;
            CloseInvoice.IsEnabled = false;
            












        }

        private  async void EXODOS(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void AKYR(object sender, EventArgs e)
        {

            

           var action = await DisplayAlert(ATIM.Text  + " Να διαγραφεί?", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {
                
              //  connection.Close();
              string[] POSO = new string[400];
              string[] KODE = new string[400];

              int ll=0;
              readtable(ref POSO, ref KODE,ref ll);

                if (ll == 0)
                {
                    await DisplayAlert(ATIM.Text + " ΔΕΝ ΔΙΑΓΡΑΦΗΚΕ.ΣΒΗΣΤΕ ΑΠΟ ΤΗΝ ΛΙΣΤΑ?", "", "Ναι", "Οχι");
                    return;
                }


                for (int  k = 1; k <= ll; k++)

                {



                    // ΚΑΝΩ ΑΡΝΗΤΙΚΗ ΕΝΗΜΕΡΩΣΗ ΓΙΑ ΑΥΤΟ ΠΟΥ ΣΒΗΝΩ
                   string dbPath = Path.Combine(
                      Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                      "adodemo.db3");
                    SqliteConnection connection2 = new SqliteConnection("Data Source=" + dbPath);
                    // Open the database connection and create table with data
                    connection2.Open();







                    if (ATIM.Text.Substring(0, 1) == "τ")
                    {

                        //var c = connection2.CreateCommand();
                        //c.CommandText = ;

                        //var rowcount = c.ExecuteNonQuery();


                        //c = connection2.CreateCommand();
                        //c.CommandText = ;
                        // rowcount = c.ExecuteNonQuery();


                         MainPage.ExecuteSqlite("update EID SET YPOL=YPOL-" + POSO[k] + " WHERE KOD='" + KODE[k] + "'");
                        MainPage.ExecuteSqlite("update EID SET YPOL = 0 WHERE YPOL<0 AND KOD = '" + KODE[k] + "'");
                    }
                    else
                    {
                       //var c = connection2.CreateCommand();
                       // c.CommandText = "update EID SET DESM = DESM - " + POSO[k] + " WHERE KOD = '" + KODE[k] + "'";
                       // var rowcount = c.ExecuteNonQuery();
  
                       //  c = connection2.CreateCommand();
                       // c.CommandText = "update EID SET DESM=0 WHERE DESM<0 AND  KOD='" + KODE[k] + "'";
                       // rowcount = c.ExecuteNonQuery();




                          MainPage.ExecuteSqlite("update EID SET DESM=DESM-" + POSO[k] + " WHERE KOD='" + KODE[k] + "'");
                        MainPage.ExecuteSqlite("update EID SET DESM=0 WHERE DESM<0 AND  KOD='" + KODE[k] + "'");
                    }


                   // connection2.Close;



                }


              //  var c2 = connection.CreateCommand();
              //  c2.CommandText = "delete from EGGTIM where ATIM = '" + ATIM.Text  + "'";
               // var rowcount2 = c2.ExecuteNonQuery();

                MainPage.ExecuteSqlite("delete from EGGTIM where ATIM = '" + ATIM.Text + "'");

              //  c2 = connection.CreateCommand();
              //  c2.CommandText = "delete from    TIM where ATIM='" + ATIM.Text + "'";
              //  rowcount2 = c2.ExecuteNonQuery();
                MainPage.ExecuteSqlite("delete from TIM where ATIM = '" + ATIM.Text + "'");

              //   connection.Close();
                await DisplayAlert("διαγραφτηκε", "", "OK");

                await Navigation.PopAsync();
                // Show_list();


               

            }

        }


        private void readtable(ref string[]  POSO,ref string[]  KODE,ref int ll ) 
        {

            // ΚΑΝΩ ΑΡΝΗΤΙΚΗ ΕΝΗΜΕΡΩΣΗ ΓΙΑ ΑΥΤΟ ΠΟΥ ΣΒΗΝΩ
            string dbPath = Path.Combine(
              Environment.GetFolderPath(Environment.SpecialFolder.Personal),
              "adodemo.db3");
            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            // Open the database connection and create table with data
            connection.Open();
            // query the database to prove data was inserted!
            var contents = connection.CreateCommand();
            contents.CommandText = "SELECT * from EGGTIM where ATIM='" + ATIM.Text + "'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");

           // string[] POSO = new string[400];
           // string[] KODE = new string[400];

             ll = 0;
            string cc = "";
            while (r.Read())
            {
                ll++;
                cc = r["POSO"].ToString();
                cc = cc.Replace(",", ".");
                string ck = r["KODE"].ToString();
                POSO[ll] = cc;
                KODE[ll] = ck;
            }


           // connection.Close;


           
        }

        private void CHANGE_EKPT(object sender, EventArgs e)
        {

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