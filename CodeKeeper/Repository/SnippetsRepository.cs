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

            string sql = "INSERT INTO " + EntityDataTable.TableName + " (Tag, Language, Url, Content, " +
                                                    "CreateDate, UpdateDate) VALUES" +
                     " ('" + tag + "', " + 
                             "'N/A', " + "'N/A', " + "'N/A', '" + DateTime.Now + "', '" + DateTime.Now + "')";

            try
            {
                id = MasterRepository.ExecuteNonQuery(EntityDataTable, sql);

                if (id > 0)
                {
                    DataRow row = EntityDataTable.NewRow();

                    row["Id"] = id;
                    row["Tag"] = tag;
                    row["Language"] = "N/A";
                    row["Url"] = "N/A";
                    row["Content"] = "N/A";
                    row["CreateDate"] = DateTime.Now;
                    row["UpdateDate"] = DateTime.Now;

                    EntityDataTable.Rows.Add(row);
                    EntityDataTable.AcceptChanges();
                }
            }
            catch(Exception x)
            {
                MessageBox.Show("SaveSnippet: " + x.Message);
            }

            return id;
        }

        public void UpdateSniipet(DataRowView drv)
        {
            string content = Utilities.NormalizeText.Normalize(drv["Content"].ToString());

            // TODO Finish
        }
    }
}
