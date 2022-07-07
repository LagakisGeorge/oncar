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
using System.Windows.Input;
using test4sql;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oncar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class trapeziEpil : ContentPage      
    {

        public IList<Monkey> Monkeys { get; private set; }
        public  MainXAR1PageModel pageModelx;

        public  MainEIDHPageModel pageModel;
        public trapeziEpil()
        {
            InitializeComponent();

            Show_list_Eidon();


            pageModel = new MainEIDHPageModel(this);
            BindingContext = pageModel;






        }


        private async void okdone(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            //  App.Current.Mainpage.Navigation.PopModalAsync();
        }

        private void subplus(object sender, EventArgs e)
        {

        }

        private void subminus(object sender, EventArgs e)
        {

        }

        void Show_list_Eidon()
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
            contents.CommandText = "SELECT  ifnull(KOD,'') as KODI,ifnull(EPO,'') AS PER,ifnull(THL,'') as THL,KINHTO,ID from PEL   order by EPO ; "; // +BARCODE.Text +"'";
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

                /*  Monkeys.Add(new Monkey
                  {
                      Name = (r["PER"].ToString() + "                         ").Substring(0, 18),

                      Location = (r["KODI"].ToString() + "      ").Substring(0, 5),
                      ImageUrl = (r["THL"].ToString()+" "+ r["KINHTO"].ToString() + "                    ").Substring(0, 20),
                      idPEL = r["ID"].ToString()
                  });
                */


            }

           // listview.ItemsSource = Monkeys;
           // BindingContext = this;


            connection.Close();

            BindingContext = this;
        }

        private void YourSub()
        {

        }



        private void LISTEIDH(object sender, ItemTappedEventArgs e)
        {
            //  Show_list_Eidon();


           // τσιμπαω αυτο που πατησε
            MainPageModel tappedItem = e.Item as MainPageModel;
            string c= e.Item.ToString();



            string[] lines =c.Split(' ');

            // αδειο τρπαζζι  π.χ. 12 345
            if (lines.Length > 1)
            {
                Globals.gIDEIDOS = lines[lines.Length-1];

            }
            else
            {
                Globals.gKathg = "0";
            }








           // Globals.gIDEIDOS = e.Item.ToString();
            // DisplayAlert("Τραπέζι Νο ", e.Item.ToString(), "Ok");

            pageModelx = new MainXAR1PageModel(this);
            BindingContext = pageModelx;



        }

        private void LISTXAR1(object sender, ItemTappedEventArgs e)
        {

        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }


    public class MainEIDHPageModel : BindableObject
    {
        public SqlConnection con;
        private trapeziEpil mainPage;

        public MainEIDHPageModel(trapeziEpil mainPage)
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
                SqlCommand cmd3 = new SqlCommand("SELECT  ONO, ID FROM EIDH WHERE KATHG=" + Globals.gKathg +"  order by ONO ; ", con);
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
                    ItemsEidh.Add(string.Format("{0}", fONO + " " + fID));



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

        public ObservableCollection<string> ItemsEidh
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
                    OnPropertyChanged(nameof(ItemsEidh));
                }
            }
        }

        public Command ItemTappedCommand => new Command((data) =>
        {
            mainPage.DisplayAlert("Τραπέζι Νο ", data + "", "Ok");
            //Globals.gIDPARAGG = (string)data;  





        });

        public static implicit operator MainEIDHPageModel(MainXAR1PageModel v)
        {
            throw new NotImplementedException();
        }
    }


    public class MainXAR1PageModel : BindableObject
    {
        public SqlConnection con;
        private trapeziEpil mainPage;

       // public ICommand ItemTappedCommand { get; set; }

        public MainXAR1PageModel(trapeziEpil mainPage)
        {
            this.mainPage = mainPage;
            AddItems();
           // ItemTappedCommand = new Command(() => YourSub());
        }

      

        private  void AddItems()

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


                // ΒΡΙΣΚΩ ΤΟ CH2 POY EXEI TA PROSUETA TOY EIDOYS
                DataTable dt0 = new DataTable();
                SqlCommand cmd30 = new SqlCommand("select CH2 FROM EIDH WHERE ID="+Globals.gIDEIDOS,  con);
                var adapter20 = new SqlDataAdapter(cmd30);
                adapter20.Fill(dt0);
                string cc = dt0.Rows[0][0].ToString()+"0";



                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("SELECT    ONO, ID FROM XAR1 where ID IN ("+cc+") ; ", con);
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
                    Itemsx.Add(string.Format("{0}", fONO + " " + fID));



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

        public ObservableCollection<string> Itemsx
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
                    OnPropertyChanged(nameof(Itemsx));
                }
            }
        }

        public Command ItemTappedCommand => new Command((data) =>
        {
            mainPage.DisplayAlert("Τραπέζι Νο ", data + "", "Ok");
            Globals.gIDPARAGG = (string)data;      
        });
    }



}