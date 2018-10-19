using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DRapid.Utility.Linq.Expressions;

namespace DRapid.Web.VisitAnalyse.Storage.SqlServer
{
    public class SqlBulckCopyWriter<T> : IDisposable
    {
        public SqlBulckCopyWriter(string dbConnection,
            string tableName = null, IList<string> fieldNames = null,
            Func<Dictionary<string, object>> valueProvider = null)
        {
            SqlConnectionStringBuilder = new SqlConnectionStringBuilder(dbConnection)
            {
                MaxPoolSize = 10,
                ConnectTimeout = 2
            };
            TableName = tableName ?? typeof(T).Name;
            _valueProvider = valueProvider;
            FieldNames = fieldNames;
        }

        public SqlConnectionStringBuilder SqlConnectionStringBuilder { get; }

        public IList<string> FieldNames { get; }

        private Func<Dictionary<string, object>> _valueProvider;

        private DataTable _tableTemplate;

        public string TableName { get; }

        /// <summary>
        /// 将一个数据项集合写入数据库服务器
        /// </summary>
        /// <param name="dataCollection"></param>
        /// <param name="sqlConn"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task InertToServerAsync(IList<T> dataCollection,
            SqlConnection sqlConn = null, SqlTransaction transaction = null)
        {
            LoadTemplateIfNull();
            using (var table = _tableTemplate.Copy())
            {
                FillDataTable(table, dataCollection);
                sqlConn = sqlConn ?? transaction?.Connection;
                if (sqlConn == null)
                {
                    sqlConn = new SqlConnection(SqlConnectionStringBuilder.ConnectionString);
                    using (sqlConn)
                    {
                        if (sqlConn.State != ConnectionState.Open)
                        {
                            sqlConn.Open();
                        }
                        await WriteToServerAsync(table, sqlConn, transaction);
                    }
                }
                else
                {
                    if (sqlConn.State != ConnectionState.Open)
                    {
                        sqlConn.Open();
                    }
                    await WriteToServerAsync(table, sqlConn, transaction);
                }
            }
        }

        /// <summary>
        /// 批量更新数据到数据库服务器
        /// </summary>
        /// <returns></returns>
        public async Task UpdateToServerAsync(IList<T> dataCollection, Expression<Func<T, object>> keySelector,
            Action<SqlBulckCopyWriter<T>, DataTable, IList<T>> updateAction, SqlConnection sqlConn = null,
            SqlTransaction transaction = null, string loadQuery = null,
            Func<DataTable, IList<T>, string, string> deleteCommandBuilder = null)
        {
            updateAction = updateAction ??
                           ((writer, dataTable, collection) => DefaultUpdateAction(dataTable, dataCollection));
            var idKey = ExpressionHelper.ReadMemberName(keySelector);
            var keyMapper = keySelector.Compile();
            var ids = dataCollection.Select(i => $"'{keyMapper(i)}'").Join(",");
            loadQuery = loadQuery ?? $"SELECT TOP 0 * FROM {TableName} WHERE {idKey} IN ({ids})";
            deleteCommandBuilder = deleteCommandBuilder ?? DefaultDeleteCommandBuilder;

            using (var dataTable = LoadDataTable(loadQuery))
            {
                updateAction(this, dataTable, dataCollection);
                var deleteCommand = deleteCommandBuilder(dataTable, dataCollection, idKey);
                bool shouldAutoCommit = false;
                if (transaction == null)
                {
                    if (sqlConn == null)
                        sqlConn = new SqlConnection(SqlConnectionStringBuilder.ConnectionString);
                    await sqlConn.OpenAsync();
                    transaction = sqlConn.BeginTransaction();
                    shouldAutoCommit = true;
                }
                if (shouldAutoCommit)
                {
                    using (transaction)
                    {
                        if (!deleteCommand.IsNullOrEmpty())
                        {
                            await ExecuteCommandAsync(deleteCommand, sqlConn, transaction);
                        }                
                        await WriteToServerAsync(dataTable, sqlConn, transaction);
                        transaction.Commit();
                    }
                }
                else
                {
                    //如果是外部指定的事务，那么该事务的生命周期应该由外部控制
                    if (!deleteCommand.IsNullOrEmpty())
                    {
                        await ExecuteCommandAsync(deleteCommand, sqlConn, transaction);
                    }
                    await WriteToServerAsync(dataTable, sqlConn, transaction);
                }
            }
        }

        /// <summary>
        /// 查询数据库（通过loadQuery）得到表模型，装载入DataSet
        /// 由SqlDataAdapter装载入DataSet直接得到DataTable
        /// </summary>
        /// <param name="loadQuery">sql语句</param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        private DataTable LoadDataTable(string loadQuery, SqlConnection sqlConn = null)
        {
            DataTable dataTable;
            if (sqlConn == null)
            {
                using (sqlConn = new SqlConnection(SqlConnectionStringBuilder.ConnectionString))
                {
                    using (var adapter = new SqlDataAdapter(loadQuery, sqlConn))
                    {
                        var set = new DataSet();
                        adapter.Fill(set);
                        dataTable = set.Tables[0];
                    }
                }
            }
            else
            {
                using (var adapter = new SqlDataAdapter(loadQuery, sqlConn))
                {
                    var set = new DataSet();
                    adapter.Fill(set);
                    dataTable = set.Tables[0];
                }
            }
            return dataTable;
        }

