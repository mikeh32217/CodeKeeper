using CodeKeeper.Configuration;
using CodeKeeper.Model;
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
        private static Stack<string> TrackingStack = new Stack<string>();

        public static string ParseSnippet(string content, string path = "")
        {
            TrackingStack.Clear();

            return Parse(content, path);
        }

        private static string Parse(string content, string path = "")
        {
            Regex rx = new Regex(@"{\{.[^\}]*\}\}");
            StringBuilder bld = new StringBuilder();

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
            string tag = match.Value.Substring(2, match.Length - 4);

            return GetReplacement(tag, path);
        }

        public static string GetReplacement(string tag, string path = "")
        { 
            string rstr = string.Empty;

            if (tag.StartsWith("!"))
            {
                string res = string.Empty;
                string snip = MasterRepository._Snippet.GetSnippetByTag(tag.Substring(1));
                if (snip != null && snip != string.Empty)
                {
                    if (TrackingStack.Contains(tag))
                    {
                        MessageBox.Show("Encountered circular reference: <" + tag + ">");
                        return string.Empty;
                    }
                    else
                    {
                        TrackingStack.Push(tag);
                        rstr = Parse(snip);
                    }
                }
            }
            else
            {
                rstr = MasterRepository._Token.GetTokenByTag(tag);
                // Set it to the value returned, if match found below will
                //  be over writen.

                if (rstr == string.Empty)
                {
                    MessageBox.Show("Unknown Tag encountered: <" + tag + ">");
                }
                else
                {
                    if (tag == "date")
                    {
                        int index = tag.IndexOf(':');
                        string format = rstr.Substring(index + 1);
                        rstr = DateTime.Now.ToString(format);
                    }
                    else if (tag == "filename")
                    {
                        if (path == null || path == string.Empty)
                        {
                           rstr = MasterRepository._Token.GetTokenByTag(tag);
                            if (rstr == string.Empty)
                            {
                                rstr = ConfigMgr.Instance.settingProvider.GetSingleValue("DefaultFilename", "name");
                                if (rstr == null || rstr == string.Empty)
                                    rstr = "filename.ext";
                            }
                        }
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
            }

            return rstr;
        }
    }
}
