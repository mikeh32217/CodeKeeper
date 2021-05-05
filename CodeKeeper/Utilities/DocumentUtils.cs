using CodeKeeper.Configuration;
using CodeKeeper.Repository;
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

        static DocumentUtils()
        {
            ParseString = ConfigMgr.Instance.settingProvider.GetSingleValue("RegexForParse", "value");
        }

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

            Regex rx = new Regex(ParseString);
            MatchCollection matches = null;
            var text = new StringBuilder();
            string res = string.Empty;

            Parallel.ForEach(
                File.ReadLines(path), //returns IEumberable<string>: lazy-loading
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                (line, state, index) =>
                {
                 matches = rx.Matches(line);
                if (matches.Count > 0)
                {
                    res = Utilities.ParseUtils.ParseSnippet(line);
                    text.Append(res + "\n");
                }
                else
                    text.AppendLine(line);
                }
            );

            //foreach (string s in File.ReadAllLines(path))
            //{
            //}

            File.WriteAllText(path + ".tmp", text.ToString());

            return true;
        }

        public static string PreviewFile(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("No such file: <" + path + ">");
                return string.Empty;
            }

            Regex rx = new Regex(ParseString);
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

            return text.ToString();
        }

        public static string LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("The file doesn't exit", 
                    "Not able to load file", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return string.Empty;
            }

            MemoryStream ms = new MemoryStream();
            
            Parallel.ForEach(
                File.ReadLines(path), //returns IEumberable<string>: lazy-loading
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                (line, state, index) =>
                {
                    ms.Write(ENC.UTF8.GetBytes(line), 0, line.Length);
                    ms.Write(new byte[] { 0x0a }, 0, 1);
                }
            );

            return ENC.UTF8.GetString(ms.ToArray());
        }
    }
}
