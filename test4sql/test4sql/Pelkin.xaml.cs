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
using Javax.Xml.Xpath;

namespace oncar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pelkin : ContentPage
    {
        public IList<Monkey> Monkeys { get; private set; }
        public IList<Monkey2> Monkeys2 { get; private set; }

        string fIDPEL;

        public Pelkin()
        {
            InitializeComponent();
            fIDPEL = "";
            FEPO.Focus();
        }


        private void BRESPEL(object sender, TextChangedEventArgs e)
        {
            listview.IsVisible = true;
            listkin.IsVisible = false;
            listERG.IsVisible = false;
            Show_list_Eidon(FEPO.Text);
           // Show_list_Egg(fIDPEL);
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

            ono = ono.ToUpper();
            var contents = connection.CreateCommand();

            contents.CommandText = "SELECT  ifnull(KOD,'') as KODI,ifnull(EPO,'') AS PER,ifnull(THL,'') as THL,ID from PEL where EPO LIKE '%" + ono + "%'  order by EPO ; "; // +BARCODE.Text +"'";
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

            listview.ItemsSource = Monkeys;
            BindingContext = this;


            connection.Close();

            BindingContext = this;
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Monkey tappedItem = e.Item as Monkey;
            pelaths.Text = tappedItem.Name +";"+ tappedItem.idPEL;
            string id= tappedItem.idPEL;
           
            EPA.Text = PARAGGELIES.ReadSQL("select IFNULL(EPA,'') AS EKTP2 FROM PEL WHERE ID=" + id);
            DIE.Text = PARAGGELIES.ReadSQL("select IFNULL(DIE,'') AS EKTP2 FROM PEL WHERE ID=" + id);
           
            THL.Text = PARAGGELIES.ReadSQL("select IFNULL(THL,'') AS EKTP2 FROM PEL WHERE ID=" + id);
            KINHTO.Text = PARAGGELIES.ReadSQL("select IFNULL(KINHTO,'') AS EKTP2 FROM PEL WHERE ID=" + id);
            MEMO.Text = PARAGGELIES.ReadSQL("select IFNULL(MEMO,'') AS MEMO FROM PEL WHERE ID=" + id);

            fIDPEL = tappedItem.idPEL;
            listview.IsVisible = false;
            btam.IsVisible = false;
            listERG.IsVisible = false;
            listkin.IsVisible = true;
            Show_list_Egg(fIDPEL);



          //  string id = tappedItem.idPEL;
          //  EPO.Text = PARAGGELIES.ReadSQL("select IFNULL(EPO,'') AS EKTP2 FROM PEL WHERE ID=" + id);
        }

        private async void EDITPEL(object sender, EventArgs e)
        {

            if (fIDPEL.Length == 0)
            {
                await DisplayAlert("δεν διαλέξατε πελάτη", "", "");
                return;

            }

            listview.IsVisible = false;
          
            listERG.IsVisible = false;
            listkin.IsVisible = true;


            Show_list_Egg(fIDPEL);

        }

        private void KATAX(object sender, EventArgs e)
        {

        }

        private void NEWPEL(object sender, EventArgs e)
        {


            try
            {
                // string SQL1 = "insert into EGG (IDPEL,AIT,ATIM,HME,KOD,XRE,PIS) VALUES ("+ fIDPEL+",'"+aiti.Text+"','" + "T0001" + "', datetime('now'),'" + "001" + "'," + poso.Text+ "," + "0.00" + ")";
                string mtam = "1";
                if (btam.Text == "TAM")
                { mtam = "1";}
                else
                { mtam = "0";}
                
                string cc2 = hmer.Date.ToString("yyyy/MM/dd"); //DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")'

                // string cc = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");


                if (poso.Text.Length == 0)
                {
                    poso.Text = "0";
                }


                if (btam.IsVisible)
                {
                    poso.Text = "-" + poso.Text;
                };

                string SQL1 = "insert into EGG (IDPEL,AIT,ATIM,HME,KOD,XRE,PIS) VALUES (" + fIDPEL + ",'" + aiti.Text + "','" + "T0001" + "','"+ cc2 +"','" + "001" + "'," + poso.Text + "," +mtam + ")";


                MainPage.ExecuteSqlite(SQL1);

                aiti.Text = "";
                poso.Text = "";

                Show_list_Egg(fIDPEL);
                btam.IsVisible = false;

            }
            catch
            {
                // await DisplayAlert("ΔΕΝ ΑΠΟΘΗΚΕΥΤΗΚΕ", "", "OK");

            }
            btam.BackgroundColor = Color.Gray;
            Show_list_Egg(fIDPEL);


        }

        void Show_list_Egg(string idpel)
        {
            Monkeys2 = new List<Monkey2>();
            BindingContext = null;//45596/


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
            contents.CommandText = "SELECT  ifnull(HME,'          ') as HM,AIT, XRE , PIS,ID from EGG where IDPEL=" + idpel+ "  order by ID; "; // +BARCODE.Text +"'";
                                                                                                                                                                             // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            float ProodYp = 0;
            while (r.Read())
            {

              
                string mx="";
                string mp = "";
                float xr = 0;
                xr = Convert.ToInt64(Convert.ToDouble(r["xre"])); //  r["xre"];
                ProodYp = ProodYp + xr;
     string ait = "";

                if (xr   >= 0)  // xreosi
                {
                    mx = (xr.ToString() + "            ").Substring(0, 9);
                    mp = ("            ").Substring(0, 9);
                    ait = (r["AIT"].ToString() + "                         ").Substring(0, 18);
                }
                else    // pistosi
                {
                    xr = -xr;
                    mp = (xr.ToString() + "            ").Substring(0, 9);
                    mx = ("            ").Substring(0, 9);


                    if (Convert.ToInt64(Convert.ToDouble(r["pis"])) > 0)  // tameiakh
                    {
                        ait = "ΑΠΥ" + (r["AIT"].ToString() + "                         ").Substring(0, 17);
                    }
                    else
                    {
                        ait = (r["AIT"].ToString() + "                         ").Substring(0, 18);

                    }




                }





                // ; //  r["xre"];

                if (xr >= 0)
                    Monkeys2.Add(new Monkey2
                    {
                        Location = ait,

                        Name = r["HM"].ToString().Substring(8, 2) + "/" + r["HM"].ToString().Substring(5, 2) + "/" + r["HM"].ToString().Substring(2, 2),

                        ImageUrl = mx,
                        idPEL = mp,
                        Prood = ProodYp.ToString (),
                    ID = r["ID"].ToString()
                    })  ;



            }

            listkin.ItemsSource = Monkeys2;
            BindingContext = this;


            connection.Close();

            BindingContext = this;
        }

        private async void OnList2ViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Monkey2 tappedItem = e.Item as Monkey2;
            //   pelaths.Text = tappedItem.Name + ";" + tappedItem.idPEL;

            //  fIDPEL = tappedItem.idPEL;
            //   listview.IsVisible = false;


            var  action = await  DisplayAlert( " Θα διαγραφεί η εγγραφή", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action==true)
            {
                  MainPage.ExecuteSqlite("delete from EGG WHERE ID="+tappedItem.ID+";");
                Show_list_Egg(fIDPEL);

            }




          
        }

        private void tameiaki(object sender, EventArgs e)
        {
           
            if (btam.Text == "TAM")
            {
                btam.Text = "-";
            }
            else
            {
                btam.Text = "TAM";
            }
          

        }

        private void eispr(object sender, EventArgs e)
        {
            beisp.BackgroundColor = Color.Green;
            btam.IsVisible = true;

        }


        void Show_listERGASION()
        {
            Monkeys = new List<Monkey>();
            BindingContext = null;


                Monkeys.Add(new Monkey{ Name = "CAVITATION", Location = "",ImageUrl ="",idPEL = ""});
            Monkeys.Add(new Monkey { Name = "RF ΣΩΜΑΤΟΣ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "RF ΠΡΟΣΩΠΟΥ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "CRYOLIPOLISIS", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "CRYOLASER", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ULTRATONE", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "FUTURA", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "NIST", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "CELLACTOR", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "VITATONE", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "STIM", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "SHOCK WAVE", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "EMS", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "LIPOSLIM", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "S E J", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "STRONG", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "BODYDER", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "PNEUMATRON", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "MESO GUN", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "HYALURONIC PEN", Location = "", ImageUrl = "", idPEL = "" });

            Monkeys.Add(new Monkey { Name = "ΦΩΤΟΛΥΣΗ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΑΠΟΤΡΙΧΩΣΗ ΣΩΜΑΤΟΣ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΑΠΟΤΡΙΧΩΣΗ ΠΡΟΣΩΠΟΥ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΘΕΡΑΠΕΙΑ ΠΡΟΣΩΠΟΥ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΘΕΡΑΠΕΙΑ ΣΩΜΑΤΟΣ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΜΑΣΑΖ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΗΛΕΚΤΡΙΚΗ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΠΡΟΚ.ΣΩΜΑΤΟΣ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΕΝΑΝΤΙ ΣΩΜΑΤΟΣ", Location = "", ImageUrl = "", idPEL = "" }); 
            Monkeys.Add(new Monkey { Name = "ΕΞΩΦΛ.ΣΩΜΑΤΟΣ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΠΡΟΚ.ΠΡΟΣΩΠΟΥ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΕΝΑΝΤΙ ΠΡΟΣΩΠΟΥ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΕΞΩΦΛ.ΠΡΟΣΩΠΟΥ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΕΝΑΝΤΙ ΦΩΤΟΛΥΣΗΣ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΕΞΩΦΛ.ΦΩΤΟΛΥΣΗΣ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΚΑΘΑΡΙΣΜΟΣ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΚΑΛΛΥΝΤΙΚΑ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΦΩΤΟΔΥΝΑΜΙΚΗ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "SSR", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΔΩΡΟ", Location = "", ImageUrl = "", idPEL = "" });
            Monkeys.Add(new Monkey { Name = "ΦΡΥΔΙΑ", Location = "", ImageUrl = "", idPEL = "" });
           



            







































            listERG.ItemsSource = Monkeys;
            BindingContext = this;


          
            BindingContext = this;
        }

        private void ergasies(object sender, FocusEventArgs e)
        {
            listview.IsVisible = false;
            listkin.IsVisible = false;
            listERG.IsVisible = true;
            Show_listERGASION();
        }

        private void OnListViewERGItemTapped(object sender, ItemTappedEventArgs e)
        {

            Monkey tappedItem = e.Item as Monkey;
            aiti.Text = tappedItem.Name ;

           

        }
    }
}