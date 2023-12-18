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


        private async void PRINTOUT(int IsSygkEpistr) // 0=timologio 1=sygkentrotiko epistrofis
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
            { mBluetoothAdapter.Enable(); }
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
                                contents.CommandText = "SELECT  KODE,ifnull(ONO,'') AS PER,ifnull(POSO,0) as POSO,IFNULL(TIMH,0 ) AS TIMH ,IFNULL(EKPT,0) AS EKPT ,ID ,IFNULL(TIMH*POSO,0) AS AXIA from EGGTIM where ATIM ='" + ATIM.Text + "' order by ID DESC ; ";                                                                                                                                                                          // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";

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
                                        te = Convert.ToInt64(Convert.ToDouble(MPOL));
                                        tt = Convert.ToInt64(Convert.ToDouble(r["POSO"])) - te;        //(Single)r["POSO"] - te ;
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
                                    lin = lin + "" + "13" + "  " + Right("        " + String.Format("{0:0.00}", tt), 7) + " ";
                                    cLine[nseira] = lin;
                                }
                                // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
                                int nCount = nseira;   // posa records exei to timologio
                                int RecPerPage = 20;
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


                                    printt(outStream, (PAR2.Text + spac40).Substring(0, 49) + (ATIM.Text + "          ").Substring(0, 9) + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt") + "\n");
                                    printt(outStream, "\n");
                                    printt(outStream, "\n");
                                    printt(outStream, "\n");
                                    printt(outStream, "\n");
                                    printt(outStream, (" ΕΠΩΝΥΜΙΑ:" + EPO.Text + "                         ").Substring(0, 30) + "                                        " + (PARAGGELIES.toGreek(Globals.cFORTHGO) + "          ").Substring(0, 9) + "\n");




                                    string EPA = PARAGGELIES.ReadSQL("select  IFNULL(EPA,'')  FROM PEL where KOD='" + af + "'");
                                    string DIE = PARAGGELIES.ReadSQL("select  IFNULL(DIE,'')  FROM PEL where KOD='" + af + "'");
                                    string AFM = PARAGGELIES.ReadSQL("select  IFNULL(AFM,'')  FROM PEL where KOD='" + af + "'");
                                    string POL = PARAGGELIES.ReadSQL("select  IFNULL(POL,'')  FROM PEL where KOD='" + af + "'");
                                    string DOY = PARAGGELIES.ReadSQL("select  IFNULL(DOY,'')  FROM PEL where KOD='" + af + "'");

                                    printt(outStream, ("ΕΠΑΓΓΕΛΜΑ: " + EPA + "                         ").Substring(0, 30) + "\n");
                                    printt(outStream, ("ΔΙΕΥΘΥΝΣΗ: " + DIE + "                              ").Substring(0, 30) + "\n");
                                    printt(outStream, ("     ΠΟΛΗ: " + POL + "                              ").Substring(0, 25) + "\n");
                                    printt(outStream, ("      ΑΦΜ: " + AFM + "                              ").Substring(0, 30) + "                          " + BCASH.Text + "\n");
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
                                        NEO = (float)fYPOLPEL;
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

                                        printt(outStream, spac40 + "             ΣΥΝ.ΑΞΙΑ       " + Right("     " + String.Format("{0:0.00}", fkauajiPro), 7) + "\n");
                                        printt(outStream, spac40 + "             ΣΥΝ.ΕΚΠΤΩΣΗ    " + Right("     " + String.Format("{0:0.00}", fkauajiPro - fkauaji), 7) + "\n");
                                        printt(outStream, Right("               " + cold, 15) + spac40 + "           " + Right("     " + String.Format("{0:0.00}", fkauaji), 7) + "\n");
                                        printt(outStream, Right("               " + cneo, 15) + spac40 + "           " + Right("     " + String.Format("{0:0.00}", fkauaji * 0.13), 7) + "\n");
                                        printt(outStream, "\n");
                                        printt(outStream, spac40 + "                           " + Right("     " + String.Format("{0:0.00}", fkauaji * 1.13), 6) + "\n");
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

            }
            catch
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





        void Show_detail(string ATIM)
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
            contents.CommandText = "SELECT  *  from EGGTIM WHERE ATIM='"+ATIM+"' order by ID ; "; // +BARCODE.Text +"'";
                                                                                                                                                                             // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            Single s = 0;
            while (r.Read())
            {
                // s = s + (Single)r["POSO"] * (Single)r["TIMH"] * (100 - (Single)r["EKPT"]) / 100;

                Monkeys.Add(new Monkey
                {
                    Name = (r["ONO"].ToString() + "                               ").Substring(0, 25),
                    Location = r["POSO"].ToString(),
                    ImageUrl = (r["TIMH"].ToString() + "      ").Substring(0, 5),
                    idPEL = r["KODE"].ToString()
                });



            }

            listdetail.ItemsSource = Monkeys;
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

            string action = await DisplayActionSheet("Επιλογή", "Ακυρο", null, "1.Ειδη Παρ/κου", "2.Επανεκτύπωση", "3.Διαγραφή");
            if (action.Substring(0, 1) == "Α") { return; }


            if (action.Substring(0, 1) == "1")
            {
                Show_detail(mATIM);
                return;
            }

            if (action.Substring(0, 1) == "2")
            //    var action = await DisplayAlert(mATIM + " Να ΞαναTυπωθί?", "Εισαι σίγουρος?", "Ναι", "Οχι");
           // if (action)
            {
                epanektyp(mID);
                return;
            }


            if (action.Substring(0, 1) == "3")
            {

              var  action2 = await DisplayAlert(mATIM + " Να διαγραφεί?", "Εισαι σίγουρος?", "Ναι", "Οχι");
                if (action2)
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

                    string[] POSO = new string[400];
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
                        {
                            MainPage.ExecuteSqlite("update EID SET YPOL=YPOL-" + POSO[k] + " WHERE KOD='" + KODE[k] + "'");
                            MainPage.ExecuteSqlite("update EID SET YPOL=0 WHERE YPOL<0 AND  KOD='" + KODE[k] + "'");
                        }
                        else
                        {
                            MainPage.ExecuteSqlite("update EID SET DESM=DESM-" + POSO[k] + " WHERE KOD='" + KODE[k] + "'");
                            MainPage.ExecuteSqlite("update EID SET DESM=0 WHERE DESM<0 AND  KOD='" + KODE[k] + "'");
                        }
                    }
                    MainPage.ExecuteSqlite("delete from EGGTIM where ATIM='" + mATIM + "'");

                    string PLIROMI = PARAGGELIES.ReadSQL("select IFNULL(TRP,' ') FROM TIM WHERE ATIM='" + mATIM + "'");  //  EIDOS='" + ATIM.Text.Substring(0, 1) + "'");

                    if (PLIROMI.Substring(0, 1) == "Π")
                    {
                        string axia = PARAGGELIES.ReadSQL("select IFNULL(AJI,0) FROM TIM WHERE ATIM='" + mATIM + "'");
                        MainPage.ExecuteSqlite("UPDATE PEL SET TYP=TYP-" + axia.Replace(",", "."));
                    }
                    else
                    {

                    }



                    MainPage.ExecuteSqlite("delete from    TIM where ATIM='" + mATIM + "'");
                    await DisplayAlert("διαγραφηκε", "", "OK");
                    Show_list();
                }
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
                s = s + (Single)r["POSO"] * (Single)r["TIMH"];  // * (100 - (Single)r["EKPT"]) / 100;
               
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

        private async void MARKARISMA(object sender, EventArgs e)
        {
            MainPage.ExecuteSqlite("UPDATE TIM SET NUM2=0");
            MainPage.ExecuteSqlite("UPDATE EGGTIM SET NUM2=0");
            await DisplayAlert("OK", "", "OK");




        }
    }
}