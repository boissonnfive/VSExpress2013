
using System;
using System.IO;

/// <summary>
/// Classe qui gère les noms des fichiers
/// </summary>
namespace WinNameChanger
{
    public class FileManager
    {
        //private StreamWriter 

        public FileManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /**
         * GetExtension
         * Get the extension string of a file.
         * @param String completeFileName: the complete name of the file
         * @return bool: true if extension is "URL". Else, false.
         */
        public String GetExtension(String completeFileName)
        {
            return completeFileName.Substring(completeFileName.Length - 3, 3);
        }

        /**
         * GetName
         * Return the name of the file, without the path, with the extension.
         * @param String completeFileName: the complete name of the file
         * @return bool: the name of the file, without the path, with the extension.
         */
        public String GetName(String completeFileName)
        {
            int afterSlashPosition = completeFileName.LastIndexOf('\\') + 1;
            int length = completeFileName.Length - afterSlashPosition;
            return completeFileName.Substring(afterSlashPosition, length);
        }

        /**
         * GetNameWithoutExtension
         * Return the name of the file, without the path, without the extension.
         * @param String completeFileName: the complete name of the file
         * @return bool: the name of the file, without the path, without the extension.
         */
        public String GetNameWithoutExtension(String completeFileName)
        {
            int afterSlashPosition = completeFileName.LastIndexOf('\\') + 1;
            int length = completeFileName.Length - afterSlashPosition;
            completeFileName = completeFileName.Substring(afterSlashPosition, length);
            if (completeFileName.IndexOf('.') != -1)
            {
                completeFileName = completeFileName.Substring(0, completeFileName.Length - 4); // 4 to include ".xxx"
            }
            return completeFileName;
        }

        /**
         * GetPath
         * Return the path of the file
         * @param String completeFileName: the complete name of the file
         * @return bool: the path of the file
         */
        public String GetPath(String completeFileName)
        {
            int afterSlashPosition = completeFileName.LastIndexOf('\\') + 1;
            int length = completeFileName.Length - afterSlashPosition;
            return completeFileName.Substring(0, afterSlashPosition);
        }

    }
}
