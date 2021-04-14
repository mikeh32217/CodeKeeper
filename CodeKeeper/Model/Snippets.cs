using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Model
{
    public class Snippets
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Tag { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public Snippets() { }
    }
}
