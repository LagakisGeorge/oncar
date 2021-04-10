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

namespace test4sql
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]

   
    public partial class Page1 : ContentPage
    {


        public List<string> MyList = new List<string>();
        public IList<Monkey> Monkeys { get; private set; }


        public Page1()
        {
            InitializeComponent();
            
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

            //Get the SmbFile specifying the file name to be created.
            var file = new SmbFile("smb://"+Globals.cIP +"/EGGTIM2.TXT");
           // fine var file = new SmbFile("smb://User:1@192.168.1.5/backpel/New2FileName.txt");
            try
            {
 //Create file.
            file.CreateNewFile();
            }
            catch {
                 await DisplayAlert("Υπαρχει ηδη το αρχειο", "....", "OK");
                return;
            }
           

            //Get writable stream.
            var writeStream = file.GetOutputStream();
            string c = "1;2;3;4;5;6;7;8;\n";
            c = c + "8;8;9;9;9;9;9;9\n";
            c = c + "18;18;19;19;19;19;19;19\n";

            //Write bytes.
            writeStream.Write(Encoding.UTF8.GetBytes(c));

            //Dispose writable stream.
            writeStream.Dispose();





        }


 




        async void ReadFile(object sender, EventArgs e)
        {
            bool f = ReadFiles();
            //Get the SmbFile specifying the file name to be created.
            var file = new SmbFile("smb://" + Globals.cIP + "/EIDH.TXT"); // "smb://User:1@192.168.1.5/backpel/New2FileName.txt");
            //Get target's SmbFile.
            // var file = new SmbFile("smb://UserName:Password@ServerIP/ShareName/Folder/FileName.txt");

            try
            {

                //Get readable stream.
                var readStream = file.GetInputStream();

                //Create reading buffer.
                var memStream = new MemoryStream();

                //Get bytes.
                ((Stream)readStream).CopyTo(memStream);

                //Dispose readable stream.
                readStream.Dispose();

                Console.WriteLine(Encoding.UTF8.GetString(memStream.ToArray()));

                byte[] bytes = memStream.ToArray();


                await DisplayAlert("Error", Encoding.UTF8.GetString(bytes), "OK");

                String g = Encoding.UTF8.GetString(memStream.ToArray());

                string[] lines = g.Split('\n');
                //for (int i=0;0;lines.count )
                    for (int k = 0; k <= lines.Length  - 1; k++)
                    {



                    }




                Lab1.Text = lines[1] + "=" + lines[2] + "=" + lines[3];
               // ΤΟ ΔΕΙΧΝΕΙ ΣΕ MSGBOX :     await DisplayAlert("Error", Encoding.UTF8.GetString(memStream.ToArray()), "OK");
            }
            catch
            {
                await DisplayAlert("Error", "....", "OK");
                return;
            }           
        }


       private bool  ReadFiles()
        {


            //   Get items in shared folder:            https://www.csharpcodi.com/csharp-examples/SharpCifs.Smb.SmbFile.GetUncPath0()/
            //using System;
            //using SharpCifs.Smb;

            //Get SmbFile-Object of a folder.
            var folder = new SmbFile("smb://" + Globals.cIP + "/"); // "smb://User:1@192.168.1.5/backpel/");

        //UnixTime
        var epocDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

//List items
        foreach (SmbFile item in folder.ListFiles())
        {
            var lastModDate = epocDate.AddMilliseconds(item.LastModified())
                                .ToLocalTime();
            var name = item.GetName();
            var type = item.IsDirectory() ? "dir" : "file";
            var date = lastModDate.ToString("yyyy-MM-dd HH:mm:ss");
            var msg = $"{name} ({type}) - LastMod: {date}";
            Console.WriteLine(msg);
        }

            return true;
        }


       

        private void LISTTIMOL(object sender, EventArgs e)
        {
            Show_list();
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
            contents.CommandText = "SELECT  IFNULL(AJI,0) AS AJI2,IFNULL(HME,'') AS HME2,IFNULL(ATIM,'') AS ATIM2,IFNULL(EPO,'') AS EPO2,ID  from TIM  order by ID DESC ; "; // +BARCODE.Text +"'";
                                                                                                                                                           // contents.CommandText = "SELECT  * from PARALABES ; "; // +BARCODE.Text +"'";
            var r = contents.ExecuteReader();
            Console.WriteLine("Reading data");
            Single s = 0;
            while (r.Read())
            {
               // s = s + (Single)r["POSO"] * (Single)r["TIMH"] * (100 - (Single)r["EKPT"]) / 100;

                Monkeys.Add(new Monkey
                {
                    Name = (r["EPO2"].ToString() + "                               ").Substring(0, 25), 
                    Location = r["ATIM2"].ToString(),
                    ImageUrl = (r["AJI2"].ToString() + "      ").Substring(0, 5),
                    idPEL =  r["ID"].ToString()
                });



            }

            listview.ItemsSource = Monkeys;
            BindingContext = this;


            connection.Close();

          //  SAJIA.Text = String.Format("{0:0.00}", s);  // s.ToString();

            BindingContext = this;
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}