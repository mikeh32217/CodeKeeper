using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public override string GetByIdString()
        {
            return "Id";
        }

        public Int64 SaveSniipet(string tag)
        {
            Int64 id = 0;
            string content = "None";

            string sql = "INSERT INTO " + EntityDataTable.TableName + " (Tag, Content) VALUES" +
                     " ('" + tag + "', '" + content + "')";

            try
            {
                id = MasterRepository.ExecuteNonQuery(EntityDataTable, sql);

                DataRow row = EntityDataTable.NewRow();

                row["Id"] = id;
                row["Tag"] = tag;
                row["Content"] = content;

                EntityDataTable.Rows.Add(row);
                EntityDataTable.AcceptChanges();
            }
            catch(Exception x)
            {
                MessageBox.Show("SaveSnippet: " + x.Message);
            }

            return id;
        }
    }
}
