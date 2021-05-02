using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Model
{
    public class TagInfo
    {
        public int SelectIndex { get; set; }
        public int SelectLength { get; set; }
        public string LinkTargetText { get; set; }

        public TagInfo(int index, int len, string tagetText)
        {
            SelectIndex = index;
            SelectLength = len;
            LinkTargetText = tagetText;
        }
    }
}
