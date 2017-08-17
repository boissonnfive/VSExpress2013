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

