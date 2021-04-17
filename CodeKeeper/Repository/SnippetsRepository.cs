using CodeKeeper.Configuration;
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

            string date = Utilities.DateTimeFormatter.DateTimeFormat(DateTime.Now.ToString());
            string defcon = ConfigMgr.Instance.settingProvider.GetSingleValue("DefaultContent", "content");
            string deflang = ConfigMgr.Instance.settingProvider.GetSingleValue("DefaultLanguage", "language");

            string sql = "INSERT INTO " + EntityDataTable.TableName + " (Tag, Language, Url, Content, " +
                                                    "CreateDate, UpdateDate) VALUES" +
                     " ('" + tag + "', " + 
                             "'" + deflang + "', " + "'N/A', " + "'" + defcon + "', '" + date + "', '" + date + "')";

            try
            {
                id = MasterRepository.ExecuteNonQuery(EntityDataTable, sql);

                if (id > 0)
                {
                    DataRow row = EntityDataTable.NewRow();

                    row["Id"] = id;
                    row["Tag"] = tag;
                    row["Language"] = deflang;
                    row["Url"] = "N/A";
                    row["Content"] = defcon;
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

        public void UpdateSnippet(DataRowView drv)
        {
            if (drv == null)
            {
                MessageBox.Show("UpdateSnippet: drv is null");
                return;
            }

            string id = drv["Id"].ToString();
            DataRow row = MasterRepository._Snippet.GetById(id);
            if (row == null)
            {
                MessageBox.Show("UpdateSnippet: Row with Id <" + id + "> not found");
                return;
            }

            try
            {
                string content = Utilities.NormalizeText.Normalize(drv["Content"].ToString());
                string sql = "UPDATE " + EntityDataTable.TableName + " SET " +
                    "Content='" + content + "', " +
                    "Language='" + drv["Language"] + "', " +
                    "Url='" + drv["Url"] + "', " +
                    "UpdateDate='" + Utilities.DateTimeFormatter.DateTimeFormat(DateTime.Now.ToString()) + "'" +
                    " WHERE Id=" + drv["Id"].ToString();

                MasterRepository.ExecuteNonQuery(EntityDataTable, sql);

                EntityDataTable.AcceptChanges();

            }
            catch (Exception x)
            {
                MessageBox.Show("UpdateSnippet: " + x.Message);
            }
        }

        public void Delete(string id)
        {
            DataRow row = GetById(id);

            if (row != null)
            {
                try
                {
                    string sql = "delete from " + EntityDataTable.TableName + " where Id = " + id;
                    MasterRepository.ExecuteNonQuery(EntityDataTable, sql);

                    row.Delete();
                    EntityDataTable.AcceptChanges();
                }
                catch (Exception x)
                {
                    MessageBox.Show("PartRepository:Delete - " + x.Message);
                }
            }
        }
    }
}
