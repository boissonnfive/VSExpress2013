using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.Devices.Notification;
using Windows.Devices.Lights;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace Minuteur
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ConfigTemps : Page
    {
        public ConfigTemps()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), tpTemps.Time.Hours);

        }

        private void tsSonDuTicTac_Toggled(object sender, RoutedEventArgs e)
        {
            afficheBoiteDeDialogue("Vous avez sélectionné le son du tic-tac.");
        }

        private void tsSonAlarme_Toggled(object sender, RoutedEventArgs e)
        {
            afficheBoiteDeDialogue("Vous avez sélectionné le son de l'alarme.");
        }

        private async void tsVibreur_Toggled(object sender, RoutedEventArgs e)
        {
            afficheBoiteDeDialogue("Vous avez sélectionné le vibreur.");
            //Windows.Devices.Lights.Lamp laLampe = new Windows.Devices.Lights[0].;
            //laLampe.IsEnabled = true;


            // source : https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/LampDevice/cs/Scenario1_GetLamp.xaml.cs
            using (var lamp = await Lamp.GetDefaultAsync())
            {
                if (lamp == null)
                {
                    ;
                }
                else
                {
                    //await LogStatusAsync(string.Format(CultureInfo.InvariantCulture, "Default lamp instance acquired, Device Id: {0}", lamp.DeviceId), NotifyType.StatusMessage);
                    //await LogStatusToOutputBoxAsync("Lamp Default settings:");
                    //await LogStatusToOutputBoxAsync(string.Format(CultureInfo.InvariantCulture, "Lamp Enabled: {0}, Brightness: {1}", lamp.IsEnabled, lamp.BrightnessLevel));

                    // Set the Brightness Level
                    //await LogStatusToOutputBoxAsync("Adjusting Brightness");
                    lamp.BrightnessLevel = 0.5F;
                    //await LogStatusAsync(string.Format(CultureInfo.InvariantCulture, "Lamp Settings After Brightness Adjustment: Brightness: {0}", lamp.BrightnessLevel), NotifyType.StatusMessage);

                    // Turn Lamp on
                    //await LogStatusToOutputBoxAsync("Turning Lamp on");
                    lamp.IsEnabled = true;
                    int bubu = 0;
                    for (int i = 0; i < 500000; i++)
                    {
                        bubu = bubu + 1;
                        lamp.IsEnabled = true;
                    }

                    //await LogStatusToOutputBoxAsync(string.Format(CultureInfo.InvariantCulture, "Lamp Enabled: {0}", lamp.IsEnabled));

                    // Turn Lamp off
                    //await LogStatusToOutputBoxAsync("Turning Lamp off");
                    //lamp.IsEnabled = false;
                    //await LogStatusToOutputBoxAsync(string.Format(CultureInfo.InvariantCulture, "Lamp Enabled: {0}", lamp.IsEnabled));
                }
            }



            try
            {
                VibrationDevice testVibrationDevice = VibrationDevice.GetDefault();
                testVibrationDevice.Vibrate(TimeSpan.FromMilliseconds(200));
            }
            catch (Exception)
            {

                //throw;
            }
            

            // v = VibrationDevice.GetDefault();
            //v.Vibrate(TimeSpan.FromMilliseconds(500));
        }

        private async void afficheBoiteDeDialogue(String message)
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = "Configuration Minuteur",
                Content = message,
                PrimaryButtonText = "OK"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
    }
}
