using CodeKeeper.Configuration;
using CodeKeeper.Keyword;
using CodeKeeper.Keyword.Model;
using CodeKeeper.Model;
using CodeKeeper.Repository;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

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

            return Parse(content, path);
        }

        private static string Parse(string content, string path = "")
        {
            Regex rx = new Regex(ParseString);
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
                        rstr = Parse(snip);
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
                {
                    innerText = innerText.Substring(1);
                    type = TagInfo.TokenType.Snippet;
                }
                else
                    type = TagInfo.TokenType.Token;

                tagInfoList.Add(new TagInfo(match.Index, match.Length, match.Value, innerText, type));
            }

            return tagInfoList;
        }
    }
}
