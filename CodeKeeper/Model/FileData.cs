using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Model
{
    public class FileData
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public FileData(string nm)
        {
            IsChecked = false;
            Name = nm;
        }
    }
}
