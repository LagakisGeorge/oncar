﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using SharpCifs.Smb;  // http://sharpcifsstd.dobes.jp/
using System.Net;

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

            Globals.useBarcodes = PARAGGELIES.ReadSQL("select DIE FROM MEM WHERE ID=1");
            BARCODES.Text = Globals.useBarcodes;

            Globals.cFORTHGO = PARAGGELIES.ReadSQL("select ifnull(THL,'') FROM MEM WHERE ID=1");
            FORTHGO.Text = Globals.cFORTHGO;

            CIPPR1.Text = Globals.cIPPR1;
            CIPPR2.Text = Globals.cIPPR2;
            CIPPR3.Text = Globals.cIPPR3;
            TitlosEKTYP.Text = Globals.gTITLOS;

            local.Text = Globals.gLocal;

            Globals.gIPKleis = PARAGGELIES.ReadSQL("select ifnull(POL,'') FROM MEM WHERE ID=2");
            cIPKleis.Text = Globals.gIPKleis;
           
        }

        async void fkatax(object sender, EventArgs e)
        {
            //c = "CREATE TABLE IF NOT EXISTS MEM( ID  INTEGER PRIMARY KEY,*IP [nvarchar](45)," +
            //     *  "[EPO] [nvarchar](255) ," +
            //     *   "[DIE] [nvarchar](35) ," +
            //          "[POL] [nvarchar](35) ," +
            //      *      "[THL] [nvarchar](35) ," +
            //          "[AFM] [nvarchar](15) )";
            string C = sqlserver.Text;
            C = C.Replace("/", "\\");
            MainPage.ExecuteSqlite("update MEM SET EPO='" + C + "', IP='" + fakelos.Text + "' WHERE ID=1");
            MainPage.ExecuteSqlite("update MEM SET DIE='" + BARCODES.Text + "' WHERE ID=1");
            MainPage.ExecuteSqlite("update MEM SET THL='" + FORTHGO.Text + "' WHERE ID=1");

            MainPage.ExecuteSqlite("update MEM SET IP='" + CIPPR1.Text + "' WHERE ID=2");
            MainPage.ExecuteSqlite("update MEM SET AFM='" + CIPPR2.Text + "' WHERE ID=2");
            MainPage.ExecuteSqlite("update MEM SET DIE='" + CIPPR3.Text + "' WHERE ID=2");
            MainPage.ExecuteSqlite("update MEM SET EPO='" + TitlosEKTYP.Text + "' WHERE ID=2");
            MainPage.ExecuteSqlite("update MEM SET POL='" + cIPKleis.Text + "' WHERE ID=2");

            MainPage.ExecuteSqlite("update MEM SET IP='" + local.Text + "' WHERE ID=3");


            Globals.cSQLSERVER = PARAGGELIES.ReadSQL("select EPO FROM MEM WHERE ID=1");
            Globals.cIP = PARAGGELIES.ReadSQL("select IP FROM MEM WHERE ID=1");
            Globals.useBarcodes = PARAGGELIES.ReadSQL("select DIE FROM MEM WHERE ID=1");

            Globals.cFORTHGO = PARAGGELIES.ReadSQL("select ifnull(THL,'') FROM MEM WHERE ID=1");
            Globals.cIPPR1 = PARAGGELIES.ReadSQL("select IP  FROM MEM WHERE ID=2");
            Globals.cIPPR2 = PARAGGELIES.ReadSQL("select AFM FROM MEM WHERE ID=2");
            Globals.cIPPR3 = PARAGGELIES.ReadSQL("select DIE FROM MEM WHERE ID=2");
            Globals.gTITLOS = PARAGGELIES.ReadSQL("select EPO FROM MEM WHERE ID=2");
            Globals.gIPKleis = PARAGGELIES.ReadSQL("select POL FROM MEM WHERE ID=2");

            Globals.gLocal = PARAGGELIES.ReadSQL("select IP FROM MEM WHERE ID=3");


        }

        async void ftest(object sender, EventArgs e)
        {


            // string[] lines = Globals.cSQLSERVER.Split(';');

            // IMPORTEID.Text = lines[1] + "=" + lines[2] + "=" + lines[3];

            Globals.cSQLSERVER = PARAGGELIES.ReadSQL("select EPO FROM MEM WHERE ID=1");

            // DESKTOP-MPGU8SB\SQL17
            //  string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678"; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";



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
                        if (nn > 2) { break; }
                        Console.WriteLine($"{workgroup.GetName()} - Server Name = {server.GetName()}");
                        await DisplayAlert($"{workgroup.GetName()} - Server Name = {server.GetName()}", "....", "OK");
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
                            await DisplayAlert($"{workgroup.GetName()}{server.GetName()} - Access Denied", "", "OK.");
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

        private void testprint(object sender, FocusEventArgs e)
        {
            printthis(CIPPR1.Text);
        }



        private async void printthis(string ipAddress)
        {

            List<string> mytext;
            mytext = new List<string>();

            mytext.Add(MainPage.ToGreek737("11ΑΒΓΔΕ ") + "\r\n");
            mytext.Add("12ABCDE" + "\r\n");
            mytext.Add(MainPage.ToGreek737("13αβγδ abcd") + "\r\n");
            //string ipAddress = Globals.cIPPR1; // "192.168.1.120";
            int portNumber = 9100;
            var printer = DependencyService.Get<test4sql.iPrinter>();
            if (printer == null)
            {
                await DisplayAlert("Error", "δεν υπαρχει συνδεση", "");
                return;
            }
            try
            {

                printer.Print(ipAddress, portNumber, mytext);
                 await DisplayAlert("Ok", "ok", "ok");
                return;
            }

            catch (Exception ex)
            {
                await DisplayAlert("αδυναμια εκτυπωσης ", ex.ToString(), "OK");
                return;
                // await DisplayAlert("error2", "", "");
            }

        }

        private void testprint2(object sender, FocusEventArgs e)
        {
            printthis(CIPPR2.Text);
        }

        private void testprint3(object sender, FocusEventArgs e)
        {
            printthis(CIPPR3.Text);
        }

        private void testprint4(object sender, FocusEventArgs e)
        {
            printthis(cIPKleis.Text);
        }
    }



}