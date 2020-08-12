using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    
    public partial class param1 : ContentPage
    {


        public param1()
        {
            InitializeComponent();            
            Globals.cIP = PARAGGELIES.ReadSQL("select IP FROM MEM WHERE ID=1");
            fakelos.Text = Globals.cIP;

            Globals.cSQLSERVER  = PARAGGELIES.ReadSQL("select EPO FROM MEM WHERE ID=1");
            sqlserver.Text = Globals.cSQLSERVER ;



        }

        async void fkatax(object sender, EventArgs e)
        {
            string C = sqlserver.Text;
            C = C.Replace("/", "\\");
            MainPage.ExecuteSqlite("update MEM SET EPO='"+ C+ "', IP='" + fakelos.Text + "' WHERE ID=1");
        }
    }
}