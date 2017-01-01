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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Minuteur
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //runs on the UI Thread
        private DispatcherTimer dispatcherTimer;

        private const int TEMPS_MAX = 15;

        private DateTime EndTime { get; set; }


        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnMarcheArret_Click(object sender, RoutedEventArgs e)
        {
            if (btnMarcheArret.Content.ToString() == "Démarrer")
            {
                btnStartClick(null, null);
            }
            else
            {
                btnStopClick(null, null);
            }
        }



        void dispatcherTimer_Tick(object sender, object e)
        {
            var remaining = this.EndTime - DateTime.Now;
            int remainingSeconds = (int)remaining.TotalSeconds;

            int minutes = remainingSeconds / 60;
            int secondes = remainingSeconds % 60;
            String prefixSecondes = "";
            String prefixMinutes = "";
            if (secondes < 10)
            {
                prefixSecondes = "0";
            }

            if (minutes < 10)
            {
                prefixMinutes = "0";
            }
            
            //this.timeSpan.Value = TimeSpan.FromSeconds(remainingSeconds);
            tbTemps.Text = prefixMinutes + minutes.ToString() + ":" + prefixSecondes + secondes.ToString();
            myMediaElement.Play();

            if (remaining.TotalSeconds <= 0)
            {
                btnStopClick(null, null);
            }
        }

        private void btnStopClick(object sender, RoutedEventArgs e)
        {
            this.dispatcherTimer.Stop();
            this.EndTime = DateTime.MinValue;
            //this.timeSpan.Value = TimeSpan.FromSeconds(0);
            tbTemps.Text = "00:00";
            btnMarcheArret.Content = "Démarrer";
        }

        private void btnStartClick(object sender, RoutedEventArgs e)
        {
            if (this.dispatcherTimer == null)
            {
                this.dispatcherTimer = new DispatcherTimer();
                this.dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                this.dispatcherTimer.Tick += dispatcherTimer_Tick;
            }

            if (this.EndTime == DateTime.MinValue)
            {
                // Ajout de 15 minutes à la date de maintenant
                this.EndTime = DateTime.Now + new TimeSpan(0, 0, TEMPS_MAX, 0, 0);//(TimeSpan)this.timeSpan.Value;
            }

            btnMarcheArret.Content = "Arrêter";
            tbTemps.Text = "15:00";
            this.dispatcherTimer.Start();
        }

        //private void btnPauseClick(object sender, RoutedEventArgs e)
        //{
        //    this.dispatcherTimer.Stop();
        //}
    }
}
