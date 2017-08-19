using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Phone.Devices.Notification;
using Windows.Devices.Lights;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation; // OnNavigatedTo
using System.Threading.Tasks;     // Task

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace Minuteur
{
    /// <summary>
    /// Page de configuration du Minuteur. On peut modifier le temps du minuteur, ajouter/enlever un bruit de tic-tac,
    /// ajouter/enlever un son quand le temps est écoulé, ajouter/enlever la vibration quand le temps est écoulé.
    /// </summary>
    public sealed partial class Config : Page
    {
        private const int TEMPS_VIBRATION = 500 ;                         // Temps de vibration du vibreur

        public Config()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Appelé quand on clique sur le bouton "Fermer"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //renvoie le nouveau temps à la fenêtre principale
            //List listeParametres = new List
            List<int> listMainPage = new List<int>(4) ;
            listMainPage.Add( tpTemps.Time.Hours) ;
            listMainPage.Add( tsSonDuTicTac.IsOn ? 1: 0) ;
            listMainPage.Add( tsSonAlarme.IsOn ? 1: 0 ) ;
            listMainPage.Add( tsVibreur.IsOn ? 1: 0 ) ;

            this.Frame.Navigate( typeof( MainPage ), listMainPage ) ;
        }

        /// <summary>
        /// Appelé quand on revient de la page principale
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            if ( e.Parameter is List<int> )
            {
                // On récupère les paramètres et on met à jour la page
                TimeSpan ts        = new TimeSpan( ( e.Parameter as List<int> )[0], 0, 0 ) ;
                tpTemps.Time       = ts ;
                ActiverLeSonDuTicTac( ( e.Parameter as List<int> )[1] == 1 ? true : false ) ;
                ActiverLeSonDeLAlarme( ( e.Parameter as List<int> )[2] == 1 ? true : false ) ;
                ActiverLeVibreur( ( e.Parameter as List<int> )[3] == 1 ? true : false ) ;
            }
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Appelé quand le bouton du tic-tac change d'état
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsSonDuTicTac_Toggled(object sender, RoutedEventArgs e)
        {
            mediaSonTicTac.Play() ;
        }

        /// <summary>
        /// Appelé quand le bouton du son de l'alarme change d'état
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsSonAlarme_Toggled(object sender, RoutedEventArgs e)
        {
            mediaSonAlarme.Play() ;
        }

        /// <summary>
        /// Appelé quand le bouton du vibreur change d'état
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsVibreur_Toggled(object sender, RoutedEventArgs e)
        {
            try
            {
                VibrationDevice testVibrationDevice = VibrationDevice.GetDefault() ;
                testVibrationDevice.Vibrate( TimeSpan.FromMilliseconds( TEMPS_VIBRATION ) ) ;
            }
            catch (Exception)
            {

                //throw;
            }
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

        /// <summary>
        /// Active le bouton responsable du son du tic-tac
        /// </summary>
        /// <param name="bSonTicTac">Active ou pas</param>
        /// <remarks>On est obligé de désenregistrer l'événement associé au changement du bouton
        /// pour ne pas lancer la fonction associé à cet événement.</remarks>
        private void ActiverLeSonDuTicTac(bool bSonTicTac)
        {
            tsSonDuTicTac.Toggled -= tsSonDuTicTac_Toggled ;
            try
            {
                tsSonDuTicTac.IsOn = bSonTicTac ;
            }
            finally
            {
                tsSonDuTicTac.Toggled += tsSonDuTicTac_Toggled ;
            }
        }

        /// <summary>
        /// Active le bouton responsable du son de l'alarme
        /// </summary>
        /// <param name="bSonAlarme">Active ou pas</param>
        /// <remarks>On est obligé de désenregistrer l'événement associé au changement du bouton
        /// pour ne pas lancer la fonction associé à cet événement.</remarks>
        private void ActiverLeSonDeLAlarme(bool bSonAlarme)
        {
            tsSonAlarme.Toggled -= tsSonAlarme_Toggled ;
            try
            {
                tsSonAlarme.IsOn = bSonAlarme ;
            }
            finally
            {
                tsSonAlarme.Toggled += tsSonAlarme_Toggled ;
            }
        }

        /// <summary>
        /// Active le bouton responsable du vibreur
        /// </summary>
        /// <param name="bVibreur">Active ou pas</param>
        /// <remarks>On est obligé de désenregistrer l'événement associé au changement du bouton
        /// pour ne pas lancer la fonction associé à cet événement.</remarks>
        private void ActiverLeVibreur(bool bVibreur)
        {
            tsVibreur.Toggled -= tsVibreur_Toggled ;
            try
            {
                tsVibreur.IsOn = bVibreur ;
            }
            finally
            {
                tsVibreur.Toggled += tsVibreur_Toggled ;
            }
        }
    }
}