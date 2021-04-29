using CodeKeeper.Configuration;
using CodeKeeper.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Keyword.Model
{
    public class Filename : iKeyword
    {
        public string Keyword { get; set; } = "Filename";

        public string Execute(object param)
        {
            string path = param.ToString();
            string rstr = string.Empty;

            if (path == null || path == string.Empty)
            {
                rstr = MasterRepository._Token.GetTokenByTag(Keyword);
                if (rstr == string.Empty)
                {
                    rstr = ConfigMgr.Instance.settingProvider.GetSingleValue("DefaultFilename", "name");
                    if (rstr == null || rstr == string.Empty)
                        rstr = "filename.ext";
                }
            }
            else
                rstr = Path.GetFileName(path);

            return rstr;
        }
    }
}
