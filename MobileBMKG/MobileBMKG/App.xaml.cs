using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileBMKG.Views;
using MobileBMKG.Models;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;
using MobileBMKG.Services;
using Microsoft.AppCenter.Push;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobileBMKG
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
          //  SignalRConnection = new SignalRClientConnection();
           // SignalRConnection.NewReceived += SignalRConnection_NewReceived;
            MainPage = new MainPage();
            MessagingCenter.Subscribe<MessagingCenterAlert>(this, "message", async (message) =>
            {
                await Current.MainPage.DisplayAlert(message.Title, message.Message, message.Cancel);

            });

        
        }


        protected  override void OnStart()
        {
            /*   OneSignal.Current.StartInit("b5e98c46-d915-4642-9c54-124e36ca57d5")
                     .EndInit();*/
            AppCenter.Start("bdcdf782-fe9b-4198-a50f-552a56f3df06", typeof(Push));
            if (!AppCenter.Configured)
            {
                Push.PushNotificationReceived += (sender, e) =>
                {
                    if (e.Message != null)
                    {

                        var message = new MessagingCenterAlert() { Message = e.Message, Title = e.Title, Cancel = "OK" };
                        MessagingCenter.Send(message, "message");
                    }
                    var a = e.CustomData;
                    var summary = a.ToString();
                    if (e.CustomData != null)
                    {
                        summary += "\n\tCustom data:\n";
                        foreach (var key in e.CustomData.Keys)
                        {
                            summary += $"\t\t{key} : {e.CustomData[key]}\n";
                        }
                    }
                };
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    public class MessagingCenterAlert
    {
        /// <summary>
        /// Init this instance.
        /// </summary>
        public static void Init()
        {
            var time = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance cancel/OK text.
        /// </summary>
        /// <value><c>true</c> if this instance cancel; otherwise, <c>false</c>.</value>
        public string Cancel { get; set; }

        /// <summary>
        /// Gets or sets the OnCompleted Action.
        /// </summary>
        /// <value>The OnCompleted Action.</value>
        public Action OnCompleted { get; set; }
    }
}
