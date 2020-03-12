using MobileBMKG.Models;
using MobileBMKG.Services;
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
	public partial class Home : ContentPage
	{
		public Home ()
		{
			InitializeComponent ();
            this.BindingContext = new HomeViewModel();
		}
	}


    public class HomeViewModel : BaseViewModel
    {
        private Gempa data;

        public Gempa DataGempa
        {
            get { return data; }
            set {SetProperty(ref data ,value); }
        }

        public HomeViewModel()
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                if (IsBusy)
                    return;

                var data = await DataStore.LastGempaDirasakanAsync();
                if (data != null)
                    DataGempa = data;
            }
            catch (Exception)
            {
               
            }
        }

        public void SetValues(Gempa value)
        {
            DataGempa = value;
            playSound();
        }

        public  void playSound()
        {
            DependencyService.Get<IAlarmService>().PlaySound();
        }
    }
}