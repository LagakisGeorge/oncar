using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

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

            Globals.cSQLSERVER  = PARAGGELIES.ReadSQL("select EPO FROM MEM WHERE ID=1");
            sqlserver.Text = Globals.cSQLSERVER ;

            Globals.cSQLSERVER = PARAGGELIES.ReadSQL("select DIE FROM MEM WHERE ID=1");
            BARCODES.Text = Globals.useBarcodes;

        }

        async void fkatax(object sender, EventArgs e)
        {
            string C = sqlserver.Text;
            C = C.Replace("/", "\\");
            MainPage.ExecuteSqlite("update MEM SET EPO='"+ C+ "', IP='" + fakelos.Text + "' WHERE ID=1");
            MainPage.ExecuteSqlite("update MEM SET DIE='"  + BARCODES .Text + "' WHERE ID=1");


            Globals.cSQLSERVER = PARAGGELIES.ReadSQL("select EPO FROM MEM WHERE ID=1");
            Globals.cIP = PARAGGELIES.ReadSQL("select IP FROM MEM WHERE ID=1");
            Globals.useBarcodes  = PARAGGELIES.ReadSQL("select DIE FROM MEM WHERE ID=1");
        }

        async void ftest(object sender, EventArgs e)
        {

        
        // DESKTOP-MPGU8SB\SQL17
        string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=EMP;Uid=sa;Pwd=12345678";
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
                 await DisplayAlert("Error", ex.ToString(), "OK");
            }

        }




    }
}