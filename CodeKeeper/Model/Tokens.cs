using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Model
{
    public class Tokens
    {
        public int TokenId { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }

        public Tokens() { }
    }
}
