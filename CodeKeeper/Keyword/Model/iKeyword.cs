using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Keyword
{
    public interface iKeyword
    {
        string Keyword { get; set; }

        string Execute(object param);
    }
}
