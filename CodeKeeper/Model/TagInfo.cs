using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Model
{
    public class TagInfo
    {
        public enum TokenType
        {
            Undefined,
            Snippet,
            Token
        };

        public int SelectIndex { get; set; }
        public int SelectLength { get; set; }
        public string LinkTargetText { get; set; }
        public string LinkTargetInnerText { get; set; }
        public bool IsValid { get; set; }
        public TokenType TagType { get; set; }

        public TagInfo() { }

        public TagInfo(int index, int len, string tagetText, string innerText, TokenType type, bool valid = false)
        {
            SelectIndex = index;
            SelectLength = len;
            LinkTargetText = tagetText;
            LinkTargetInnerText = innerText;
            IsValid = valid;
            TagType = type;
        }
    }
}
