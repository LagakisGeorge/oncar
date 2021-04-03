using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Mobile.Forms;
using System.Threading;
using Mono.Data.Sqlite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.Toast;
using SharpCifs.Smb;
using System.Data.SqlClient;
using System.Data;

namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class techn1 : ContentPage
    {
        public SqlConnection con;
        public IList<Monkey> Monkeys { get; private set; }
        public string f_cid="" ;
        public techn1()
        {
            InitializeComponent();
            Monkeys = new List<Monkey>();
        }


        async void WriteFile(object sender, EventArgs e)
        {

            //  -----------------SQLSERVER  1.SYNDESH   ---------------------------------------
            if (Globals.cSQLSERVER.Length<2)
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


            MainPage.ExecuteSqlite("delete from EID;");
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select  ID,isnull(KOD,'') AS MKOD,ISNULL(ONO,'') AS MONO,ISNULL(ERG,'') AS MERG,ISNULL(LTI5,0) AS MLTI5  FROM EID ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
               // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String mF = dt.Rows[k]["MONO"].ToString();
                    mF = mF.Replace("'", "`");
                   // MyList.Add(mF);
                    string mTYP = dt.Rows[k]["MLTI5"].ToString();
                    mTYP = mTYP.Replace(",", ".");
                 
                    /* Monkeys.Add(new Monkey
                    {
                        Name = mF,
                        Location = dt.Rows[k]["MKOD"].ToString(),
                        ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    });
                    */
                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();
                    string mERG = dt.Rows[k]["MERG"].ToString();
                    mERG = mERG.Replace("'", "`");

                    mTYP = mTYP.Replace(",", ".");
                    int n2 = MainPage.ExecuteSqlite("insert into EID (XONDR,KOD,ONO,BARCODE) VALUES (" + mTYP + ",'" + mKOD + "','" + mF + "','" + mERG + "');");
                } // FOR

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
               // await DisplayAlert("EIΔΗ", " Eιδη:" + dt.Rows.Count, "OK");
                EID.Text = " ΕΙΔΗ:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }

            MainPage.ExecuteSqlite("delete from PEL;");
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select  ID,isnull(KOD,'') AS MKOD,ISNULL(EPO,'') AS MEPO," +
                    "ISNULL(DIE,'') AS MDIE,ISNULL(TYP,0) AS MYPOL ,ISNULL(DOY,'') AS MDOY,ISNULL(AFM,'') AS MAFM,ISNULL(POL,'') AS MPOL,PEK AS MPEK " +
                    "FROM PEL WHERE EIDOS='e' ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                //List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                   // String mF = dt.Rows[k]["MEPO"].ToString();
                   // mF = mF.Replace("'", "`");
                   // MyList.Add(mF);
                    string mTYP = dt.Rows[k]["MYPOL"].ToString();
                    mTYP = mTYP.Replace(",", ".");

                    /* Monkeys.Add(new Monkey
                    {
                        Name = mF,
                        Location = dt.Rows[k]["MKOD"].ToString(),
                        ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    });
                    */
                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();
                    string mDIE = dt.Rows[k]["MDIE"].ToString();
                    mDIE = mDIE.Replace("'", "`");

                    string mEPO = dt.Rows[k]["MEPO"].ToString();
                    mEPO = mEPO.Replace("'", "`");

                    string mPOL = dt.Rows[k]["MPOL"].ToString();
                    mPOL = mPOL.Replace("'", "`");

                    string mAFM = dt.Rows[k]["MAFM"].ToString();
                    mAFM = mAFM.Replace("'", "`");

                    string mDOY = dt.Rows[k]["MDOY"].ToString();
                    mDOY = mDOY.Replace("'", "`");

                    string mPEK = dt.Rows[k]["MPEK"].ToString();




                    int n2 = MainPage.ExecuteSqlite("insert into PEL (KOD,EPO,DIE,TYP) VALUES ('"
                        +mKOD+"','" + mEPO + "','" + mDIE + "'," + mTYP + ");");
                } // FOR

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
                //  await DisplayAlert("ΠΕΛΑΤΕΣ", " Πελάτες:" + dt.Rows.Count, "OK");
                PEL.Text = " ΠΕΛΑΤΕΣ:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }





            //   TIMOKAT( [KOD][nvarchar](14) NOT NULL,"+
            //       "[EKPT] [decimal](5, 2) NOT NULL, [TIMOK]


            MainPage.ExecuteSqlite("delete from TIMOKAT;");
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select  isnull(KOD,'') AS MKOD,ISNULL(EKPT,0) AS MEKPT,ISNULL(TIMOK,0) AS MTIMOK  FROM TIMOKAT ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {

                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();

                    // MyList.Add(mF);
                    string mTYP = dt.Rows[k]["MEKPT"].ToString();
                    mTYP = mTYP.Replace(",", ".");

                    string mTIMOK = dt.Rows[k]["MTIMOK"].ToString();
                    mTIMOK = mTIMOK.Replace(",", ".");


                    /* Monkeys.Add(new Monkey
                    {
                        Name = mF,
                        Location = dt.Rows[k]["MKOD"].ToString(),
                        ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    });
                    */

                    int n2 = MainPage.ExecuteSqlite("insert into TIMOKAT(TIMOKID,EKPT,KOD,TIMOK) VALUES (0," + mTYP + ",'" + mKOD + "'," + mTIMOK + ");");
                } // FOR

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
               // await DisplayAlert("TIMOK", " TIMOK:" + dt.Rows.Count, "OK");
                TIMOKAT.Text  = " TIMOK:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }



















        }







        /*  protected override bool OnBackButtonPressed()
          {
              return false;
          }
          void  OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
          {
              Monkey selectedItem = e.SelectedItem as Monkey;
              string c = selectedItem.idPEL;
              f_cid = c;
          }        
           async void diag_barcode(object sender, EventArgs e)
          {

              var action = await DisplayAlert("Να διαγραφεί?", "Εισαι σίγουρος?", "Ναι", "Οχι");
              if (action )
              {
                  //  Navigate to first page
                  MainPage.ExecuteSqlite("delete from PARALABES WHERE ID=" + f_cid );
                  await DisplayAlert("διαγραφτηκε", "", "OK");
                  show_list();
              }




          }
          async void delete_all(object sender, EventArgs e)
          {
              var action = await DisplayAlert("Να διαγραφoύν όλα τα τιμολόγια?", "Εισαι σίγουρος?", "Ναι", "Οχι");
              if (action)
              {
                  //  Navigate to first page
                  MainPage.ExecuteSqlite("delete from PARALABES ");
                  await DisplayAlert("διαγραφτηκε", "", "OK");
                  show_list();
              }
          }
          void SaveFile(string text)
          {
              //Get the SmbFile specifying the file name to be created.
              var file = new SmbFile("smb://" + Globals.cIP + "/eggtim2.txt");
              // fine var file = new SmbFile("smb://User:1@192.168.1.5/backpel/New2FileName.txt");

              if (file.Exists())
              {
                  DisplayAlert("Θα διαγραφει το ηδη το αρχειο", "....", "OK");
                  file.Delete();  // https://csharpdoc.hotexamples.com/class/SharpCifs.Smb/SmbFile#
              }
              else
              {
                  //Create file.
                  file.CreateNewFile();
              }


  /*            try
              {
                  //Create file.
                  file.CreateNewFile();
              }
              catch
              {
                  DisplayAlert("Θα διαγραφει το ηδη το αρχειο", "....", "OK");
                  file.Delete();  // https://csharpdoc.hotexamples.com/class/SharpCifs.Smb/SmbFile#
              }


              //Get writable stream.
              var writeStream = file.GetOutputStream();

              //Write bytes.
              writeStream.Write(Encoding.UTF8.GetBytes(text));

              //Dispose writable stream.
              writeStream.Dispose();
          }
          async void  CloseInvoice(object sender, EventArgs e)
          {
              await Navigation.PopAsync();



          }
          void show_list()
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
               contents.CommandText = "SELECT  * from PARALABES where ATIM ='"+cATIM.Text  +"' order by ID DESC ; "; // +BARCODE.Text +"'";
               // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
              var r = contents.ExecuteReader();
              Console.WriteLine("Reading data");
              while (r.Read())
              {


                  Monkeys.Add(new Monkey
                  {
                      Name = r["BARCODE"].ToString(),

                      Location = r["ATIM"].ToString(),
                      ImageUrl = "",
                      idPEL = r["id"].ToString ()
                  });



              }




              connection.Close();

              BindingContext = this;
          }

          async void PaletaChanged(object sender, EventArgs e)
          {

              if (Paleta.Text.Length == 0) return;

              BindingContext = null;
              Monkeys.Add(new Monkey
              {
                  Name = Paleta.Text,

                  Location = "***",
                  ImageUrl = "---",
                  idPEL = "///"
              });

              BindingContext = this;

              Paleta.Text = ""; // to ekana etsi gia na mporei na pairnei 2 fores tin idia paleta
              MainPage.ExecuteSqlite("INSERT INTO PARALABES (ATIM,BARCODE) VALUES ('" + cATIM.Text + "','" + Paleta.Text + "')");
              show_list();
              Paleta.Focus();

          }
          async void barcfoc(object sender, EventArgs e)
          {
              string cc = "";
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
                      Thread.Sleep(500);
                      Paleta.Text = result.Text;

                      cc = Paleta.Text;




                  });
              };

          }
          void KataxTimol(object sender, EventArgs e)
          {
             // show_list();
              Paleta.Focus();




          }
      */








    }
}