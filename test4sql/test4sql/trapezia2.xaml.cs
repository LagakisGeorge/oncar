using System.Collections.Generic;
using System.Collections.ObjectModel;
using test4sql;
using Xamarin.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System;

using Xamarin.Forms.Xaml;

using System.Net.Sockets;
using System.Text;

using Mono.Data.Sqlite;
using Org.Apache.Http.Authentication;
using SharpCifs.Util.Sharpen;












namespace oncar
{
    public partial class trapezia2 : ContentPage
    {
        MainPageModel pageModel;
        public IList<Monkey> Monkeys { get; private set; }
        public trapezia2()
        {
            InitializeComponent();
            //  pageModel = new MainPageModel(this);
            //  BindingContext = pageModel;
           //   Globals.gIDBARDIA = "1"; // αυθαιρετα μεχρι να ορισω βαρδιεσ
           // this.Navigation.PopAsync();
           //this.Navigation.RemovePage( );
           // toparagg();


            //   Navigation.PushAsync(new trapparagg());  //imports

        }

 protected override void OnAppearing()
        {
            base.OnAppearing();

            //  Globals.gTrapezi = "";

           // Show_listsql_Paragg(Globals.gIDPARAGG);
            pageModel = new MainPageModel(this);
            BindingContext = pageModel;
        }


        private async  void toparagg()
        {
           // await Application.Current.MainPage.Navigation.PopAsync();
            await Navigation.PushAsync(new trapparagg ());  //imports
           
        }

       







        private async void doit(object sender, ItemTappedEventArgs e)
        {
            MainPageModel tappedItem = e.Item as MainPageModel;
            //  Items.Add(string.Format(" {0} ", fkat + fONO + "*" + fID+"*"+CH1));
            Globals.gtIDPARAGG = e.Item.ToString();  // a15 12356
            
           // DisplayAlert("Τραπέζι Νο ", e.Item.ToString(), "Ok");
            await Navigation.PushAsync(new trapparagg());  //imports
            
        }
  





    }

    public class MainPageModel : BindableObject
    {

        public SqlConnection con;
        private trapezia2 mainPage;

        public MainPageModel(trapezia2 mainPage)
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


            string SYNT = "";

            try
            {
                //Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("SELECT  ONO,ISNULL(KATEILHMENO,0) AS KATEILHMENO,ISNULL(IDPARAGG,0) AS IDPARAGG,ISNULL(CH1,'') AS CH1 from TABLES WHERE NUM1="+ Globals.gUserWaiter, con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    string fONO;
                    fONO = dt.Rows[k]["ONO"].ToString();
                    string CH1;
                    CH1= dt.Rows[k]["CH1"].ToString();
                    if (CH1.Length == 0)
                    { CH1 = ""; }
                    else { CH1 = CH1 + "€"; }
                    string fID;
                    fID = dt.Rows[k]["idparagg"].ToString();
                    string fkat = dt.Rows[k]["kateilhmeno"].ToString();
                    if (fkat == "0") { fkat = ""; } else { fkat = "# "; }
                    Items.Add(string.Format(" {0} ", fkat + fONO + "*" + fID+"*   "+CH1));
                   // Items.Add(string.Format(" {0} ", fkat + fONO + " " + fID));

                    // Items.Add(string.Format("{0}", fONO + " " + fID));



                } // FOR
                //  BindingContext = this;
            }
            catch (Exception ex)
            {
                string error=ex.ToString();
                //await  DisplayAlert("Error", ex.ToString(), "OK");
            }

            con.Close();





            //string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"adodemo.db3");
            //SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            //connection.Open();
            //string ff = "";
            //try
            //{   var contents = connection.CreateCommand();
            //    contents.CommandText = "SELECT  ONO,IFNULL(KATEILHMENO,0) AS KATEILHMENO,IFNULL(IDPARAGG,0) AS IDPARAGG from TABLES";
            //    var r = contents.ExecuteReader();
            //    while (r.Read())
            //    {
            //        string fkat= r["kateilhmeno"].ToString();
            //        string fONO = r["ono"].ToString();
            //        string fID = r["idparagg"].ToString();
            //        // fEKPTNUM1 = float.Parse(r["NUM12"].ToString());
            //        // fYPOLPEL = float.Parse(r["TYP2"].ToString());
            //        // LPLIR.Text = r["ID"].ToString() + " Εκπ:" + r["NUM12"].ToString();
            //        if (fkat=="0") { fkat = ""; } else { fkat = "# "; }
            //        Items.Add(string.Format(" {0} ", fkat+fONO+" "+fID ));
            //    }
            //}
            //catch
            //{               ff = "error";            
            //}
            //connection.Close();
        }

        private ObservableCollection<string> _items = new ObservableCollection<string>();
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
            //string[] lines = Globals.gIDPARAGG.Split(' ');
            //if (lines.Length > 1)
            //{
            //  Globals.gIDPARAGG = lines[1];
            //}
            //else
            //{
            //    Globals.gIDPARAGG = "0";
            //}
            //// ClearItems(Items);
            //// Items.Clear();
            //// for (int i = 0; i < 40; i++)
            //////    Items.Add(string.Format("Τραπ {0} table{0}", i));
            ////  AddItems();
        });
    }







}