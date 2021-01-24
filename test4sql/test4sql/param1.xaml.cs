using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using SharpCifs.Smb;  // http://sharpcifsstd.dobes.jp/

namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class param1 : ContentPage
    {

        public SqlConnection con;
        public param1()
        {
            InitializeComponent();
            Globals.cIP = PARAGGELIES.ReadSQL("select IP FROM MEM WHERE ID=1");
            fakelos.Text = Globals.cIP;

            Globals.cSQLSERVER = PARAGGELIES.ReadSQL("select EPO FROM MEM WHERE ID=1");
            sqlserver.Text = Globals.cSQLSERVER;

            Globals.cSQLSERVER = PARAGGELIES.ReadSQL("select DIE FROM MEM WHERE ID=1");
            BARCODES.Text = Globals.useBarcodes;

        }

        async void fkatax(object sender, EventArgs e)
        {
            string C = sqlserver.Text;
            C = C.Replace("/", "\\");
            MainPage.ExecuteSqlite("update MEM SET EPO='" + C + "', IP='" + fakelos.Text + "' WHERE ID=1");
            MainPage.ExecuteSqlite("update MEM SET DIE='" + BARCODES.Text + "' WHERE ID=1");


            Globals.cSQLSERVER = PARAGGELIES.ReadSQL("select EPO FROM MEM WHERE ID=1");
            Globals.cIP = PARAGGELIES.ReadSQL("select IP FROM MEM WHERE ID=1");
            Globals.useBarcodes = PARAGGELIES.ReadSQL("select DIE FROM MEM WHERE ID=1");
        }

        async void ftest(object sender, EventArgs e)
        {


            // DESKTOP-MPGU8SB\SQL17
            string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";
            // ok fine string constring = @"Data Source=DESKTOP-MPGU8SB\SQL17,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";
            // ok works fine string constring = @"Data Source=192.168.1.10,51403;Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            con = new SqlConnection(constring);

            try
            {
                con.Open();
                await DisplayAlert("Συνδεθηκε", "οκ", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }

        }

        //using System;
        //using SharpCifs.Smb;

        //Set Local UDP-Broadcast Port.
        //When using the host name when connecting,
        //Change default local port(137) to a value larger than 1024.
        //In many cases, use of the well-known port is restricted.
        //
        // ** If possible, using IP addresses instead of host names 
        // ** to get better performance.
        //
        async void fLISTSERVER(object sender, EventArgs e)
        {
             SharpCifs.Config.SetProperty("jcifs.smb.client.lport", "8137");

            //Get local workgroups.
            var lan = new SmbFile("smb://" + Globals.cIP + "/", "");  //   User:1@192.168.2.7/", "");
            var workgroups = lan.ListFiles();

            foreach (var workgroup in workgroups)
            {
                Console.WriteLine($"Workgroup Name = {workgroup.GetName()}");
               // await DisplayAlert($"Workgroup Name = {workgroup.GetName()}","","");
                try
                {
                    //Get servers in workgroup.
                    var servers = workgroup.ListFiles();
                    int nn = 0;
                    foreach (var server in servers)
                    {
                        nn++;
                        if(nn>2) { break; }
                        Console.WriteLine($"{workgroup.GetName()} - Server Name = {server.GetName()}");
                      await   DisplayAlert($"{workgroup.GetName()} - Server Name = {server.GetName()}", "....", "OK");
                        try
                        {
                            //Get shared folders in server.
                            var shares = server.ListFiles();
                            int n = 0;
                            foreach (var share in shares)
                            {
                                Console.WriteLine($"{workgroup.GetName()}{server.GetName()} - Share Name = {share.GetName()}");
                                await DisplayAlert($"{workgroup.GetName()}{server.GetName()} - Share Name = {share.GetName()}", "", "OK");
                                n++;
                                if (n > 2) { break; }
                            
                            
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"{workgroup.GetName()}{server.GetName()} - Access Denied");
                            await DisplayAlert($"{workgroup.GetName()}{server.GetName()} - Access Denied","","OK.");
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"{workgroup.GetName()} - Access Denied");
                    await DisplayAlert($"{workgroup.GetName()} - Access Denied", "", "OK-");
                }
            }


        }

    }
}