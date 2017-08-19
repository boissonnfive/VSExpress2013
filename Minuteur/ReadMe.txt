# Minuteur #

## Présentation ##

Crée un minuteur pour windows phone 10 (en attendant de le rendre universel).
Ce minuteur consiste en :

- Un écran vertical
- Un aspect N&B (le plus simple possible)
- Le nom du programme en haut
- Au centre, en grands charactères, le temps en MM:SS
- Un bouton Démarrer/Arrêter pour démarrer et arrêter le minuteur
- En bas, en petit, une notice de copyright
- Une page de paramètres à laquelle on peut accéder en double tapant sur le temps du minuteur. Elle consiste en :
    + un champ qui permet de modifier le temps de départ du minuteur (seulement les minutes, les secondes sont ignorées)
    + un bouton pour activer/Désactiver le son du tic-tac
    + un bouton pour activer/Désactiver l'alarme de temps écoulé
    + un bouton pour activer/Désactiver la vibration lorsque le temps est écoulé.
    + Un bouton Fermer pour revenir à la page principale


## Version 2.0.0 ##

- Changement de calcul du temps qui reste : on ne soustrait pas le temps présent du temps de départ enregistré; on enlève 1 seconde à chaque tic.
- Ajout des variables pour récupérer les paramètres modifiés par l'utilisateur.
- Refactorisation
- Ajout de commentaires XML

- Assets :
    + Modification du son du tic-tac pour avoir un son plus fort
    + Ajout d'un son de temps écoulé "Concierge.wav"
- AssemblyInfo.cs : 
    + modification du numéro de version 2.0.0.0
    + Date du Copyright en 2017
    + Suppression des using inutiles
- App.xaml.cs :
    + Suppression des using inutiles
- ConfigTemps.xaml :
    + Renommage de la classe ConfigTemps en Config
    + Réarrangement des objets pour correspondre à leur arrangement dans la page et pour une meilleure lecture
- ConfigTemps.xaml.cs :
    + Renvoie tous les paramètres à la page principale via une liste quand on clique sur le bouton "Fermer"
    + Récupère tous les paramètres de la page principale
    + Création des fonctions "ActiverLeSonDuTicTac", "ActiverLeSonDeLAlarme", "ActiverLeVibreur" qui désenregistre la méthode qui écoute si on a fait changer d'état le bouton, le temps de changer le bouton d'état, puis qui réenregistre la méthode (afin de ne pas faire les actions contenues dans la méthode)
    + Suppression de la boîte de dialogue quand on change l'état d'un bouton
    + Formatage des commentaires
    + Ajout d'une constante pour le temps du vibreur : TEMPS_VIBRATION
- MainPage.xaml :
    + Réalignement des éléments pour ressembler à la page Config
    + Ajout du son Alarme
- MainPage.xaml.cs :
    + suppression des using inutiles
    + Commentaire XML du code
    + Renommage des fonctions
    + Factorisation du code
    + Ajout des variables correspondant aux paramètres de la page Config et des variable pour calculer le temps qui reste.
- Minuteur.csproj :
    + Ajout du fichier Concierge.wav pour le son quand le temps est écoulé.
- Package.appxmanifest :
    + Suppression de la possibilité de se connecter à internet (inutile pour cette application)

NOTE : j'ai eu un bug difficile à trouver. Quand j'envoyais les paramètres à la page Config, la page ne se chargeait pas et j'avais une exception : « La fenêtre est déjà lancée ». En fait, quand je mettais à jour les boutons d'état, je lançais une boîte de dialogue. Or, ceci était activé à la réception des données quand je mettais à jour l'état des boutons. D'où les méthodes pour changer l'état des boutons sans passer par la méthode lancée sur le changement d'état des boutons.


## Version 1.0.1 ##

- Modification du thème de Light => Dark dans App.xaml
- Modification de Description, Conpany et Copyright dans le fichier AssemblyInfo.cs
- MainPage.xaml :
    + Grid utilise le pinceau pour le fond et la police définie par le thème
    + Modification du titre de "Minuteur (Bubu)" => "Minuteur (Boissonnet)"
    + Le champ temps peut être double tapé pour ouvrir une page paramètres
- Nouvelle page ConfigTemps.xaml.cs :
    + un champ qui permet de modifier le temps de départ du minuteur (seulement les minutes, les secondes sont ignorées)
    + un bouton pour activer/Désactiver le son du tic-tac
    + un bouton pour activer/Désactiver l'alarme de temps écoulé
    + un bouton pour activer/Désactiver la vibration lorsque le temps est écoulé.
    + Un bouton Fermer pour revenir à la page principale qui renvoie le temps configuré 
- MainPage.xaml.cs :
    + Ajout d'un Windows.System.Display.DisplayRequest pour empêcher l'activation de l'écran de veille (lancé dans "btnStartClick" et arrêté dans "btnStopClick")
    + Ajout d'une variable tempsQuiReste pour stocker la valeur définie par l'utilisateur. Au départ, elle vaut le temps défini par la constante TEMP_MAX.
    + Création d'une fonction "miseALHeure" qui gère l'affichage (formatage) du temps à l'écran => Factorisation du code
    + Affichage d'une boîte de dialogue lorsque le temps est écoulé.
    + Création d'une fonction surchargée "OnNavigatedTo" qui permet de récupérer le temps défini par l'utilisateur
    + Création d'une fonction "tbTemps_DoubleTapped" qui affiche la page des paramètres quand on double tape sur le temps.
- Package.appxmanifest :
    + Orientation portrait obligatoire

## Version 1.0.0 ##

