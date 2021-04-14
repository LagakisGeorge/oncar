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

namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View1 : ContentPage
    {
        public IList<Monkey> Monkeys { get; private set; }
        public string f_cid = "";
        public SqlConnection con;

        public View1()
        {
            InitializeComponent();
            Monkeys = new List<Monkey>();
        }
        //protected override bool OnBackButtonPressed()
        //{
        //    return false;
        //}
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Monkey selectedItem = e.SelectedItem as Monkey;
            string c = selectedItem.idPEL;
            f_cid = c;
            /*  
            */
        }
        async void Diag_barcode(object sender, EventArgs e)
        {

            var action = await DisplayAlert("Να διαγραφεί?", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {
                //  Navigate to first page
                MainPage.ExecuteSqlite("delete from PARALABES WHERE ID=" + f_cid);
                await DisplayAlert("διαγραφτηκε", "", "OK");
                Show_list();
            }




        }

        
        async void delete_all(object sender, EventArgs e)
        {
            var action = await DisplayAlert("Να διαγραφoύν όλα τα σκαναρισματα?", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {
                //  Navigate to first page
                MainPage.ExecuteSqlite("delete from PARALABES ");
                await DisplayAlert("διαγραφτηκε", "", "OK");
               // show_list();
            }
        }
        



        async void SaveFile(string text)
        {

            // DESKTOP-MPGU8SB\SQL17
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

//            string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            // ok fine string constring = @"Data Source=DESKTOP-MPGU8SB\SQL17,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";
            // ok works fine string constring = @"Data Source=192.168.1.10,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            con = new SqlConnection(constring);

            try
            {
                con.Open();

                // await DisplayAlert("Συνδεθηκε", "οκ", "OK");

                // ***************  demo πως τρεχω εντολη στον sqlserver ********************************
                SqlCommand cmd = new SqlCommand("insert into PALETES(PALET) values (1)");
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }

            return;

            try
            {


                //Get the SmbFile specifying the file name to be created.
                var file = new SmbFile("smb://" + Globals.cIP + "/eggtim2.txt");
                // fine var file = new SmbFile("smb://User:1@192.168.1.5/backpel/New2FileName.txt");

                if (file.Exists())
                {
                    DisplayAlert("Θα διαγραφει το παλιό αρχειο", "....", "OK");
                    file.Delete();  // https://csharpdoc.hotexamples.com/class/SharpCifs.Smb/SmbFile#
                }
                // else
                {
                    //Create file.
                    file.CreateNewFile();
                }


                /*            try
                            {
                                //Create file.
                                file.CreateNewFile();
                            }
                            catch
                            {
                                DisplayAlert("Θα διαγραφει το ηδη το αρχειο", "....", "OK");
                                file.Delete();  // https://csharpdoc.hotexamples.com/class/SharpCifs.Smb/SmbFile#
                            }
                */

                //Get writable stream.
                var writeStream = file.GetOutputStream();

                //Write bytes.
                writeStream.Write(Encoding.UTF8.GetBytes(text));

                //Dispose writable stream.
                writeStream.Dispose();


            }
            catch
            {
                DisplayAlert("Δεν γράφτηκε το αρχειο", "....", "OK");
                // file.Delete();  // https://csharpdoc.hotexamples.com/class/SharpCifs.Smb/SmbFile#
            }





        }

        async void CloseInvoice(object sender, EventArgs e)
        {
            await Navigation.PopAsync();



        }

        void Show_list()
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
  //          contents.CommandText = "SELECT  * from PARALABES where ATIM ='" + cATIM.Text + "' order by ID DESC ; "; // +BARCODE.Text +"'";
                                                                                                                    // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            while (r.Read())
            {


                Monkeys.Add(new Monkey
                {
                    Name = r["BARCODE"].ToString(),

                    Location = r["ATIM"].ToString(),
                    ImageUrl = "",
                    idPEL = r["id"].ToString()
                });



            }




            connection.Close();

            BindingContext = this;
        }

        async void WriteFile(object sender, EventArgs e)
        {
           // DIABAZO sqlite database KAI TA SOZO :
            // se sqlserver
            // KAI SE KOINO FAKELO SAN ARXEIO  EGGTIM2.TXT
            //  -----------------SQLSERVER  1.SYNDESH   ---------------------------------------
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

//            string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            con = new SqlConnection(constring);
            try
            {
                con.Open();                
            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }
            //  -----------------SQLSERVER ---------------------------------------
            /*
            // '=====================  EGGTIM2.TXT  ======================================================'
            try
            {
                //Get the SmbFile specifying the file name to be created.
                var file = new SmbFile("smb://" + Globals.cIP + "/eggtim2.txt");
            // fine var file = new SmbFile("smb://User:1@192.168.1.5/backpel/New2FileName.txt");
            if (file.Exists())
            {
                DisplayAlert("Θα διαγραφει το παλιό αρχειο", "....", "OK");
                file.Delete();  // https://csharpdoc.hotexamples.com/class/SharpCifs.Smb/SmbFile#
            }
           // else
            {
                //Create file.
                file.CreateNewFile();
            }
                       //try
                       // {
                       //     //Create file.
                       //     file.CreateNewFile();
                       // }
                       // catch
                       // {
                       //     DisplayAlert("Θα διαγραφει το ηδη το αρχειο", "....", "OK");
                       //     file.Delete();  // https://csharpdoc.hotexamples.com/class/SharpCifs.Smb/SmbFile#
                       // }
           

               //Get writable stream.
               var writeStream = file.GetOutputStream();

               //Write bytes.
               writeStream.Write(Encoding.UTF8.GetBytes(text));

               //Dispose writable stream.
               writeStream.Dispose();


            }
            catch
            {
                DisplayAlert("Δεν γράφτηκε το αρχειο", "....", "OK");
               // file.Delete();  // https://csharpdoc.hotexamples.com/class/SharpCifs.Smb/SmbFile#
            }
            // '=====================  EGGTIM2.TXT  ======================================================'
            */

            // ***************  demo πως τρεχω εντολη στον sqlserver ********************************
          //  SqlCommand cmd = new SqlCommand("insert into PALETES(PALET,KOD,PARTIDA,POSO) values (1)");
          //  cmd.Connection = con;
          //  cmd.ExecuteNonQuery();





            string dbPath = Path.Combine(
                     Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                     "adodemo.db3");
            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            // Open the database connection and create table with data
            connection.Open();
            // query the database to prove data was inserted!
            var contents = connection.CreateCommand();
            contents.CommandText = "SELECT* FROM EGGTIM";
            var r = contents.ExecuteReader();
            // Console.WriteLine("Reading data");

            String cPal, cPart, cPos, cKod;
            string cc;//= "INSERT INTO EGGTIM (ATIM,HME,KODE,POSO,TIMM) VALUES (";


            try
            {


                while (r.Read())
                {
                    string[] lines2 = r["HME"].ToString().Split('/');
                    cc = "INSERT INTO PEGGTIM (ATIM,HME,KODE,POSO,TIMM) VALUES ('";
                    cc +=  r["ATIM"].ToString() + "','";
                    cc +=  lines2[1]+"/"+lines2[0]+"/"+lines2[2].Substring(0,4) + "','";
                    cc +=  r["KODE"].ToString() + "',";
                    cc +=  r["POSO"].ToString().Replace(",",".") + ",";
                    cc += r["TIMH"].ToString().Replace(",", ".") + ")";
                    // cc = cc + r["TIMH"].ToString() + "\n";  11  12->  21,5




                   


                    SqlCommand cmd = new SqlCommand(cc);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();




                }
               



                CrossToastPopUp.Current.ShowToastMessage("Αποθηκεύτηκε");
                //  SaveFile(cc);

              //  MainPage.ExecuteSqlite("DELETE FROM  PARALABES");


            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }





             contents = connection.CreateCommand();
            contents.CommandText = "SELECT* From TIM";
            r = contents.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    string[] lines2 = r["HME"].ToString().Split('/');
                    cc = "INSERT INTO PTIM (ATIM,HME,KPE,AJI) VALUES ('";
                    cc += r["ATIM"].ToString() + "','";
                    cc += lines2[1] + "/" + lines2[0] + "/" + lines2[2].Substring(0, 4) + "','";
                    cc += r["KPE"].ToString() + "',";
                    cc += r["AJI"].ToString().Replace(",", ".") + ")";
                   // cc += r["TIMH"].ToString().Replace(",", ".") + ")";

                    SqlCommand cmd = new SqlCommand(cc);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                connection.Close();



                CrossToastPopUp.Current.ShowToastMessage("Αποθηκεύτηκε");
                //  SaveFile(cc);

                //  MainPage.ExecuteSqlite("DELETE FROM  PARALABES");


            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }




        }








        async void PaletaChanged(object sender, EventArgs e)
        {


        }
      
        
        
        async void barcfoc(object sender, EventArgs e)
        {

 
        }


        async void paletfoc()
        {
           
           
        }



        void ViewPalet(object sender, EventArgs e)
        {
            Show_list();
            paletfoc();

          //  Paleta.Focus();




        }

    }
}