using CodeKeeper.Repository;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CodeKeeper.Utilities
{
    public class ParseUtils
    {
        private static DataView TokenView = null;

        public static string Parse(DataRowView row, string path)
        {
            Regex rx = new Regex(@"{\{.[^\}]*\}\}");
            StringBuilder bld = new StringBuilder();

            string content = row["Content"].ToString();
            string replStr = string.Empty;

            int cursor = 0;

            MatchCollection matches = rx.Matches(content);
            foreach (Match match in matches)
            {
                bld.Append(content, cursor, match.Index - cursor);
                replStr = GetReplacement(match, path);
                if (replStr != string.Empty)
                    bld.Append(replStr);

                cursor = match.Index + match.Length;
            }

            bld.Append(content, cursor, content.Length - cursor);

            return bld.ToString();
        }

        private static string GetReplacement(Match match, string path)
        {
            string rstr = string.Empty;
            string tmp = string.Empty;
            string tag = match.Value.Substring(2, match.Length - 4);

            tmp = MasterRepository._Token.GetTokenByTag(tag);
            // Set it to the value returned, if match found below will
            //  be over writen.

            rstr = tmp;

            if (rstr == string.Empty)
            {
                MessageBox.Show("Unknown Tag encountered: " + tag);
            }
            else
            {
                if (tag == "date")
                {
                    int index = tag.IndexOf(':');
                    string format = tmp.Substring(index + 1);
                    rstr = DateTime.Now.ToString(format);
                }
                else if (tag == "filename")
                {
                    if (path == string.Empty)
                        rstr = "filename.ext";
                    else
                        rstr = Path.GetFileName(path);
                }
                else if (tag == "prompt")
                {
                    SnippetEditorPromptWindow win = new SnippetEditorPromptWindow(rstr);
                    win.ShowDialog();

                    if (win.DialogResult == true)
                        rstr = win.ViewModel.PromptTextBoxText;
                    else
                        rstr = string.Empty;
                }
            }

            return rstr;
        }
    }
}
