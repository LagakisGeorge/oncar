﻿using Android.Views.Animations;
using Mono.Data.Sqlite;
using SharpCifs.Util.Sharpen;
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
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using static Android.Content.ClipData;

namespace oncar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class trapeziEpil : ContentPage      
    {

        public IList<Monkey> Monkeys { get; private set; }
        public  MainXAR1PageModel pageModelx;
        public int[] fIDEIDON =  new int[500];
        public  MainEIDHPageModel pageModel;
        public double fTem;

        public trapeziEpil()
           
        {
            InitializeComponent();

          //  Show_list_Eidon();
            pageModel = new MainEIDHPageModel(this);
            BindingContext = pageModel;
        }


        private async void okdone(object sender, EventArgs e)
        {
            addParagg();
           // for (int n = 1; n <= (Int32.Parse(tem.Text));  n++)
           // {
                Globals.indexParaggLine = Globals.indexParaggLine + 1;
                Globals.PARAGGlines[Globals.indexParaggLine, 0] = EIDOS.Text;
                Globals.PARAGGlines[Globals.indexParaggLine, 1] =  tem.Text;
                Globals.PARAGGlines[Globals.indexParaggLine, 2] = "0";// tem.Text;
                Globals.PARAGGlines[Globals.indexParaggLine, 3] = "0"; // tem.Text;
                Globals.PARAGGlines[Globals.indexParaggLine, 5] = COMMENTS.Text; // tem.Text;
           // }
            // 10 ψωμια σε 10 σειρες
            //for (int n = 1; n <= (Int32.Parse(tem.Text)); n++)
            //{
            //    Globals.indexParaggLine = Globals.indexParaggLine + 1;
            //    Globals.PARAGGlines[Globals.indexParaggLine, 0] = EIDOS.Text;
            //    Globals.PARAGGlines[Globals.indexParaggLine, 1] = "1"; // tem.Text;
            //    Globals.PARAGGlines[Globals.indexParaggLine, 2] = "0";// tem.Text;
            //    Globals.PARAGGlines[Globals.indexParaggLine, 3] = "0"; // tem.Text;
            //    Globals.PARAGGlines[Globals.indexParaggLine, 5] = COMMENTS.Text; // tem.Text;
            //}





            // await Navigation.PopAsync();


            COMMENTS.Text = "";
            PROSU.Text = "";
            ok.IsEnabled = false;
            tem.Text = "0";

            // ΞΑΝΑΔΕΙΧΝΩ ΤΑ ΕΙΔΗ ΤΗΣ ΚΑΤΗΓΟΡΙΑΣ
            pageModel = new MainEIDHPageModel(this);
            BindingContext = pageModel;
        }


        private void AllExecute(string Query)
        {
            if (Globals.gLocal == "0")
            {
                Globals.ExecuteSQLServer(Query);

               
            }
            else
            {
                Query = Query.ReplaceAll("ISNULL", "ifnull");
                Query = Query.ReplaceAll("GETDATE", "DATE");
                MainPage.ExecuteSqlite(Query);
                
            }

        }
        // AllRead
        private string AllRead(string Query)
        {
            if (Globals.gLocal == "0")
            {
                return Globals.AllRead(Query);

               
            }
            else
            {
                Query = Query.ReplaceAll("ISNULL", "ifnull");
                Query = Query.ReplaceAll("GETDATE", "DATE");
                return PARAGGELIES.ReadSQL(Query); ;
            }

        }




        private void addParagg()
        {


            if (Globals.indexParaggLine == -1)  // νεα παραγγελια
            {
                AllExecute("INSERT INTO PARAGGMASTER (IDERGAZ,NUM1,AJIA,TRAPEZI,IDBARDIA,HME) VALUES ("+ Globals.gUserWaiter+",0, 0,'" + Globals.gTrapezi + "'," + Globals.gIDBARDIA+",GETDATE() )");
                string cIDParagg = "";
                cIDParagg =Globals.AllRead ("select CAST( ISNULL(max(ID),0) AS VARCHAR)  from PARAGGMASTER where TRAPEZI='" + Globals.gTrapezi + "'");
              


                AllExecute("update TABLES SET NUM1="+ Globals.gUserWaiter+" ,KATEILHMENO=1,IDPARAGG=" + cIDParagg + " WHERE ONO='" + Globals.gTrapezi + "';");


          
                Globals.gIDPARAGG = cIDParagg;
            }
            if (tem.Text.Length == 0)
            {
                tem.Text = "0";
                fTem = 0.0;

            }
            else { 

            //  for (int n = 1; n <= (Int32.Parse(tem.Text)); n++)
            // {
                AllExecute("INSERT INTO PARAGG (IDPARAGG,TRAPEZI,ONO,POSO,TIMH,PROSUETA,CH2,NUM1,CH1) VALUES (" + Globals.gIDPARAGG + ",'" + Globals.gTrapezi + "','" + EIDOS.Text + "'," + tem.Text + "," + timh.Text.Replace(",", ".") + ",'" + PROSU.Text + "','',0,'" + COMMENTS.Text + "')");
               // ENA ENA  AllExecute("INSERT INTO PARAGG (IDPARAGG,TRAPEZI,ONO,POSO,TIMH,PROSUETA,CH2,NUM1,CH1) VALUES (" + Globals.gIDPARAGG + ",'" + Globals.gTrapezi + "','" + EIDOS.Text + "',1," + timh.Text.Replace(",", ".") + ",'" + PROSU.Text + "','',0,'" + COMMENTS.Text + "')");
            //  }
  
            
            
            //  cIDParagg = Globals.AllRead("select max(ID) from PARAGGMASTER");
            // TA APLHRVTA  EINAI TO ΝUΜ1=0
            string caji = Globals.AllRead("SELECT cast(round(SUM(   ISNULL(POSO*TIMH,0) )  ,2) as varchar)  FROM PARAGG WHERE NUM1=0 AND IDPARAGG=" + Globals.gIDPARAGG + "");

            AllExecute("update PARAGGMASTER SET AJIA="+caji.Replace(",",".")+" WHERE   ID="+ Globals.gIDPARAGG +  ";");

            AllExecute("update TABLES SET KATEILHMENO=1,CH1='" + caji.Replace(",", ".") + "',NUM1="+ Globals.gUserWaiter+"  WHERE IDPARAGG=" + Globals.gIDPARAGG + "");







            }
         
        }



        private void subplus(object sender, EventArgs e)
        {
            fTem = fTem + 1;
            tem.Text =fTem.ToString() ;

        }

        private void subminus(object sender, EventArgs e)
        {
            fTem = fTem - 1;
            tem.Text = fTem.ToString();

        }

        //void Show_list_Eidon()
        //{
        //    Monkeys = new List<Monkey>();
        //    BindingContext = null;

        //    string dbPath = Path.Combine(
        //      Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        //      "adodemo.db3");
        //    bool exists = File.Exists(dbPath);
        //    if (!exists)
        //    {
        //        Console.WriteLine("Creating database");
        //        // Need to create the database before seeding it with some data
        //        Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);

        //    }

        //    SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
        //    // Open the database connection and create table with data
        //    connection.Open();


        //    var contents = connection.CreateCommand();
        //    contents.CommandText = "SELECT  ifnull(KOD,'') as KODI,ifnull(EPO,'') AS PER,ifnull(THL,'') as THL,KINHTO,ID from PEL   order by EPO ; "; // +BARCODE.Text +"'";
        //                                                                                                                                                                            // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
        //    var r = contents.ExecuteReader();
        //    Console.WriteLine("Reading data");
        //    while (r.Read())
        //    {


        //        Monkeys.Add(new Monkey
        //        {
        //            Name = (r["PER"].ToString() + "                                   ").Substring(0, 28),

        //            Location = (r["KODI"].ToString() + "      ").Substring(0, 5),
        //            ImageUrl = (r["THL"].ToString() + "              ").Substring(0, 11),
        //            idPEL = r["ID"].ToString()
        //        });

        //        /*  Monkeys.Add(new Monkey
        //          {
        //              Name = (r["PER"].ToString() + "                         ").Substring(0, 18),

        //              Location = (r["KODI"].ToString() + "      ").Substring(0, 5),
        //              ImageUrl = (r["THL"].ToString()+" "+ r["KINHTO"].ToString() + "                    ").Substring(0, 20),
        //              idPEL = r["ID"].ToString()
        //          });
        //        */


        //    }

        //   // listview.ItemsSource = Monkeys;
        //   // BindingContext = this;


        //    connection.Close();

        //    BindingContext = this;
        //}

        //private void YourSub()
        //{

        //}



        private void LISTEIDH(object sender, ItemTappedEventArgs e)
        {
            //  Show_list_Eidon();

            //  εχω σημαδεψει το ειδος και δειχνω την τιμη του=========================================
     
            //string cID;
           // τσιμπαω αυτο που πατησε
            MainPageModel tappedItem = e.Item as MainPageModel;
            string c= e.Item.ToString();
            ok.IsEnabled = true;
           

            //  EIDOS.Text = c;


            string[] lines =c.Split('*');
            EIDOS.Text = lines[0];
            timh.Text = lines[1];
            Globals.gIDEIDOS = EIDOS.Text; //  lines[1];

            // αδειο τρπαζζι  π.χ. 12 345
            //if (lines.Length > 1)
            //{
            //     
            //    if (lines.Length == 2)
            //    {
            //        EIDOS.Text = lines[0] ;
            //    }
            //    else
            //    {
            //        if (lines.Length == 3)
            //        {
            //            EIDOS.Text = lines[0] + " " + lines[1];
            //        }else
            //        {  // 4 kai anv
            //            EIDOS.Text = lines[0] + " " + lines[1] + " " + lines[2];
            //        }                        
            //    }                  

            //}
            //else
            //{
            //    EIDOS.Text = lines[0];
            //}
            fTem = 1;
            tem.Text = fTem.ToString ();






          //  Globals.gIDEIDOS = cID ; // e.Item.ToString();
            // DisplayAlert("Τραπέζι Νο ", e.Item.ToString(), "Ok");


            // δειχνω τα προσθετα
            pageModelx = new MainXAR1PageModel(this);
            BindingContext = pageModelx;



        }

        private void LISTXAR1(object sender, ItemTappedEventArgs e)
        {
            // τσιμπαω TO PROSUETO  που πατησε
            //----------------------------------------
            MainXAR1PageModel tappedItem = e.Item as MainXAR1PageModel;
            string c = e.Item.ToString();
            PROSU.Text = PROSU.Text + c;

           //string cID = "";

           // string[] lines = c.Split(' ');

           // // αδειο τρπαζζι  π.χ. 12 345
           // if (lines.Length > 1)
           // {
           //     cID = lines[lines.Length - 1];
           //     if (lines.Length == 2)
           //     {
           //         PROSU.Text =PROSU.Text +" "+ lines[0];
           //     }
           //     else
           //     {
           //         if (lines.Length == 3)
           //         {
           //             PROSU.Text = PROSU.Text +" "+ lines[0] + " " + lines[1];
           //         }
           //         else
           //         {  // 4 kai anv
           //             PROSU.Text = PROSU.Text +" "+ lines[0] + " " + lines[1] + " " + lines[2];
           //         }
           //     }

           // }
           // else
           // {
           //     PROSU.Text = PROSU.Text + lines[0];
           // }

            COMMENTS.Focus();


        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void deleprosu(object sender, EventArgs e)
        {
            PROSU.Text = "";
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

        private void addItemsSqlite()


        {

            try
            {
                string dbPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        "adodemo.db3");
                SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
                connection.Open();
                string CZ = "";
                if (Globals.gTrapezi.Substring(0, 1) == "Π")
                {
                    CZ = "SELECT  ONO, ID,IfNULL(NUM2,0) AS TIMH FROM EIDH WHERE KATHG=" + Globals.gKathg + "  order by ONO ; ";
                }
                else
                {
                    CZ = "SELECT  ONO, ID,TIMH FROM EIDH WHERE KATHG=" + Globals.gKathg + "  order by ONO ; ";
                }




                var contents = connection.CreateCommand();
                contents.CommandText =CZ;

                var r = contents.ExecuteReader();
                while (r.Read())
                {


                    string fONO;
                    fONO = r["ONO"].ToString().Replace("*", " ");
                    string ft;
                    ft = r["timh"].ToString();
                    ItemsEidh.Add(string.Format("{0}", fONO + "*" + ft));



                    


                }
                connection.Close();
            }
            catch
            {
                //ff = "error";
            }




        }





        private async void AddItems()

        {

            if (Globals.gLocal == "0")  // global sqlite or sqlserver{
            {

            } else
            {
                addItemsSqlite();
                return;
            }




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
                string CZ = "";
                if (Globals.gTrapezi.Substring(0, 1) == "Π")
                {
                    CZ = "SELECT  ONO, ID,ISNULL(NUM2,0) AS TIMH FROM EIDH WHERE KATHG=" + Globals.gKathg + "  order by ONO ; ";
                }
                else
                {
                    CZ = "SELECT  ONO, ID,TIMH FROM EIDH WHERE KATHG=" + Globals.gKathg + "  order by ONO ; ";
                }
                    SqlCommand cmd3 = new SqlCommand(CZ, con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    string fONO;
                    fONO = dt.Rows[k]["ONO"].ToString().Replace("*"," ");
                    string fID;
                    fID = dt.Rows[k]["ID"].ToString();
                    string ft;
                    ft = dt.Rows[k]["timh"].ToString();
                    ItemsEidh.Add(string.Format("{0}", fONO + "*"+ft));



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

        private void addItemsSqlite()


        {

        try 
         { 
            string dbPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    "adodemo.db3");
            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();

            // ΒΡΙΣΚΩ ΤΟ CH2 POY EXEI TA PROSUETA TOY EIDOYS
         //   DataTable dt0 = new DataTable();
            string CZ = "";
            string cc = "";
            CZ = "select CH2 FROM EIDH WHERE ONO='" + Globals.gIDEIDOS + "'";

            var contents1 = connection.CreateCommand();
            contents1.CommandText = CZ;
            var r1 = contents1.ExecuteReader();
            while (r1.Read())
            {
                 cc = r1["CH2"].ToString() + "0";

            }




                //string ff = "";
           


                var contents = connection.CreateCommand();
                contents.CommandText = "SELECT    ONO, ID FROM XAR1 where ID IN (" + cc + ") ; ";
                var r = contents.ExecuteReader();
                while (r.Read())
                {


                    string fONO;
                    fONO = r["ONO"].ToString();

                    Itemsx.Add(string.Format("{0}", fONO));


                } 
                connection.Close();
            }
            catch
            {
                //ff = "error";
            }
           



        }

    

        

            



            private void AddItems()

        {

            if (Globals.gLocal == "0")  // global sqlite or sqlserver
            { }
            else { 
                addItemsSqlite();
                return;
            }




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
                string CZ = "";
                //if (Globals.gTrapezi.Substring(0, 1) == "Π")
                //{

                // CZ = "select CH2 FROM EIDH WHERE ID=" + Globals.gIDEIDOS;
                CZ = "select ISNULL(CH2,'') AS CH2 FROM EIDH WHERE ONO='" + Globals.gIDEIDOS+"'";


                // }
                // else
                // {
                //     CZ = "select CH2 FROM EIDH WHERE ID=" + Globals.gIDEIDOS;
                //  }


                SqlCommand cmd30 = new SqlCommand(CZ,  con);
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
                    Itemsx.Add(string.Format("{0}", fONO ));

                    //  Itemsx.Add(string.Format("{0}", fONO + " " + fID));


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