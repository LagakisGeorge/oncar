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
using Plugin.Toast;
using System.Threading;

namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
            test22.IsVisible = false;
            
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

            int l = MainPage.ExecuteSqlite(c);

            await DisplayAlert("ΕΙΔΗ ΟΚ", "ΕΙΔΗ ΔΗΜΙΟΥΡΓΗΘΗΚΑΝ", "OK");

            // Κωδικός;Α.Φ.Μ.;Επωνυμία;Διεύθυνση;Πόλη;Τηλ.1

            c = "CREATE TABLE IF NOT EXISTS PEL( ID  INTEGER PRIMARY KEY,KOD [nvarchar](25)," +
                    "[EPO] [nvarchar](255) ," +
                     "[DIE] [nvarchar](35) ," +
                       "[POL] [nvarchar](35) ," +
                         "[THL] [nvarchar](35) ," +                    
                       "[AFM] [nvarchar](15) )";


            l = MainPage.ExecuteSqlite(c);

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

            l = MainPage.ExecuteSqlite(c);



            c = "CREATE TABLE IF NOT EXISTS ARITMISI( ID  INTEGER PRIMARY KEY,ARITMISI [int] )";
                

            l = MainPage.ExecuteSqlite(c);

            l = MainPage.ExecuteSqlite("INSERT INTO ARITMISI (ARITMISI) VALUES (0)");
            await DisplayAlert("ΑΡΙΘΜΗΣΗ ΟΚ", " ΔΗΜΙΟΥΡΓΗΘΗΚΑΝ", "OK");



            c = "CREATE TABLE IF NOT EXISTS MEM( ID  INTEGER PRIMARY KEY,IP [nvarchar](45)," +
                    "[EPO] [nvarchar](255) ," +
                     "[DIE] [nvarchar](35) ," +
                       "[POL] [nvarchar](35) ," +
                         "[THL] [nvarchar](35) ," +
                       "[AFM] [nvarchar](15) )";
            l = MainPage.ExecuteSqlite("INSERT INTO MEM (IP) VALUES ('*')");
            await DisplayAlert("ΑΡΙΘΜΗΣΗ ΟΚ", " ΔΗΜΙΟΥΡΓΗΘΗΚΑΝ", "OK");

            l = MainPage.ExecuteSqlite(c);






        }

        async void ImportPEL(object sender, EventArgs e)
        {

            await DisplayAlert("Εναρξη μεταφοράς πελατών 3-5λεπτά", "Πελάτες  ", "OK");

            // await Navigation.PushAsync(new param1());

            // διαβαζω το αρχείο των ειδων  
            // διαβαζω το αρχείο των ειδων
            //Get the SmbFile specifying the file name to be created.


           // var file = new SmbFile("smb://DESKTOP-MPGU8SB/backpel/PEL.txt");
            var file = new SmbFile("smb://"+Globals.cIP +"/PEL.txt");
           
            
            //Get target's SmbFile.
            // var file = new SmbFile("smb://UserName:Password@ServerIP/ShareName/Folder/FileName.txt");
            // ΧΩΡΙς ΚΩΔΙΚΟ :   var file = new SmbFile("smb://192.168.1.4/backpel/EID.txt");
            try
            {

                test22.IsVisible = true;

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
                    n2 = MainPage.ExecuteSqlite("insert into PEL (KOD,EPO,AFM) VALUES ('" + lines2[0] + "','" + cc + "','"+lines2[1]+"');");
                    IMPORTEID.Text = lines[n];


                }

                await DisplayAlert("τελος μεταφοράς", "ΠΕΛΑΤΕΣ που περάστηκαν " + (lines.Length - 1).ToString(), "OK"); ;





            }
            catch
            {
                await DisplayAlert("ΔΕΝ ΔΙΑΒΑΖΕΙ ΤΟ ΑΡΧΕΙΟ EID.txt", "....", "OK");
                return;
            }
        }


        async void test(object sender, EventArgs e)
        {

          // ασυγχρονη εκτελεση  (δεν τρεχει με την πρωτη και ασπριζει η οθονη μετα το πέρας της ενημερωσης




            await DisplayAlert("Εναρξη μεταφοράς ειδών ", "Είδη  ", "OK");
            CrossToastPopUp.Current.ShowToastMessage("loading ειδη");
            Imp_EIDH();
            IMPORTEID.Text = "*** ΠΑΡΑΚΑΛΩ ΠΕΡΙΜΕΝΕΤΕ 133λεπτά*****";
            await DisplayAlert("2h-Εναρξη μεταφοράς ειδών", "Είδη  ", "OK");
            Imp_EIDH();
            await DisplayAlert("Telos--Εναρξη μεταφοράς ειδών", "Είδη  " , "OK");











        }



        async void ImportEID(object sender, EventArgs e)
        {

            await DisplayAlert("Εναρξη μεταφοράς ειδών 3-5λεπτά", "Είδη  ", "OK");
            CrossToastPopUp.Current.ShowToastMessage("loading ειδη");
            int count = Imp_EIDH();
            await DisplayAlert("Εναρξη μεταφοράς ειδών ", "Είδη  "+count.ToString(), "OK");



            /*  Task<int> task = new Task<int>(Imp_EIDH);
              IMPORTEID.Text = "*** ΠΑΡΑΚΑΛΩ ΠΕΡΙΜΕΝΕΤΕ 3λεπτά*****";
              task.Start();
              int count = await task;

              IMPORTEID.Text = count.ToString();
              await DisplayAlert("--Εναρξη μεταφοράς ειδών", "Είδη που περάσ "+count.ToString (), "OK");
            */

        }


         int Imp_EIDH() {
           
            IMPORTEID.Text = "*** ΠΑΡΑΚΑΛΩ ΠΕΡΙΜΕΝΕΤΕ 3λεπτά*****";
            // Task.Delay(1000).Wait();
            Thread.Sleep(500);
           

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
                IMPORTEID.Text=  lines[1] + "=" + lines[2] + "=" + lines[3];
                //  await DisplayAlert("Error", Encoding.UTF8.GetString(memStream.ToArray()), "OK");
                int n2;// = MainPage.ExecuteSqlite("dd");

                

               
                for (int n=0;n<lines.Length-1;n++ )
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


                    // Κωδικός0;Περιγραφή1;ΕΝΑΛ.ΚΩΔΙΚΟΣ2;Χονδρικής3;Υπολ.1-4; Δεσμευμένα5; Αναμενόμενα6; Barcode 7   
                    n2 = MainPage.ExecuteSqlite("insert into EID (KOD,ONO,XONDR,ENAL,YPOL,DESM,ANAM,BARCODE) VALUES ('"+lines2[0]+ "','" + cc + "',"+xondr +",'"+enal+"',"+ypol+","+desm+","+anam+",'"+lines2[7]+"');");
                    IMPORTEID.Text =  lines[n] ;


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



                  //  return true; // runs again, or false to stop
               // });

               // await DisplayAlert("τελος μεταφοράς","Είδη που περάστηκαν "+ (lines.Length - 1).ToString(), "OK"); ;

                return lines.Length;




            }
            catch
            {
              //  await DisplayAlert("ΔΕΝ ΔΙΑΒΑΖΕΙ ΤΟ ΑΡΧΕΙΟ EID.txt", "....", "OK");
                return 0;
            }



            // το αποθηκευω στο mysql
            //  int n = MainPage.ExecuteSqlite("dd");




        }



    }
}