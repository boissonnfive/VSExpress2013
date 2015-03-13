## WinNameChanger

Programme qui permet de renommer des fichiers par lot (inspiré par l'application mac OS X ["NameChanger"](http://mrrsoftware.com/namechanger/) ).

## Fonctions

- Remplace la première occurrence
- Remplace la dernière occurrence
- Remplace toutes les occurrences
- Ajoute au début du nom
- Ajoute à la fin du nom


# A faire

- "Utilise des caractères spéciaux (*,?)",
- "Supprime les caractères",
- "Crée une séquence...",
- "Ajoute la Date...",
- "Utilise des expressions régulières"

## Questions

- Dois-je utiliser 4 listes ou 2 listes plus des fonctions ?

	+ fileNames: liste des noms des fichiers
    + filePaths: liste des chemins des fichiers
    + fileExtensions: liste des extensions
    + newFileNames: liste des noms des fichiers modifiés

Ou:
	+ originalFileNames: liste des noms complets des fichiers
	+ modifiedFileNames: liste des noms complets des fichiers, mais modifiés

La deuxième méthode est plus facile si je veux vérifier qu'un fichier ajouté existe déjà
(En ce moment, pour savoir si un fichier est déjà dans la liste, je dois reconstruire
une liste des fichiers pour voir si elle contient le fichier => complexité).
MAIS si je n'utilise que 2 listes, je dois tout le temps transformer ma liste des noms
complet en liste de noms (avec ou sans extension)


- Le réarrangement des fichiers dans la fenêtre

