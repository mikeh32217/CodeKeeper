/****************************************************************
 *  Header File: RepositoryBase.cs
 *  Description: Base for all Repository classes
 *
 *    History:
 *     Date        Description
 *     ---------- --------------------------------------
 *    11/23/2019 - Created
 *    7/26/2020 - Aeeded the DataViewPool.ClearViewPool method and updated otheres.
 *    8/9/2020 - Refactored GetView, only caled when aqctuaally need to create a view.
 *
 * Copyright (c) DIY Solutions, 2020 (Mike Hankey)
 */

using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace CodeKeeper.Repository
{
    public class RepositoryBase
    {
        #region Properties

        public DataTable EntityDataTable { get; set; }
        public bool IsTableLoaded { get; set; } = false;

        public DataView DefaultView { get { return EntityDataTable.DefaultView; } }

        #endregion

        #region ctor

        public RepositoryBase(string tablename, bool lazyload)
        {
            // Create a table for the entity to use
            EntityDataTable = new DataTable(tablename);

            if (!lazyload)
                MasterRepository.ExecuteQuery(EntityDataTable, GetQueryString());
            else
                MasterRepository.ExecuteQuery(EntityDataTable, GetLazyLoadQueryString());
        }

        #endregion

        #region Virtual methods

        // Uses the DefaultView
        virtual public DataView GetAllAsView(object sender = null)
        {
// TODO Check           DataView view = GetView();
            
            MasterRepository.ExecuteQuery(EntityDataTable, GetQueryString());

            return EntityDataTable.DefaultView;
 // TODO Check           return view;
        }

        virtual public string GetQueryString()
        {
            return "select * from " + EntityDataTable.TableName;
        }

        virtual public string GetLazyLoadQueryString()
        {
            return "select * from " + EntityDataTable.TableName + " where " + EntityDataTable.TableName + "id = 0";
        }

        virtual public string GetTableNameString()
        {
            return EntityDataTable.TableName;
        }

        virtual public DataRow GetById(string id)
        {
            string idStr = GetTableNameString() + "Id";

            // First check the rows that are already loaded
            DataRow[] rows = EntityDataTable.Select(idStr + "='" + id + "'");
            if (rows.Length > 0)
                return rows[0];

            // Next query the DB for the record
            string sql = "SELECT * FROM " + EntityDataTable.TableName + " WHERE " + idStr + "=" + id;
            MasterRepository.ExecuteQuery(EntityDataTable, sql);
            if (EntityDataTable.Rows.Count > 0)
                return EntityDataTable.Rows[0];

            // Finally return null indicating that we couldn't find it.
            return null;

        }

        virtual public DataRow CreateNewRow()
        {
            return EntityDataTable.NewRow();
        }

        virtual public DataRowView Create()
        {
            DataRow dr = EntityDataTable.NewRow();
            DataRowView drv = null;

            // NewRow returns a DataRow but binding needs a DataRowView so after creating
            //  the new row we have to find it in the DefaultView, we do this by using the
            //  RowStateFilter and filtering on the Add row state.
            EntityDataTable.DefaultView.RowStateFilter = DataViewRowState.Added;
            if (EntityDataTable.DefaultView.Count > 0)
                drv = EntityDataTable.DefaultView[0];
            else
            {
                MessageBox.Show("Create: Couldn't find new record in view");
            }

            return drv;
        }

        #endregion
    }
}
