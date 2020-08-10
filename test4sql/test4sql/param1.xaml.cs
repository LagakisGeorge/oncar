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
            Globals.cIP = PARAGGELIES.ReadSQL("select IP FROM MEM");
            fakelos.Text = Globals.cIP;
        }

        async void fkatax(object sender, EventArgs e)
        {
            MainPage.ExecuteSqlite("update MEM SET IP='" + fakelos.Text + "' WHERE ID=1");
        }
    }
}