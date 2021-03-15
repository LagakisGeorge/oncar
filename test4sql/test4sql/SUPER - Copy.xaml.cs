using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using SharpCifs.Smb;  // http://sharpcifsstd.dobes.jp/
using Plugin.SimpleAudioPlayer;
using System.Data.SqlClient;
using ZXing;






namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SUPER2 : ContentPage
    {
        public SqlConnection con;
        private int nn=1;
        public SUPER2()
        {
            InitializeComponent();
            TITLOS.Text  = PARAGGELIES.ReadSQL("select TITLOS FROM PARASTAT WHERE ID=1");


            ARITMISI.Text = PARAGGELIES.ReadSQL("select ARITMISI FROM PARASTAT WHERE ID=1");


            EIDOS.Text = PARAGGELIES.ReadSQL("select EIDOS FROM PARASTAT WHERE ID=1");
            MainPage.ExecuteSqlite("UPDATE PARASTAT SET ARITMISI=0 WHERE ARITMISI=NULL ");

        }


        async void fPREV(object sender, EventArgs e)
        {
            if (nn > 1)
            {
                nn = nn - 1;
            }
            TITLOS.Text = PARAGGELIES.ReadSQL("select TITLOS FROM PARASTAT WHERE ID="+nn.ToString ());


            ARITMISI.Text = PARAGGELIES.ReadSQL("select ARITMISI FROM PARASTAT WHERE ID=" + nn.ToString());


            EIDOS.Text = PARAGGELIES.ReadSQL("select EIDOS FROM PARASTAT WHERE ID=" + nn.ToString());
        }

        async void fNEXT(object sender, EventArgs e)
        {

             if (nn<PARAGGELIES.NReadSQL("select count(*) from PARASTAT") )
            {
                nn = nn + 1;
            }
            TITLOS.Text = PARAGGELIES.ReadSQL("select ifnull(TITLOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());


           ARITMISI.Text = PARAGGELIES.ReadSQL("select  ARITMISI  FROM PARASTAT WHERE  ID=" + nn.ToString()) ;


            EIDOS.Text = PARAGGELIES.ReadSQL("select ifnull(EIDOS,'') AS C FROM PARASTAT WHERE ID=" + nn.ToString());




        }
        async void fkatax(object sender, EventArgs e)
        {
            MainPage.ExecuteSqlite("update PARASTAT SET TITLOS='" + TITLOS.Text + "' where  ID=" + nn.ToString());
            MainPage.ExecuteSqlite("update PARASTAT SET ARITMISI=" + ARITMISI.Text  + " WHERE  ID=" + nn.ToString());
            MainPage.ExecuteSqlite("update PARASTAT SET EIDOS='" + EIDOS.Text  + "' WHERE  ID=" + nn.ToString());




            // string[] lines = Globals.cSQLSERVER.Split(';');

            // IMPORTEID.Text = lines[1] + "=" + lines[2] + "=" + lines[3];





        }


    }


}