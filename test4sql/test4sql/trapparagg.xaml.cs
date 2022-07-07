using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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
            
            
            string[] lines = Globals.gIDPARAGG.Split(' ');
            if (lines[1] == "#")
            {     // οχι αδειο τρπαζζι  π.χ. # 12 345
                if (lines.Length > 3)
                {
                    IDPARAGG = lines[3];
                    trapezi = lines[2];
                    titlos.Text = "Τραπέζι: " + trapezi;
                    Show_listsql_Paragg(IDPARAGG);
                }
                else
                {
                    IDPARAGG = "0";
                    trapezi = "0";
                }
            }
            else
            {              // αδειο τρπαζζι  π.χ. 12 345
                if (lines.Length > 2)
                {
                    IDPARAGG = lines[2];
                    trapezi = lines[1];
                    titlos.Text = "Τραπέζι: " + trapezi;
                    Show_listsql_Paragg(IDPARAGG);
                }
                else
                {
                    IDPARAGG = "0";
                    trapezi = "0";
                }

            }
            pageModel = new Main2PageModel(this);
            BindingContext = pageModel;



            //pageModel = new MainPageModel(this);
            //BindingContext = pageModel;
        }

        void Show_list_Paragg(string ono)
        {
            Monkeys = new List<Monkey>();
            BindingContext = null;

            string dbPath = Path.Combine(
              Environment.GetFolderPath(Environment.SpecialFolder.Personal),
              "adodemo.db3");
            //bool exists = File.Exists(dbPath);
            //if (!exists)
            //{
            //    Console.WriteLine("Creating database");
            //    // Need to create the database before seeding it with some data
            //    Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);

            //}

            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            // Open the database connection and create table with data
            connection.Open();

            ono = ono.ToUpper();
            var contents = connection.CreateCommand();

            contents.CommandText = "SELECT  ONO,POSO,TIMH FROM PARAGG where IDPARAGG=" + Globals.gIDPARAGG + "  order by ID ; "; // +BARCODE.Text +"'";
                                                                                                                                 // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
          while (r.Read())
            {


                Monkeys.Add(new Monkey
                {
                    Name = (r["PER"].ToString() + "                                   ").Substring(0, 28),

                    Location = (r["KODI"].ToString() + "      ").Substring(0, 5),
                    ImageUrl = (r["THL"].ToString() + "              ").Substring(0, 11),
                    idPEL = r["ID"].ToString()
                });



            }

            listERG.ItemsSource = Monkeys;
            BindingContext = this;


            connection.Close();

            BindingContext = this;
        }






        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

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

        }

        private void apoth(object sender, EventArgs e)
        {

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
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("SELECT  ONO, POSO, TIMH,ID FROM PARAGG where IDPARAGG = " + ono + "  order by ID ; ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    Monkeys.Add(new Monkey
                    {  // dt.Rows[k]["ONO"].ToString();
                        Name = (dt.Rows[k]["ONO"].ToString() + "                                   ").Substring(0, 18),

                        Location = (dt.Rows[k]["POSO"].ToString() + "                                   ").Substring(0, 8),
                        ImageUrl = (dt.Rows[k]["TIMH"].ToString() + "              ").Substring(0, 11),
                        idPEL = (dt.Rows[k]["ID"].ToString() + "              ").Substring(0, 11)
                    });





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

    