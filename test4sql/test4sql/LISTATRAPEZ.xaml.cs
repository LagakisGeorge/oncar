using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using PCLStorage;
using SharpCifs.Smb;  // http://sharpcifsstd.dobes.jp/
using System.IO;
using ZXing.Net.Mobile.Forms;
using Mono.Data.Sqlite;


using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace test4sql
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]

   
    public partial class Page11 : ContentPage
    {
        

        public List<string> MyList = new List<string>();
        public IList<Monkey> Monkeys { get; private set; }



        public float fEKPTNUM1 = 0; // εχτρα εκπτωση πελατη
        public float faji = 0;  // SYNOLO ME FPA
        public float fkauaji = 0;  // SYNOLO ME FPA
                                   // public string fIDTimDior = "0";
        public float fkauajiPro = 0;  // SYNOLO ME FPA

        public float fYPOLPEL = 0;  // YPOLOIPO PELATH







        public Page11()
        {
            InitializeComponent();
            Show_list();


        }


         void Click_Login(object sender, EventArgs e)
        {
            // To create a new subfolder in the local folder, call the CreateFolderAsync method.
           // String folderName = "csharp";
          //  IFolder folder = FileSystem.Current.LocalStorage;
          //  folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
          
        }

         async void Shared_Folder(object sender, EventArgs e)
        {

           





        }



        // ΠΡΟΣΟΧΗ ΤΟ PRINTOUT ΕΙΝΑΙ ΑΚΡΙΒΩΣ ΙΔΙΟ ΜΕ ΤΟ PARAGGELIES.PRINTOUT
        // ΓΙΑΥΤΟ ΚΑΝΕ ΤΙΣ ΑΛΛΑΓΕΣ ΣΤΟ PARAGGELIES KAI META COPY PASTE ΕΔΩ


        private async void PRINTOUT(int IsSygkEpistr) // 0=timologio 1=sygkentrotiko epistrofis
        {


            
        }



        public static string Right(string original, int numberCharacters)
        {
            return original.Substring(original.Length - numberCharacters);
        }





        void printt(Stream outs, string qq)
        {
          


        }



        async void ReadFile(object sender, EventArgs e)
        {
           
        }


       private bool  ReadFiles()
        {


            return true;
        }


       

        private void LISTTIMOL(object sender, EventArgs e)
        {
            Show_list();
        }
        private DataTable ReadSQLServer(string cSQL)

        {
            DataTable dt = new DataTable();
            //  -----------------SQLSERVER  1.SYNDESH   ---------------------------------------
            if (Globals.cSQLSERVER.Length < 2)
            {
                //  await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return dt;
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";



            // private SqlConnection con;
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();

            }
            catch (Exception ex)
            {
                // await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }
            try
            {
                //Monkeys = new List<Monkey>();


                // ΤΟ ΠΑΡΑΚΑΤΩ ΠΗΓΑΙΝΕ ΠΑΝΤΟΥ ΚΑΙ ΜΕ ΜΠΕΡΔΕΥΕ ΕΝΩ ΠΡΕΠΕΙ ΝΑ ΕΙΝΑΙ ΜΕΤΑ ΤΗΝ ΠΛΗΡΩΜΗ
                //  Globals.indexParaggLine = -1;  // για να καταλαβαίνω αν ειναι αδεια η παραγγελία

                SqlCommand cmd3 = new SqlCommand(cSQL, con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);

            }
            catch (Exception ex)
            {
                //await DisplayAlert("Error", ex.ToString(), "OK");
            }
            return dt;
        }



        void Show_list()
        {
            Monkeys = new List<Monkey>();
            BindingContext = null;


            DataTable DT2 = new DataTable();
            
            DT2 = ReadSQLServer("SELECT TRAPEZI,HME,AJIA,ISNULL([TROPOS],'') AS TROPOS  FROM PARAGGMASTER   where  IDBARDIA=" + Globals.gIDBARDIA);


            // string[] CASH;
            List<string> myText2 = new List<string>();
            string mm2 = "";
            myText2.Add("---");
            for (int K = 0; K <= DT2.Rows.Count - 1; K++)
            {
                string v = DT2.Rows[K]["TRAPEZI"].ToString() + "----" + DT2.Rows[K]["HME"].ToString() + "----" + DT2.Rows[K]["AJIA"].ToString() + "--" + DT2.Rows[K]["TROPOS"].ToString();
                mm2 = mm2 + v;
                myText2.Add(v);
                Monkeys.Add(new Monkey
                {
                    Name = (DT2.Rows[K]["TRAPEZI"].ToString() + "                               ").Substring(0, 10),
                    Location = DT2.Rows[K]["HME"].ToString(),
                    ImageUrl = (DT2.Rows[K]["AJIA"].ToString() + "      ").Substring(0, 5),
                    idPEL = DT2.Rows[K]["TROPOS"].ToString()
                });

            }
           
           




            listview.ItemsSource = Monkeys;
            BindingContext = this;


        }

        private async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

           



        }

        private void epanektyp(string mID)
        {
               }




      private async Task DELETETIMOL()
        {

         

        }

        private void delt(object sender, EventArgs e)
        {
            DELETETIMOL();
        }


        void Show2_list(string mATIM)
        {
          
            
        }





        private void LISTEIDH(object sender, EventArgs e)
        {

           












        }

        private void Testprinting(object sender, EventArgs e)
        {
               List<byte> outputList1 = new List<byte>();

                outputList1.Add(Byte.Parse(PRINT.Text));

               // outputList1.Add(0x0D);

                Socket pSocket1 = new Socket(SocketType.Stream, ProtocolType.IP);
                // Connect to the printer
                pSocket1.Connect(Globals.cIPPR1, 9100);
                pSocket1.Send(outputList1.ToArray());
                pSocket1.Close();


            
        }









    }
}