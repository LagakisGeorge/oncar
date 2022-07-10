using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public trapparagg()
        {
            InitializeComponent();
            Globals.gtIDPARAGG=Globals.gtIDPARAGG.Replace("#", "");
            //  Items.Add(string.Format(" {0} ", fkat + fONO + "*" + fID+"*"+CH1));
            string[] lines = Globals.gtIDPARAGG.Split('*');
            Globals.gTrapezi = lines[0];
            Globals.gTrapezi=Globals.gTrapezi.TrimStart();
            Globals.gTrapezi= Globals.gTrapezi.TrimEnd();
            Globals.gIDPARAGG = lines[1];
            titlos.Text = "Τραπέζι: " + Globals.gTrapezi;



            //if (lines[0].Substring(0,1) == "#")
            //{     // οχι αδειο τρπαζζι  π.χ. # 12 345
            //    if (lines.Length > 3)
            //    {
            //        IDPARAGG = lines[3];
            //        trapezi = lines[2];
            //        titlos.Text = "Τραπέζι: " + trapezi;
            //        Show_listsql_Paragg(IDPARAGG);
            //    }
            //    else
            //    {
            //        IDPARAGG = "0";
            //        trapezi = "0";
            //    }
            //}
            //else
            //{              // αδειο τρπαζζι  π.χ. 12 345
            //    if (lines.Length > 2)
            //    {
            //        IDPARAGG = lines[2];
            //        trapezi = lines[1];
            //        titlos.Text = "Τραπέζι: " + trapezi;
            //        Show_listsql_Paragg(IDPARAGG);
            //    }
            //    else
            //    {
            //        IDPARAGG = "0";
            //        trapezi = "0";
            //    }

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






        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

            // PARAGG -> NUM1 = TROPOS PLHROMHS  1,2,3     -1=ΔΙΑΓΡΑΦΤΗΚΕ
            // ENERGOS=1  ΤΥΠΟΜΕΝΟ

            string action = await DisplayActionSheet("Επιλογή", "Ακυρο", null, "1.Μετρητά Πληρωμή", "2.Κάρτα Πληρωμή", "3.Διαγραφή");
            if (action.Substring(0, 1) == "Α") { return; }


            Monkey tappedItem = e.Item as Monkey;
         //   LIDtest.Text = e.ItemIndex.ToString();
             string cc = tappedItem.idPEL;
             string mc = tappedItem.Name ;
            //Location = mTimh,
            //           ImageUrl = mPoso




            string[] lines = cc.Split('~');

            if (action.Substring(0, 1) == "3") 
            {
                if (cc.Substring(0, 2) == "**")
                {
                    await DisplayAlert("αδυνατη η διαγραφή","εγινε πληρωμή", "OK");
                }
                else
                {
                    Globals.ExecuteSQLServer("delete from PARAGG WHERE ID=" + lines[1]);
                }
            }

            if (action.Substring(0, 1) == "1" || action.Substring(0, 1) == "2")
            {
               
                Globals.ExecuteSQLServer("UPDATE  PARAGG SET ONO='**'+ONO , NUM1="+ action.Substring(0, 1) +" WHERE ID=" + lines[1]);
            }


            // ΑΞΙΑ ΧΩΡΙΣ ΤΟ ΠΛΗΡΩΜΕΝΟ
            string caji = Globals.ReadSQLServer("SELECT str(round(SUM(POSO*TIMH),2),6,2 ) FROM PARAGG WHERE NUM1=0 AND  IDPARAGG=" + Globals.gIDPARAGG + "");
            // ΑΞΙΑ ΗΔΗ ΠΛΗΡΩΜΕΝΩΝ
            string cPLIR = Globals.ReadSQLServer("SELECT str(round(SUM(POSO*TIMH),2),6,2 ) FROM PARAGG WHERE NUM1>0 AND  IDPARAGG=" + Globals.gIDPARAGG + "");

            Globals.ExecuteSQLServer("update PARAGGMASTER SET AJIA=" + caji.Replace(",", ".")+",NUM1="+ cPLIR.Replace(",", ".") + " WHERE  WHERE ID=" + Globals.gIDPARAGG + ";");

           // Globals.ExecuteSQLServer("update PARAGGMASTER SET NUM1=" + caji.Replace(",", ".") + " WHERE  WHERE ID=" + Globals.gIDPARAGG + ";");

            Globals.ExecuteSQLServer("update TABLES SET KATEILHMENO=1,CH1='" + caji.Replace(",", ".") + "' WHERE IDPARAGG=" + Globals.gIDPARAGG + "");
            Show_listsql_Paragg(Globals.gIDPARAGG);
        }

        private async void doit2(object sender, ItemTappedEventArgs e)
        {
            Main2PageModel tappedItem = e.Item as Main2PageModel;
            Globals.gKathg = e.Item.ToString();


            string[] lines = Globals.gKathg.Split(' ');
           
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
            await Navigation.PushAsync(new trapeziEpil ());  //imports






        }

        private void exodos(object sender, EventArgs e)
        {

        }

        private void typlog(object sender, EventArgs e)
        {
            printing();
        }

        private void apoth(object sender, EventArgs e)
        {

        }

        void printt(Stream outs, string qq)
        {
            byte[] toBytes;
            string fff;
            fff = qq; // toGreek(qq);

            toBytes = Encoding.Unicode.GetBytes(fff);
            outs.Write(toBytes, 0, toBytes.Length);



        }



        private async void printing() //  object sender, EventArgs e)
        {
            string ipAddress = "192.168.1.120";
            int portNumber = 9100;
            List<string> myText = new List<string>()
            {PARAGGELIES.toGreek( "ΓΕΙΑ ΣΟΥ ΜΕΓΑΛΕ ΜΟΥ"),"From","Replace","MrNashad","Please Like"};

            var printer = DependencyService.Get<test4sql.iPrinter>();
            if (printer == null)
            {
                await DisplayAlert("Error", "δεν υπαρχει συνδεση", "");
                return;

            }
            try
            {
                printer.Print(ipAddress, portNumber, myText);
            }
            catch
            {
                await DisplayAlert("error2", "", "");
            }

            // Ethernet or WiFi
            // var printer = new ESCPOS_NET.NetworkPrinter(ipAddress: "192.168.1.80", port: 9000, reconnectOnTimeout: true);
            // printer.Write()
            /*
            var e = new EPSON();
            printer.Write(
              ByteSplicer.Combine(
                e.CenterAlign(),
                e.PrintImage(File.ReadAllBytes("images/pd-logo-300.png"), true),
NewMethod(e),
                e.SetBarcodeHeightInDots(360),
                e.SetBarWidth(BarWidth.Default),
                e.SetBarLabelPosition(BarLabelPrintPosition.None),
                e.PrintBarcode(BarcodeType.ITF, "0123456789"),
                e.PrintLine(),
                e.PrintLine("B&H PHOTO & VIDEO"),
                e.PrintLine("420 NINTH AVE."),
                e.PrintLine("NEW YORK, NY 10001"),
                e.PrintLine("(212) 502-6380 - (800)947-9975"),
                e.SetStyles(PrintStyle.Underline),
                e.PrintLine("www.bhphotovideo.com"),
                e.SetStyles(PrintStyle.None),
                e.PrintLine(),
                e.LeftAlign(),
                e.PrintLine("Order: 123456789        Date: 02/01/19"),
                e.PrintLine(),
                e.PrintLine(),
                e.SetStyles(PrintStyle.FontB),
                e.PrintLine("1   TRITON LOW-NOISE IN-LINE MICROPHONE PREAMP"),
                e.PrintLine("    TRFETHEAD/FETHEAD                        89.95         89.95"),
                e.PrintLine("----------------------------------------------------------------"),
                e.RightAlign(),
                e.PrintLine("SUBTOTAL         89.95"),
                e.PrintLine("Total Order:         89.95"),
                e.PrintLine("Total Payment:         89.95"),
                e.PrintLine(),
                e.LeftAlign(),
                e.SetStyles(PrintStyle.Bold | PrintStyle.FontB),
                e.PrintLine("SOLD TO:                        SHIP TO:"),
                e.SetStyles(PrintStyle.FontB),
                e.PrintLine("  FIRSTN LASTNAME                 FIRSTN LASTNAME"),
                e.PrintLine("  123 FAKE ST.                    123 FAKE ST."),
                e.PrintLine("  DECATUR, IL 12345               DECATUR, IL 12345"),
                e.PrintLine("  (123)456-7890                   (123)456-7890"),
                e.PrintLine("  CUST: 87654321"),
                e.PrintLine(),
                e.PrintLine()
              )
            );

            */


        }







        private  async void  Show_listsql_Paragg(string ono)
         {
            Monkeys = new List<Monkey>();
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
                SqlCommand cmd3 = new SqlCommand("SELECT  ISNULL(ONO,'') AS ONO, POSO, TIMH,ID,ISNULL(PROSUETA,'') AS PROSUETA  FROM PARAGG where IDPARAGG = " + ono + "  order by ID ; ", con);
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
                    Monkeys.Add(new Monkey
                    {  // dt.Rows[k]["ONO"].ToString();
                        Name = mOno,

                        Location = mTimh,
                        ImageUrl = mPoso,
                        idPEL = mPROSU+"  ~"+ midEggtim
                    }) ;

                    Globals.PARAGGlines[k, 0] = mOno;
                    Globals.PARAGGlines[k, 1] = mPoso;
                    Globals.PARAGGlines[k, 2] = mTimh;
                    Globals.PARAGGlines[k, 3] = mPROSU;
                    Globals.PARAGGlines[k, 4] = midEggtim;
                    Globals.indexParaggLine = k;
                } // FOR

              
                //  BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
            listERG.ItemsSource = Monkeys;
            BindingContext = this;
            con.Close();

            BindingContext = this;






        }

        private async void PLIROMI(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Τρόπος Πληρωμής", "Ακυρο", null, "1.μετρητα", "2.Κάρτα", "3.Κερασμένα");
           if (action.Substring(0, 1) == "Α") { return; }
            Globals.ExecuteSQLServer("UPDATE TABLES SET KATEILHMENO=0,CH1='',IDPARAGG=0 WHERE ONO='" + Globals.gTrapezi  + "'");
            Globals.ExecuteSQLServer("UPDATE PARAGGMASTER SET CH2= CONVERT(CHAR(10),GETDATE(),103),TROPOS="+action.Substring(0,1)+"   WHERE ID=" + Globals.gIDPARAGG );
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
                    Items.Add(string.Format("{0}",  fONO + " " + fID));



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

    