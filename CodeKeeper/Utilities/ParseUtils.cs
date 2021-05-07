using CodeKeeper.Configuration;
using CodeKeeper.Keyword;
using CodeKeeper.Keyword.Model;
using CodeKeeper.Model;
using CodeKeeper.Repository;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

using ENC = System.Text.Encoding;

namespace CodeKeeper.Utilities
{
    public class ParseUtils
    {
        private static Stack<string> TrackingStack = new Stack<string>();

        private static string ParseString { get; set; }

        static ParseUtils()
        {
            ParseString = ConfigMgr.Instance.settingProvider.GetSingleValue("RegexForParse", "value");
        }

        public static string ParseSnippet(string content, string path = "")
        {
            TrackingStack.Clear();

            return ParseFile(content, path);
        }

        public static string ParseFile(string content, string path = "")
        {
            Regex rx = new Regex(ParseString);

            string replStr = string.Empty;
            string res = string.Empty;

            int cursor = 0;

            byte[] ary = ENC.ASCII.GetBytes(content);

            MemoryStream ms = new MemoryStream();

            MatchCollection matches = rx.Matches(content);
            foreach (Match match in matches)
            {
               ms.Write(ENC.ASCII.GetBytes(content), cursor, match.Index - cursor);
               cursor = match.Index + match.Length;
               replStr = GetReplacement(match, path);
                if (replStr != string.Empty)
                    ms.Write(ENC.UTF8.GetBytes(replStr), 0, replStr.Length);
            }

            ms.Write(ENC.ASCII.GetBytes(content), cursor, content.Length - cursor);

            res = ENC.ASCII.GetString(ms.ToArray());

            ms.Dispose();

            return res;
        }

        private static string GetReplacement(Match match, string path)
        {
            string tag = match.Value.Substring(2, match.Length - 4);

            return GetReplacement(tag, path);
        }

        public static string GetReplacement(string tag, string path = "")
        { 
            string rstr = string.Empty;

            // If the tag begins with an '!' then it is a snippet within a snippt
            //  but by using a stack we make sur there are no circular references.
            // TODO The user is notified multiple times where there are mutiple tags.
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
                        rstr = ParseFile(snip, path);
                    }
                }
                else
                {
                    MessageBox.Show("No such Snippet: <" + tag.Substring(1) + ">", 
                        "Input Error", 
                        MessageBoxButton.OK, 
                        MessageBoxImage.Error);
                }
            }
            else
            {
                // Go throught the keyword list first
                rstr = KeywordManager.ParseKeywords(tag, path);
                if (rstr == string.Empty)
                {
                    // Then if not found check the DB for User replacement strings
                    rstr = MasterRepository._Token.GetTokenByTag(tag);
                    if (rstr == string.Empty)
                        MessageBox.Show("Unknown Tag encountered: <" + tag + ">",
                            "Input Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                }
            }

            return rstr;
        }

        public static ObservableCollection<TagInfo> GetTagInfo(string text)
        {
            Regex rx = new Regex(ParseString);
            MatchCollection matches = rx.Matches(text);
            string innerText = string.Empty;
            TagInfo.TokenType type = TagInfo.TokenType.Undefined;

            ObservableCollection<TagInfo> tagInfoList = new ObservableCollection<TagInfo>();

            foreach(Match match in matches)
            {
                innerText = match.Value.Trim().Substring(2, match.Length - 4);
                if (innerText.StartsWith("!"))
                    type = TagInfo.TokenType.Snippet;
                else
                    type = TagInfo.TokenType.Token;

                tagInfoList.Add(new TagInfo(match.Index, match.Length, match.Value, innerText, type));
            }

            return tagInfoList;
        }
    }
}
