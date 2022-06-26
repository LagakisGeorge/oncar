using System.Collections.Generic;
using System.Collections.ObjectModel;
using test4sql;
using Xamarin.Forms;

namespace oncar
{
    public partial class trapezia2 : ContentPage
    {
        MainPageModel pageModel;
        public IList<Monkey> Monkeys { get; private set; }
        public trapezia2()
        {
            InitializeComponent();
              pageModel = new MainPageModel(this);
              BindingContext = pageModel;
           // this.Navigation.PopAsync();
            //this.Navigation.RemovePage( );
            // toparagg();
            Navigation.PushAsync(new trapparagg());  //imports

        }

        private async  void toparagg()
        {
           // await Application.Current.MainPage.Navigation.PopAsync();
            await Navigation.PushAsync(new trapparagg ());  //imports
           
        }

        private async void doit(object sender, ItemTappedEventArgs e)
        {
            MainPageModel tappedItem = e.Item as MainPageModel;
            Globals.gIDPARAGG = e.Item.ToString();
            DisplayAlert("Τραπέζι Νο ",  "", "Ok");
            await Navigation.PushAsync(new trapparagg());  //imports
        }
        //{
        //    for (int i = 0; i < 40; i++)
        //        Monkeys.Add(new Monkey
        //        {
        //            Name = "11",

        //            Location = "",
        //            ImageUrl = "",
        //            idPEL = ""
        //        });
        //    //  Items.Add(string.Format("Τραπεζι {0}", i));
        //}






    }
}