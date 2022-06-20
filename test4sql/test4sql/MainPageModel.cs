using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace oncar
{
    public class MainPageModel : BindableObject
    {
        private trapezia2 mainPage;

        public MainPageModel(trapezia2 mainPage)
        {
            this.mainPage = mainPage;
            AddItems();
        }

        private void AddItems()
        {
            for (int i = 0; i < 40; i++)
                Items.Add(string.Format("Τραπεζι {0}", i));
        }

        private ObservableCollection<string> _items = new ObservableCollection<string>();
        public ObservableCollection<string> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        public Command ItemTappedCommand
        {
            get
            {
                return new Command((data) =>
                {
                    mainPage.DisplayAlert("Τραπέζι Νο ", data + "", "Ok");
                });
            }
        }
    }
}

