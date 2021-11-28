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
using test4sql;


namespace oncar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pelkin : ContentPage
    {
        public IList<Monkey> Monkeys { get; private set; }
        public IList<Monkey> Monkeys2 { get; private set; }

        string fIDPEL;

        public Pelkin()
        {
            InitializeComponent();
            fIDPEL = "";
        }


        private void BRESPEL(object sender, TextChangedEventArgs e)
        {
            Show_list_Eidon(FEPO.Text);
        }



        void Show_list_Eidon(string ono)
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
            contents.CommandText = "SELECT  ifnull(KOD,'') as KODI,ifnull(EPO,'') AS PER,ifnull(THL,'') as THL,ID from PEL where EPO LIKE '%" + ono + "%'  order by EPO ; "; // +BARCODE.Text +"'";
                                                                                                                                                                             // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {


                Monkeys.Add(new Monkey
                {
                    Name = (r["PER"].ToString() + "                         ").Substring(0, 18),

                    Location = (r["KODI"].ToString() + "      ").Substring(0, 5),
                    ImageUrl = (r["THL"].ToString() + "            ").Substring(0, 9),
                    idPEL = r["ID"].ToString()
                });



            }

            listview.ItemsSource = Monkeys;
            BindingContext = this;


            connection.Close();

            BindingContext = this;
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Monkey tappedItem = e.Item as Monkey;
            pelaths.Text = tappedItem.Name +";"+ tappedItem.idPEL;

            fIDPEL = tappedItem.idPEL;

          

          //  string id = tappedItem.idPEL;
          //  EPO.Text = PARAGGELIES.ReadSQL("select IFNULL(EPO,'') AS EKTP2 FROM PEL WHERE ID=" + id);
        }

        private void EDITPEL(object sender, EventArgs e)
        {

            Show_list_Egg(fIDPEL);

        }

        private void KATAX(object sender, EventArgs e)
        {

        }

        private void NEWPEL(object sender, EventArgs e)
        {


            try
            {
                string SQL1 = "insert into EGG (IDPEL,AIT,ATIM,HME,KOD,XRE,PIS) VALUES ("+ fIDPEL+",'"+aiti.Text+"','" + "T0001" + "', datetime('now'),'" + "001" + "'," + poso.Text+ "," + "0.00" + ")";
                MainPage.ExecuteSqlite(SQL1);
                
            }
            catch
            {
                // await DisplayAlert("ΔΕΝ ΑΠΟΘΗΚΕΥΤΗΚΕ", "", "OK");

            }

        }

        void Show_list_Egg(string idpel)
        {
            Monkeys2 = new List<Monkey>();
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
            contents.CommandText = "SELECT  ifnull(HME,'') as HM,AIT,XRE,PIS from EGG where IDPEL="+idpel+"  order by HME ; "; // +BARCODE.Text +"'";
                                                                                                                                                                             // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {


                Monkeys2.Add(new Monkey
                {
                    Name = (r["AIT"].ToString() + "                         ").Substring(0, 18),

                    Location = (r["HM"].ToString() + "      ").Substring(4, 4),
                    ImageUrl = (r["AIT"].ToString() + "            ").Substring(0, 9),
                    idPEL = r["XRE"].ToString()
                });



            }

            listkin.ItemsSource = Monkeys2;
            BindingContext = this;


            connection.Close();

            BindingContext = this;
        }

        private void OnList2ViewItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}