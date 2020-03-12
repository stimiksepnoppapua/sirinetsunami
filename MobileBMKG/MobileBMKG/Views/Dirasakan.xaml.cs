using MobileBMKG.Models;
using MobileBMKG.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileBMKG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dirasakan : ContentPage
    {
        public Dirasakan()
        {
            InitializeComponent();
            this.BindingContext = new DirasakanViewModel();
        }
    }

    public class DirasakanViewModel:BaseViewModel
    {
        public List <Gempa> Source { get; set; }
        public DirasakanViewModel()
        {
            Source = new List<Gempa>();
            LoadCommand = new Command(LoadAction);
            LoadAction(null);
        }

        private async void LoadAction(object obj)
        {
            if (IsBusy)
                return;
            else
            {
                var items =await DataStore.GetGempaDirasakanAsync();
                if(items!=null && items.Count > 0)
                {
                    Source.Clear();
                    foreach (var item in items)
                    {
                        Source.Add(item);
                    }
                }
              
            }
        }

        public Command LoadCommand { get; }
    }
}