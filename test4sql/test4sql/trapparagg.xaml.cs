using Android.Content;
using Android.Drm;
using Java.IO;
using Java.Nio.Channels;
using Mono.Data.Sqlite;
using Org.Apache.Http.Authentication;
using SharpCifs.Util.Sharpen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using test4sql;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;



namespace oncar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class trapparagg : ContentPage

    {
        Main2PageModel pageModel;
        public SqlConnection con;

        public IList<Monkey> Monkeys { get; private set; }
        public IList<Monkey2> Monkeys2 { get; private set; }
        public string trapezi;
        public string IDPARAGG;
        public Single fypol = 0;

        public trapparagg()
        {
            InitializeComponent();
            Globals.gtIDPARAGG = Globals.gtIDPARAGG.Replace("#", "");
            //  Items.Add(string.Format(" {0} ", fkat + fONO + "*" + fID+"*"+CH1));
            string[] lines = Globals.gtIDPARAGG.Split('*');
            Globals.gTrapezi = lines[0];
            Globals.gTrapezi = Globals.gTrapezi.TrimStart();
            Globals.gTrapezi = Globals.gTrapezi.TrimEnd();
            Globals.gIDPARAGG = AllRead("SELECT  cast(ISNULL(IDPARAGG,0) as varchar ) FROM TABLES WHERE  ONO='" + Globals.gTrapezi+"'");
           // Globals.gIDPARAGG = lines[1];
            titlos.Text = "Τραπέζι: " + Globals.gTrapezi;
            cmdtimologio.Text= AllRead("SELECT  ISNULL(CH2,'') AS CH2 FROM TABLES WHERE  ONO='" + Globals.gTrapezi + "'");




            //}
            //  Globals.gTrapezi = trapezi;
            // Globals.gIDPARAGG = IDPARAGG;
            pageModel = new Main2PageModel(this);
            BindingContext = pageModel;



            //pageModel = new MainPageModel(this);
            //BindingContext = pageModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //  Globals.gTrapezi = "";

            Show_listsql_Paragg(Globals.gIDPARAGG);
            pageModel = new Main2PageModel(this);
            BindingContext = pageModel;
        }


        public bool IsNumeric(string input)
        {
            float test;
            return float.TryParse(input, out test);
        }

        //void Show_list_Paragg(string ono)
        //{
        //    Monkeys = new List<Monkey>();
        //    BindingContext = null;

        //    string dbPath = Path.Combine(
        //      Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        //      "adodemo.db3");
        //    //bool exists = File.Exists(dbPath);
        //    //if (!exists)
        //    //{
        //    //    Console.WriteLine("Creating database");
        //    //    // Need to create the database before seeding it with some data
        //    //    Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);

        //    //}

        //    SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
        //    // Open the database connection and create table with data
        //    connection.Open();

        //    ono = ono.ToUpper();
        //    var contents = connection.CreateCommand();

        //    contents.CommandText = "SELECT  ONO,POSO,TIMH FROM PARAGG where IDPARAGG=" + Globals.gIDPARAGG + "  order by ID ; "; // +BARCODE.Text +"'";
        //                                                                                                                         // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
        //    var r = contents.ExecuteReader();
        //    Console.WriteLine("Reading data");
        //  while (r.Read())
        //    {


        //        Monkeys.Add(new Monkey
        //        {
        //            Name = (r["PER"].ToString() + "                                   ").Substring(0, 28),

        //            Location = (r["KODI"].ToString() + "      ").Substring(0, 5),
        //            ImageUrl = (r["THL"].ToString() + "              ").Substring(0, 11),
        //            idPEL = r["ID"].ToString()
        //        });



        //    }

        //    listERG.ItemsSource = Monkeys;
        //    BindingContext = this;


        //    connection.Close();

        //    BindingContext = this;
        //}




        /*  <!--           Location = mTimh,
                         ImageUrl = mPoso,
                         idPEL = mPROSU,
                         Prood= midEggtim, 
                         Name = mOno,
                         ID=mCOMMENTS   -->    */



        private async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

            // PARAGG -> NUM1 = TROPOS PLHROMHS  1,2,3     -1=ΔΙΑΓΡΑΦΤΗΚΕ
            // ENERGOS=1  ΤΥΠΟΜΕΝΟ

            string action = await DisplayActionSheet("Επιλογή", "Ακυρο", null, "1.Μετρητά Πληρωμή", "2.Κάρτα Πληρωμή", "3.Κερασμένα", "4.Διαγραφή","5.Αλλαγή Ποσότητας");
            if (action.Substring(0, 1) == "Α") { return; }


            Monkey2 tappedItem = e.Item as Monkey2;
            //   LIDtest.Text = e.ItemIndex.ToString();
            string cc = tappedItem.Prood;
            string mc = tappedItem.Name;


            string mt = tappedItem.Location;
            mt = mt.Replace(",", ".");
            string mp = tappedItem.ImageUrl;
            //           ImageUrl = mPoso




           // string[] lines = cc.Split('~');

            string cTYPOMENO = AllRead("SELECT  ISNULL(ENERGOS,0) AS TYP FROM PARAGG WHERE   ID=" + cc);
          if (action.Substring(0, 1) == "4")
          {
              
                 if (cTYPOMENO=="1")
                 {
                    await DisplayAlert("αδυνατη η διαγραφή", "ΕΙΝΑΙ ΤΥΠΩΜΕΝΟ", "OK");
                    return;

                 }
                
                
                
                
                if (cc.Substring(0, 2) == "**")
                {
                    await DisplayAlert("αδυνατη η διαγραφή", "εγινε πληρωμή", "OK");
                }
                else
                {
                   AllExecute("delete from PARAGG WHERE ID=" + cc);
                }
                return;
          }


            if (action.Substring(0, 1) == "5")  // αλλαγη βαρους
            {

                string cc2 = await DisplayPromptAsync("Δώσε Ποσότητα/Βάρος σε Kιλ", "π.χ. 500γρ=> 0,5");
               AllExecute("UPDATE  PARAGG SET POSO=" + cc2.Replace(",", ".") + " WHERE ID=" + cc);
              //  string caji2 = AllRead("SELECT str(round(SUM(POSO*TIMH),2),6,2 ) FROM PARAGG WHERE IDPARAGG=" + Globals.gIDPARAGG + "");
               AllExecute("UPDATE PARAGGMASTER SET AJIA=(SELECT SUM(POSO*TIMH) FROM PARAGG WHERE IDPARAGG=" + Globals.gIDPARAGG+" ) WHERE  ID=" + Globals.gIDPARAGG + ";");

                Show_listsql_Paragg(Globals.gIDPARAGG);
                pageModel = new Main2PageModel(this);
                BindingContext = pageModel;






                // string cc2 =  await DisplayPromptAsync("Δώσε Ποσότητα/Βάρος σε Kιλ", "π.χ. 500γρ=> 0,5");






                //  AllExecute("UPDATE  PARAGG SET POSO="+cc2.Replace(",",".")+" WHERE ID=" + cc);


            }











            //------------- PLHRVMH ------------------------


            if (action.Substring(0, 1) == "1" || action.Substring(0, 1) == "2" || action.Substring(0, 1) == "3")
            {
                if (Globals.gEKTPRINPLIR == 1)
                {
                    if (cTYPOMENO == "0")
                    {
                        await DisplayAlert("ΑΔΥΝΑΤΗ Η ΠΛΗΡΩΜΗ", "ΔΕΝ ΕΙΝΑΙ ΤΥΠΩΜΕΝΟ", "OK");
                        return;
                    }
                }


                if (cc.Substring(0, 2) == "**")
                {
                    await DisplayAlert("αδυνατη η πληρωμή", "ειναι πληρωμένο", "OK");
                }
                else
                {


                   AllExecute("UPDATE  PARAGG SET ONO='**" + action.Substring(0, 1) + "-'+ONO , NUM1=" + action.Substring(0, 1) + " WHERE ID=" + cc);
                    if (action.Substring(0, 1) == "1")
                    {
                       AllExecute("UPDATE  PARAGGMASTER SET CASH=ISNULL(CASH,0)+" + mt + " WHERE ID=" + Globals.gIDPARAGG.ToString());
                    }
                    if (action.Substring(0, 1) == "2")
                    {
                       AllExecute("UPDATE  PARAGGMASTER SET PIS1=ISNULL(PIS1,0)+" + mt + " WHERE ID=" + Globals.gIDPARAGG.ToString());
                    }


                    if (action.Substring(0, 1) == "3")
                    {
                       AllExecute("UPDATE  PARAGGMASTER SET KERA=ISNULL(KERA,0)+" + mt + " WHERE ID=" + Globals.gIDPARAGG.ToString());
                    }
                }




                // ΑΞΙΑ ΧΩΡΙΣ ΤΟ ΠΛΗΡΩΜΕΝΟ
                string caji = AllRead("SELECT cast(round(SUM(POSO*TIMH),2) as varchar ) FROM PARAGG WHERE NUM1=0 AND  IDPARAGG=" + Globals.gIDPARAGG + "");
                // ΑΞΙΑ ΗΔΗ ΠΛΗΡΩΜΕΝΩΝ
                string cPLIR = AllRead("SELECT cast(round(SUM(POSO*TIMH),2) as varchar ) FROM PARAGG WHERE NUM1>0 AND  IDPARAGG=" + Globals.gIDPARAGG + "");

               AllExecute("update PARAGGMASTER SET AJIA=" + caji.Replace(",", ".") + ",NUM1=" + cPLIR.Replace(",", ".") + " WHERE  WHERE ID=" + Globals.gIDPARAGG + ";");

                //AllExecute("update PARAGGMASTER SET NUM1=" + caji.Replace(",", ".") + " WHERE  WHERE ID=" + Globals.gIDPARAGG + ";");

               AllExecute("update TABLES SET KATEILHMENO=1,CH1='" + caji.Replace(",", ".") + "' WHERE IDPARAGG=" + Globals.gIDPARAGG + "");
                Show_listsql_Paragg(Globals.gIDPARAGG);
                pageModel = new Main2PageModel(this);
                BindingContext = pageModel;
            }



        }

        private async void doit2(object sender, ItemTappedEventArgs e)
        {
            Main2PageModel tappedItem = e.Item as Main2PageModel;
            Globals.gKathg = e.Item.ToString();
            //  int idx = Items.IndexOf("BBB");

            //change Core.Models.Data.TaskItem to the object you want to cast to.
            // var SelectedItem = (Core.Models.Data.TaskItem)e.SelectedItem;

            // await DisplayAlert("Info", $"{SelectedItem.Description}}", "Ok");
            //where SelectedItem.Description is a field in my model
            string cc= AllRead("SELECT CAST(ISNULL(ID,0) AS VARCHAR ) FROM KATHG WHERE ONO='" + e.Item.ToString() + "'");
            Globals.gKathg = cc;

            //string[] lines = Globals.gKathg.Split(';');

            //// αδειο τρπαζζι  π.χ. 12 345
            //if (lines.Length > 1)
            //{
            //    Globals.gKathg = lines[1];

            //}
            //else
            //{
            //    Globals.gKathg = "0";
            //}

            // DisplayAlert("Τραπέζι Νο ", e.Item.ToString(), "Ok");
            await Navigation.PushAsync(new trapeziEpil());  //imports






        }

        private void exodos(object sender, EventArgs e)
        {

        }

        private async void typlog(object sender, EventArgs e)
        {
            try
            {
              printing(1);
            }
            
             catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
           

        }

        private void apoth(object sender, EventArgs e)
        {
              Navigation.PopAsync(); // new PelReports());
        }

        void print(Stream outs, string qq)
        {
            byte[] toBytes;
            string fff;
            fff = qq; // toGreek(qq);

            toBytes = Encoding.Unicode.GetBytes(fff);
            outs.Write(toBytes, 0, toBytes.Length);



        }

        private async void printingBill(int part) //  object sender, EventArgs e)
        {
            string ipAddress = Globals.gIPKleis;//cFORTcIPPR1; // "192.168.1.120";
            int portNumber = 9100;
            List<string> myText = new List<string>();
            List<string> myTextTitlos = new List<string>();
            DataTable dt;
            string que;
                que= "SELECT   P.ONO AS ONO, SUM(P.POSO) as POSO,  P.TIMH AS TIMH ,SUM(P.POSO*P.TIMH) AS AXIA ,K.CH1 " +
                " FROM PARAGG P  inner join EIDH  E ON E.ONO=P.ONO  " +
                " INNER JOIN KATHG K ON E.KATHG=K.ID " +
                " where P.NUM1=0 AND  IDPARAGG =  " + Globals.gIDPARAGG + " GROUP BY P.TIMH,P.ONO,K.CH1 ORDER BY K.CH1";

            //que = "SELECT   P.ONO, SUM(P.POSO) as POSO,  P.TIMH ,SUM(P.POSO*P.TIMH) AS AXIA  " +
            //    "FROM PARAGG P    " +               
            //    "where P.NUM1=0 AND  P.IDPARAGG =  " + Globals.gIDPARAGG + " GROUP BY P.TIMH,P.ONO";

            try
            {



                // dt = ReadSQLServer (que); 
                // }
                // Monkeys.Add(new Monkey
                myText.Add((MainPage.ToGreek737(Globals.gTITLOS)));

                myText.Add("ΣΕΡΒ.:"+Globals.gUserWaiter.ToString());
                //myText.Add(Globals.gTrapezi.ToString() + MainPage.ToGreek737(" * TΡΑΠΕΖΙ * ")) ;
                string DDD = MainPage.ToGreek737(titlos.Text + "  ." + "ΣΕΡΒ.:" + Globals.gUserWaiter.ToString());
                myText.Add(DDD);
                //myText.Add(Globals.gTrapezi.ToString() +MainPage.ToGreek737( " * TΡΑΠΕΖΙ * ") +Globals.gTrapezi.ToString ()  + ".                \r\n");

                myText.Add("-\r\n");
                myText.Add("\r\n");
                //  string BIG="";
                float ss = 0;
                string PROS = "";

                if (Globals.gLocal == "0")
                {  //=======================sqlserver=========
                    dt = ReadSQLServer(que);
                    for (int k = 0; k <= dt.Rows.Count - 1; k++)
                    {
                        //ola
                        myText.Add((MainPage.ToGreek737(dt.Rows[k]["ONO"].ToString() + "                                               ").Substring(0, 40)));
                        myText.Add("              " + dt.Rows[k]["POSO"].ToString() + "  X  " + MainPage.Right("    " + String.Format("{0:0.00}", dt.Rows[k]["TIMH"]), 5) + "       " + MainPage.Right("   " + String.Format("{0:0.00}", dt.Rows[k]["AXIA"]), 6));
                        ss = ss + float.Parse(dt.Rows[k]["AXIA"].ToString());
                        //  BIG = BIG + MainPage.ToGreek737(dt.Rows[k]["PROSUETA"].ToString().Substring(0, 19)) + " " + MainPage.ToGreek737(dt.Rows[k]["SXOLIA"].ToString().Substring(0, 29));
                        // myText.Add("\r\n");
                    }
                }
                else
                {
                    string dbPath = Path.Combine(
                                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                    "adodemo.db3");
                    SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
                    connection.Open();
                    var contents = connection.CreateCommand();
                    contents.CommandText = que;
                    var r = contents.ExecuteReader();
                    int k = 0;
                    while (r.Read())
                    {
                        //ola
                        myText.Add((MainPage.ToGreek737(r["ONO"].ToString() + "                                               ").Substring(0, 40)));
                        myText.Add("              " + r["POSO"].ToString() + "  X  " + MainPage.Right("    " + String.Format("{0:0.00}", r["TIMH"]), 5) + "       " + MainPage.Right("   " + String.Format("{0:0.00}", r["AXIA"]), 6));
                        ss = ss + float.Parse(r["AXIA"].ToString());

                    }


                }





                if (part == 1)
                {
                    myText.Add("              " + "                                                                          " + MainPage.ToGreek737(ss.ToString()));

                }
                else
                {
                    myText.Add("\r\n");
                    myText.Add("              " + MainPage.ToGreek737("ΣΥΝΟΛΟ ") + "           " + MainPage.Right("   " + String.Format("{0:0.00}", ss), 6));
                }


                DataTable dt2;
                string ekpt = "0";
                ekpt = Globals.AllRead("SELECT   cast(ISNULL(PIS2,0) as varchar ) AS PIS2 FROM PARAGGMASTER  where   ID = " + Globals.gIDPARAGG + " ; ");
                // ekpt = dt2.Rows[0]["PIS2"].ToString();
                if (ekpt.Length == 0) { ekpt = "0"; }
                ekpt = ekpt.Replace(".", ",");

                if (float.Parse(ekpt) == 0) // ΔΕΝ ΕΧΕΙ ΑΠΟΘΗΚΕΥΜΕΝΗ ΕΚΠΤΩΣΗ
                {

                    // string caji = ypol.ToString().Replace (",",".");
                    ekpt = await DisplayPromptAsync("Ποσό Εκπτωσης/Στρογγυλοποίησης", "Συνολο " + ss + "€");




                    if (ekpt == null) ekpt = "0";

                    if (ekpt.Length == 0)
                    {
                        ekpt = "0";
                    }
                    else
                    {
                        if (IsNumeric(ekpt)) { }
                        else
                        {
                            ekpt = "0";
                            await DisplayAlert("Error", "λαθος εκπτωση", "..");
                            return;
                        }

                        ekpt = ekpt.Replace(".", ",");
                    }

                    if (ekpt == "0") { }
                    else
                    {
                        AllExecute("UPDATE  PARAGGMASTER SET  PIS2=ISNULL(PIS2,0)+" + ekpt.Replace(",", ".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());


                    }



                }

                if (ekpt == "0") { }
                else
                {
                    //Globals.ExecuteSQLServer("UPDATE  PARAGGMASTER SET  PIS2=ISNULL(PIS2,0)+" + ekpt.Replace(",", ".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());

                    float nekpt = 0;
                    nekpt = float.Parse(ekpt.Replace(".", ","));
                    myText.Add("              " + MainPage.ToGreek737("εκπτωση") + "           " + MainPage.Right("   " + String.Format("{0:0.00}", nekpt), 6));
                    //if (ss - nekpt < 0)
                    //    {
                    //        await DisplayAlert("Error", "Λάθος έκπτωση", "");
                    //        return;
                    //    }
                    myText.Add("              " + MainPage.ToGreek737("Πληρωτέο ") + "         " + MainPage.Right("   " + String.Format("{0:0.00}", ss - nekpt), 6));

                    string synolon = String.Format("{0:0.00}", ss - nekpt);

                    AllExecute("UPDATE TABLES SET CH1='" + synolon + "' WHERE ONO='" + Globals.gTrapezi + "'");

                }
                AllExecute("UPDATE  PARAGGMASTER SET  NUM2=1 WHERE ID=" + Globals.gIDPARAGG.ToString());
                myText.Add("\r\n");
                myText.Add("\r\n");
                myText.Add("\r\n");
                myText.Add("\r\n");
                myText.Add("\r\n");
                myText.Add("\r\n");
                MainPage.SaveFile(myText, "Bill");

                var printer = DependencyService.Get<test4sql.iPrinter>();
                if (printer == null)
                {
                    await DisplayAlert("Error", "δεν υπαρχει συνδεση", "ok");
                    return;
                }
                try
                {
                    // 'τυπωνω τον τιτλο πρωτα'
                    //MainPage.BigLetters(ipAddress);
                    //myTextTitlos.Add((MainPage.ToGreek737(Globals.gTITLOS)));
                    //printer.Print(ipAddress, portNumber, myText);


                    PrintSmall(ipAddress);
                    /*
                       List<byte> outputList1 = new List<byte>();

                       //outputList1.Add(141);
                       // BIG LETTERS  2H


                       outputList1.Add(0x1D);
                       outputList1.Add(0x21);
                       outputList1.Add(0x11);  // 01 BIG HEIGHT   10 BIG WIDTH  11 BOTH BIGG   00 NOTHING BIG


                       //outputList1.Add(0x1B);
                       //outputList1.Add(0x4C);



                       // TEST ΒΓΔ  ΗΕΧ
                       //outputList1.Add(0x81);
                       //outputList1.Add(0x82);
                       //outputList1.Add(0x83);
                       //outputList1.Add(141);// ΤΕΣΤ ΝΕΧΤ 3  
                       //outputList1.Add(142);
                       //outputList1.Add(143);
                       //    byte[] bytes = Encoding.ASCII.GetBytes(BIG);

                       Socket pSocket1 = new Socket(SocketType.Stream, ProtocolType.IP);
                       pSocket1.Connect(ipAddress, portNumber);
                       pSocket1.Send(outputList1.ToArray());
                       pSocket1.Close();
                    */
                    //---------------------------------------------------------------------------
                    printer.Print(ipAddress, portNumber, myText);
                    //---------------------------------------------------------------------------
                    CutPaper(ipAddress);
                    /*
                       List<byte> outputList = new List<byte>();
                       //  CUT PAPER NEXT 2 A);
                       outputList.Add(0x1B);
                       outputList.Add(0x69);
                       // BIG LETTERS ΨΑΝΨΕΚ
                       outputList1.Add(0x1D);
                       outputList1.Add(0x21);
                       outputList1.Add(0x00);
                       Socket pSocket = new Socket(SocketType.Stream, ProtocolType.IP);
                       // Connect to the printer
                       pSocket.Connect(ipAddress, portNumber);

                       // ToDo: Send some commands to the printer
                       // Send the command to the printer
                       pSocket.Send(outputList.ToArray());
                       // Close the socket connection when done
                       pSocket.Close();
                    */
                    // AllExecute("update PARAGG set ENERGOS=1 where IDPARAGG = " + Globals.gIDPARAGG);

                }

                catch (Exception ex)
                {
                    await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
                    // await DisplayAlert("error2", "", "");
                }


            }
            catch
            {

            }


        }



        public static void CutPaper(string ipAddress)
        {
            try
            {


                List<byte> outputList1 = new List<byte>();

                outputList1.Add(0x1B);
                outputList1.Add(0x69);


                Socket pSocket1 = new Socket(SocketType.Stream, ProtocolType.IP);
                // Connect to the printer
                pSocket1.Connect(ipAddress, 9100);
                pSocket1.Send(outputList1.ToArray());
                pSocket1.Close();
            }
            catch
            {

            }

        }

        private async void PrintParWin(int part)
        {

            // ΔΙΑΒΑΖΩ ΤΟ SQLITE GIA NA TA METAFERC STO SQLSERVER
            Globals.gconnection.Open();
            // query the database to prove data was inserted!
            var contents = Globals.gconnection.CreateCommand();
           string Query="SELECT  IFNULL(IDPARAGG,0) AS IDPARAGG,IFNULL(TRAPEZI,0) AS TRAPEZI,IFNULL(PARAGG.ONO,'') AS ONO, IfnULL(POSO,0) as POSO, IfNULL(PARAGG.TIMH,0) AS TIMH,PARAGG.ID,IfNULL(PROSUETA,'') AS PROSUETA,IfNULL(PARAGG.CH1,'') AS CH1,IfNULL(POSO*PARAGG.TIMH,0) AS AXIA , IfNULL(EIDH.NUM1,1) as PRINTER  " +
                          " FROM PARAGG INNER JOIN EIDH ON PARAGG.ONO=EIDH.ONO " +
                          " INNER JOIN KATHG ON EIDH.KATHG=KATHG.ID where ifnull(ENERGOS,0)=0 AND PARAGG.IDPARAGG = " + Globals.gIDPARAGG + "  order by KATHG.CH1 ; ";
            contents.CommandText = Query;
            var r = contents.ExecuteReader();
            
            string cc = "";
            int ncount = 0;
            string mCASH = "", mPIS1 = "", mPIS2 = "", mKERA = "";

            string Q = "";
            while (r.Read())
            {
                ncount = ncount + 1;
                Q = "INSERT INTO PARAGGPDA (PRINTER,IDPARAGG,TRAPEZI,ONO,PROSUETA,CH1SXOLIA,HME,POSO,TIMH,PDA) VALUES (";
                Q = Q + r["printer"].ToString ()+"," +r["idparagg"].ToString ()+",'"+r["trapezi"].ToString()+ "','" + r["ONO"].ToString() + "','" + r["PROSUETA"].ToString();
                Q = Q + "','" + r["CH1"].ToString() + "',GETDATE()," + r["poso"].ToString() + "," + r["timh"].ToString ()+"," + Globals.gUserWaiter + ")";
                if(Globals.ExecuteSQLServer(Q, 1) == 1)
                {
                    MainPage.ExecuteSqlite("update PARAGG SET ENERGOS=1 where  IDPARAGG = " + Globals.gIDPARAGG);
                }

            }
            Globals.gconnection.Close();





        }




        private async void printing(int part) //  object sender, EventArgs e)
        {


            if (Globals.gLocal == "3")
            {  //=============
                PrintParWin(1);
                return;
            }



                string ipAddress = Globals.cIPPR1; // "192.168.1.120";
            int portNumber = 9100;
            List<string> myText = new List<string>();
            List<string> myText2 = new List<string>();
            List<string> myText3 = new List<string>();
            DataTable dt;

            // {PARAGGELIES.toGreek( "ΓΕΙΑ ΣΟΥ ΜΕΓΑΛΕ ΜΟΥ"),"From","Replace","MrNashad","Please Like"};
            // if (part == 1) // τα νεα μονο
            // {

            string que;
            //que= "SELECT   P.ONO, SUM(P.POSO) as POSO,  P.TIMH ,SUM(P.POSO*P.TIMH) AS AXIA ,K.CH1 " +
            //"FROM PARAGG P  inner join EIDH  E ON E.ONO=P.ONO  " +
            //"INNER JOIN KATHG K ON E.KATHG=K.ID " +
            //"where P.NUM1=0 AND  IDPARAGG =  " + Globals.gIDPARAGG + " GROUP BY P.TIMH,P.ONO,K.CH1 ORDER BY K.CH1";






                      //}
           // else
           // {     // ολα 
                // dt = ReadSQLServer("SELECT  ISNULL(ONO,'')+SPACE(32) AS ONO, ISNULL(POSO,0) as POSO, ISNULL(TIMH,0) AS TIMH,ID,ISNULL(PROSUETA,'')+SPACE(22) AS PROSUETA,LEFT(ISNULL(CH1,'')+SPACE(31),31) AS SXOLIA,ISNULL(POSO*TIMH,0) AS AXIA  FROM PARAGG where NUM1=0 AND  IDPARAGG = " + Globals.gIDPARAGG + "  order by ID ; ");
           // }
            // Monkeys.Add(new Monkey

            //myText.Add(Globals.gTrapezi.ToString() + MainPage.ToGreek737(" * TΡΑΠΕΖΙ * ")) ;
            string DDD = MainPage.ToGreek737(titlos.Text+" " + "ΣΕΡΒ.:" + Globals.gUserWaiter.ToString());
            myText.Add(DDD+"  "+ MainPage.ToGreek737(cmdtimologio.Text + "  ."));
            myText2.Add(DDD);
            myText3.Add(DDD);

            //myText.Add(Globals.gTrapezi.ToString() +MainPage.ToGreek737( " * TΡΑΠΕΖΙ * ") +Globals.gTrapezi.ToString ()  + ".                \r\n");

            myText.Add("\r\n");
            myText.Add("\r\n");

            myText2.Add("\r\n");
            myText2.Add("\r\n");

            myText3.Add("\r\n");
            myText3.Add("\r\n");




            //  string BIG="";
            float ss = 0;
            string PROS="";
            int i1, i2, i3;
            i1 = 0;
            i2 = 0;
            i3 = 0;
            string KANON = "";

            string sqlq1 = "SELECT  ISNULL(PARAGG.ONO,'')+SPACE(32) AS ONO, ISNULL(POSO,0) as POSO, ISNULL(PARAGG.TIMH,0) AS TIMH,PARAGG.ID,ISNULL(PROSUETA,'')+SPACE(22) AS PROSUETA,LEFT(ISNULL(PARAGG.CH1,'')+SPACE(31),31) AS SXOLIA,ISNULL(POSO*PARAGG.TIMH,0) AS AXIA , ISNULL(EIDH.NUM1,1) as PRINTER  " +
                           " FROM PARAGG INNER JOIN EIDH ON PARAGG.ONO=EIDH.ONO " +
                           " INNER JOIN KATHG ON EIDH.KATHG=KATHG.ID where (ENERGOS IS NULL) AND PARAGG.IDPARAGG = " + Globals.gIDPARAGG + "  order by KATHG.CH1 ; ";
            //dt = ReadSQLServer(sqlq1);

            if (Globals.gLocal == "0") {  //=======================sqlserver==============================
                dt = ReadSQLServer(sqlq1);
                for (int k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    // if (dt.Rows[k]["ONO"].ToString()=="1")
                    // {
                    KANON = dt.Rows[k]["POSO"].ToString().Trim() + "x" + (MainPage.ToGreek737(dt.Rows[k]["ONO"].ToString().Trim())); // + "        " + dt.Rows[k]["TIMH"].ToString() + "    " + dt.Rows[k]["AXIA"].ToString()) ;

                    // ΚΑΝΟΝΙΚΑ ΠΡΕΠΕΙ ΝΑ ΧΩΡΙΖΕΙ ΤΟΝ ΠΡΙΝΤΕΡ 1 ΑΠΟ ΤΟΥΣ ΑΛΛΟΥΣ 
                    // ΑΛΛΑ ΤΟ ΕΛΛΗΝΙΚΟ ΘΕΛΕΙ Ο 1 ΕΚΤ ΝΑ ΠΑΙΡΝΕΙ ΟΛΑ ΤΑ ΕΙΔΗ  Globals.Ola1Printer = 1

                    if (Globals.gOla1Printer >= 1) {
                        // ΟΛΑ ΤΑ ΕΙΔΗ ΤΟ 1 (ΤΑ ΔΙΚΑ ΤΟΥ + 2+ 3) ORDER BY KATHG
                        myText.Add(KANON.Trim());
                        i1 = i1 + 1;
                    
                    }
                    else
                    {
                        if (dt.Rows[k]["PRINTER"].ToString() == "1")
                        {
                            myText.Add(KANON.Trim());
                            i1 = i1 + 1;
                        }
                    }

                    if (Globals.gOla1Printer >= 2)
                    {
                        // ΟΛΑ ΤΑ ΕΙΔΗ ΤΟ 1 (ΤΑ ΔΙΚΑ ΤΟΥ + 2+ 3) ORDER BY KATHG
                        myText.Add(KANON.Trim());
                        i1 = i1 + 1;

                    }
                    else
                    {
                        if (dt.Rows[k]["PRINTER"].ToString() == "2")
                        {
                            myText2.Add(KANON.Trim());
                            i2 = i2 + 1;
                        }
                    }


                    if (Globals.gOla1Printer >= 3)
                    {
                        // ΟΛΑ ΤΑ ΕΙΔΗ ΤΟ 1 (ΤΑ ΔΙΚΑ ΤΟΥ + 2+ 3) ORDER BY KATHG
                        myText.Add(KANON.Trim());
                        i1 = i1 + 1;

                    }
                    else
                    {

                        if (dt.Rows[k]["PRINTER"].ToString() == "3")
                        {
                            myText3.Add(KANON.Trim());
                            i3 = i3 + 1;
                        }
                    }







                    PROS = MainPage.ToGreek737(dt.Rows[k]["PROSUETA"].ToString().Substring(0, 19).TrimEnd()) + "*" + MainPage.ToGreek737(dt.Rows[k]["SXOLIA"].ToString().Substring(0, 29));
                    if (PROS.Trim().Length > 0)
                    {
                        myText.Add(PROS.Trim());
                        i1 = i1 + 1;
                        
                            if (dt.Rows[k]["PRINTER"].ToString() == "2")
                            {
                                myText2.Add(PROS.Trim());
                                i2 = i2 + 1;
                            }
                            if (dt.Rows[k]["PRINTER"].ToString() == "3")
                            {
                                myText3.Add(PROS.Trim());
                                i3 = i3 + 1;
                            }
                        
                    }
                }
            } else  { //==============  sqlite =============================
                      //sqlq1 = "SELECT  IfNULL(PARAGG.ONO,'') AS ONO, IfnULL(POSO,0) as POSO, IfNULL(PARAGG.TIMH,0) AS TIMH,PARAGG.ID,IfNULL(PROSUETA,'') AS PROSUETA,IfNULL(PARAGG.CH1,'') AS SXOLIA,IfNULL(POSO*PARAGG.TIMH,0) AS AXIA , IfNULL(EIDH.NUM1,1) as PRINTER  " +
                      //          " FROM PARAGG INNER JOIN EIDH ON PARAGG.ONO=EIDH.ONO " +
                      //          " INNER JOIN KATHG ON EIDH.KATHG=KATHG.ID where (ENERGOS IS NULL) AND PARAGG.IDPARAGG = " + Globals.gIDPARAGG + "  order by KATHG.CH1 ; ";


                sqlq1 = "SELECT  ISNULL(PARAGG.ONO,'') || printf('%*s', 32, ' ') AS ONO, ISNULL(POSO,0) as POSO " +
                    ", ISNULL(PARAGG.TIMH,0) AS TIMH,PARAGG.ID,ISNULL(PROSUETA,'')|| printf('%*s', 22, ' ') AS PROSUETA " +
                   ", substr(ISNULL(PARAGG.CH1,'')|| printf('%*s', 31, ' '),1,31) AS SXOLIA,ISNULL(POSO*PARAGG.TIMH,0) AS AXIA  " +
                    ", ISNULL(EIDH.NUM1,1) as PRINTER  " +
                          " FROM PARAGG INNER JOIN EIDH ON PARAGG.ONO=EIDH.ONO " +
                          " INNER JOIN KATHG ON EIDH.KATHG=KATHG.ID where (ENERGOS IS NULL) AND PARAGG.IDPARAGG = " + Globals.gIDPARAGG + "  order by KATHG.CH1 ; ";





                string dbPath = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                 "adodemo.db3");
                SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
                connection.Open();

               // gemizei ta text,text2,text3 rolarontas to paragg
               
                try
                {
                    Globals.indexParaggLine = -1;  // για να καταλαβαίνω αν ειναι αδεια η παραγγελία

                    sqlq1 = sqlq1.ReplaceAll("ISNULL", "ifnull");
                    //sqlq1 = sqlq1.ReplaceAll("+", " || ");

                    var contents = connection.CreateCommand();
                    contents.CommandText = sqlq1;
                    var r = contents.ExecuteReader();
                    int k = 0;
                    while (r.Read())
                    {
                        KANON = r["POSO"].ToString().Trim() + "x" + (MainPage.ToGreek737(r["ONO"].ToString().Trim()));
                        // ΟΛΑ ΤΑ ΕΙΔΗ ΤΟ 1 (ΤΑ ΔΙΚΑ ΤΟΥ + 2+ 3) ORDER BY KATHG
                        myText.Add(KANON.Trim());
                        i1 = i1 + 1;
                        
                            if (r["PRINTER"].ToString() == "2")
                            {
                                myText2.Add (KANON.Trim());
                                i2 = i2 + 1;
                            }
                            if (r["PRINTER"].ToString() == "3")
                            {
                                myText3.Add(KANON.Trim());
                                i3 = i3 + 1;
                            }

                        PROS = MainPage.ToGreek737(r["PROSUETA"].ToString().Substring(0, 19).TrimEnd()) + "*" + MainPage.ToGreek737(r["SXOLIA"].ToString().Substring(0, 29));
                        if (PROS.Trim().Length > 0)
                        { 
                            myText.Add(PROS.Trim());
                            i1 = i1 + 1;
                            
                                if (r["PRINTER"].ToString() == "2")
                                {
                                    myText2.Add(PROS.Trim());
                                    i2 = i2 + 1;
                                }
                                if (r["PRINTER"].ToString() == "3")
                                {
                                    myText3.Add(PROS.Trim());
                                    i3 = i3 + 1;
                                }
                            
                        }


                    }



}
                //catch
                //{
                //    //ff = "error";
                //}
                catch (Exception ex)
                {
                    await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
                }
                connection.Close();

            }








          
                string cc1="                                                                          "+MainPage.ToGreek737( ss.ToString());
               myText.Add(cc1);
               myText2.Add(cc1);
               myText3.Add(cc1);

           
               
            myText.Add( "\r\n");
            myText.Add("\r\n");
            myText.Add("\r\n");
            myText.Add("\r\n");
            myText.Add("\r\n");
            myText.Add("\r\n");

            myText2.Add("\r\n");
            myText2.Add("\r\n");
            myText2.Add("\r\n");
            myText2.Add("\r\n");
            myText2.Add("\r\n");
            myText2.Add("\r\n");


            myText3.Add("\r\n");
            myText3.Add("\r\n");
            myText3.Add("\r\n");
            myText3.Add("\r\n");
            myText3.Add("\r\n");
            myText3.Add("\r\n");

            MainPage.SaveFile(myText,"kitch");

            var printer = DependencyService.Get<test4sql.iPrinter>();
            if (printer == null)
            {

                await DisplayAlert("Error", "δεν υπαρχει συνδεση", ".");
                return;

            }
            try
            {
                
                // gia kaue ektypvth exei metrhth i1,i2,i3 kai typvnei ta antistoixa myText,myText2,myText3
                
                //printthis : typvnei to mytext kai bobei to xarti 
                int Ok = 0;
                int must = 0;
                if (i1 > 0){ 
                    Ok=Ok+printthis(myText, Globals.cIPPR1);
                    
                    must = must + 1;
                    
                   
                }

                
                if (i2 > 0) {Ok=Ok+ printthis(myText2, Globals.cIPPR2); 
                    
                    must = must + 1;
                }
               
                if (i3 > 0) {Ok=Ok+ printthis(myText3, Globals.cIPPR3);
                    
                    must = must + 1;
                }
                if (Ok == must)
                {
                   AllExecute("update PARAGG set ENERGOS=1 where IDPARAGG = " + Globals.gIDPARAGG);
                }
                else
                {
                    await DisplayAlert("αδυναμια εκτυπωσης ", "", "OK"); // ex.ToString()
                }
                             





            }
            
             catch (Exception ex)
            {
                await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
               // await DisplayAlert("error2", "", "");
            }
          



          






        }


        private void PrintSmall(string ipAddress)
        {
            try
            {


                List<byte> outputList1 = new List<byte>();
                outputList1.Add(0x1B);
                outputList1.Add(0x40);

                //outputList1.Add(0x1D);
                //outputList1.Add(0x21);
                //outputList1.Add(0x00);
                Socket pSocket1 = new Socket(SocketType.Stream, ProtocolType.IP);
                // Connect to the printer
                pSocket1.Connect(ipAddress, 9100);
                pSocket1.Send(outputList1.ToArray());
                pSocket1.Close();
            } catch
            {

            }

        }






        private  int printthis(List<string> mytext,string ipAddress)
        {


            
            // typvnei to mytext kai bobei to xarti






            int Ok = 0;

            //string ipAddress = Globals.cIPPR1; // "192.168.1.120";
            int portNumber = 9100;
            var printer = DependencyService.Get<test4sql.iPrinter>();
            if (printer == null)
            {
               // await DisplayAlert("Error", "δεν υπαρχει συνδεση", "");
                return Ok;
            }
            try
            {
                int l = MainPage.LF2(ipAddress);
                if (l==0)  {
                    Ok = 0;
                    return 0;
                }
                MainPage.LF(ipAddress);
                
                MainPage.LF(ipAddress);
                MainPage.BigLetters(ipAddress);
                printer.Print(ipAddress, portNumber, mytext);
                MainPage.CutPaper(ipAddress);
                Ok = 1;

            }

            catch (Exception ex)
            {
                // await DisplayAlert("αδυναμια εκτυπωσης ", "", "OK"); // ex.ToString()
                Ok = 0;
                // await DisplayAlert("error2", "", "");
            }
            return Ok;

        }





        public static  DataTable ReadSQLServer(string cSQL)

        {
            DataTable dt = new DataTable();
            //  -----------------SQLSERVER  1.SYNDESH   ---------------------------------------
            if (Globals.cSQLSERVER.Length < 2)
            {
                //  await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return dt;
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";



            // private SqlConnection con;
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();

            }
            catch (Exception ex)
            {
                 // DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }
try
{
    //Monkeys = new List<Monkey>();
   
                
                // ΤΟ ΠΑΡΑΚΑΤΩ ΠΗΓΑΙΝΕ ΠΑΝΤΟΥ ΚΑΙ ΜΕ ΜΠΕΡΔΕΥΕ ΕΝΩ ΠΡΕΠΕΙ ΝΑ ΕΙΝΑΙ ΜΕΤΑ ΤΗΝ ΠΛΗΡΩΜΗ
                //  Globals.indexParaggLine = -1;  // για να καταλαβαίνω αν ειναι αδεια η παραγγελία
   
    SqlCommand cmd3 = new SqlCommand(cSQL , con);
    var adapter2 = new SqlDataAdapter(cmd3);
    adapter2.Fill(dt);
  
}
catch (Exception ex)
{
    //await DisplayAlert("Error", ex.ToString(), "OK");
}
            return dt;
}



        private void ShowItemsSqlite(string ono)
        {
            string dbPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    "adodemo.db3");
            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();
            string ff = "";
            Monkeys2 = new List<Monkey2>();
            BindingContext = null;
            try
            {
                Globals.indexParaggLine = -1;  // για να καταλαβαίνω αν ειναι αδεια η παραγγελία
                string Q = "SELECT  ISNULL(ONO,'') AS ONO, POSO, TIMH,ID,ISNULL(PROSUETA,'') AS PROSUETA,ISNULL(CH1,'') AS SXOLIA  FROM PARAGG where IDPARAGG = " + ono + "  order by ID ; ";
                Q = Q.ReplaceAll("ISNULL", "ifnull");

                var contents = connection.CreateCommand();
                contents.CommandText = Q;
                var r = contents.ExecuteReader();
                int k = 0;
                while (r.Read())
                {

                    string mPoso = (r["POSO"].ToString() + "                                   ").Substring(0, 3);
                    string mOno = (r["ONO"].ToString() + "                                   ").Substring(0, 28);
                    string midEggtim = (r["ID"].ToString() + "              ").Substring(0, 5);
                    string mTimh = (r["TIMH"].ToString() + "              ").Substring(0, 5);
                    string mPROSU = (r["PROSUETA"].ToString() + "              ").Substring(0, 11);
                    string mCOMMENTS = (r["SXOLIA"].ToString() + "                                           ").Substring(0, 31);
                    Monkeys2.Add(new Monkey2
                    {  // dt.Rows[k]["ONO"].ToString();
                        Name = mOno,

                        Location = mTimh,
                        ImageUrl = mPoso,
                        idPEL = mPROSU,   //+"  ~"+ midEggtim,
                        Prood = midEggtim,
                        ID = mCOMMENTS
                    });

                    Globals.PARAGGlines[k, 0] = mOno;
                    Globals.PARAGGlines[k, 1] = mPoso;
                    Globals.PARAGGlines[k, 2] = mTimh;
                    Globals.PARAGGlines[k, 3] = mPROSU;
                    Globals.PARAGGlines[k, 4] = midEggtim;
                    Globals.PARAGGlines[k, 5] = mCOMMENTS;
                    Globals.indexParaggLine = k;
                    k = k + 1;

                }
            }
            catch
            {
                ff = "error";
            }
            connection.Close();
            listERG.ItemsSource = Monkeys2;
            BindingContext = this;

            // listERG.ScrollTo(viewModel.DataCollection[viewModel.DataCollection.Count - 1], ScrollToPosition.End, true);
            //con.Close();
            BindingContext = this;

            if (listERG.ItemsSource != null)
            {
                var lastItem = listERG.ItemsSource.Cast<object>().LastOrDefault();
                listERG.ScrollTo(lastItem, ScrollToPosition.End, true);
            }



        }







        private  async void  Show_listsql_Paragg(string ono)
         {

            if (Globals.gLocal == "0")  // global sqlite or sqlserver
            { }
            else { 
                ShowItemsSqlite(ono);
                return;
            }




            Monkeys2 = new List<Monkey2>();
            BindingContext = null;
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

            }
            catch (Exception ex)
            {
                // await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }


           // String SYNT = "";

            try
            {
                //Monkeys = new List<Monkey>();
                Globals.indexParaggLine = -1;  // για να καταλαβαίνω αν ειναι αδεια η παραγγελία
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("SELECT  ISNULL(ONO,'') AS ONO, POSO, TIMH,ID,ISNULL(PROSUETA,'') AS PROSUETA,ISNULL(CH1,'') AS SXOLIA  FROM PARAGG where IDPARAGG = " + ono + "  order by ID ; ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    string mPoso = (dt.Rows[k]["POSO"].ToString() + "                                   ").Substring(0, 3);
                    string mOno= (dt.Rows[k]["ONO"].ToString() + "                                   ").Substring(0, 28);
                    string midEggtim = (dt.Rows[k]["ID"].ToString() + "              ").Substring(0, 5);
                    string mTimh = (dt.Rows[k]["TIMH"].ToString() + "              ").Substring(0, 5);
                    string mPROSU = (dt.Rows[k]["PROSUETA"].ToString() + "              ").Substring(0, 11);
                    string mCOMMENTS = (dt.Rows[k]["SXOLIA"].ToString() + "                                           ").Substring(0, 31);
                    Monkeys2.Add(new Monkey2
                    {  // dt.Rows[k]["ONO"].ToString();
                        Name = mOno,

                        Location = mTimh,
                        ImageUrl = mPoso,
                        idPEL = mPROSU,   //+"  ~"+ midEggtim,
                        Prood= midEggtim,
                        ID=mCOMMENTS 
                    }) ;

                    Globals.PARAGGlines[k, 0] = mOno;
                    Globals.PARAGGlines[k, 1] = mPoso;
                    Globals.PARAGGlines[k, 2] = mTimh;
                    Globals.PARAGGlines[k, 3] = mPROSU;
                    Globals.PARAGGlines[k, 4] = midEggtim;
                    Globals.PARAGGlines[k, 5] = mCOMMENTS;
                    Globals.indexParaggLine = k;
                } // FOR

              
                //  BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
            listERG.ItemsSource = Monkeys2;
            BindingContext = this;

            // listERG.ScrollTo(viewModel.DataCollection[viewModel.DataCollection.Count - 1], ScrollToPosition.End, true);
            con.Close();
            BindingContext = this;

            if (listERG.ItemsSource != null)
            {
                var lastItem = listERG.ItemsSource.Cast<object>().LastOrDefault();
                listERG.ScrollTo(lastItem, ScrollToPosition.End, true);
            }




           

        }

        private async void PLIROMI(object sender, EventArgs e)
        {
            try
            {

                if (Globals.gEKTPRINPLIR == 1)
                {
                    string catyp = AllRead("SELECT COUNT(*) AS TYP FROM PARAGG WHERE  (ENERGOS IS NULL) AND IDPARAGG=" + Globals.gIDPARAGG + "");
                    if (Int32.Parse(catyp) > 0)
                    {
                        await DisplayAlert("ΑΔΥΝΑΤΗ Η ΠΛΗΡΩΜΗ", "ΔΕΝ ΕΙΝΑΙ ΟΛΑ ΤΥΠΩΜΕΝΑ", "OK");
                        return;
                    }
                }
                if (Globals.gLOGPRINPLIR == 1)
                {
                    string catyp = AllRead("SELECT  cast(ISNULL(NUM2,0) as varchar ) AS ISTYP FROM PARAGGMASTER WHERE   ID=" + Globals.gIDPARAGG + "");
                    if (catyp.Length == 0) { catyp = "0"; }
                    if (Int32.Parse(catyp) == 0)
                    {
                        await DisplayAlert("ΑΔΥΝΑΤΗ Η ΠΛΗΡΩΜΗ", "ΔΕΝ ΤΥΠΩΘΗΚΕ ΛΟΓΑΡΙΑΣΜΟΣ", "OK");
                        return;
                    }
                }

            }
            catch
            {

            }


            // string proekpt = "";,ISNULL(NUM2,0) AS ISTYP
            // float nproekpt = 0;
            string caji = AllRead("SELECT CAST(ISNULL(AJIA,0)-ISNULL(CASH,0)-ISNULL(PIS1,0)-ISNULL(PIS2,0)-ISNULL(KERA,0) AS varchar ) AS AA  FROM PARAGGMASTER WHERE   ID=" + Globals.gIDPARAGG + "");
            // nproekpt = float.Parse(proekpt.Replace(",", "."));
            //  float nekpt = 0;
            // float  nekpt = float.Parse(ekpt);
            // float ypol = (nproekpt -nekpt);

            // string caji = ypol.ToString().Replace (",",".");
          //  string ekpt = await DisplayPromptAsync("Ποσό Εκπτωσης/Στρογγυλοποίησης", "Συνολο " +caji + "€");
           // if (ekpt.Length == 0) { ekpt = "0"; }

            string action = await DisplayActionSheet("Τρόπος Πληρωμής", caji+"€", null, "1.μετρητα", "2.Κάρτα", "3.Κερασμένα","4.Ακυρο");

           if (action.Substring(0, 1) == "4") { return; }
           if (action.Substring(0, 1) == " ") { return; }
           AllExecute("UPDATE TABLES SET KATEILHMENO=0,CH2='',CH1='',IDPARAGG=0 WHERE ONO='" + Globals.gTrapezi  + "'");
           if(Globals.gLocal == "0")
            {
                 AllExecute("UPDATE PARAGGMASTER SET CH2= CONVERT(CHAR(10),GETDATE(),103),TROPOS="+action.Substring(0,1)+"   WHERE ID=" + Globals.gIDPARAGG );

            }
            else
            {
                AllExecute("update PARAGGMASTER SET CH2=DATE(),TROPOS="+action.Substring(0,1)+"    WHERE ID=" + Globals.gIDPARAGG);
            }
        
            if (action.Substring(0, 1) == "1")
            {
               AllExecute("UPDATE  PARAGGMASTER SET CASH=ISNULL(CASH,0)+" + caji.Replace(",",".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());
               //AllExecute("UPDATE  PARAGGMASTER SET CASH=ISNULL(CASH,0)-" + ekpt.Replace(",", ".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());
            }
            if (action.Substring(0, 1) == "2")
            {
               AllExecute("UPDATE  PARAGGMASTER SET PIS1=ISNULL(PIS1,0)+" + caji.Replace(",", ".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());
              // AllExecute("UPDATE  PARAGGMASTER SET PIS1=ISNULL(PIS1,0)-" + ekpt.Replace(",", ".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());
            }
            if (action.Substring(0, 1) == "3")
            {
               AllExecute("UPDATE  PARAGGMASTER SET  KERA=ISNULL(KERA,0)+" + caji.Replace(",", ".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());
              // AllExecute("UPDATE  PARAGGMASTER SET  KERA=ISNULL(KERA,0)-" + ekpt.Replace(",", ".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());
            }
            //if (ekpt == "0") { }
            //else
            //{
            //   AllExecute("UPDATE  PARAGGMASTER SET  PIS2=ISNULL(PIS2,0)+" + ekpt.Replace(",", ".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());
            //}
            Globals.indexParaggLine = -1;  // για να καταλαβαίνω αν ειναι αδεια η παραγγελία

            await Navigation.PopAsync();


        }



        private void AllExecute (string Query)
        {
            if (Globals.gLocal=="0") {
                

                Globals.ExecuteSQLServer(Query);
            }
            else
            {
            Query = Query.ReplaceAll("ISNULL", "ifnull");
                Query = Query.ReplaceAll("GETDATE", "DATE");
                MainPage.ExecuteSqlite(Query);    
            }
           
        }
        // AllRead
        private string AllRead(string Query)
        {
            if (Globals.gLocal == "0")
            {
return Globals.ReadSQLServer(Query);

               
            }
            else
            {
               Query = Query.ReplaceAll("ISNULL", "ifnull");
                Query = Query.ReplaceAll("GETDATE", "DATE");
                return PARAGGELIES.ReadSQL(Query); ;  
            }

        }




        private async void TYPLOGALL(object sender, EventArgs e)
        {


           


            try
            {
                printingBill(2);
            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        private async void Allagh(object sender, EventArgs e)
        {
            string cc = await DisplayPromptAsync("Δώσε Αριθμό Νέου Τραπεζιού", "Μεταφορά σε τραπέζι");
            string iskat = AllRead("SELECT  ISNULL(KATEILHMENO,0) AS KATEILHMENO FROM TABLES WHERE   ONO='" + cc+"'");
            string user = AllRead("SELECT  ISNULL(NUM1,0) AS CC FROM TABLES WHERE   ONO='" + cc + "'");
            if (Globals.gUserWaiter.ToString()== user.ToString())
            {
                //ok
            }
            else
            {
                await DisplayAlert("ΑΔΥΝΑΤΗ Η ΜΕΤΑΦΟΡΑ", "ΕΙΝΑΙ ΣΕ ΑΛΛΟ ΣΕΡΒΙΤΟΡΟ", "OK");
                return;
            }

            if (iskat == "0")
            {
               AllExecute("update PARAGGMASTER SET TABLE="+cc+" WHERE ID="+ Globals.gIDPARAGG.ToString());
               AllExecute("update TABLES SET KATEILHMENO=0,IDPARAGG=0,CH1=''  WHERE IDPARAGG=" + Globals.gIDPARAGG.ToString());
               AllExecute("update TABLES SET CH1='',KATEILHMENO=1,IDPARAGG=" + Globals.gIDPARAGG.ToString()+" WHERE ONO='"+cc+"'");
                Globals.gTrapezi = cc;
                await Navigation.PopAsync(); // new PelReports());
            }
            else
            {
                await DisplayAlert("ΑΔΥΝΑΤΗ Η ΜΕΤΑΦΟΡΑ", "ΕΙΝΑΙ ΚΑΤΕΙΛΗΜΕΝΟ ΤΟ "+cc, "OK");
                return;
            }
            

        }

        private void timologio(object sender, EventArgs e)
        {
           if(cmdtimologio.Text == "Απόδειξη")
            {
                cmdtimologio.Text = "Τιμολόγιο";
            }
            else
            {
                cmdtimologio.Text = "Απόδειξη";
            }
           AllExecute("UPDATE TABLES SET CH2='" + cmdtimologio.Text + "' WHERE ONO='" + Globals.gTrapezi + "'");



        }

        private void OnClick4(object sender, EventArgs e)
        {

        }

        private void OnClick1(object sender, EventArgs e)
        {

        }

        private void OnClick2(object sender, EventArgs e)
        {

        }

        private void OnClick3(object sender, EventArgs e)
        {

        }
    }







    public class Main2PageModel : BindableObject
    {
        public SqlConnection con;
        private trapparagg mainPage;

        public Main2PageModel(trapparagg mainPage)
        {
            this.mainPage = mainPage;
            AddItems();
        }

        private void addItemsSqlite()
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
                contents.CommandText = "SELECT  ONO, ID FROM KATHG   order by CH1 ;";
                var r = contents.ExecuteReader();
                while (r.Read())
                {
                    

                    string fONO;
                    fONO = r["ONO"].ToString();

                    Items.Add(string.Format("{0}", fONO));


                }
            }
            catch
            {
                ff = "error";
            }
            connection.Close();



        }

        private async void AddItems()

        {

            if (Globals.gLocal != "0")  // global sqlite or sqlserver
            {
                addItemsSqlite();
                return;
            }



            if (Globals.cSQLSERVER.Length < 2)
            {
               // await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return;
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            con = new SqlConnection(constring);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                // await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }


            String SYNT = "";

            try
            {
                //Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("SELECT  ONO, ID FROM KATHG   order by CH1 ; ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    string fONO;
                    fONO = dt.Rows[k]["ONO"].ToString();
                    string fID;
                    fID = dt.Rows[k]["ID"].ToString();
                    // Items.Add(string.Format("{0}",  fONO + ";" + fID));
                    Items.Add(string.Format("{0}", fONO ));


                } // FOR
                //  BindingContext = this;
            }
            catch (Exception ex)
            {
               // await DisplayAlert("Error", ex.ToString(), "OK");
            }

            con.Close(); 
            //Items.Add(string.Format("kat {0} ", fkat + fONO + " " + fID));
        }

        private ObservableCollection<string> _items = new ObservableCollection<string>();
        private trapparagg trapparagg;

        public ObservableCollection<string> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        public Command ItemTappedCommand => new Command((data) =>
        {
            //mainPage.DisplayAlert("Τραπέζι Νο ", data + "", "Ok");
            //Globals.gIDPARAGG = (string)data;      
        });
    }








}

    