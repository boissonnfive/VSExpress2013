using System;
using System.Collections.Generic;
using Windows.Phone.Devices.Notification;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;  // OnNavigatedTo

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Minuteur
{
    /// <summary>
    /// Page principale du minuteur. Elle affiche le temps en cours. Le minuteur se déclenche quand on clique
    /// sur le bouton "Démarrer" qui se transforme en bouton "Arrêter". Si on veut accéder aux paramètres,
    /// on double tape sur le temps et la page Config apparaît.
    /// </summary>
    /// <remarks>On utilise un Timer pour compter les secondes.</remarks>
    public sealed partial class MainPage : Page
    {
        private const int TEMPS_MAX = 15 ;                               // Temps de départ du minuteur
        private int tempsQuiReste ;                                      // Temps qui reste à s'écouler
        private DispatcherTimer dispatcherTimer ;                        // Timer du minuteur
        private Windows.System.Display.DisplayRequest _displayRequest ;  // Pour empêcher l'écran de veille
        private int minutes ;                                            // minutes en cours
        private int secondes ;                                           // secondes en cours
        private bool bSonTicTac ;                                        // Le son du tic-tac est activé ou pas
        private bool bSonAlarme ;                                        // Le son de l'alarme est activé ou pas
        private bool bVibreur ;                                          // Le vibreur est activé ou pas

        public MainPage()
        {
            this.InitializeComponent() ;
            // Au lancement du programme tempsQuiReste vaut la constante TEMPS_MAX
            // Mais l'utilisateur peut la changer pendant l'execution
            tempsQuiReste = TEMPS_MAX ;
            bVibreur      = true ;
            bSonTicTac    = true ;
            bSonAlarme    = true ; 
            ReinitialiserMinuteur() ;
        }

        /// <summary>
        /// Appelée quand on appuie sur le bouton "Démarrer/Arrêter"
        /// </summary>
        /// <remarks>Si on appuie sur le bouton "Démarrer", on lance le minuteur.
        /// Si on appuie sur le bouton "Arrêter", on arrête le minuteur.</remarks>
        private void btnMarcheArret_Click( object sender, RoutedEventArgs e )
        {
            if ( "Démarrer" == btnMarcheArret.Content.ToString() )
            {
                // Si on appuie sur Démarrer
                DemarrerMinuteur() ;
            }
            else
            {
                // Si on appuie sur Arrêter
                ArreterMinuteur() ;
            }
        }

        /// <summary>
        /// Lance toutes les étapes pour démarrer le minuteur
        /// </summary>
        private void DemarrerMinuteur()
        {
            // On change le nom du bouton : "Démarrer" => "Arrêter"
            btnMarcheArret.Content = "Arrêter" ;
            AfficherTempsQuiReste( tempsQuiReste, 0 ) ;
            BloquerVerrouillageTelephone() ;
            DemarrerTimer() ;
        }

        /// <summary>
        /// Méthode appelée par le timer selon la fréquence définie (ie 1 seconde)
        /// </summary>
        /// <param name="sender">Non utilisé</param>
        /// <param name="e">Non utilisé</param>
        /// <remarks>On supprime 1 seconde au membre secondes de la classe MainPage.
        /// Dès que cette variable vaut 0, on enlève 1 au membre minutes.
        /// Quand secondes et minutes valent 0, on arrête le minuteur.</remarks>
        void dispatcherTimer_Tick( object sender, object e )
        {
            if  (0 == minutes && 0 == secondes ) // Si le temps est écoulé
            {
                //AfficheBoiteDeDialogueFinMinuteur();
                Sonner() ;
                Vibrer() ;
                ArreterMinuteur() ;
            }
            else
            {
                if ( 0 == secondes ) // Il n'y a plus de secondes à retrancher, c'est qu'il faut retrancher une minute
                {
                    minutes -= 1 ;
                    secondes = 59 ;
                }
                else
                {
                    secondes -= 1 ;
                }
                // MAJ de l'affichage avec le nouveau temps
                AfficherTempsQuiReste( minutes, secondes ) ;
                // Son du Tic-Tac
                FaireTicTac() ;
            }

        }
        
        /// <summary>
        /// Affiche le temps qui reste à partir des variables secondes et minutes
        /// </summary>
        /// <param name="minutes">Temps en minutes qui reste</param>
        /// <param name="secondes">Temps en secondes qui reste</param>
        private void AfficherTempsQuiReste( int minutes, int secondes )
        {
            tbTemps.Text = string.Format( "{0:00}:{1:00}", minutes, secondes ) ;
        }

        /// <summary>
        /// Lance ReinitialiserMinuteur.
        /// </summary>
        private void ArreterMinuteur()
        {
            ReinitialiserMinuteur() ;
        }

        /// <summary>
        /// Lance toute les étapes pour arrêter le minuteur.
        /// </summary>
        private void ReinitialiserMinuteur()
        {
            ArreterTimer() ;
            ReinitialiserTempsQuiReste() ;
            ReinitialiserAffichage() ;
            DebloquerVerrouillageTelephone() ;
        }

        /// <summary>
        /// Remets à jour les variables minutes et secondes pour un prochain démarrage du minuteur.
        /// </summary>
        /// <remarks>Les secondes sont remises à 0. Les minutes ont la valeur définie par l'utilisateur (ou par défaut).</remarks>
        private void ReinitialiserTempsQuiReste()
        {
            minutes = tempsQuiReste;
            secondes = 0 ;
        }

        /// <summary>
        /// Remets à jour l'affichage pour un prochain démarrage du minuteur.
        /// </summary>
        /// <remarks>On met à jour l'affichage du temps et on change le nom du bouton en "Démarrer".</remarks>
        private void ReinitialiserAffichage()
        {
            AfficherTempsQuiReste(tempsQuiReste, 0);
            btnMarcheArret.Content = "Démarrer" ;
        }

        /// <summary>
        /// Démarre le timer qui va permettre de décompter le temps
        /// </summary>
        /// <remarks>Le décompte est de 1 seconde (codé en dur)</remarks>
        private void DemarrerTimer()
        {
            if (dispatcherTimer == null)
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                dispatcherTimer.Tick += dispatcherTimer_Tick;
            }
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Arrête le timer qui décompte le temps
        /// </summary>
        private void ArreterTimer()
        {
            if (dispatcherTimer != null)
                dispatcherTimer.Stop();
        }

        /// <summary>
        /// Empêche le vérrouillage du téléphone
        /// </summary>
        /// <remarks>Le téléphone se vérouille au bout du temps configuré dans les paramètres,
        ///ce qui empêche l'utilisateur de voir le temps s'écouler.</remarks>
        private void BloquerVerrouillageTelephone()
        {
            if (_displayRequest == null)
                _displayRequest = new Windows.System.Display.DisplayRequest();
            _displayRequest.RequestActive();
        }

        /// <summary>
        /// Arrête de bloquer le vérrouillage du téléphone
        /// </summary>
        private void DebloquerVerrouillageTelephone()
        {
            if (_displayRequest != null)
                _displayRequest.RequestRelease();
        }

        /// <summary>
        /// Fait vibrer le téléphone en fonction de la variable bVibreur.
        /// </summary>
        private void Vibrer()
        {
            if ( bVibreur )
            {
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
        }

        /// <summary>
        /// Fait sonner le téléphone en fonction de la variable bSonAlarme
        /// </summary>
        private void Sonner()
        {
            if ( bSonAlarme )
            {
                mediaSonAlarme.Play() ;
            }
        }
        
        /// <summary>
        /// Fait le bruit du tic-tac en fonction de la variable bSonTicTac.
        /// </summary>
        private void FaireTicTac()
        {
            if (bSonTicTac)
            {
                mediaSonTicTac.Play();
            }
        }
        
        /// <summary>
        /// Affiche une fenêtre de dialogue qui informe que le temps est écoulé
        /// </summary>
        private async void AfficheBoiteDeDialogueFinMinuteur()
        {
            ContentDialog dlgFinTemps = new ContentDialog()
            {
                Title             = "Fin du temps",
                Content           = "Cliquez sur OK pour arrêter le minuteur.",
                PrimaryButtonText = "OK"
            };

            if ( bVibreur )
            {
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

            if ( bSonAlarme )
            {
                mediaSonAlarme.Play() ;
            }

            ContentDialogResult result = await dlgFinTemps.ShowAsync();
        }

        /// <summary>
        /// Appelé quand on double tape sur le temps. Permet d'ouvrir la page Config
        /// </summary>
        /// <param name="sender">Non utilisé</param>
        /// <param name="e">Non utilisé</param>
        private void tbTemps_DoubleTapped( object sender, DoubleTappedRoutedEventArgs e )
        {
            // envoie les paramètres à la page de paramètres
            List<int> listConfig = new List<int>( 4 ) ;
            listConfig.Add( tempsQuiReste ) ;
            listConfig.Add( bSonTicTac ? 1 : 0 ) ;
            listConfig.Add( bSonAlarme ? 1 : 0 ) ;
            listConfig.Add( bVibreur ? 1 : 0 ) ;

            // Lancement de la page de configuration du temps
            this.Frame.Navigate( typeof( Config ), listConfig ) ;
        }

        /// <summary>
        /// Appelé quand on revient de la page paramètres
        /// </summary>
        /// <param name="e">Contient la liste des valeurs des paramètres de l'application Minuteur</param>
        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            if ( e.Parameter is List<int> )
            {
                // On récupère les paramètres
                tempsQuiReste = ( e.Parameter as List<int> )[0] ;
                bSonTicTac = ( e.Parameter as List<int> )[1] == 1 ? true: false ;
                bSonAlarme = ( e.Parameter as List<int> )[2] == 1 ? true : false ;
                bVibreur   = ( e.Parameter as List<int> )[3] == 1 ? true : false ;
                ReinitialiserMinuteur() ;
            }
            base.OnNavigatedTo( e ) ;
        }

        //private void btnPauseClick(object sender, RoutedEventArgs e)
        //{
        //    this.dispatcherTimer.Stop();
        //}
    }
}
