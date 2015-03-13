## WinNameChanger

Programme qui permet de renommer des fichiers par lot (inspir� par l'application mac OS X ["NameChanger"](http://mrrsoftware.com/namechanger/) ).

## Fonctions

- Remplace la premi�re occurrence
- Remplace la derni�re occurrence
- Remplace toutes les occurrences
- Ajoute au d�but du nom
- Ajoute � la fin du nom


# A faire

- "Utilise des caract�res sp�ciaux (*,?)",
- "Supprime les caract�res",
- "Cr�e une s�quence...",
- "Ajoute la Date...",
- "Utilise des expressions r�guli�res"

## Questions

- Dois-je utiliser 4 listes ou 2 listes plus des fonctions ?

	+ fileNames: liste des noms des fichiers
    + filePaths: liste des chemins des fichiers
    + fileExtensions: liste des extensions
    + newFileNames: liste des noms des fichiers modifi�s

Ou:
	+ originalFileNames: liste des noms complets des fichiers
	+ modifiedFileNames: liste des noms complets des fichiers, mais modifi�s

La deuxi�me m�thode est plus facile si je veux v�rifier qu'un fichier ajout� existe d�j�
(En ce moment, pour savoir si un fichier est d�j� dans la liste, je dois reconstruire
une liste des fichiers pour voir si elle contient le fichier => complexit�).
MAIS si je n'utilise que 2 listes, je dois tout le temps transformer ma liste des noms
complet en liste de noms (avec ou sans extension)


- Le r�arrangement des fichiers dans la fen�tre

