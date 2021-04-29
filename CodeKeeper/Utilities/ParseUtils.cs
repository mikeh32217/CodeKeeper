using CodeKeeper.Keyword;
using CodeKeeper.Keyword.Model;
using CodeKeeper.Repository;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
                // Go throught the keyword list first
                rstr = KeywordManager.ParseKeywords(tag, path);
                if (rstr == string.Empty)
                {
                    // Then if not found check the DB.
                    rstr = MasterRepository._Token.GetTokenByTag(tag);
                    if (rstr == string.Empty)
                        MessageBox.Show("Unknown Tag encountered: <" + tag + ">");
                }
            }

            return rstr;
        }
    }
}
