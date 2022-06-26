using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test4sql;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oncar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class trapparagg : ContentPage
    {
        public trapparagg()
        {
            InitializeComponent();
            titlos.Text = Globals.gIDPARAGG;
        }
    }
}