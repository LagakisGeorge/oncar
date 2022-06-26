using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test4sql;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mono.Data.Sqlite;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace oncar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PelReports : ContentPage
    {



        public IList<Monkey> Monkeys { get; private set; }




        public PelReports()
        {
            InitializeComponent();
        }

        private async void fPELREP2(object sender, EventArgs e)
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
               // contents.CommandText = "SELECT  ifnull(KOD,'') as KODI,ifnull(EPO,'') AS PER,ifnull(THL,'') as THL,KINHTO,ID from PEL where EPO LIKE '%%'  order by EPO ; "; // +BARCODE.Text +"'";
                contents.CommandText = "SELECT  ifnull(KOD,'') as KODI,ifnull(EPO,'') AS PER,ifnull(THL,'') as THL,ID from PEL where EPO LIKE '%%'  order by EPO ; "; // +BARCODE.Text +"'";


            // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
                Console.WriteLine("Reading data");
                while (r.Read())
                {


                    //Monkeys.Add(new Monkey
                    //{
                    //    Name = (r["PER"].ToString() + "                         ").Substring(0, 18),

                    //    Location = (r["KODI"].ToString() + "      ").Substring(0, 5),
                    //    ImageUrl = (r["THL"].ToString() + "            ").Substring(0, 9),
                    //    idPEL = r["ID"].ToString()
                    //});

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

        }
    }

}