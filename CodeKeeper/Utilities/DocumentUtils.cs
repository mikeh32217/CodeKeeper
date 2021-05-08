using CodeKeeper.Configuration;
using CodeKeeper.Repository;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

using ENC = System.Text.Encoding;

namespace CodeKeeper.Utilities
{
    public class DocumentUtils
    {
        private static string ParseString { get; set; }
        public static string LoadedText { get; set; }

        static DocumentUtils()
        {
            ParseString = ConfigMgr.Instance.settingProvider.GetSingleValue("RegexForParse", "value");
        }

        // TODO Not using this yet but may use it in the future!
        public static bool InsertSnippetInFile(string path, int line, string tag)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("No such file: <" + path + ">");
                return false;
            }

            var text = new StringBuilder();
            int cnt = 0;
            string res = string.Empty;

            if (tag.StartsWith("!"))
                res = Utilities.ParseUtils.GetReplacement(tag) + "\n";
            else
                res = MasterRepository._Token.GetTokenByTag(tag);

            if (res == string.Empty)
            {
                MessageBox.Show("No such Snippet: <" + tag + ">");
                return false;
            }

            foreach (string s in File.ReadAllLines(path))
            {
                if (cnt++ == line)
                    text.Append(res);
                
                text.AppendLine(s);
            }

            File.WriteAllText(path, text.ToString());

            return true;
        }

        public static string LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("The file doesn't exit", 
                    "Not able to load file", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return "File not found";
            }

            MemoryStream LoadedMemoryStream = new MemoryStream();

            // NOTE Tried loading using Paralell librasry but would load in raNDOME ORDER.
            foreach(string s in File.ReadLines(path))
            {
                LoadedMemoryStream.Write(ENC.ASCII.GetBytes( s), 0, s.Length);
                LoadedMemoryStream.Write(new byte[] { 0x0a }, 0, 1);
            }

            LoadedText = ENC.UTF8.GetString(LoadedMemoryStream.ToArray());

            LoadedMemoryStream.Dispose();

            return LoadedText;
        }

        public static void SaveFile(string path, string content)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            if (path != null)
            {
                dlg.FileName = Path.GetDirectoryName(path);
                dlg.InitialDirectory = Path.GetFullPath(path);
            }

            if (dlg.ShowDialog() != null)
            {
                path = dlg.FileName;
                File.WriteAllText(path, content);
            }
        }
    }
}
