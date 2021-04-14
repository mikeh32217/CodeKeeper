using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Repository
{
    public class SnippetsRepository : RepositoryBase
    {
        public SnippetsRepository(string tablename, bool lazyload) : base(tablename, lazyload)
        {

        }

        public override string GetLazyLoadQueryString()
        {
            return "select * from " + EntityDataTable.TableName + " where Id = 0";
        }
    }
}
