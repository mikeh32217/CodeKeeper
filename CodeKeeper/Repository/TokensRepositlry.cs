using CodeKeeper.Configuration;
using CodeKeeper.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public override string GetByIdString()
        {
            return "TokenId";
        }

        public string GetTokenByTag(string tag)
        {
            WorkingView.RowFilter = "Tag = '" + tag + "'";

            if (WorkingView.Count > 0)
                return WorkingView[0].Row["Content"].ToString();

            return string.Empty;
        }

        // TODO Do I need this?
        public DataRowView GetTokenObjectByTag(string tag)
        {
            WorkingView.RowFilter = "Tag = '" + tag + "'";

            if (WorkingView.Count > 0)
                return WorkingView[0];

            return null;
        }

        public Int64 SaveTag(string tag)
        {
            Int64 id = 0;

            string defcon = ConfigMgr.Instance.settingProvider.GetSingleValue("DefaultContent", "content");

            string sql = "INSERT INTO " + EntityDataTable.TableName + " (Tag, Content, Description) VALUES" +
                     " ('" + tag + "', '" + defcon + "', 'None')";

            try
            {
                id = MasterRepository.ExecuteNonQuery(EntityDataTable, sql);

                if (id > 0)
                {
                    DataRow row = EntityDataTable.NewRow();

                    row["TokenId"] = id;
                    row["Tag"] = tag;
                    row["Content"] = defcon;
                    row["Description"] = "None";

                    EntityDataTable.Rows.Add(row);
                    EntityDataTable.AcceptChanges();
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("SaveTag: " + x.Message);
            }

            return id;
        }

        public void UpdateTag(DataRowView drv)
        {
            if (drv == null)
            {
                MessageBox.Show("UpdateTag: drv is null");
                return;
            }

            string id = drv["TokenId"].ToString();
            DataRow row = MasterRepository._Token.GetById(id);
            if (row == null)
            {
                MessageBox.Show("UpdateSnippet: Row with TokenId <" + id + "> not found");
                return;
            }

            try
            {
                string content = Utilities.NormalizeText.Normalize(drv["Content"].ToString());
                string desc = Utilities.NormalizeText.Normalize(drv["Description"].ToString());

                string sql = "UPDATE " + EntityDataTable.TableName + " SET " +
                    "Tag='" + drv["Tag"] + "', " +
                    "Content='" + content + "', " +
                    "Description='" + desc + "' " +
                    " WHERE TokenId=" + drv["TokenId"].ToString();

                MasterRepository.ExecuteNonQuery(EntityDataTable, sql);

                EntityDataTable.AcceptChanges();

            }
            catch (Exception x)
            {
                MessageBox.Show("UpdateTag: " + x.Message);
            }
        }

        public void Delete(string id)
        {
            DataRow row = GetById(id);

            if (row != null)
            {
                try
                {
                    string sql = "delete from " + EntityDataTable.TableName + " where TokenId = " + id;
                    MasterRepository.ExecuteNonQuery(EntityDataTable, sql);

                    row.Delete();
                    EntityDataTable.AcceptChanges();
                }
                catch (Exception x)
                {
                    MessageBox.Show("TagRepository:Delete - " + x.Message);
                }
            }
        }
    }
}