        private void DefaultUpdateAction(DataTable dataTable, IList<T> dataCollection)
        {
            FillDataTable(dataTable, dataCollection, FieldNames, _valueProvider?.Invoke());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="dataCollection"></param>
        /// <param name="idKey"></param>
        /// <returns></returns>
        private string DefaultDeleteCommandBuilder(DataTable dataTable, IList<T> dataCollection, string idKey)
        {
            var rows = dataTable.Rows.Cast<DataRow>().Where(i => i.RowState == DataRowState.Modified).ToList();
            //todo 这里当更新的数量超过一定量的时候抛出异常
            //if (rows.Count() > 10000)
            //{
            //    throw new Exception("一次性更新数据过多");
            //}
            if (!rows.Any())
            {
                return string.Empty;
            }
            var ids = rows.Select(i => $"'{i[idKey]}'").Join(",");
            return $"DELETE {TableName} WHERE {idKey} in ({ids})";
        }

        /// <summary>
        /// 更新数据项到数据表
        /// </summary>
        /// <param name="table"></param>
        /// <param name="dataCollection"></param>
        /// <param name="fields"></param>
        /// <param name="additionalInfos"></param>
        public static void FillDataTable(DataTable table, IEnumerable<T> dataCollection,
            IList<string> fields = null, Dictionary<string, object> additionalInfos = null)
        {
            foreach (var data in dataCollection)
            {
                UpdateDataRow(table, data, fields, additionalInfos);
            }
        }

        /// <summary>
        /// 更新数据项到数据行
        /// </summary>
        /// <param name="table"></param>
        /// <param name="data"></param>
        /// <param name="fields"></param>
        /// <param name="additionalInfos"></param>
        public static void UpdateDataRow(DataTable table, T data, IList<string> fields = null,
            Dictionary<string, object> additionalInfos = null)
        {
            var dataRow = table.NewRow();
            table.Rows.Add(dataRow);

            fields = fields ?? table
                .Columns.Cast<DataColumn>().Select(i => i.ColumnName).ToList();
            foreach (var field in fields)
                dataRow[field] = PropertyFieldLoader.Instance.Load(data, field) ?? DBNull.Value;

            if (additionalInfos == null) return;
            foreach (var key in additionalInfos.Keys)
                dataRow[key] = additionalInfos[key];
        }

        /// <summary>
        /// 将数据表内的数据更新到数据库
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task WriteToServerAsync(DataTable dataTable, SqlConnection connection = null,
            SqlTransaction transaction = null)
        {
            ClearUnchangedRows(dataTable);
            SqlBulkCopy bulkCopy;
            if (transaction == null && connection == null)
            {
                throw new NotSupportedException("必须指定一个连接或者指定一个事务");
            }
            if (transaction == null)
            {
                bulkCopy = new SqlBulkCopy(connection);
            }
            else
            {
                bulkCopy = new SqlBulkCopy(transaction.Connection, SqlBulkCopyOptions.Default, transaction);
            }
            bulkCopy.BatchSize = dataTable.Rows.Count;
            bulkCopy.DestinationTableName = TableName;
            await bulkCopy.WriteToServerAsync(dataTable);
        }

        public async Task ExecuteCommandAsync(string commandText, SqlConnection sqlConn = null,
            SqlTransaction transaction = null)
        {
            if (transaction == null && sqlConn == null)
            {
                throw new NotSupportedException("必须指定一个连接或者指定一个事务");
            }
            if (transaction != null)
            {
                sqlConn = transaction.Connection;
            }
            using (var command = new SqlCommand(commandText, sqlConn))
            {
                command.Transaction = transaction;
                if (sqlConn.State != ConnectionState.Open)
                    await sqlConn.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// 清除没有任何变更的数据行
        /// </summary>
        /// <param name="dataTable"></param>
        private void ClearUnchangedRows(DataTable dataTable)
        {
            var unchangedRows =
                dataTable.Rows.Cast<DataRow>().Where(i => i.RowState == DataRowState.Unchanged).ToList();
            foreach (var unchangedRow in unchangedRows)
            {
                dataTable.Rows.Remove(unchangedRow);
            }
        }

        /// <summary>
        /// 从数据库服务器加载数据表模版
        /// </summary>
        private void LoadTemplateIfNull()
        {
            lock (this)
            {
                if (_tableTemplate == null)
                {
                    _tableTemplate = LoadDataTable($"SELECT TOP 0 * FROM [{TableName}]");
                }
            }
        }

        public void Dispose()
        {
            _tableTemplate.Dispose();
        }
    }
}