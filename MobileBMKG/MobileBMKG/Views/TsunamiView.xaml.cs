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
    public partial class TsunamiView : ContentPage
    {
        public TsunamiView()
        {
            InitializeComponent();
            BindingContext = new TsunamiViewModel();
        }
    }


    public class TsunamiViewModel : BaseViewModel
    {
        private Gempa data;

        public Gempa DataGempa
        {
            get { return data; }
            set { SetProperty(ref data, value); }
        }

        public Command OpenWebCommand { get; private set; }

        public TsunamiViewModel()
        {
           
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                if (IsBusy)
                    return;

                var data = await DataStore.GetLastTsunamiAsync();
                if (data != null)
                {
                    DataGempa = data.Gempa.FirstOrDefault();
                    OpenWebCommand = new Command(() => Device.OpenUri(new Uri(DataGempa.Linkdetail+"/?act=tsuevents")));
                }
                  
            }
            catch (Exception)
            {

            }
        }

     
    }
}