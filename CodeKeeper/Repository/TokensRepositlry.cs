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

    }
}
