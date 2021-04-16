using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeKeeper.Utilities
{
    public class DocumentUtils
    {
        public static bool InsertTextInFile(string path, int line, string txt)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("No such file");
                return false;
            }

            var text = new StringBuilder();
            int cnt = 0;

            foreach (string s in File.ReadAllLines(path))
            {
                if (cnt++ == line)
                    text.Append(txt);

                text.AppendLine(s);
            }

            File.WriteAllText(path, text.ToString());

            return true;
        }
    }
}
