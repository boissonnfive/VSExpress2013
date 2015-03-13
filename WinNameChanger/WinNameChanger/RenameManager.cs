using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinNameChanger
{
    public class RenameManager
    {
        public RenameManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /**
         * rename
         * Change the name.
         * @param int index: the complete name of the file
         * @param String completeFileName: the complete name of the file
         * @param String completeFileName: the complete name of the file
         * @param String completeFileName: the complete name of the file
         * @return String: the name "renamed".
         */
        public String rename(int index, String name, String text1, String text2)
        {
            //String extension = completeFileName.Substring(completeFileName.Length - 3, 3);
            //return (extension.ToUpper().Contains("URL"));
            return "";
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
         * ReplaceFirstOccurence
         * Replace first occurence of search in text by replace.
         * Do nothing if search is not found in text.
         * @param String text: the string to search in
         * @param String search: the string to replace
         * @param String search: the string used to replace search
         * @return String: the modified text or the initial text if search is a zero length string.
         */
        public String ReplaceFirstOccurence(String text, String search, String replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        /**
         * ReplaceLastOccurence
         * Replace last occurence of search in text by replace.
         * Do nothing if search is not found in text.
         * @param String text: the string to search in
         * @param String search: the string to replace
         * @param String search: the string used to replace search
         * @return String: the modified text or the initial text if search is a zero length string.
         */
        public String ReplaceLastOccurence(String text, String search, String replace)
        {
            int pos = text.LastIndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        /**
         * Replace
         * Replace all occurences of search in text by replace.
         * Do nothing if search is not found in text.
         * @param String text: the string to search in
         * @param String search: the string to replace
         * @param String search: the string used to replace search
         * @return String: the modified text or the initial text if search is a zero length string.
         */
        public String Replace(String text, String search, String replace)
        {
            if (text != null & search.Length > 0) // Exception handling
            {
                text = text.Replace(search, replace);
            }
            return text;
        }

        /**
         * Append
         * Add a string at the end of the source string
         * @param String text: the string to search in
         * @param String append: the string to add at the end
         * @return String: the modified text or the initial text if search is a zero length string.
         */
        public String Append(String text, String append)
        {
            return text + append;
        }

        /**
         * Prepend
         * Add a string at the begining of the source string
         * @param String text: the string to search in
         * @param String append: the string to add at the end
         * @return String: the modified text or the initial text if search is a zero length string.
         */
        public String Prepend(String text, String prepend)
        {
            return prepend + text;
        }

    }
}
