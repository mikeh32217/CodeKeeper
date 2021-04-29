using CodeKeeper.Keyword.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Keyword
{
    public static class KeywordManager
    {
        private static List<iKeyword> KeywordList = new List<iKeyword>();

        static KeywordManager()
        {
            KeywordList.Add(new Filename());
            KeywordList.Add(new Date());
            KeywordList.Add(new Prompt());
        }

        public static string ParseKeywords(string tag, object param)
        {
            string rstr = string.Empty;

            foreach(iKeyword key in KeywordList)
            {
                if (key.Keyword.ToUpper() == tag.ToUpper())
                    rstr = key.Execute(param);
            }

            return rstr;
        }
    }
}
