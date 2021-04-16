using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Repository
{
    public class TokensRepository : RepositoryBase
    {
        public TokensRepository(string tablename, bool lazyload) : base(tablename, lazyload)
        {
        }

        public override string GetLazyLoadQueryString()
        {
            return "select * from " + EntityDataTable.TableName + " where TokenId = 0";
        }

        public string GetTokenByTag(string tag)
        {
            EntityDataTable.DefaultView.RowFilter = "Tag = '" + tag + "'";

            if (EntityDataTable.DefaultView.Count > 0)
                return EntityDataTable.DefaultView[0].Row["Content"].ToString();

            return string.Empty;
        }
    }
}
