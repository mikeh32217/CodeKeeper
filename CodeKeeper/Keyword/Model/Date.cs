using CodeKeeper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Keyword.Model
{
    public class Date : iKeyword
    {
        public string Keyword { get; set; } = "Date";

        public string Execute(object param)
        {
            string tag = string.Empty;

            // Format examples https://www.csharp-examples.net/string-format-datetime/
            string format = MasterRepository._Token.GetTokenByTag(Keyword);
            if (format != null)
                tag = DateTime.Now.ToString(format);

            return tag;
        }
    }
}
