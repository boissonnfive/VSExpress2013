using System;
using System.Collections.Generic;


/// <summary>
/// Summary description for Class1
/// </summary>
namespace WinNameChanger
{
    public class Controller
    {
        private FileManager fm;                     // Manage files for us
        private FormNameChanger form;                // Our view
        private RenameManager rm;                   // Manage renaming stuff
        private List<String> fileNames;             // Store the original file name
        private List<String> filePaths;             // Store the path of the original file name
        private List<String> fileExtensions;        // Strore the extension of the original file name
        private List<String> newFileNames;          // Store the new file name (without path and/or extension)
        private Boolean hiddenExtension;            // Do we work on extension also ?
        private Boolean ignoreCase;                 // Do we take care of case ?

        public Controller(FormNameChanger frm)
        {
            form = frm;
            fm = new FileManager();
            rm = new RenameManager();
            fileNames = new List<String>();
            filePaths = new List<String>();
            fileExtensions = new List<String>();
            newFileNames = new List<String>();
            hiddenExtension = false;
            ignoreCase = false;
        }

        /**
         * addFile
         * Store the complete file name and
         * display only the file name in the Form.
         * Remark: we can't have 2 instances of the same file
         * @param String completeFileName: the complete name of the file
         */
        public void addFile(String completeFileName)
        {
            // We can't have 2 instances of the same file
            // Either the list is empty or it doesn't contain the file name
            if (fileNames == null || this.isFound(completeFileName) == false)
            {
                filePaths.Add(fm.GetPath(completeFileName));
                fileExtensions.Add(fm.GetExtension(completeFileName));

                String originalFileName = fm.GetName(completeFileName);
                if (hiddenExtension)
                {
                    // We get the file name without extension
                    originalFileName = fm.GetNameWithoutExtension(originalFileName);
                }
                fileNames.Add(originalFileName);        // with extension by default
                newFileNames.Add(originalFileName);     // with extension by default
                form.addName(originalFileName, originalFileName);
            }
        }

        /**
         * removeFile
         * Remove file from the list and from the display
         * @param int index: index of the file name in the list
         */
        public void removeFile(int index)
        {
            fileNames.RemoveAt(index);
            filePaths.RemoveAt(index);
            newFileNames.RemoveAt(index);
            form.removeName(index);
        }

        /**
         * removeAllFiles
         * Remove all files from the list and from the display
         */
        public void removeAllFiles()
        {
            fileNames.Clear();
            filePaths.Clear();
            newFileNames.Clear();
            form.clear();
        }

        /**
         * rename
         * Manage the rename function via the function index and the file name index
         * @param int indexFunction: index to find the rename function to apply
         * @param int indexName: index to find the file name to use
         */
        public void rename(int indexFunction, int indexName)
        {
            if (form.isChecked(indexName) == 1)
            {
                String modifiedName;
                switch (indexFunction)
                {
                    case 0: //Remplacer la première occurence par
                        modifiedName = rm.ReplaceFirstOccurence(form.getOldName(indexName), form.getSearchText(), form.getReplaceText());
                        break;
                    case 1: //Remplace la dernière occurence par
                        modifiedName = rm.ReplaceLastOccurence(form.getOldName(indexName), form.getSearchText(), form.getReplaceText());
                        break;
                    case 2: //Remplace toutes les occurences par
                        modifiedName = rm.Replace(form.getOldName(indexName), form.getSearchText(), form.getReplaceText());
                        break;
                    case 3: //Ajoute au début
                        modifiedName = rm.Prepend(form.getOldName(indexName), form.getReplaceText());
                        break;
                    case 4: //Ajoute à la fin
                        modifiedName = rm.Append(form.getOldName(indexName), form.getReplaceText());
                        break;
                    case 5: //Utilise des caractères spéciaux (*,?)
                        modifiedName = form.getOldName(indexName);
                        break;
                    case 6: //Supprime les caractères
                        modifiedName = form.getOldName(indexName);
                        break;
                    case 7: //Crée une séquence...
                        modifiedName = form.getOldName(indexName);
                        break;
                    case 8: //Ajoute la Date...
                        modifiedName = form.getOldName(indexName);
                        break;
                    case 9: //Utilise des expressions régulières
                        modifiedName = form.getOldName(indexName);
                        break;
                    default:
                        modifiedName = form.getOldName(indexName);
                        break;
                }
                newFileNames[indexName] = modifiedName;
                form.setModifiedName(indexName, modifiedName);
            }

        }

        /**
         * setHideExtension
         * Set the flag in the class and update display
         * @param Boolean hide: if true, we don't work on extension. Else we work on file name + extension
         */
        public void setHideExtension(Boolean hide)
        {
            hiddenExtension = hide;
            // TODO:
            // Enlever la complexité de cette fonction
            // On ne devrait pas faire ça ici, on devrait vérifier si on travaille
            // sur les extensions au moment du renommage
            // MAIS on doit quand même mettre à jour la fenêtre
            if (fileNames.Count > 0)
            {
                for (int i = 0; i < fileNames.Count; i++)
                {
                    if (hiddenExtension)
                    {
                        // We need to remove extension included by default
                        fileNames[i] = fm.GetNameWithoutExtension(fileNames[i]);
                        newFileNames[i] = fm.GetNameWithoutExtension(newFileNames[i]);
                        // update of the display
                        form.setModifiedName(i, newFileNames[i]);
                        form.setOldName(i, fileNames[i]);
                    }
                    else
                    {
                        // We need to add extension in file names lists
                        fileNames[i] = fileNames[i] + "." + fileExtensions[i];
                        newFileNames[i] = newFileNames[i] + "." + fileExtensions[i];
                        // update of the display
                        form.setModifiedName(i, newFileNames[i]);
                        form.setOldName(i, fileNames[i]);
                    }
                }
            }
        }

        public Boolean getHideExtension()
        {
            return hiddenExtension;
        }

        public void setIgnoreCase(Boolean ignore)
        {
            ignoreCase = ignore;
        }

        public Boolean getIgnoreCase()
        {
            return ignoreCase;
        }

        public int renameFiles()
        {
            for (int i = 0; i < fileNames.Count; i++)
            {
                String sourceFile, destinationFile;
                if (hiddenExtension)
                {
                    sourceFile = filePaths[i] + fileNames[i] + "." + fileExtensions[i];
                    destinationFile = filePaths[i] + newFileNames[i] + "." + fileExtensions[i];
                }
                else
                {
                    sourceFile = filePaths[i] + fileNames[i];
                    destinationFile = filePaths[i] + newFileNames[i];
                }
                System.IO.File.Move(sourceFile, destinationFile);
                form.setOldName(i, newFileNames[i]);
            }
            for (int i = 0; i < fileNames.Count; i++)
            {
                fileNames[i] = newFileNames[i];
            }
            return 0;
        }


        /**
         * isFound
         * Tell if the file already exists in the list
         * @param String completeFileNameToFind: the complete file name to find in the list
         * @return bool: true if file is found in the list
         */
        private Boolean isFound(String completeFileNameToFind)
        {
            Boolean found = false;
            for (int i = 0; i < fileNames.Count; i++)
            {
                String completeFileName = filePaths[i] + fileNames[i];
                if (hiddenExtension)
                {
                    completeFileName += "." + fileExtensions[i];
                }

                if (completeFileNameToFind == completeFileName)
                {
                    found = true;
                    break; // we go out of loop
                }
            }
            return found;
        }
    }
}