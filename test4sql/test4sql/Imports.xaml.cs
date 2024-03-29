﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using PCLStorage;
using SharpCifs.Smb;  // http://sharpcifsstd.dobes.jp/
using System.IO;
using Plugin.Toast;
using System.Threading;
using Xamarin.Forms.PlatformConfiguration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Input;
using Xamarin.Essentials;
using Mono.Data.Sqlite;



using System.Linq.Expressions;
using oncar;
using static Android.Provider.Telephony.Mms;
using Java.Nio.Channels;
using static Java.Text.Normalizer;
using System.Text.RegularExpressions;
using static Android.Provider.Telephony.Sms;
using SharpCifs.Util;
using System.Xml;
using Java.Net;
using Java.Util.Concurrent;
using System;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using static Android.Provider.ContactsContract.CommonDataKinds;
using System.Xml.Linq;
using System.Web;

namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Repository
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("html_url")]
        public Uri GitHubHomeUrl { get; set; }

        [JsonProperty("homepage")]
        public Uri Homepage { get; set; }

        [JsonProperty("watchers")]
        public int Watchers { get; set; }
    }

    public static class Constants
    {
        public const string GitHubReposEndpoint = "https://api.github.com/orgs/dotnet/repos";
    }

    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();

            if (Device.RuntimePlatform == Device.UWP)
            {
                _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            }
        }

        public async Task<List<Repository>> GetRepositoriesAsync(string uri)
        {
            List<Repository> repositories = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    repositories = JsonConvert.DeserializeObject<List<Repository>>(content);
                }
            }
            catch (Exception ex)
            {
               // Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return repositories;
        }
    }

    public partial class Page2 : ContentPage
    {

        public SqlConnection con;
        

        public IList<Monkey> Monkeys { get; private set; }
        public Page2()
        {
            InitializeComponent();
           // LEIDHPEL.IsVisible = false;
            
        }









        async void MIDAPOG(object sender, EventArgs e)
        {


             MainPage.ExecuteSqlite("delete FROM EGGTIM");

            await DisplayAlert("ΔΙΑΓΡΑΦΗΚΕ ΟΚ", "ΔΙΑΓΡΑΦΗΚΕ Η ΑΠΟΓΡΑΦΗ", "OK");
            


        }


        async void CreateTables(object sender, EventArgs e)
        {

            string c = "CREATE TABLE IF NOT EXISTS EID( ID  INTEGER PRIMARY KEY,KOD [nvarchar](25)," +
                    "[ONO] [nvarchar](255) ," +
                     "[ENAL] [nvarchar](25) ," +
                      "[YPOL] [real] ," +
                       "[XONDR] [real] ," +
                        
                         "[DESM] [real] ," +
                         "[ANAM] [real] ," +
                       "[BARCODE] [nvarchar](15)  )  " ;

             MainPage.ExecuteSqlite(c);

            await DisplayAlert("ΕΙΔΗ ΟΚ", "ΕΙΔΗ ΔΗΜΙΟΥΡΓΗΘΗΚΑΝ", "OK");


            // αν δεν υπαρχει το πεδιο "MON" ΠΡΟΣΘΕΣΕ ΤΟ
            string nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('EID') WHERE name='MON' ");
            if (Int16.Parse(nc) == 0)
            {
                
                MainPage.ExecuteSqlite("alter table EID ADD MON [varchar](5)");
                MainPage.ExecuteSqlite("alter table EID ADD FPA INT");

            }

            // αν δεν υπαρχει το πεδιο "MON" ΠΡΟΣΘΕΣΕ ΤΟ
            nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('EID') WHERE name='XTI' ");
            if (Int16.Parse(nc) == 0)
            {

                MainPage.ExecuteSqlite("alter table EID ADD XTI [real] ");
               

            }




            // Κωδικός;Α.Φ.Μ.;Επωνυμία;Διεύθυνση;Πόλη;Τηλ.1

            c = "CREATE TABLE IF NOT EXISTS PEL( ID  INTEGER PRIMARY KEY,KOD [nvarchar](25)," +
                    "[EPO] [nvarchar](255) ," +
                     "[DIE] [nvarchar](35) ," +
                       "[POL] [nvarchar](35) ," +
                         "[THL] [nvarchar](35) ," +                    
                       "[AFM] [nvarchar](15) )";


             MainPage.ExecuteSqlite(c);











            // αν δεν υπαρχει το πεδιο "TYP" ΠΡΟΣΘΕΣΕ ΤΟ
             nc=PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('PEL') WHERE name='TYP' ");
            if (Int16.Parse(nc) == 0)
            {
                MainPage.ExecuteSqlite("alter table PEL ADD TYP REAL");
                MainPage.ExecuteSqlite("alter table PEL ADD DOY [varchar](20)");
                MainPage.ExecuteSqlite("alter table PEL ADD PEK [INT] ");
                MainPage.ExecuteSqlite("alter table PEL ADD MEMO  [varchar](250)");

            }

            // αν δεν υπαρχει το πεδιο "EPA" ΠΡΟΣΘΕΣΕ ΤΟ
             nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('PEL') WHERE name='EPA' ");
            if (Int16.Parse(nc) == 0)
            {
                MainPage.ExecuteSqlite("alter table PEL ADD R1 REAL");
                MainPage.ExecuteSqlite("alter table PEL ADD EPA [varchar](20)");
                MainPage.ExecuteSqlite("alter table PEL ADD CH1 [varchar](20)");
                MainPage.ExecuteSqlite("alter table PEL ADD INT1 [INT] ");

            }


            // αν δεν υπαρχει το πεδιο "EPA" ΠΡΟΣΘΕΣΕ ΤΟ
            nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('PEL') WHERE name='NUM1' ");
            if (Int16.Parse(nc) == 0)
            {
                MainPage.ExecuteSqlite("alter table PEL ADD NUM1 REAL");              
                MainPage.ExecuteSqlite("alter table PEL ADD CH2 [varchar](20)");
                MainPage.ExecuteSqlite("alter table PEL ADD INT2 [INT] ");

            }



            // αν δεν υπαρχει το πεδιο "EPA" ΠΡΟΣΘΕΣΕ ΤΟ
            nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('PEL') WHERE name='PEK' ");
            if (Int16.Parse(nc) == 0)
            {
                MainPage.ExecuteSqlite("alter table PEL ADD PEK REAL");
            }


            // αν δεν υπαρχει το πεδιο "EPA" ΠΡΟΣΘΕΣΕ ΤΟ
            nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('PEL') WHERE name='CH3' ");
            if (Int16.Parse(nc) == 0)
            {
                
                MainPage.ExecuteSqlite("alter table PEL ADD CH3 [varchar](40)");
             
            }


            // αν δεν υπαρχει το πεδιο "EPA" ΠΡΟΣΘΕΣΕ ΤΟ
            nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('PEL') WHERE name='DOY' ");
            if (Int16.Parse(nc) == 0)
            {
                MainPage.ExecuteSqlite("alter table PEL ADD DOY [varchar](40)");
            }



            // αν δεν υπαρχει το πεδιο "EPA" ΠΡΟΣΘΕΣΕ ΤΟ
            nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('PEL') WHERE name='EMAIL' ");
            if (Int16.Parse(nc) == 0)
            {
                MainPage.ExecuteSqlite("alter table PEL ADD EMAIL [varchar](40)");
                MainPage.ExecuteSqlite("alter table PEL ADD EMAIL2 [varchar](40)");
                MainPage.ExecuteSqlite("alter table PEL ADD TK [varchar](5)");
                MainPage.ExecuteSqlite("alter table PEL ADD KINHTO [varchar](40)");
            }
























            await DisplayAlert("ΠΕΛΑΤΕΣ ΟΚ", "ΠΕΛΑΤΕΣ ΔΗΜΙΟΥΡΓΗΘΗΚΑΝ", "OK");

            c = "CREATE TABLE IF NOT EXISTS EGGTIM (" +
                    "[ATIM] [varchar](55)," +
                    "[HME] [datetime] ," +
                    "[IDPARAGG] [int] ," +
                    "[KODE] [nvarchar](55) ," +
                    "[POSO] [real] ," +
                    "[TIMH] [real] ," +
                    "[ONO] [varchar](55) ," +
                    "[PROSUETA] [varchar](55) ," +
                    "[CH1] [varchar](55) ," +
                    "[CH2] [varchar](55) ," +
                    "[NUM1] [int] ," +
                    "[NUM2] [int] ," +
                    "[ENERGOS] [int] ," +
                    "[ID]  INTEGER PRIMARY KEY )";


   MainPage.ExecuteSqlite(c);





            c = " CREATE TABLE IF NOT EXISTS EIDHPEL (" +
      "[KODPEL][varchar](55) NULL," +
      "[KODE][varchar] (55) NULL," +
      "[POSO][real] NULL," +
      "[AJIA][real] NULL," +
      "[TELTIMH][real] NULL )";
            MainPage.ExecuteSqlite(c);
            await DisplayAlert("ειδη πελατών", "ειδη πελατών ΔΗΜΙΟΥΡΓΗΘΗΚΑΝ", "OK");



            c = "CREATE TABLE IF NOT EXISTS EGG (" +
                   "[ATIM] [varchar](55)," +
                   "[HME] [datetime] ," +                 
                   "[KOD] [nvarchar](55) ," +
                   "[IDPEL] [int] ," +
                   "[XRE] [real] ," +
                   "[PIS] [real] ," +
                   "[AIT] [varchar](55) ," +
                   "[CH1] [varchar](55) ," +
                   "[CH2] [varchar](55) ," +
                   "[NUM1] [int] ," +
                   "[NUM2] [int] ," +
                   "[ID]  INTEGER PRIMARY KEY )";


            MainPage.ExecuteSqlite(c);











            // αν δεν υπαρχει το πεδιο "EPA" ΠΡΟΣΘΕΣΕ ΤΟ
            nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('EGGTIM') WHERE name='FPA' ");
            if (Int16.Parse(nc) == 0)
            {
                MainPage.ExecuteSqlite("alter table EGGTIM ADD FPA INT");
              
            }

         



            c = "CREATE TABLE IF NOT EXISTS TIM (" +
                   "[ATIM] [varchar](55)," +
                   "[HME] [datetime] ," +
                   "[TRP] [varchar](10) ," +
                   "[KPE] [nvarchar](55) ," +
                   "[AJI] [real] ," +
                   "[AJ1] [real] ," +
                   "[AJ2] [real] ," +
                   "[AJ3] [real] ," +
                   "[AJ4] [real] ," +
                   "[AJ5] [real] ," +
                   "[FPA1] [real] ," +
                   "[FPA2] [real] ," +
                   "[FPA3] [real] ," +
                   "[FPA4] [real] ," +
                   "[EPO] [varchar](55) ," +
                   "[PROSUETA] [varchar](55) ," +
                   "[CH1] [varchar](55) ," +
                   "[CH2] [varchar](55) ," +
                   "[NUM1] [int] ," +
                   "[NUM2] [int] ," +
                   "[TYP] [int] ," +
                   "[ID]  INTEGER PRIMARY KEY )";

            MainPage.ExecuteSqlite(c);










            // αν δεν υπαρχει το πεδιο "TYP" ΠΡΟΣΘΕΣΕ ΤΟ
            nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('EGGTIM') WHERE name='EKPT' ");
            if (Int16.Parse(nc) == 0)
            {
                MainPage.ExecuteSqlite("alter table EGGTIM ADD EKPT REAL");
                MainPage.ExecuteSqlite("alter table EGGTIM ADD KAU_AJ REAL");
                MainPage.ExecuteSqlite("alter table EGGTIM ADD MIK_AJ REAL");

            }





            c = "CREATE TABLE IF NOT EXISTS ARITMISI( ID  INTEGER PRIMARY KEY,ARITMISI [int] )";
                

             MainPage.ExecuteSqlite(c);

            MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");
            await DisplayAlert("ΑΡΙΘΜΗΣΗ ΟΚ", " ΔΗΜΙΟΥΡΓΗΘΗΚΑΝ", "OK");

            if (PARAGGELIES.NReadSQL("select count(*) from ARITMISI") < 10)
            {

                MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");
                MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");
                MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");
                MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");
                MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");
                MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");
                MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");
                MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");
                MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");

                // ID=8  PELATES 


            }



            c = "CREATE TABLE IF NOT EXISTS PARALABES( ID  INTEGER PRIMARY KEY,ATIM [nvarchar](35),BARCODE [nvarchar](45) )";


             MainPage.ExecuteSqlite(c);



            c = "CREATE TABLE IF NOT EXISTS BARCODES( ID  INTEGER PRIMARY KEY,KOD [nvarchar](25),BARCODE [nvarchar](15) )";


             MainPage.ExecuteSqlite(c);



            c = "CREATE TABLE IF NOT EXISTS MEM( ID  INTEGER PRIMARY KEY,IP [nvarchar](45)," +
                    "[EPO] [nvarchar](255) ," +
                     "[DIE] [nvarchar](35) ," +
                       "[POL] [nvarchar](35) ," +
                         "[THL] [nvarchar](35) ," +
                       "[AFM] [nvarchar](15) )";
            

            if (PARAGGELIES.NReadSQL("select count(*) from MEM") < 5)
            {
                MainPage.ExecuteSqlite("INSERT INTO MEM (IP) VALUES ('*')");
                MainPage.ExecuteSqlite("INSERT INTO MEM (IP) VALUES ('*')");
                
            }

            await DisplayAlert("MEM ΟΚ", " ΔΗΜΙΟΥΡΓΗΘΗΚΑΝ", "OK");

            MainPage.ExecuteSqlite(c);

            c = "CREATE TABLE IF NOT EXISTS BARDIA( [HME] [datetime]  , " +
                "[IDERGAZ] [int]  , [NUM1] [int]  , [NUM2] [int]  , [CH1] [nvarchar](55)  ," +
                " [CH2] [nvarchar](55)  , [ISOPEN] [int]  , [OPENH] [nvarchar](55)  , " +
                "[CLOSEH] [nvarchar](55)  , [CASHTOT] [int]  , " +
                "[CASH1] [int]  , [CASH2] [int]  , [CASH3] [int]  , [CASH4] [int]  ," +
                " [CASH5] [int]  , [ID]  INTEGER PRIMARY KEY )  ";





            await DisplayAlert("BARDIA", " ΔΗΜΙΟΥΡΓΗΘΗΚΑΝ", "OK");

             MainPage.ExecuteSqlite(c);
            if (PARAGGELIES.NReadSQL("select count(*) from BARDIA") < 1)
            {
                MainPage.ExecuteSqlite("INSERT INTO BARDIA (ISOPEN) VALUES (0)");
         
            }



            c = " CREATE TABLE  IF NOT EXISTS TIMOKAT ( [KOD] [nvarchar](14) NOT NULL,"+
                "[EKPT] [decimal](5, 2) NOT NULL, [TIMOK] [int] NOT NULL,[ONO] [nvarchar](35) NULL,"+
                "[TIMOKPEL] [varchar](5) NULL,[TIMOKEID] [varchar](5) NULL,	[TIMH] [decimal](18, 0) NULL,"+
                "[TIMOKID] [int] NOT NULL) ";
             MainPage.ExecuteSqlite(c);

             c = "CREATE TABLE IF NOT EXISTS PARASTAT( ID  INTEGER PRIMARY KEY,EIDOS [nvarchar](5)," +
                    "[TITLOS] [nvarchar](30) ," +
                     
                      "[ARITMISI] [INT] ," +
                       "[N1] [real] ," +
                         "[N2] [real] ," +
                         "[N3] [real] ," +
                       "[C1] [nvarchar](15) ,[C2] [nvarchar](15) , [C3] [nvarchar](15) )  ";

             MainPage.ExecuteSqlite(c);

            if (PARAGGELIES.NReadSQL("select count(*) from PARASTAT") < 2)
            {
                 MainPage.ExecuteSqlite("INSERT INTO PARASTAT (TITLOS,EIDOS) VALUES ('ΤΙΜΟΛΟΓΙΟ ΠΩΛΗΣΗΣ-Δ.Α.','T')");
                 MainPage.ExecuteSqlite("INSERT INTO PARASTAT (TITLOS,EIDOS) VALUES ('ΔΕΛΤΙΟ ΑΠΟΣΤΟΛΗΣ','A')");
            }

            if (PARAGGELIES.NReadSQL("select count(*) from PARASTAT") < 3)
            {
                MainPage.ExecuteSqlite("INSERT INTO PARASTAT (TITLOS,EIDOS) VALUES ('ΣΥΓΚΕΝΤΡΩΤΙΚΟ ΔΕΛΤΙΟ ΑΠΟΣΤΟΛΗΣ','τ')");
               
            }
            if (PARAGGELIES.NReadSQL("select count(*) from PARASTAT") < 4)
            {
                MainPage.ExecuteSqlite("INSERT INTO PARASTAT (TITLOS,EIDOS) VALUES ('ΠΙΣΤΩΤΙΚΟ ΤΙΜΟΛΟΓΙΟ','P')");
                MainPage.ExecuteSqlite("UPDATE PARASTAT SET TITLOS='ΤΙΜΟΛΟΓΙΟ ΠΩΛΗΣΗΣ-ΔΑ ΣΕΙΡΑ Β',EIDOS='ρ' WHERE ID=2");


            }


            MainPage.ExecuteSqlite("CREATE TABLE IF NOT EXISTS KATHG (ID  INTEGER PRIMARY KEY ," +
                   "[KOD] [int] ," +
                   "[ONO] [nvarchar](55) ," +
                   "[PICTURE] [nvarchar](55) ," +
                   "[CH1] [nvarchar](55) ," +
                   "[CH2] [nvarchar](55)  ); ");


            // Toast.makeText(getApplicationContext(), "2.KATHG ok", Toast.LENGTH_SHORT).show();

            MainPage.ExecuteSqlite("CREATE TABLE IF NOT EXISTS EIDH (ID  INTEGER PRIMARY KEY ," +
                   "[ONO] [nvarchar](55) ," +
                   "[TIMH] [real] ," +
                   "[SYNOLO] [int] ," +
                   "[NUM1] [real] ," +
                   "[NUM2] [real] ," +
                   "[CH1] [nvarchar](55), " +
                   "[CH2] [nvarchar](55)" +
                   " );");

            // αν δεν υπαρχει το πεδιο "EPA" ΠΡΟΣΘΕΣΕ ΤΟ
            nc = PARAGGELIES.ReadSQL("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('EIDH') WHERE name='KATHG' ");
            if (Int16.Parse(nc) == 0)
            {
                MainPage.ExecuteSqlite("alter table EIDH ADD KATHG INT");

            }





            MainPage.ExecuteSqlite("CREATE TABLE IF NOT EXISTS TABLES (ID  INTEGER PRIMARY KEY ," +
                    "[ONO] [nvarchar](55) ," +
                    "[KATEILHMENO] [int] ," +
                    "[SYNOLO] [int] ," +
                    "[NUM1] [int] ," +
                    "[NUM2] [int] ," +
                    "[CH1] [nvarchar](55) ," +
                    "[CH2] [nvarchar](55)," +
                    "[IDPARAGG] [int] );");


            // Toast.makeText(getApplicationContext(), "3.TABLES ok", Toast.LENGTH_SHORT).show();



            MainPage.ExecuteSqlite("CREATE TABLE IF NOT EXISTS PARAGG (" +
                    "[TRAPEZI] [varchar](55)," +
                    "[HME] [datetime] ," +
                    "[IDPARAGG] [int] ," +
                    "[KOD] [nvarchar](55) ," +
                    "[POSO] [real] ," +
                    "[TIMH] [real] ," +
                    "[ONO] [varchar](55) ," +
                    "[PROSUETA] [varchar](55) ," +
                    "[CH1] [varchar](55) ," +
                    "[CH2] [varchar](55) ," +
                    "[NUM1] [int] ," +
                    "[NUM2] [int] ," +
                    "[ENERGOS] [int] ," +
                    "[KERASMENOAPO] [varchar](55) ," +
                    "[KERASMENOSE] [varchar](55) ," +
                    "[ID]  INTEGER PRIMARY KEY )");



            MainPage.ExecuteSqlite("CREATE TABLE IF NOT EXISTS   PARAGGMASTER ("+
     "[TRAPEZI][nvarchar](55) ,"+
	"[IDERGAZ][int] ,"+
	"[HME][datetime] ,"+
	"[IDBARDIA][int] ,"+
	"[AJIA][real] ,"+
	"[TROPOS][int] ,"+
	"[NUM1][int] ,"+
	"[NUM2][int] ,"+
	"[CH1][nvarchar] (255) ,"+
	"[CH2][nvarchar] (255) ,"+
	"[TABLETN][int] ,"+
	"[IDPARAGG][int] ,"+
	"[CASH][real] ,"+
	"[PIS1][real] ,"+
	"[PIS2][real] ,"+
	"[KERA][real] ,"+
    "[ID] INTEGER PRIMARY KEY )");


























        }

        async void ImportPEL(object sender, EventArgs e)
        {

            await DisplayAlert("Εναρξη μεταφοράς πελατών 3-5λεπτά", "Πελάτες  ", "OK");
            BindingContext = null;
            Monkeys = new List<Monkey>();
            // await Navigation.PushAsync(new param1());

            // διαβαζω το αρχείο των ειδων  
            // διαβαζω το αρχείο των ειδων
            //Get the SmbFile specifying the file name to be created.
            CrossToastPopUp.Current.ShowToastMessage("loading...");

           // var file = new SmbFile("smb://DESKTOP-MPGU8SB/backpel/PEL.txt");
            var file = new SmbFile("smb://"+Globals.cIP +"/PEL.txt");
           
            
            //Get target's SmbFile.
            // var file = new SmbFile("smb://UserName:Password@ServerIP/ShareName/Folder/FileName.txt");
            // ΧΩΡΙς ΚΩΔΙΚΟ :   var file = new SmbFile("smb://192.168.1.4/backpel/EID.txt");
            try
            {

                LEIDHPEL .IsVisible = true;

                int n22 = MainPage.ExecuteSqlite("delete from PEL;");


                //Get readable stream.
                var readStream = file.GetInputStream();
                //Create reading buffer.
                var memStream = new MemoryStream();
                //Get bytes.
                ((Stream)readStream).CopyTo(memStream);
                //Dispose readable stream.
                readStream.Dispose();



                //  Console.WriteLine(Encoding.UTF8.GetString(memStream.ToArray()));
                byte[] bytes = memStream.ToArray();
                //   await DisplayAlert("Error", Encoding.UTF8.GetString(bytes), "OK");
                String g = Encoding.UTF8.GetString(memStream.ToArray());
                string[] lines = g.Split('\n');
                IMPORTEID.Text = lines[1] + "=" + lines[2] + "=" + lines[3];
                //  await DisplayAlert("Error", Encoding.UTF8.GetString(memStream.ToArray()), "OK");
                int n2;// = MainPage.ExecuteSqlite("dd");
                for (int n = 1; n < lines.Length - 1; n++)
                {
                    string[] lines2 = lines[n].Split(';');
                    string cc = lines2[2];
                    cc = cc.Replace("'", "`");

                    if (n < 1000)
                    {
                        Monkeys.Add(new Monkey
                        {
                            Name = lines2[0],

                            Location = cc,
                            ImageUrl = lines2[1],
                            idPEL = ""
                        });
                    }



                    n2 = MainPage.ExecuteSqlite("insert into PEL (KOD,EPO,AFM) VALUES ('" + lines2[0] + "','" + cc + "','"+lines2[1]+"');");
                    IMPORTEID.Text = lines[n];


                }
                BindingContext = this;
                await DisplayAlert("τελος μεταφοράς", "ΠΕΛΑΤΕΣ που περάστηκαν " + (lines.Length - 1).ToString(), "OK"); ;





            }
            catch
            {
                BindingContext = this;
                await DisplayAlert("ΔΕΝ ΔΙΑΒΑΖΕΙ ΤΟ ΑΡΧΕΙΟ EID.txt", "....", "OK");
                return;
            }
        }



        // SQLQUERYF
        async void SQLQUERYF(object sender, EventArgs e)
        {
            DOSQLQUERY(4);
        }

    async void DOSQLQUERY(int num1 )

        {
           // RESULTS.Text=PARAGGELIES.ReadSQL(QUERY.Text );
           try
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
            contents.CommandText = QUERY.Text;
                                                                                                                                                           // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            
            while (r.Read())
            {
                    if (num1 == 1)
                    {
                       await DisplayAlert(r[0].ToString(),"ok","ok");

                        break;

                    }
                    else
                    {

                        Monkeys.Add(new Monkey
                        {
                            Name = r[0].ToString(),
                            Location = r[1].ToString(),
                            ImageUrl = r[2].ToString(),
                            idPEL = r[3].ToString()
                        });
                    }
            }

            listview.ItemsSource = Monkeys;
            BindingContext = this;
            
            connection.Close();

           

            BindingContext = this;

            }
            catch
            {
                BindingContext = this;
                await DisplayAlert("λαθος", "....", "OK");
                return;
            }

















        }

        async void SQLEXECUTEF(object sender, EventArgs e)
        {
            //RESULTS.Text = PARAGGELIES.ReadSQL(QUERY.Text);
            MainPage.ExecuteSqlite(QUERY.Text);

        }

        async void test(object sender, EventArgs e)
        {

          // ασυγχρονη εκτελεση  (δεν τρεχει με την πρωτη και ασπριζει η οθονη μετα το πέρας της ενημερωσης




            await DisplayAlert("Εναρξη μεταφοράς ειδών ", "Είδη  ", "OK");
            CrossToastPopUp.Current.ShowToastMessage("loading ειδη");
           // Imp_EIDH();
            IMPORTEID.Text = "*** ΠΑΡΑΚΑΛΩ ΠΕΡΙΜΕΝΕΤΕ 133λεπτά*****";
            await DisplayAlert("2h-Εναρξη μεταφοράς ειδών", "Είδη  ", "OK");
         //   Imp_EIDH();
            await DisplayAlert("Telos--Εναρξη μεταφοράς ειδών", "Είδη  " , "OK");











        }

        async void LImportEID(object sender, EventArgs e)
        {
           

            string path = "/storage/emulated/0/lagakis2";  // Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments  );  // "/data/lagakis2";  // 


            string filename = Path.Combine(path, "lagakis.txt");

            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(DateTime.UtcNow);
            }

            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(content);
            }



            /*

            String filename2 ="eidh.txt";
            IFolder folder = FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename2, CreationCollisionOption.ReplaceExisting);

          //  string fileName = "eidh2.txt";
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
            string text = "1234567890  αβγδεζη thue";
            File.WriteAllText(fileName, text);


            bool doesExist = File.Exists(fileName);
            if (doesExist)
            {
                string text2 = File.ReadAllText(fileName);
            }
            else
            {
               await DisplayAlert("ΔΕΝ υπαρχει ΤΟ ΑΡΧΕΙΟ EID.txt", "....", "OK");
            }
              */



        }

         async  void ImportEID(object sender, EventArgs e)
        {

           await DisplayAlert("Εναρξη μεταφοράς ειδών 3-5λεπτά", "Είδη  ", "OK");
         //   CrossToastPopUp.Current.ShowToastMessage("loading ειδη");
          //  int count = Imp_EIDH();
         //   await DisplayAlert("ok ", "Είδη  "+count.ToString(), "OK");



            /*  Task<int> task = new Task<int>(Imp_EIDH);
              IMPORTEID.Text = "*** ΠΑΡΑΚΑΛΩ ΠΕΡΙΜΕΝΕΤΕ 3λεπτά*****";
              task.Start();
              int count = await task;

              IMPORTEID.Text = count.ToString();
              await DisplayAlert("--Εναρξη μεταφοράς ειδών", "Είδη που περάσ "+count.ToString (), "OK");
            */

        //  }


         //  int  Imp_EIDH() {

            BindingContext = null;
            Monkeys = new List<Monkey>();
            IMPORTEID.Text = "*** ΠΑΡΑΚΑΛΩ ΠΕΡΙΜΕΝΕΤΕ 3λεπτά*****";
            // Task.Delay(1000).Wait();
            Thread.Sleep(500);
            int n = 0;
           
            var file = new SmbFile("smb://"+Globals.cIP +"/EID.txt");
            //Get target's SmbFile.
            // var file = new SmbFile("smb://UserName:Password@ServerIP/ShareName/Folder/FileName.txt");
            // ΧΩΡΙς ΚΩΔΙΚΟ :   var file = new SmbFile("smb://192.168.1.4/backpel/EID.txt");
            try
            {

               int n22 = MainPage.ExecuteSqlite("delete from EID;");
                Thread.Sleep(1500);
                // Κωδικός0;       Περιγραφή1;                        ΕΝΑΛ.ΚΩΔΙΚΟΣ2; Χονδρικής3; Υπολ.1-4; Δεσμευμένα5; Αναμενόμενα6; Barcode 7   
                //  12 - 01 - 30; ΛΑΜΠΑΔΑ ΚΕΡΙΝΗ KOKKINΗ ΚΑΛΛΑΣ/ ΤΡΙΑΝ; 12 - 01 - 30; 2,7000; ; 3; ; 5200001201306
                //  12 - 01 - 31; ΛΑΜΠΑΔΑ ΚΕΡΙΝΗ ΚΙΤΡΙΝΗ ΚΑΛΛΑΣ/ ΤΡΙΑΝ; 12 - 01 - 31; 2,7000; ; 3; ; 5200001201313

                /*   string c = "CREATE TABLE IF NOT EXISTS EID( ID  INTEGER PRIMARY KEY,KOD [nvarchar](25)," +
                      "[ONO] [nvarchar](255) ," +
                       "[ENAL] [nvarchar](25) ," +
                        "[YPOL] [real] ," +
                         "[XONDR] [real] ," +
                           "[DESM] [real] ," +
                           "[ANAM] [real] ," +
                         "[BARCODE] [nvarchar](15)  )  ";   */






                //Get readable stream.
                var readStream = file.GetInputStream();
                //Create reading buffer.
                var memStream = new MemoryStream();
                //Get bytes.
                ((Stream)readStream).CopyTo(memStream);
                //Dispose readable stream.
                readStream.Dispose();
              //  Console.WriteLine(Encoding.UTF8.GetString(memStream.ToArray()));
                byte[] bytes = memStream.ToArray();
             //   await DisplayAlert("Error", Encoding.UTF8.GetString(bytes), "OK");
                String g = Encoding.UTF8.GetString(memStream.ToArray());
                string[] lines = g.Split('\n');
               // IMPORTEID.Text=  lines[1] + "=" + lines[2] + "=" + lines[3];


                //  await DisplayAlert("Error", Encoding.UTF8.GetString(memStream.ToArray()), "OK");
                int n2;// = MainPage.ExecuteSqlite("dd");

                

               // ξεκιναει απο 1 για να αποφυγει την επικεφαλιδα

                for (n=1;n<lines.Length-1;n++ )



                {


                    try
                    { 


                    string[] lines2 = lines[n].Split(';');
                    string cc = lines2[1];
                    cc = cc.Replace("'", "`");

                    string enal = lines2[2];
                    if (enal.Length == 0) { enal = "0"; };
                    enal = enal.Replace("'", "`");

                    string ypol = lines2[4];
                    if (ypol.Length == 0) { ypol = "0"; };
                    ypol = ypol.Replace(",", ".");

                    string xondr = lines2[3];
                    if (xondr.Length == 0) { xondr = "0"; };
                    xondr = xondr.Replace(",", ".");

                    string desm = lines2[5];
                    if (desm.Length == 0) { desm = "0"; };
                    desm = desm.Replace(",", ".");

                    string anam = lines2[6];
                    if (anam.Length == 0) { anam = "0"; };
                    anam = anam.Replace(",", ".");

                    if (n < 1000)
                    {
                        Monkeys.Add(new Monkey
                        {
                            Name = lines2[0],

                            Location = cc,
                            ImageUrl = ypol,
                            idPEL = xondr
                        });
                    }



                 





                    // Κωδικός0;Περιγραφή1;ΕΝΑΛ.ΚΩΔΙΚΟΣ2;Χονδρικής3;Υπολ.1-4; Δεσμευμένα5; Αναμενόμενα6; Barcode 7   
                    n2 = MainPage.ExecuteSqlite("insert into EID (KOD,ONO,BARCODE) VALUES ('"+lines2[0]+ "','" + lines2[1] + "','"+lines2[0]+"');");
                    //    IMPORTEID.Text = lines[n] ;
                    }
                    catch
                    {
                       // BindingContext = this;
                        //  await DisplayAlert("ΔΕΝ ΔΙΑΒΑΖΕΙ ΤΟ ΑΡΧΕΙΟ EID.txt", "....", "OK");
                       // return n;
                    }

                    // do something every 60 seconds
                    // IMPORTEID.Text = lines[n];
                    //  IMPORTEID.Text = lines[n];
                    //  Device.BeginInvokeOnMainThread(() =>
                    //  {
                    // interact with UI elements
                    // IMPORTEID.Text = lines[n];
                    //     IMPORTEID.Text = lines[n];
                    //  });
                    //   return true; // runs again, or false to stop



                    if (n == 1000)
                    {
                    //    int c1 = 0;
                       // PROG.ProgressColor = Color.Blue ;
                       // await PROG.ProgressTo(1, 255500, Easing.Linear);
                    }


                      




                }  // for 


                Thread.Sleep(3500);
              //  BindingContext = this;

                //  return true; // runs again, or false to stop
                // });

                 await DisplayAlert("τελος μεταφοράς","Είδη που περάστηκαν "+ (lines.Length - 1).ToString(), "OK"); ;

            //    return lines.Length;




            }
            catch
            {
              //  BindingContext = this;
                //  await DisplayAlert("ΔΕΝ ΔΙΑΒΑΖΕΙ ΤΟ ΑΡΧΕΙΟ EID.txt", "....", "OK");
              //  return n;
            }



            // το αποθηκευω στο mysql
            //  int n = MainPage.ExecuteSqlite("dd");




        }

        private async void SUBIMPBAR(object sender, EventArgs e)
        {
            //  -----------------SQLSERVER  1.SYNDESH   ---------------------------------------
            if (Globals.cSQLSERVER.Length < 2)
            {
                await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return;
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            con = new SqlConnection(constring);
            try
            {
                con.Open();
                // ***************  demo πως τρεχω εντολη στον sqlserver ********************************
                // SqlCommand cmd = new SqlCommand("insert into PALETES(PALET) values (1)");
                // cmd.Connection = con;
                // cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }




            var action = await DisplayAlert("Θα διαγραφουν τα τιμολόγια του κινητού", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {

            }
            else
            {
                return;
            }


            MainPage.ExecuteSqlite("delete from TABLES;");
            MainPage.ExecuteSqlite("delete from PARAGG;");
            MainPage.ExecuteSqlite("delete from KATHG;");

            String SYNT = "";
          //  if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
            //    SYNT = " TOP 20 ";


            //--------------------------------------  TRAPEZIA -----------------------------------------------------------
            try
            {
               //Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select " + SYNT + " ISNULL(ONO,'') AS ONO,ISNULL(KATEILHMENO,0) AS KATEILHMENO,ISNULL(SYNOLO,0) AS SYNOLO,ISNULL(NUM1,'') AS NUM1,ISNULL(NUM2,0) AS NUM2,ISNULL(CH1,'') AS CH1,ISNULL(CH2,'') AS CH2,ISNULL(IDPARAGG,0) AS IDPARAGG   FROM TABLES ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String ONO = dt.Rows[k]["ONO"].ToString();
                    ONO = ONO.Replace("'", "`");

                    String CH1 = dt.Rows[k]["CH1"].ToString();
                    CH1 = CH1.Replace("'", "`");

                    String CH2 = dt.Rows[k]["CH2"].ToString();
                    CH2 = CH2.Replace("'", "`");



                    string KATEILHMENO = dt.Rows[k]["KATEILHMENO"].ToString();
                    KATEILHMENO = KATEILHMENO.Replace(",", ".");


                    // MyList.Add(mF);
                    string SYNOLO = dt.Rows[k]["SYNOLO"].ToString();
                    SYNOLO = SYNOLO.Replace(",", ".");




                    string NUM1 = dt.Rows[k]["NUM1"].ToString();
                    NUM1 = NUM1.Replace(",", ".");

                    string NUM2 = dt.Rows[k]["NUM2"].ToString();
                    NUM2 = NUM2.Replace(",", ".");

                    string IDPARAGG = dt.Rows[k]["IDPARAGG"].ToString();
                    IDPARAGG = IDPARAGG.Replace(",", ".");



                    int n2 = MainPage.ExecuteSqlite("insert into TABLES (ONO,CH1,CH2,KATEILHMENO,SYNOLO,NUM1,NUM2,IDPARAGG) VALUES ('" + ONO + "','" + CH1 + "','" + CH2 + "'," + KATEILHMENO + "," + SYNOLO + "," + NUM1 + "," + NUM2 +","+ IDPARAGG+ ");");
                } // FOR

               
               

              //  BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }


            //-------------------------   KATHG  --------------------------------------------
            try
            {
                //Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select " + SYNT + " ISNULL(ONO,'') AS ONO,ISNULL(CH1,'') AS CH1,ISNULL(CH2,'') AS CH2   FROM KATHG ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String ONO = dt.Rows[k]["ONO"].ToString();
                    ONO = ONO.Replace("'", "`");

                    String CH1 = dt.Rows[k]["CH1"].ToString();
                    CH1 = CH1.Replace("'", "`");

                    String CH2 = dt.Rows[k]["CH2"].ToString();
                    CH2 = CH2.Replace("'", "`");



                    int n2 = MainPage.ExecuteSqlite("insert into KATHG (ONO,CH1,CH2) VALUES ('" + ONO + "','" + CH1 + "','" + CH2 + "');");
                } // FOR




                //  BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }




            //--------------------------------------  EIDH -----------------------------------------------------------
            try
            {
                //Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select " + SYNT + " ISNULL(ONO,'') AS ONO,ISNULL(CH1,'') AS CH1,ISNULL(CH2,'') AS CH2,ISNULL(NUM2,0) AS NUM2 ,ISNULL(TIMH,0) AS TIMH,ISNULL(NUM1,0) AS NUM1,ISNULL(KATHG,0) AS KATHG  FROM EIDH ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String ONO = dt.Rows[k]["ONO"].ToString();
                    ONO = ONO.Replace("'", "`");

                    String CH1 = dt.Rows[k]["CH1"].ToString();
                    CH1 = CH1.Replace("'", "`");

                    String CH2 = dt.Rows[k]["CH2"].ToString();
                    CH2 = CH2.Replace("'", "`");



                    string TIMH = dt.Rows[k]["TIMH"].ToString();
                    TIMH = TIMH.Replace(",", ".");


                    // MyList.Add(mF);
                    string KATHG = dt.Rows[k]["KATHG"].ToString();
                    KATHG = KATHG.Replace(",", ".");




                    string NUM1 = dt.Rows[k]["NUM1"].ToString();
                    NUM1 = NUM1.Replace(",", ".");

                    string NUM2 = dt.Rows[k]["NUM2"].ToString();
                    NUM2 = NUM2.Replace(",", ".");

                 



                    int n2 = MainPage.ExecuteSqlite("insert into EIDH (ONO,CH1,CH2,TIMH,KATHG,NUM1,NUM2) VALUES ('" + ONO + "','" + CH1 + "','" + CH2 + "'," + TIMH + "," + KATHG + "," + NUM1 + "," + NUM2 + ");");
                } // FOR




                //  BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }


















        }

        private async void updateWeb(object sender, EventArgs e)
        {
            // public AboutViewModel()
            //  {
            // Title = "About";
            // OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            // }

            //var br = new WebView
            //{
            //    Source = "https://dotnet.microsoft.com/apps/xamarin"


            //};
            await Launcher.OpenAsync("https://www.lagakis.gr/apk/23031.apk");
            // br.

        }



        private void SQLQUERYF1(object sender, EventArgs e)
        {
            DOSQLQUERY(1);
        }
        private async void EIDHPEL(object sender, EventArgs e)
        {

            //  -----------------SQLSERVER  1.SYNDESH   ---------------------------------------
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";



            var action = await DisplayAlert("Θα μεταφερθουν τα είδη πελάτη (10min)", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {

            }
            else
            {
                return;
            }





            //            string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            con = new SqlConnection(constring);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
                return;
            }


            // TO EKANA NA TO DIAGRAFEI APO TO EMPORIKO
            // ALLA MPOREI NA ΞΕΧΑΣΤΕΙ ΚΑΙ ΝΑ ΤΟ ΣΤΕΙΛΕΙ 2 ΦΟΡΕΣ
            string c = "DROP TABLE  EIDHPEL";

            try
            {

                SqlCommand cmd2 = new SqlCommand(c);
                cmd2.Connection = con;
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                await DisplayAlert("ΔΕΝ ΒΡΕΘΗΚΕ Ο ΠΙΝΑΚΑΣ EIDHPEL", ex.ToString(), "OK");
                return;
            }

            c ="Select PELKOD AS KODPEL, KODE, SUM(POSO) AS POSO, SUM(POSO * TIMM * (100 - EKPT) / 100) AS AJIA, MAX(TIMM) AS TELTIMH into EIDHPEL FROM EGGTIM where EIDOS = 'e' GROUP BY PELKOD,KODE";


            SqlCommand cmd = new SqlCommand(c);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();






            c = "select isnull(KODPEL,'') as  KODPEL,ISNULL(KODE,'') AS KODE,ISNULL(POSO,0) AS POSO,ISNULL(AJIA,0) AS AJIA,ISNULL(TELTIMH,0) AS TELTIMH from EIDHPEL";




            int n20 = MainPage.ExecuteSqlite("delete from EIDHPEL");


            //--------------------------------------  TRAPEZIA -----------------------------------------------------------
            try
            {
                //Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand(c, con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;






                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String KODPEL = dt.Rows[k]["KODPEL"].ToString();
                    KODPEL = KODPEL.Replace("'", "`");

                    String KODE = dt.Rows[k]["KODE"].ToString();
                    KODE = KODE.Replace("'", "`");               


                    string AJIA = dt.Rows[k]["AJIA"].ToString();
                    AJIA = AJIA.Replace(",", ".");


                    // MyList.Add(mF);
                    string POSO = dt.Rows[k]["POSO"].ToString();
                    POSO = POSO.Replace(",", ".");                  

                    string TELTIMH = dt.Rows[k]["TELTIMH"].ToString();
                    TELTIMH = TELTIMH.Replace(",", ".");

                    string cc4 = "";
                    try
                    {

                        cc4 = "insert into EIDHPEL (KODPEL,KODE,POSO,AJIA,TELTIMH) VALUES ('" + KODPEL + "','" + KODE + "'," + POSO + "," + AJIA + "," + TELTIMH + ");";
                        int n2 = MainPage.ExecuteSqlite(cc4);
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", cc4, "OK");
                    }
                } // FOR


                await DisplayAlert("OK ΕΙΔΗ ΠΕΛΑΤΗ.", k.ToString(), "OK");

                //  BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
            





        }

        private async void CXMLTEST(object sender, EventArgs e)
        {
           // string path = "/storage/emulated/0";  // Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments  );  // "/data/lagakis2";  // 


           // string filename = Path.Combine(path, "lagakis.txt");

           // var file = new SmbFile("smb://" + Globals.cIP + "/eggtim2.txt");

            XmlWriterSettings writerSettings = new XmlWriterSettings();      writerSettings.OmitXmlDeclaration = true;
            writerSettings.ConformanceLevel = ConformanceLevel.Fragment;
            writerSettings.CloseOutput = false;
            MemoryStream localMemoryStream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(localMemoryStream, writerSettings))
            {
                writer.WriteStartElement("book");
                writer.WriteElementString("title", "A Programmer's Guide to ADO.NET");
                writer.WriteElementString("author", "Mahesh Chand");
                writer.WriteElementString("publisher", "APress");
                writer.WriteElementString("price", "44.95");
                writer.WriteEndElement();
                writer.Flush();
            }


            
            string test = "Testing 1-2-3";

            // convert string to stream
      //      byte[] byteArray = Encoding.ASCII.GetBytes(test);
     //       MemoryStream stream = new MemoryStream(byteArray);



            // convert stream to string
            StreamReader reader = new StreamReader(localMemoryStream);
            localMemoryStream.Seek(0, SeekOrigin.Begin);
            string text = reader.ReadToEnd();
            DisplayAlert(text, "a", "mm", "xx");



            //XmlDocument doc = new XmlDocument();
            //XmlElement el = (XmlElement)doc.AppendChild(doc.CreateElement("Order"));
            //el.SetAttribute("CallConfirm", "1");
            //el.SetAttribute("PayMethod", "Безнал");
            //el.SetAttribute("QtyPerson", "");
            //el.SetAttribute("Type", "2");
            //el.SetAttribute("PayStateID", "0");
            //el.SetAttribute("Remark", "{Comment}");
            //el.SetAttribute("RemarkMoney", "0");
            //el.SetAttribute("TimePlan", "");
            //el.SetAttribute("Brand", "1");
            //el.SetAttribute("DiscountPercent", "0");
            //el.SetAttribute("BonusAmount", "0");
            //el.SetAttribute("Department", "");

            //XmlElement el2 = (XmlElement)el.AppendChild(doc.CreateElement("Customer"));
            //el2.SetAttribute("Login", "");
            //el2.SetAttribute("FIO", "{FIO}");

           

            //DisplayAlert("αα", doc.ToString (), "mm", "xx");



            

            var doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0",
                "UTF-8", string.Empty);

            doc.AppendChild(xmlDeclaration);

            XmlElement usersNode = doc.CreateElement("users");
            doc.AppendChild(usersNode);

        //    foreach (var (_, value) in users)
            {
                XmlElement userEl = doc.CreateElement("user");
                usersNode.AppendChild(userEl);

                XmlAttribute e22 = doc.CreateAttribute("id");
                e22.Value = "aaa";
                userEl.Attributes.Append(e22);

                XmlElement e2 = doc.CreateElement("name");
                e2.InnerText = "valueName";
                userEl.AppendChild(e2);

                XmlElement e3 = doc.CreateElement("occupation");
                e3.InnerText = "Occupation";
                userEl.AppendChild(e3);
            }
           
            string text2=doc.OuterXml ; // Console.Out);
            SaveFile(text2, "testxml.xml");
            //string text2;
            //  doc.Save(text);
            DisplayAlert("αα", text2, "mm", "xx");
        }

        private async void cwebsrvice(object sender, EventArgs e)
        {
            //doyleyei teleia sthn RequestTransmittedDocs
            HttpClient client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty );
            try
            {
                client.DefaultRequestHeaders.Add("aade-user-id", "glagakis22");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "534044b048f4023151f37c2a44282200");

                string uri = "https://mydataapidev.aade.gr/RequestTransmittedDocs?mark=400000000000000";// ' + queryString.ToString

                //   public  HttpResponseMessage response;// As 
                // SmbFile file = new SmbFile("smb://" + Globals.cIP + "/inv.xml");
                // Stream ccc=file.ToString ();    
                //       string xl = XDocument.Load(file).ToString();// ' "--> εκει έχω αποθηκεύσει το xml που εφτιαξα"
                //  Byte[] byteData = Encoding.UTF8.GetBytes(xl);

                //   ByteArrayContent content = new ByteArrayContent(byteData);
                //     content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                var response = await client.GetAsync(uri); //   PostAsync(uri, content);
                string result = await response.Content.ReadAsStringAsync();

                // TextBox2.Text = result.ToString
                //   ' "είναι το textbox πανω στη φόρμα που σου επιστρέφει το response xml"
                SaveFile(result,"eggtim23.txt");

                DisplayAlert("αα", result.Substring(0,50), "mm", "xx");



            } catch {
                DisplayAlert("latoe", "λαθος", "mm", "xx");
                // ex As Excepti
                // n
                // MsgBox(ex.ToString)
            }




        }


        void SaveFile(string text,string filname)
        {
            //Get the SmbFile specifying the file name to be created.
            var file = new SmbFile("smb://" + Globals.cIP + "/"+filname);
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
           

            //Write bytes.
            writeStream.Write(Encoding.UTF8.GetBytes(text));

            //Dispose writable stream.
            writeStream.Dispose();
        }
        
        private async void cSendInv(object sender, EventArgs e)
        {
            //doyleyei  sthn SendInvoices
            HttpClient client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            try
            {
                client.DefaultRequestHeaders.Add("aade-user-id", "glagakis22");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "534044b048f4023151f37c2a44282200");

                string uri = "https://mydataapidev.aade.gr/SendInvoices";// ' + queryString.ToString

                //   public  HttpResponseMessage response;// As 
                SmbFile file = new SmbFile("smb://" + Globals.cIP + "/inv.xml");
                string g;
                try
                {
                    //Get readable stream.
                    var readStream = file.GetInputStream();
                    //Create reading buffer.
                    var memStream = new MemoryStream();
                    //Get bytes.
                    ((Stream)readStream).CopyTo(memStream);
                    //Dispose readable stream.
                    readStream.Dispose();
                    //  Console.WriteLine(Encoding.UTF8.GetString(memStream.ToArray()));
                    byte[] bytes = memStream.ToArray();
                    //   await DisplayAlert("Error", Encoding.UTF8.GetString(bytes), "OK");
                    g = Encoding.UTF8.GetString(memStream.ToArray());
                    string[] lines = g.Split('\n');



                    //  Stream ccc=file.ToString ();    
                    //       string xl = XDocument.Load(file).ToString();// ' "--> εκει έχω αποθηκεύσει το xml που εφτιαξα"
                    // Byte[] byteData = Encoding.UTF8.GetBytes(xl);

                    ByteArrayContent content = new ByteArrayContent(bytes);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                    var response = await client.PostAsync(uri, content);
                    string result = await response.Content.ReadAsStringAsync();

                    // TextBox2.Text = result.ToString
                    //   ' "είναι το textbox πανω στη φόρμα που σου επιστρέφει το response xml"
                    SaveFile(result, "apant.xml");

                    DisplayAlert("αα", result.Substring(0, 150), "mm", "xx");
                }
                catch
                {
                    DisplayAlert("latos1", "λαθος1", "mm", "xx");



                }
            }
            catch
            {
                DisplayAlert("latoe", "λαθος", "mm", "xx");
                // ex As Excepti
                // n
                // MsgBox(ex.ToString)
            }





        }
    }
}






           
        
 