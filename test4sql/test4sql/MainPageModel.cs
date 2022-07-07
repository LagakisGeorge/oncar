using Mono.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using System.IO;
using test4sql;
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
           
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"adodemo.db3");
            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();
            string ff = "";
            try
            {   var contents = connection.CreateCommand();
                contents.CommandText = "SELECT  ONO,IFNULL(KATEILHMENO,0) AS KATEILHMENO,IFNULL(IDPARAGG,0) AS IDPARAGG from TABLES";
                var r = contents.ExecuteReader();
                while (r.Read())
                {
                    string fkat= r["kateilhmeno"].ToString();
                    string fONO = r["ono"].ToString();
                    string fID = r["idparagg"].ToString();
                    // fEKPTNUM1 = float.Parse(r["NUM12"].ToString());
                    // fYPOLPEL = float.Parse(r["TYP2"].ToString());
                    // LPLIR.Text = r["ID"].ToString() + " Εκπ:" + r["NUM12"].ToString();
                    if (fkat=="0") { fkat = ""; } else { fkat = "# "; }
                    Items.Add(string.Format(" {0} ", fkat+fONO+" "+fID ));
                }
            }
            catch
            {               ff = "error";            
            }
            connection.Close();
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

        public Command ItemTappedCommand => new Command((data) =>
                                                          {
                                                              //mainPage.DisplayAlert("Τραπέζι Νο ", data + "", "Ok");
                                                              //Globals.gIDPARAGG = (string)data;
                                                              //string[] lines = Globals.gIDPARAGG.Split(' ');
                                                              //if (lines.Length > 1)
                                                              //{
                                                              //  Globals.gIDPARAGG = lines[1];
                                                              //}
                                                              //else
                                                              //{
                                                              //    Globals.gIDPARAGG = "0";
                                                              //}
                                                              //// ClearItems(Items);
                                                              //// Items.Clear();
                                                              //// for (int i = 0; i < 40; i++)
                                                              //////    Items.Add(string.Format("Τραπ {0} table{0}", i));
                                                              ////  AddItems();
                                                          });
    }
}

