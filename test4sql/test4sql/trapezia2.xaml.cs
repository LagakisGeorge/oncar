using Xamarin.Forms;

namespace oncar
{
    public partial class trapezia2 : ContentPage
    {
        MainPageModel pageModel;
        public trapezia2()
        {
            InitializeComponent();
            pageModel = new MainPageModel(this);
            BindingContext = pageModel;
        }
    }
}