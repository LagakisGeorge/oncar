﻿using System;
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
    public partial class PELATES : ContentPage
    {

      
  public IList<Monkey> Monkeys { get; private set; }

        public PELATES()
        {
            InitializeComponent();
            bkatax.IsVisible = false;
            diagpel.IsVisible = false;
            antig.IsVisible = false;
            bkin.IsVisible = false;
            bedit.IsVisible = false;


            // EPO.IsEnabled = false;
        }

        private void NEWPEL(object sender, EventArgs e)
        {
            bkatax.IsEnabled = true;
            bkatax.IsVisible = true;
            bnew.IsEnabled = false;
            bnew.BackgroundColor = Color.Green;
            bedit.IsVisible  = false ; // IsEnabled = false;

        }

        private void EDITPEL(object sender, EventArgs e)
        {

            bkatax.IsEnabled = true;
            bkatax.IsVisible = true;
            bnew.IsEnabled = false;
            bnew.BackgroundColor = Color.Yellow ;
            bedit.IsVisible = false; // IsEnabled = false;
            diagpel.IsVisible = false;
            antig.IsVisible = false;
            bkin.IsVisible = false;






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
            contents.CommandText = "SELECT  ifnull(KOD,'') as KODI,ifnull(EPO,'') AS PER,ifnull(THL,'') as THL,KINHTO,ID from PEL where EPO LIKE '%" + ono + "%'  order by EPO ; "; // +BARCODE.Text +"'";
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

            listview.ItemsSource = Monkeys;
            BindingContext = this;


            connection.Close();

            BindingContext = this;
        }


        private async void KATAX(object sender, EventArgs e)
        {
            Kataxorisi();
            await DisplayAlert("Κατεχωρήθη", "", "ok");
            bkatax.IsVisible =false;
            bnew.IsEnabled = true;
            bnew.BackgroundColor = Color.Gray ;




            // bedit.IsVisible = true; // IsEnabled = false;
        }


        private async void Kataxorisi()
        {
            string DD;
            bedit.IsEnabled = false;
           

            if (bnew.IsEnabled == false && bnew.BackgroundColor == Color.Green) // προκειται για νεα εγγραφη
            {
                try
                {
                    if (EPO.Text==null)
                
                    {
                      await  DisplayAlert("δεν συμπληρώσατε όνομα", "","ok");
                        return;
                    }
                    string mmKod ;
                    mmKod = PARAGGELIES.ReadSQL("select IFNULL(ARITMISI,0) AS EKTP2 FROM ARITMISI WHERE ID=8");


                    MainPage.ExecuteSqlite("INSERT INTO PEL (KOD,EPO) VALUES ('" + mmKod + "','" + EPO.Text + "')");

                    DD  = PARAGGELIES.ReadSQL("select MAX(ID) FROM PEL ");

                }
                catch
                {
                    await DisplayAlert("ΔΕΝ ΑΠΟΘΗΚΕΥΤΗΚΕ", "", "OK");
                    return;

                }

                MainPage.ExecuteSqlite("UPDATE ARITMISI SET ARITMISI=ARITMISI+1 WHERE ID=8");
            }    
            else
            {   // diortosi
                DD = LID.Text;
                if (DD.Length == 0)
                {   // den διαλεξα τιποτα
                    return;
                }   else
                {
                    // ok dioruvnei
                }
            }
            try
            {
                MainPage.ExecuteSqlite("UPDATE PEL  SET EPO='" + EPO.Text + "' WHERE ID="+DD);
                MainPage.ExecuteSqlite("UPDATE PEL  SET EPA='" + EPA.Text + "' WHERE ID="+DD);
                MainPage.ExecuteSqlite("UPDATE PEL  SET POL='" + POL.Text + "' WHERE ID="+DD);

                MainPage.ExecuteSqlite("UPDATE PEL  SET DIE='" + DIE.Text + "' WHERE ID="+DD);
                MainPage.ExecuteSqlite("UPDATE PEL  SET THL='" + THL.Text + "' WHERE ID="+DD);
                MainPage.ExecuteSqlite("UPDATE PEL  SET TK='" + TK.Text + "' WHERE ID="+DD);
                MainPage.ExecuteSqlite("UPDATE PEL  SET KINHTO='" + KINHTO.Text + "' WHERE ID="+DD);
                MainPage.ExecuteSqlite("UPDATE PEL  SET EMAIL='" + EMAIL.Text + "' WHERE ID="+DD);
                MainPage.ExecuteSqlite("UPDATE PEL  SET EMAIL2='" + EMAIL2.Text + "' WHERE ID="+DD);
                MainPage.ExecuteSqlite("UPDATE PEL  SET MEMO='" + MEMO.Text + "' WHERE ID="+DD);
            }
            catch
            {
                await DisplayAlert("ΑΠΟΘΗΚΕΥΤΗΚΕ O ΠΕΛΑΤΗΣ //ΔΕΝ ΑΠΟΘΗΚΕΥΤΗΚΑΝ ΟΛΑ ΤΑ ΠΕΔΙΑ", "", "OK");
                return;

            }
            bnew.IsEnabled = true;
            bkatax.IsEnabled = false;


        }

        private void BRESPEL(object sender, TextChangedEventArgs e)
        {
            Show_list_Eidon( FEPO.Text);
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

            Monkey tappedItem = e.Item as Monkey;
            LIDtest.Text = e.ItemIndex.ToString() ;
            LID.Text= tappedItem.idPEL;
            // tappedItem.Location=>'00182'
            //tappedItem.Name=>"ΜΙΖΑΜΤΣΙΔΟΥ ΔΕΣΠΟΙΝΑ"
            // if (fisEIDH == 0)
            // {
            //  BRESafm.IsEnabled = false;
            string id= tappedItem.idPEL ;
            EPO.Text = PARAGGELIES.ReadSQL("select IFNULL(EPO,'') AS EKTP2 FROM PEL WHERE ID="+id );
            EPA.Text = PARAGGELIES.ReadSQL("select IFNULL(EPA,'') AS EKTP2 FROM PEL WHERE ID=" + id);
            DIE.Text = PARAGGELIES.ReadSQL("select IFNULL(DIE,'') AS EKTP2 FROM PEL WHERE ID=" + id);
            TK.Text = PARAGGELIES.ReadSQL("select IFNULL(TK,'') AS EKTP2 FROM PEL WHERE ID=" + id);
            THL.Text = PARAGGELIES.ReadSQL("select IFNULL(THL,'') AS EKTP2 FROM PEL WHERE ID=" + id);
            KINHTO.Text = PARAGGELIES.ReadSQL("select IFNULL(KINHTO,'') AS EKTP2 FROM PEL WHERE ID=" + id);

            EMAIL.Text = PARAGGELIES.ReadSQL("select IFNULL(EMAIL,'') AS EKTP2 FROM PEL WHERE ID=" + id);
            EMAIL2.Text = PARAGGELIES.ReadSQL("select IFNULL(EMAIL2,'') AS EKTP2 FROM PEL WHERE ID=" + id);
            MEMO.Text = PARAGGELIES.ReadSQL("select IFNULL(MEMO,'') AS MEMO FROM PEL WHERE ID=" + id);
           
            bkatax.IsVisible = false;  // IsEnabled = true;
            bnew.IsVisible = false;
            bedit.IsVisible = true;
            bedit.IsEnabled  = true;
            antig.IsVisible = true;
            diagpel.IsVisible = true;
            diagpel.IsEnabled = true;



        }

      

        private async void newkin(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Pelkin ());

        }

        private async void Antig(object sender, EventArgs e)
        {
            if (EPO.Text == null)
            {
                await DisplayAlert("δεν διαλεξατε πελάτη", "", "ok");
                return;
            }
 
                bnew.IsEnabled = false;
            bnew.BackgroundColor = Color.Green;
                antig.IsEnabled = false;
                bkatax.IsEnabled = false;
            //  (bnew.IsEnabled == false && bnew.BackgroundColor == Color.Green) // προκειται για νεα εγγραφη
            Kataxorisi();
            bnew.BackgroundColor = Color.Gray ;

            await DisplayAlert("Η αντιγραφή έγινε", "", "ok");
            EPO.Text = "";
            EPA.Text = "";
            DIE.Text  = "";
            POL.Text = "";



           





        }

        private async void Diagrpel(object sender, EventArgs e)
        {

            var action = await DisplayAlert(" Να διαγραφεί η εγγραφή", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action == true)
            {
                string DD;
                DD = PARAGGELIES.ReadSQL("select count(*)  FROM EGG WHERE IDPEL=" + LID.Text);

                float xr = 0;
                xr = Convert.ToInt64(Convert.ToDouble(DD)); //  r["xre"];

                if (xr>0) {
                    action = await DisplayAlert(" Εχει κινήσεις αδύνατη η διαγραφή", "", "ΟΚ", "");
                    return;
                }

                MainPage.ExecuteSqlite("delete from PEL where  ID='" + LID.Text  + "'");
                Show_list_Eidon(FEPO.Text);

                diagpel.IsVisible = false;
                diagpel.IsEnabled = false;


                antig.IsVisible = false;
                antig.IsEnabled = false;

            }



        }
    }
    }