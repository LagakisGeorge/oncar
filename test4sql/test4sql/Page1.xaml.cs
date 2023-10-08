using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using PCLStorage;
using SharpCifs.Smb;  // http://sharpcifsstd.dobes.jp/
using System.IO;
using ZXing.Net.Mobile.Forms;
using Mono.Data.Sqlite;

using Plugin.BLE;
using Android.Bluetooth;



namespace test4sql
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]

   
    public partial class Page1 : ContentPage
    {
        public BluetoothDevice mmDevice;

        public List<string> MyList = new List<string>();
        public IList<Monkey> Monkeys { get; private set; }



        public float fEKPTNUM1 = 0; // εχτρα εκπτωση πελατη
        public float faji = 0;  // SYNOLO ME FPA
        public float fkauaji = 0;  // SYNOLO ME FPA
                                   // public string fIDTimDior = "0";
        public float fkauajiPro = 0;  // SYNOLO ME FPA

        public float fYPOLPEL = 0;  // YPOLOIPO PELATH







        public Page1()
        {
            InitializeComponent();
            Show_list();


        }


         void Click_Login(object sender, EventArgs e)
        {
            // To create a new subfolder in the local folder, call the CreateFolderAsync method.
           // String folderName = "csharp";
          //  IFolder folder = FileSystem.Current.LocalStorage;
          //  folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
          
        }

         async void Shared_Folder(object sender, EventArgs e)
        {

            //Get the SmbFile specifying the file name to be created.
            var file = new SmbFile("smb://"+Globals.cIP +"/EGGTIM2.TXT");
           // fine var file = new SmbFile("smb://User:1@192.168.1.5/backpel/New2FileName.txt");
            try
            {
 //Create file.
            file.CreateNewFile();
            }
            catch {
                 await DisplayAlert("Υπαρχει ηδη το αρχειο", "....", "OK");
                return;
            }
           

            //Get writable stream.
            var writeStream = file.GetOutputStream();
            string c = "1;2;3;4;5;6;7;8;\n";
            c = c + "8;8;9;9;9;9;9;9\n";
            c = c + "18;18;19;19;19;19;19;19\n";

            //Write bytes.
            writeStream.Write(Encoding.UTF8.GetBytes(c));

            //Dispose writable stream.
            writeStream.Dispose();





        }



        // ΠΡΟΣΟΧΗ ΤΟ PRINTOUT ΕΙΝΑΙ ΑΚΡΙΒΩΣ ΙΔΙΟ ΜΕ ΤΟ PARAGGELIES.PRINTOUT
        // ΓΙΑΥΤΟ ΚΑΝΕ ΤΙΣ ΑΛΛΑΓΕΣ ΣΤΟ PARAGGELIES KAI META COPY PASTE ΕΔΩ




        public static string Right(string original, int numberCharacters)
        {
            return original.Substring(original.Length - numberCharacters);
        }





        void printt(Stream outs, string qq)
        {
            byte[] toBytes;
            string fff;
            fff = PARAGGELIES.toGreek (qq);
           // fff = qq;
            toBytes = Encoding.Unicode.GetBytes(fff);
            outs.Write(toBytes, 0, toBytes.Length);



        }



        async void ReadFile(object sender, EventArgs e)
        {
            bool f = ReadFiles();
            //Get the SmbFile specifying the file name to be created.
            var file = new SmbFile("smb://" + Globals.cIP + "/EIDH.TXT"); // "smb://User:1@192.168.1.5/backpel/New2FileName.txt");
            //Get target's SmbFile.
            // var file = new SmbFile("smb://UserName:Password@ServerIP/ShareName/Folder/FileName.txt");

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

                Console.WriteLine(Encoding.UTF8.GetString(memStream.ToArray()));

                byte[] bytes = memStream.ToArray();


                await DisplayAlert("Error", Encoding.UTF8.GetString(bytes), "OK");

                String g = Encoding.UTF8.GetString(memStream.ToArray());

                string[] lines = g.Split('\n');
                //for (int i=0;0;lines.count )
                    for (int k = 0; k <= lines.Length  - 1; k++)
                    {
                    }




              //  Lab1.Text = lines[1] + "=" + lines[2] + "=" + lines[3];
               // ΤΟ ΔΕΙΧΝΕΙ ΣΕ MSGBOX :     await DisplayAlert("Error", Encoding.UTF8.GetString(memStream.ToArray()), "OK");
            }
            catch
            {
                await DisplayAlert("Error", "....", "OK");
                return;
            }           
        }


       private bool  ReadFiles()
        {


            //   Get items in shared folder:            https://www.csharpcodi.com/csharp-examples/SharpCifs.Smb.SmbFile.GetUncPath0()/
            //using System;
            //using SharpCifs.Smb;

            //Get SmbFile-Object of a folder.
            var folder = new SmbFile("smb://" + Globals.cIP + "/"); // "smb://User:1@192.168.1.5/backpel/");

        //UnixTime
        var epocDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

//List items
        foreach (SmbFile item in folder.ListFiles())
        {
            var lastModDate = epocDate.AddMilliseconds(item.LastModified())
                                .ToLocalTime();
            var name = item.GetName();
            var type = item.IsDirectory() ? "dir" : "file";
            var date = lastModDate.ToString("yyyy-MM-dd HH:mm:ss");
            var msg = $"{name} ({type}) - LastMod: {date}";
            Console.WriteLine(msg);
        }

            return true;
        }


       

        private void LISTTIMOL(object sender, EventArgs e)
        {
            Show_list();
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
            contents.CommandText = "SELECT  IFNULL(AJI,0) AS AJI2,IFNULL(HME,'') AS HME2,IFNULL(ATIM,'') AS ATIM2,IFNULL(EPO,'') AS EPO2,ID  from TIM  order by ID DESC ; "; // +BARCODE.Text +"'";
                                                                                                                                                           // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            Single s = 0;
            while (r.Read())
            {
               // s = s + (Single)r["POSO"] * (Single)r["TIMH"] * (100 - (Single)r["EKPT"]) / 100;

                Monkeys.Add(new Monkey
                {
                    Name = (r["EPO2"].ToString() + "                               ").Substring(0, 25), 
                    Location = r["ATIM2"].ToString(),
                    ImageUrl = (r["AJI2"].ToString() + "      ").Substring(0, 5),
                    idPEL =  r["ID"].ToString()
                });



            }

            listview.ItemsSource = Monkeys;
            BindingContext = this;


            connection.Close();

          //  SAJIA.Text = String.Format("{0:0.00}", s);  // s.ToString();

            BindingContext = this;
        }

        private async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

            Monkey tappedItem = e.Item as Monkey;
            // tappedItem.Location=>'00182'
            string mID = tappedItem.idPEL;
            string mATIM = tappedItem.Location;

            var action = await DisplayAlert(mATIM + " Να ΞαναTυπωθί?", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {
                epanektyp(mID);
                return;
            }




                action = await DisplayAlert(mATIM +" Να διαγραφεί?", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
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
                contents.CommandText = "SELECT * from EGGTIM where ATIM='" + mATIM + "'";
                var r = contents.ExecuteReader();
                Console.WriteLine("Reading data");

                string[] POSO = new string [400];
                string[] KODE = new string[400];

                int ll = 0;
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
                connection.Close();


                for (int k = 1; k <= ll; k++)
             
                {
                    if (mATIM.Substring(0, 1) == "τ")
                    {  MainPage.ExecuteSqlite("update EID SET YPOL=YPOL-" + POSO[k]+ " WHERE KOD='" + KODE[k] + "'");
                        MainPage.ExecuteSqlite("update EID SET YPOL=0 WHERE YPOL<0 AND  KOD='" + KODE[k] + "'");
                    }
                    else
                    {   MainPage.ExecuteSqlite("update EID SET DESM=DESM-" + POSO[k] + " WHERE KOD='" + KODE[k] + "'");
                        MainPage.ExecuteSqlite("update EID SET DESM=0 WHERE DESM<0 AND  KOD='" + KODE[k] + "'");
                    }
                }
                MainPage.ExecuteSqlite("delete from EGGTIM where ATIM='"+mATIM +"'");
                MainPage.ExecuteSqlite("delete from    TIM where ATIM='" + mATIM + "'");
                await DisplayAlert("διαγραφτηκε", "", "OK");
                Show_list();
            }

        }

        private void epanektyp(string mID)
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
            contents.CommandText = "SELECT ATIM,KPE,HME,TRP,IFNULL(TYP,0) AS TYP from TIM where ID='" + mID + "'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            string f_hme;
            int ll = 0;
            string cc = "";
           
            while (r.Read())
            {
                ATIM.Text  = r["ATIM"].ToString();               
                AFM.Text  = r["KPE"].ToString();

                BCASH.Text = r["TRP"].ToString();
                f_hme= r["HME"].ToString();

                fYPOLPEL = float.Parse(r["TYP"].ToString());  // YPOLOIPO PELATH
            }
            connection.Close();
           


                if (BCASH.Text.Substring(0, 1) == "Π")
            {
                BCASH.Text ="ΕΠΙ ΠΙΣΤΩΣΕΙ";
            }
            else
            {
                BCASH.Text = "ΜΕΤΡΗΤΟΙΣ";
            }


            EPO.Text = PARAGGELIES.ReadSQL("select EPO FROM PEL WHERE KOD='" + AFM.Text  + "'");

            PAR2.Text =PARAGGELIES.ReadSQL("select TITLOS FROM PARASTAT WHERE EIDOS='" + ATIM.Text.Substring(0, 1) + "'");
           
            string ypol= PARAGGELIES.ReadSQL("select TYP FROM PEL WHERE KOD='" + AFM.Text  + "'");
            string EKPTNUM1 = PARAGGELIES.ReadSQL("select NUM1 FROM PEL WHERE KOD='" + AFM.Text + "'");


         //   fYPOLPEL = float.Parse(ypol);  // YPOLOIPO PELATH
              fEKPTNUM1 = float.Parse(EKPTNUM1 );  // εχτρα εκπτωση πελατη


            // 
            if (ATIM.Text.Substring(0, 1) == "ρ")  // στην σειρα α δεν εχει εκπτωση
            {
                fEKPTNUM1 = 0;


            }



                faji = 0;  // SYNOLO ME FPA
         fkauaji = 0;  // SYNOLO ME FPA                                   // public string fIDTimDior = "0";
         fkauajiPro = 0;  // SYNOLO ME FPA
            Show2_list(ATIM.Text );
            

           
            if (ATIM.Text.Substring(0, 1) == "τ")
            {
                    try
                    {
                        PRINTOUT(1);
                    }
                    catch
                    {

                    }
                   // PRINTOUT(1);  // συγκεντρωτικο
            }
            else
            {
                    try
                    {
                        PRINTOUT(0);
                    }
                    catch
                    {

                    }
                   // PRINTOUT(0); // απλο τιμολογιο
            }

           








    }




    private async Task DELETETIMOL()
        {

         var action = await DisplayAlert("Να διαγραφεί?", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {

                MainPage.ExecuteSqlite("delete from EGGTIM");
                MainPage.ExecuteSqlite("delete from TIM");
                MainPage.ExecuteSqlite("update EID SET YPOL=0,DESM=0");

                await DisplayAlert("διαγραφτηκε", "", "OK");
                Show_list();
            }   


        }

        private void delt(object sender, EventArgs e)
        {
            DELETETIMOL();
        }


        void Show2_list(string mATIM)
        {
          //  Monkeys = new List<Monkey>();
         //   BindingContext = null;

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
            contents.CommandText = "SELECT  KODE,ifnull(ONO,'') AS PER,POSO,TIMH,EKPT,ID from EGGTIM where ATIM ='" + mATIM + "' order by ID DESC ; "; // +BARCODE.Text +"'";
                                                                                                                                                           // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
     //       Console.WriteLine("Reading data");
            Single s = 0;
            while (r.Read())
            {
                s = s + (Single)r["POSO"] * (Single)r["TIMH"] * (100 - (Single)r["EKPT"]) / 100;
               
            }

            listview.ItemsSource = Monkeys;
         //   BindingContext = this;
            fkauajiPro = s;
            s = s - (s * fEKPTNUM1) / 100;
            fkauaji = s;
            faji = s * 113 / 100;
            connection.Close();

            

          //  BindingContext = this;
        }





        private void LISTEIDH(object sender, EventArgs e)
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
            contents.CommandText = "SELECT ONO,KOD,YPOL,DESM FROM EID  WHERE DESM>0 OR YPOL>0 ; "; // +BARCODE.Text +"'";
                                                                                                                                                                             // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");


            Monkeys.Add(new Monkey
            {
                Name = "ΠΕΡΙΓΡΑΦΗ ΕΙΔΟΥΣ",
                Location = "ΚΩΔΙΚΟΣ ΕΙΔΟΥΣ",
                ImageUrl ="ΥΠΟΛ",
                idPEL ="ΠΩΛΗΣ"
            });




            Single s = 0;
            while (r.Read())
            {
                // s = s + (Single)r["POSO"] * (Single)r["TIMH"] * (100 - (Single)r["EKPT"]) / 100;

                Monkeys.Add(new Monkey
                {
                    Name = (r["ONO"].ToString() + "                               ").Substring(0, 25),
                    Location = r["KOD"].ToString(),
                    ImageUrl = (r["YPOL"].ToString() + "      ").Substring(0, 5),
                    idPEL = r["DESM"].ToString()
                });



            }

            listview.ItemsSource = Monkeys;
            BindingContext = this;


            connection.Close();

            //  SAJIA.Text = String.Format("{0:0.00}", s);  // s.ToString();

            BindingContext = this;





        }
    }
}