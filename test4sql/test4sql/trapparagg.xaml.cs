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
            Globals.gIDPARAGG = lines[1];
            titlos.Text = "Τραπέζι: " + Globals.gTrapezi;



          

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

            string action = await DisplayActionSheet("Επιλογή", "Ακυρο", null, "1.Μετρητά Πληρωμή", "2.Κάρτα Πληρωμή", "3.Κάρτα2 Πληρωμή","4.Κερασμένα", "5.Διαγραφή");
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

            string cTYPOMENO = Globals.ReadSQLServer("SELECT  ISNULL(ENERGOS,0) AS TYP FROM PARAGG WHERE   ID=" + cc);
          if (action.Substring(0, 1) == "5")
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
                    Globals.ExecuteSQLServer("delete from PARAGG WHERE ID=" + cc);
                }
          }


            //------------- PLHRVMH ------------------------
            if (cTYPOMENO == "0")
            {
                await DisplayAlert("ΑΔΥΝΑΤΗ Η ΠΛΗΡΩΜΗ", "ΔΕΝ ΕΙΝΑΙ ΤΥΠΩΜΕΝΟ", "OK");
                return;
            }

            if (action.Substring(0, 1) == "1" || action.Substring(0, 1) == "2" || action.Substring(0, 1) == "3" || action.Substring(0, 1) == "4")
            {
                if (cc.Substring(0, 2) == "**")
                {
                    await DisplayAlert("αδυνατη η πληρωμή", "ειναι πληρωμένο", "OK");
                }
                else
                {


                    Globals.ExecuteSQLServer("UPDATE  PARAGG SET ONO='**"+ action.Substring(0, 1)+"-'+ONO , NUM1=" + action.Substring(0, 1) + " WHERE ID=" + cc);
                    if (action.Substring(0, 1) == "1")
                    {
                        Globals.ExecuteSQLServer("UPDATE  PARAGGMASTER SET CASH=ISNULL(CASH,0)+" +mt+ " WHERE ID=" + Globals.gIDPARAGG.ToString() );
                    }
                    if (action.Substring(0, 1) == "2")
                    {
                    Globals.ExecuteSQLServer("UPDATE  PARAGGMASTER SET PIS1=ISNULL(PIS1,0)+" + mt+ " WHERE ID=" + Globals.gIDPARAGG.ToString() );
                    }
                    if (action.Substring(0, 1) == "3")
                    {
                      Globals.ExecuteSQLServer("UPDATE  PARAGGMASTER SET PIS2=ISNULL(PIS2,0)+" + mt+ " WHERE ID=" + Globals.gIDPARAGG.ToString() );
                    }

                    if (action.Substring(0, 1) == "4")
                    {
                        Globals.ExecuteSQLServer("UPDATE  PARAGGMASTER SET KERA=ISNULL(KERA,0)+" + mt + " WHERE ID=" + Globals.gIDPARAGG.ToString());
                    }
                }
                   
            }


            // ΑΞΙΑ ΧΩΡΙΣ ΤΟ ΠΛΗΡΩΜΕΝΟ
            string caji = Globals.ReadSQLServer("SELECT str(round(SUM(POSO*TIMH),2),6,2 ) FROM PARAGG WHERE NUM1=0 AND  IDPARAGG=" + Globals.gIDPARAGG + "");
            // ΑΞΙΑ ΗΔΗ ΠΛΗΡΩΜΕΝΩΝ
            string cPLIR = Globals.ReadSQLServer("SELECT str(round(SUM(POSO*TIMH),2),6,2 ) FROM PARAGG WHERE NUM1>0 AND  IDPARAGG=" + Globals.gIDPARAGG + "");

            Globals.ExecuteSQLServer("update PARAGGMASTER SET AJIA=" + caji.Replace(",", ".") + ",NUM1=" + cPLIR.Replace(",", ".") + " WHERE  WHERE ID=" + Globals.gIDPARAGG + ";");

            // Globals.ExecuteSQLServer("update PARAGGMASTER SET NUM1=" + caji.Replace(",", ".") + " WHERE  WHERE ID=" + Globals.gIDPARAGG + ";");

            Globals.ExecuteSQLServer("update TABLES SET KATEILHMENO=1,CH1='" + caji.Replace(",", ".") + "' WHERE IDPARAGG=" + Globals.gIDPARAGG + "");
            Show_listsql_Paragg(Globals.gIDPARAGG);
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



            string[] lines = Globals.gKathg.Split(';');

            // αδειο τρπαζζι  π.χ. 12 345
            if (lines.Length > 1)
            {
                Globals.gKathg = lines[1];

            }
            else
            {
                Globals.gKathg = "0";
            }

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
            string ipAddress = Globals.cIPPR1; // "192.168.1.120";
            int portNumber = 9100;
            List<string> myText = new List<string>();
            DataTable dt;          
                dt = ReadSQLServer("SELECT   ONO, SUM(POSO) as POSO,  TIMH ,SUM(POSO*TIMH) AS AXIA FROM PARAGG  where NUM1=0 AND  IDPARAGG = " + Globals.gIDPARAGG + " GROUP BY TIMH,ONO  ; ");
           // }
            // Monkeys.Add(new Monkey

            //myText.Add(Globals.gTrapezi.ToString() + MainPage.ToGreek737(" * TΡΑΠΕΖΙ * ")) ;
            string DDD = MainPage.ToGreek737(titlos.Text + "  .");
            myText.Add(DDD);
            //myText.Add(Globals.gTrapezi.ToString() +MainPage.ToGreek737( " * TΡΑΠΕΖΙ * ") +Globals.gTrapezi.ToString ()  + ".                \r\n");

            myText.Add("-\r\n");
            myText.Add("\r\n");
           //  string BIG="";
            float ss = 0;
            string PROS = "";
            for (int k = 0; k <= dt.Rows.Count - 1; k++)
            {
                //ola
                myText.Add((MainPage.ToGreek737(dt.Rows[k]["ONO"].ToString() + "                              ").Substring(0, 20)));
                myText.Add( dt.Rows[k]["POSO"].ToString()  + "  X  " + (dt.Rows[k]["TIMH"].ToString() + "     ").Substring(0, 5) + "    " + (dt.Rows[k]["AXIA"].ToString() + "     ").Substring(0, 5));
                    ss = ss + float.Parse(dt.Rows[k]["AXIA"].ToString());
               
                //  BIG = BIG + MainPage.ToGreek737(dt.Rows[k]["PROSUETA"].ToString().Substring(0, 19)) + " " + MainPage.ToGreek737(dt.Rows[k]["SXOLIA"].ToString().Substring(0, 29));
                // myText.Add("\r\n");

            }
            if (part == 1)
            {
                myText.Add("                                                                          " + MainPage.ToGreek737(ss.ToString()));

            }
            else
            {
                myText.Add(MainPage.ToGreek737("ΣΥΝΟΛΟ ") + "    " + ss.ToString());
            }
            myText.Add("\r\n");
            myText.Add("\r\n");
            myText.Add("\r\n");
            myText.Add("\r\n");
            myText.Add("\r\n");
            myText.Add("\r\n");


            var printer = DependencyService.Get<test4sql.iPrinter>();
            if (printer == null)
            {
                await DisplayAlert("Error", "δεν υπαρχει συνδεση", "");
                return;
            }
            try
            {

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

                //---------------------------------------------------------------------------
                printer.Print(ipAddress, portNumber, myText);
                //---------------------------------------------------------------------------


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

              //  Globals.ExecuteSQLServer("update PARAGG set ENERGOS=1 where IDPARAGG = " + Globals.gIDPARAGG);

            }

            catch (Exception ex)
            {
                await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
                // await DisplayAlert("error2", "", "");
            }
        }

        private async void printing(int part) //  object sender, EventArgs e)
        {
            string ipAddress = Globals.cIPPR1; // "192.168.1.120";
            int portNumber = 9100;
            List<string> myText = new List<string>();
            List<string> myText2 = new List<string>();
            List<string> myText3 = new List<string>();
            DataTable dt;

            // {PARAGGELIES.toGreek( "ΓΕΙΑ ΣΟΥ ΜΕΓΑΛΕ ΜΟΥ"),"From","Replace","MrNashad","Please Like"};
            if (part == 1) // τα νεα μονο
            {
                 dt = ReadSQLServer("SELECT  ISNULL(PARAGG.ONO,'')+SPACE(32) AS ONO, isnull(POSO,0) as POSO, ISNULL(PARAGG.TIMH,0) AS TIMH,ID,ISNULL(PROSUETA,'')+SPACE(22) AS PROSUETA,LEFT(ISNULL(PARAGG.CH1,'')+SPACE(31),31) AS SXOLIA,ISNULL(POSO*PARAGG.TIMH,0) AS AXIA ,(select KATHG.CH2 FROM EIDH INNER JOIN KATHG ON EIDH.KATHG=KATHG.ID WHERE EIDH.ONO=PARAGG.ONO) AS PRINTER FROM PARAGG where (ENERGOS IS NULL) AND IDPARAGG = " + Globals.gIDPARAGG + "  order by ID ; ");
            }
            else
            {     // ολα 
                 dt = ReadSQLServer("SELECT  ISNULL(ONO,'')+SPACE(32) AS ONO, isnull(POSO,0) as POSO, ISNULL(TIMH,0) AS TIMH,ID,ISNULL(PROSUETA,'')+SPACE(22) AS PROSUETA,LEFT(ISNULL(CH1,'')+SPACE(31),31) AS SXOLIA,ISNULL(POSO*TIMH,0) AS AXIA  FROM PARAGG where NUM1=0 AND  IDPARAGG = " + Globals.gIDPARAGG + "  order by ID ; ");
            }
            // Monkeys.Add(new Monkey

            //myText.Add(Globals.gTrapezi.ToString() + MainPage.ToGreek737(" * TΡΑΠΕΖΙ * ")) ;
            string DDD = MainPage.ToGreek737(titlos.Text+"  .");
            myText.Add(DDD);
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
           for (int k = 0; k <= dt.Rows.Count - 1; k++)
            {
                // if (dt.Rows[k]["ONO"].ToString()=="1")
                // {
                KANON=(MainPage.ToGreek737(dt.Rows[k]["ONO"].ToString().Trim())); // + "        " + dt.Rows[k]["TIMH"].ToString() + "    " + dt.Rows[k]["AXIA"].ToString()) ;


                if (dt.Rows[k]["PRINTER"].ToString() == "1")
                {
                    myText.Add(KANON.Trim());
                    i1 = i1 + 1;
                }
                {
                    if (dt.Rows[k]["PRINTER"].ToString() == "2")
                    {
                        myText2.Add(KANON.Trim());
                        i2 = i2 + 1;
                    }
                    if (dt.Rows[k]["PRINTER"].ToString() == "3")
                    {
                        myText3.Add(KANON.Trim());
                        i3 = i3 + 1;
                    }
                }





                // myText.Add(dt.Rows[k]["POSO"].ToString() + " " + PARAGGELIES.toGreek(dt.Rows[k]["ONO"].ToString().Substring(0, 30))); // + "        " + dt.Rows[k]["TIMH"].ToString() + "    " + dt.Rows[k]["AXIA"].ToString()) ;

                PROS = MainPage.ToGreek737(dt.Rows[k]["PROSUETA"].ToString().Substring(0, 19)) + " " + MainPage.ToGreek737(dt.Rows[k]["SXOLIA"].ToString().Substring(0, 29));
             if (PROS.Trim().Length > 0)
               
                
                
                
                
                {
                    //  if (dt.Rows[k]["PRINTER"].ToString() == "1")
                    //      myText.Add(PROS.Trim());
                    //  i1 = i1 + 1;


                    if (dt.Rows[k]["PRINTER"].ToString() == "1")
                    {
                        myText.Add(PROS.Trim());
                        i1 = i1 + 1;
                    }
                    {
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


               
               
               
                    
                     
                   
               // } else
               // {    //ola
                //    myText.Add((dt.Rows[k]["POSO"].ToString() + " " + MainPage.ToGreek737(dt.Rows[k]["ONO"].ToString()+"                              ").Substring(0, 30))+ "   " + (dt.Rows[k]["TIMH"].ToString()+"     ").Substring(0, 5) + "    " + (dt.Rows[k]["AXIA"].ToString()+"     ").Substring(0, 5)) ;
                 //   ss = ss + float.Parse(dt.Rows[k]["AXIA"].ToString());
               // }
              //  BIG = BIG + MainPage.ToGreek737(dt.Rows[k]["PROSUETA"].ToString().Substring(0, 19)) + " " + MainPage.ToGreek737(dt.Rows[k]["SXOLIA"].ToString().Substring(0, 29));
               // myText.Add("\r\n");

           }
          //  if (part == 1) 
          //  {
                string cc1="                                                                          "+MainPage.ToGreek737( ss.ToString());
               myText.Add(cc1);
               myText2.Add(cc1);
               myText3.Add(cc1);

           // }
           // else
           // {
            //    myText.Add(MainPage.ToGreek737("ΣΥΝΟΛΟ ") + "    " + ss.ToString());
           // }
               
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









            //  myText.Add(Convert.ToChar(27).ToString() + Convert.ToChar(105).ToString());
            //  myText.Add(Convert.ToChar(27).ToString() + Convert.ToChar(105).ToString());
            // for (int ll = 128; ll < 255; ll++)
            // {
            // myText.Add(ll.ToString()+" "+Convert.ToChar(ll).ToString());

            //  }

            var printer = DependencyService.Get<test4sql.iPrinter>();
            if (printer == null)
            {

                await DisplayAlert("Error", "δεν υπαρχει συνδεση", "");
                return;

            }
            try
            {
                if (i1 > 0){ printthis(myText, Globals.cIPPR1);
                   
                }

                
                if (i2 > 0) { printthis(myText2, Globals.cIPPR2); }
               
                if (i3 > 0) { printthis(myText3, Globals.cIPPR3); }

                Globals.ExecuteSQLServer("update PARAGG set ENERGOS=1 where IDPARAGG = " + Globals.gIDPARAGG);
                //List<byte> outputList1 = new List<byte>();

                ////outputList1.Add(141);
                //// BIG LETTERS

                //if (part == 1)
                //{
                // outputList1.Add(0x1D);
                //outputList1.Add(0x21);
                //outputList1.Add(0x11);

                //}else
                //{
                //    outputList1.Add(0x1D);
                //    outputList1.Add(0x21);
                //    outputList1.Add(0x01);
                //}


                //    // TEST ΒΓΔ  ΗΕΧ
                //    //outputList1.Add(0x81);
                //    //outputList1.Add(0x82);
                //    //outputList1.Add(0x83);

                //    //outputList1.Add(141);// ΤΕΣΤ ΝΕΧΤ 3  
                //    //outputList1.Add(142);
                //    //outputList1.Add(143);



                ////    byte[] bytes = Encoding.ASCII.GetBytes(BIG);

                //    Socket pSocket1 = new Socket(SocketType.Stream, ProtocolType.IP);

                //    // Connect to the printer
                //    pSocket1.Connect(ipAddress, portNumber);

                //    // ToDo: Send some commands to the printer
                //    // Send the command to the printer
                //    pSocket1.Send(outputList1.ToArray());
                // //  pSocket1.Send(bytes.ToArray());
                //    // Close the socket connection when done
                //    pSocket1.Close();








                //    printer.Print(ipAddress, portNumber, myText);

                // List<byte> outputList = new List<byte>();
                // //  CUT PAPER NEXT 2 A);
                //outputList.Add(0x1B);
                //outputList.Add(0x69);
                //    // BIG LETTERS ΨΑΝΨΕΚ
                //    outputList1.Add(0x1D);
                //    outputList1.Add(0x21);
                //    outputList1.Add(0x00);





                //    Socket pSocket = new Socket(SocketType.Stream, ProtocolType.IP);

                //// Connect to the printer
                //pSocket.Connect(ipAddress, portNumber);

                //// ToDo: Send some commands to the printer
                //// Send the command to the printer
                //pSocket.Send(outputList.ToArray());
                //// Close the socket connection when done
                //pSocket.Close();



            }
            
             catch (Exception ex)
            {
                await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
               // await DisplayAlert("error2", "", "");
            }
          



          






        }

        private async void printthis(List<string> mytext,string ipAddress)
        {


            //string ipAddress = Globals.cIPPR1; // "192.168.1.120";
            int portNumber = 9100;
            var printer = DependencyService.Get<test4sql.iPrinter>();
            if (printer == null)
            {
                await DisplayAlert("Error", "δεν υπαρχει συνδεση", "");
                return;
            }
            try
            {
                
                MainPage.LF(ipAddress);
                
                MainPage.LF(ipAddress);
                MainPage.BigLetters(ipAddress);
                printer.Print(ipAddress, portNumber, mytext);
            }

            catch (Exception ex)
            {
                await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
                // await DisplayAlert("error2", "", "");
            }
            MainPage.CutPaper(ipAddress);
        }





        public static DataTable ReadSQLServer(string cSQL)

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
                // await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
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
        










        private  async void  Show_listsql_Paragg(string ono)
         {
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


            String SYNT = "";

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
            con.Close();

            BindingContext = this;






        }

        private async void PLIROMI(object sender, EventArgs e)
        {
            string catyp = Globals.ReadSQLServer("SELECT COUNT(*) AS TYP FROM PARAGG WHERE  (ENERGOS IS NULL) AND IDPARAGG=" + Globals.gIDPARAGG + "");
            if (Int32.Parse(catyp) > 0)
            {
                await DisplayAlert("ΑΔΥΝΑΤΗ Η ΠΛΗΡΩΜΗ", "ΔΕΝ ΕΙΝΑΙ ΟΛΑ ΤΥΠΩΜΕΝΑ", "OK");
                return;
            }

            string caji = Globals.ReadSQLServer("SELECT str(round(SUM(POSO*TIMH),2),6,2 ) FROM PARAGG WHERE NUM1=0 AND  IDPARAGG=" + Globals.gIDPARAGG + "");
            string action = await DisplayActionSheet("Τρόπος Πληρωμής", caji+"€", null, "1.μετρητα", "2.Κάρτα","3.Κάρτα 2", "3.Κερασμένα");
           if (action.Substring(0, 1) == "Α") { return; }
            Globals.ExecuteSQLServer("UPDATE TABLES SET KATEILHMENO=0,CH1='',IDPARAGG=0 WHERE ONO='" + Globals.gTrapezi  + "'");
            Globals.ExecuteSQLServer("UPDATE PARAGGMASTER SET CH2= CONVERT(CHAR(10),GETDATE(),103),TROPOS="+action.Substring(0,1)+"   WHERE ID=" + Globals.gIDPARAGG );

            if (action.Substring(0, 1) == "1")
            {
                Globals.ExecuteSQLServer("UPDATE  PARAGGMASTER SET CASH=isnull(CASH,0)+" + caji.Replace(",",".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());
            }
            if (action.Substring(0, 1) == "2")
            {
                Globals.ExecuteSQLServer("UPDATE  PARAGGMASTER SET PIS1=isnull(PIS1,0)+" + caji.Replace(",", ".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());
            }
            if (action.Substring(0, 1) == "3")
            {
                Globals.ExecuteSQLServer("UPDATE  PARAGGMASTER SET  KERA=isnull(KERA,0)+" + caji.Replace(",", ".") + " WHERE ID=" + Globals.gIDPARAGG.ToString());
            }








            Globals.indexParaggLine = -1;  // για να καταλαβαίνω αν ειναι αδεια η παραγγελία

            await Navigation.PopAsync();


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

      

        private async void AddItems()

        {
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
                SqlCommand cmd3 = new SqlCommand("SELECT  ONO, ID FROM KATHG   order by ONO ; ", con);
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
                    Items.Add(string.Format("{0}",  fONO + ";" + fID));



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

    