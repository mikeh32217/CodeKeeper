using CodeKeeper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace CodeKeeper.Repository
{
    public class MasterRepository
    {

        public static string ConnectionString { get; set; }

        public static SnippetsRepository _Snippet { get; set; }
        public static TokensRepository _Token { get; set; }

        static SQLiteConnection conn = null;
        static private SQLiteCommand _command = null;
        static private SQLiteDataReader reader = null;

        static MasterRepository()
        {
#if DEBUG
            ConnectionString = ConfigMgr.Instance.ConnectionString;
#else
            ConnectionString = ConfigMgr.Instance.ConnectionStringLive;
#endif

            _Snippet = new SnippetsRepository("Snippets", false);
            _Token = new TokensRepository("Tokens", false);
        }

        public static void LoadTable(RepositoryBase tab)
        {
            ExecuteQuery(tab.DefaultView.Table, tab.GetQueryString());
            tab.IsTableLoaded = true;
        }

        /// <summary>
        /// Executes a scalar query using query string.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static object ExecuteScalar(DataTable table, string sql)
        {
            object obj = null;

            try
            {
                conn = new SQLiteConnection(ConnectionString);
                conn.Open();

                _command = conn.CreateCommand();
                _command.CommandText = sql;

                obj = _command.ExecuteScalar();
            }
            catch (Exception x)
            {
                DataRow[] rows;
                string error = string.Empty;

                //NOTE Found this online as a way to view error detail.
                rows = table.GetErrors();
                for (int i = 0; i < rows.Length; i++)
                {
                    foreach (DataColumn col in table.Columns)
                        error += rows[i].GetColumnError(col) + Environment.NewLine;
                }
                MessageBox.Show("ExecuteScalar:DBFillTableError: " + x.Message);
            }
            finally
            {
                conn.Close();
                _command.Dispose();
            }
            return obj;
        }

        public static Int64 ExecuteNonQuery(DataTable table, string sql)
        {
            Int64 retval = 0;

            try
            {
                conn = new SQLiteConnection(ConnectionString);
                conn.Open();

                _command = conn.CreateCommand();
                _command.CommandText = sql;
                retval = (Int64)_command.ExecuteNonQuery();

                if (_command.CommandText.Contains("INSERT"))
                {
                    reader.Close();

                    sql = "SELECT last_insert_rowid()";
                    _command.CommandText = sql;

                    retval = (Int64)_command.ExecuteScalar();
                }
            }
            catch (Exception x)
            {
                DataRow[] rows;
                string error = string.Empty;

                //NOTE Found this online as a way to view error detail.
                rows = table.GetErrors();
                for (int i = 0; i < rows.Length; i++)
                {
                    foreach (DataColumn col in table.Columns)
                        error += rows[i].GetColumnError(col) + Environment.NewLine;
                }
                MessageBox.Show("ExecuteNonQuery:Error: " + x.Message);
            }
            finally
            {
                if (!reader.IsClosed)
                    reader.Close();
                conn.Close();
                _command.Dispose();
            }

            return retval;
        }
        public static Int64 ExecuteQuery(DataTable table, string sql)
        {
            Int64 retval = 0;

            try
            {
                conn = new SQLiteConnection(ConnectionString);
                conn.Open();

                table.Clear();

                _command = conn.CreateCommand();
                _command.CommandText = sql;
                reader = _command.ExecuteReader();

                table.Load(reader);

            }
            catch (Exception x)
            {
                DataRow[] rows;
                string error = string.Empty;

                //NOTE Found this online as a way to view error detail.
                rows = table.GetErrors();
                for (int i = 0; i < rows.Length; i++)
                {
                    foreach (DataColumn col in table.Columns)
                        error += rows[i].GetColumnError(col) + Environment.NewLine;
                }
                MessageBox.Show("ExecuteQuery:Error: " + x.Message);
            }
            finally
            {
                if (!reader.IsClosed)
                    reader.Close();
                conn.Close();
                _command.Dispose();
            }

            return retval;
        }
    }
}
