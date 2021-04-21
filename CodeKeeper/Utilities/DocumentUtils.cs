using CodeKeeper.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CodeKeeper.Utilities
{
    public class DocumentUtils
    {
        public static bool InsertTextInFile(string path, int line, string txt)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("No such file: <" + path + ">");
                return false;
            }

            var text = new StringBuilder();
            int cnt = 0;

            foreach (string s in File.ReadAllLines(path))
            {
                if (cnt++ == line)
                    text.Append(txt);

                text.AppendLine(s);
            }

            File.WriteAllText(path, text.ToString());

            return true;
        }

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

        public static bool ParseFile(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("No such file: <" + path + ">");
                return false;
            }

            Regex rx = new Regex(@"{\{.[^\}]*\}\}");
            MatchCollection matches = null;
            var text = new StringBuilder();
            string res = string.Empty;

            foreach (string s in File.ReadAllLines(path))
            {
                matches = rx.Matches(s);
                if (matches.Count > 0)
                {
                    res = Utilities.ParseUtils.ParseSnippet(s);
                    text.Append(res + "\n");
                }
                else
                    text.AppendLine(s);
            }

            File.WriteAllText(path + ".tmp", text.ToString());

            return true;
        }
    }
}
