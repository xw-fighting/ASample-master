using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using DRapid.Utility.Hash;
using DRapid.Web.VisitAnalyse.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using CommandFlags = StackExchange.Redis.CommandFlags;
using DRapid.Utility.Linq.Enumerable;

namespace DRapid.Web.VisitAnalyse.Core
{
    public static class Extension
    {
        /// <summary>
        /// 根据开始、结束时间和一个时间刻度获取一个时间轴
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <param name="dateTimePart"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> Range(this DateTime dateTime1,
            DateTime dateTime2, DateTimeParts dateTimePart)
        {
            var from = dateTime1 > dateTime2 ? dateTime2 : dateTime1;
            var to = dateTime1 < dateTime2 ? dateTime2 : dateTime1;

            var flt = from;
            while (flt < to)
            {
                yield return flt.GetPart(dateTimePart);
                to = to.Add(dateTimePart, 1);
            }
            yield return to.GetPart(dateTimePart);
        }

        /// <summary>
        /// 根据指定的粗细粒度获取一个时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateTimePart"></param>
        /// <returns></returns>
        public static DateTime GetPart(this DateTime dateTime, DateTimeParts dateTimePart)
        {
            switch (dateTimePart)
            {
                case DateTimeParts.Year: return new DateTime(dateTime.Year, 1, 1);
                case DateTimeParts.Month: return new DateTime(dateTime.Year, dateTime.Month, 1);
                case DateTimeParts.Day: return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                case DateTimeParts.Hour:
                    return new DateTime(dateTime.Year, dateTime.Month,
                        dateTime.Day, dateTime.Hour, 0, 0);
                case DateTimeParts.Minute:
                    return new DateTime(dateTime.Year, dateTime.Month,
                        dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
                case DateTimeParts.Second:
                    return new DateTime(dateTime.Year, dateTime.Month,
                        dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                default: throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 向一个时间对象追加指定数量的和指定刻度的时间，返回一个新的时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateTimePart"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime Add(this DateTime dateTime, DateTimeParts dateTimePart, int value)
        {
            switch (dateTimePart)
            {
                case DateTimeParts.Year: return dateTime.AddYears(value);
                case DateTimeParts.Month: return dateTime.AddMonths(value);
                case DateTimeParts.Day: return dateTime.AddDays(value);
                case DateTimeParts.Hour: return dateTime.AddHours(value);
                case DateTimeParts.Minute: return dateTime.AddMinutes(value);
                case DateTimeParts.Second: return dateTime.AddSeconds(value);
                default: throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 判断一个时间是否和另外一个时间属于同一个刻度内
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="testDateTime"></param>
        /// <param name="dateTimePart"></param>
        /// <returns></returns>
        public static bool Equals(this DateTime dateTime, DateTime testDateTime, DateTimeParts dateTimePart)
        {
            return dateTime.GetPart(dateTimePart) == testDateTime.GetPart(dateTimePart);
        }

        /// <summary>
        /// 获取一个字符串，该字符串精确到指定的时间部分
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateTimePart"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static string ToString(this DateTime dateTime, DateTimeParts dateTimePart, string formatter = null)
        {
            switch (dateTimePart)
            {
                case DateTimeParts.Year: return dateTime.ToString(formatter ?? "yyyy");
                case DateTimeParts.Month: return dateTime.ToString(formatter ?? "yyyy-MM");
                case DateTimeParts.Day: return dateTime.ToString(formatter ?? "yyyy-MM-dd");
                case DateTimeParts.Hour: return dateTime.ToString(formatter ?? "yyyy-MM-dd HH");
                case DateTimeParts.Minute: return dateTime.ToString(formatter ?? "yyyy-MM-dd HH:mm");
                case DateTimeParts.Second: return dateTime.ToString(formatter ?? "yyyy-MM-dd HH:mm:ss");
                default: throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 为一个类型生成一个唯一的字符串hash和一个可读性高的字符串
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public static UniqueTypeIdentifier BuildUniqueIdentifier(this Type typeInfo)
        {
            return new UniqueTypeIdentifier(typeInfo);
        }

        /// <summary>
        /// 类型的唯一（伪）标识符，和程序集版本无关
        /// </summary>
        public struct UniqueTypeIdentifier
        {
            public UniqueTypeIdentifier(Type typeInfo)
            {
                string displayName = null;
                string hashName = null;
                BuildName(typeInfo, ref displayName, i => i.Name.Split('`').FirstOrDefault());
                BuildName(typeInfo, ref hashName, i => $"{i.Namespace}.{i.Name}");
                var hasher = new MD5Hasher();
                var hash = hasher.Hash(hashName);
                DisplayName = displayName;
                Hash = hash;
                TypeFullName = typeInfo.FullName;
            }

            public string DisplayName { get; }

            public string Hash { get; }

            public string TypeFullName { get; }

            public override bool Equals(object obj)
            {
                return obj != null && obj.GetType() == GetType() &&
                       ((UniqueTypeIdentifier) obj).Hash == Hash;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((DisplayName != null ? DisplayName.GetHashCode() : 0) * 397) ^
                           (Hash != null ? Hash.GetHashCode() : 0);
                }
            }

            private static void BuildName(Type typeInfo, ref string name, Func<Type, string> nameBuilder)
            {
                if (name == null)
                    name = nameBuilder(typeInfo);
                if (typeInfo.IsGenericType)
                {
                    var typeArgs = typeInfo.GetGenericArguments();
                    var length = typeArgs.Length;
                    for (var i = 0; i < length; i++)
                    {
                        var type = typeArgs[i];
                        if (i == 0)
                        {
                            if (length > 1)
                            {
                                name = $"{name}<{nameBuilder(type)},";
                            }
                            else
                            {
                                name = $"{name}<{nameBuilder(type)}";
                            }
                        }
                        else if (i == length - 1)
                        {
                            name = $"{name}{nameBuilder(type)}>";
                        }
                        else
                        {
                            name = $"{name}{nameBuilder(type)},";
                        }

                        BuildName(type, ref name, nameBuilder);
                    }
                }
            }

            public override string ToString()
            {
                return $"{DisplayName}_{Hash}";
            }
        }

        /// <summary>
        /// 根据类型计算一个字段列表
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <param name="ignoreAttribute"></param>
        /// <returns></returns>
        public static IEnumerable<string> SelectColumnNameList(this Type typeInfo, bool ignoreAttribute = false)
        {
            var key = typeInfo.FullName;
            return DataFieldCache.GetOrAdd($"{key}_ignoreAttribute_{ignoreAttribute}", k =>
            {
                var builder = ignoreAttribute ? (p => p.Name) : ColumnNameBuilder;
                var tempBuilder = builder;
                builder = ignoreAttribute ? builder : p => $"[{tempBuilder(p)}]";
                var properties = typeInfo.GetProperties();
                var result = properties
                    .Where(i => i.GetCustomAttribute(typeof(NotMappedAttribute)) == null)
                    .Select(builder)
                    .Where(i => !i.IsNullOrWhiteSpace());

                return result;
            });
        }

        public static Dictionary<string,string> SelectColumnNameDic(this Type typeInfo, 
            bool ignoreAttribute = false, bool ignoreConflict = false)
        {
            var key = typeInfo.FullName;
            return DataFieldDicCache.GetOrAdd($"{key}_ignoreAttribute_{ignoreAttribute}", k =>
            {
                var builder = ignoreAttribute ? (p => p.Name) : ColumnNameBuilder;
                var tempBuilder = builder;
                var tempBuild2 = ignoreConflict ? tempBuilder : p => $"[{tempBuilder(p)}]";
                builder = ignoreAttribute ? builder : tempBuild2;
                var properties = typeInfo.GetProperties();
                var result = properties
                    .Where(i => i.GetCustomAttribute(typeof(NotMappedAttribute)) == null)
                    .ToDictionary(i => i.Name, builder);

                return result;
            });
        }

        public static IEnumerable<string> SelectColumnParamList(this Type typeInfo)
        {
            var columns = SelectColumnNameList(typeInfo, true);
            foreach (var item in columns)
            {
                yield return $"@{item[0].ToString().ToLower()}{item.Substring(1)}";
            }
        }

        public static string GetTableName(this Type typeInfo, bool ignoreConflict = false)
        {
            var attr = typeInfo.GetCustomAttribute<TableAttribute>();
            if (ignoreConflict)
                return attr?.Name ?? typeInfo.Name;
            return $"[{attr?.Name ?? typeInfo.Name}]";
        }

        private static readonly Func<PropertyInfo, string> ColumnNameBuilder = p =>
        {
            var attr = p.GetCustomAttribute<ColumnAttribute>();
            return attr?.Name ?? p.Name;
        };

        private static readonly ConcurrentDictionary<string, IEnumerable<string>> DataFieldCache =
            new ConcurrentDictionary<string, IEnumerable<string>>();

        private static readonly ConcurrentDictionary<string, Dictionary<string, string>> DataFieldDicCache =
            new ConcurrentDictionary<string, Dictionary<string, string>>();

        public static Task AddAsync<T>(this DbConnection connection, T item)
        {
            var columns = typeof(T).SelectColumnNameList().Join(",");
            var paramList = typeof(T).SelectColumnParamList().Join(",");
            var tableName = typeof(T).GetTableName();

            var query = $"INSERT INTO {tableName} ({columns}) VALUES ({paramList})";
            return connection.ExecuteAsync(query, item);
        }

        /// <summary>
        /// 强类型消息发布
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subscriber"></param>
        /// <param name="item"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static Task PublishAsync<T>(this ISubscriber subscriber, T item, CommandFlags flags = CommandFlags.None)
        {
            var identifier = new UniqueTypeIdentifier(item.GetType());
            var json = JsonConvert.SerializeObject(item);
            return subscriber.PublishAsync(identifier.ToString(), json, flags);
        }

        /// <summary>
        /// 强类型消息订阅
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="typeInfo"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static Task SubscribeAsync(this ISubscriber subscriber, Type typeInfo, Func<object,RedisChannel, RedisValue, Task> handler)
        {
            var identifier = new UniqueTypeIdentifier(typeInfo);
            return subscriber.SubscribeAsync(identifier.ToString(), async (channel, message) =>
            {
                object item;
                try
                {
                    item = JsonConvert.DeserializeObject(message, typeInfo);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"无法将字符串{message}解析为类型{typeInfo}", ex);
                }
                await handler(item, channel, message);
            });
        }

        public static IServiceCollection ConfigureDefault<T>(this IServiceCollection serviceCollection,
            IConfigurationRoot configurationRoot) where T : class
        {
            //return serviceCollection.Configure<T>(configurationRoot.GetSection(typeof(T).Name));
            //注意，由于整个构建的特殊要求，这里不方便使用单例
            return serviceCollection.ConfigureDefault(typeof(T), configurationRoot);
        }

        public static IServiceCollection ConfigureDefault(this IServiceCollection serviceCollection,
            Type optionType, IConfigurationRoot configurationRoot)
        {
            var config = configurationRoot.GetSection(optionType.Name);

            var genericTokenSourceInter = typeof(IOptionsChangeTokenSource<>).MakeGenericType(optionType);
            var genericTokenSource = typeof(ConfigurationChangeTokenSource<>).MakeGenericType(optionType);
            var genericTokenSourceInstance = Activator.CreateInstance(genericTokenSource, config);
            serviceCollection.Add(new ServiceDescriptor(genericTokenSourceInter, p => genericTokenSourceInstance,
                ServiceLifetime.Singleton));

            var genericOptionsInter = typeof(IConfigureOptions<>).MakeGenericType(optionType);
            var genericOptions = typeof(ConfigureFromConfigurationOptions<>).MakeGenericType(optionType);
            var genericOptionsInstance = Activator.CreateInstance(genericOptions, config);
            serviceCollection.Add(new ServiceDescriptor(genericOptionsInter, p => genericOptionsInstance,
                ServiceLifetime.Singleton));

            return serviceCollection;
        }

        /// <summary>
        /// 向数据库一次性批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="dataRows"></param>
        /// <returns></returns>
        public static async Task AddManyAsync<T>(this SqlConnection conn, IEnumerable<T> dataRows)
        {
            var identifier = new UniqueTypeIdentifier(typeof(T));
            var tableName = GetTableName(typeof(T));
            var dataTable = DataTableCache.GetOrAdd(identifier.ToString(), k => LoadDataTable(tableName, conn));
            dataTable.TableName = tableName;

            using (var table = dataTable.Copy())
            {
                FillDataTable(table, dataRows);
                table.TableName = tableName;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                await WriteToServerAsync(table, conn);
            }
        }

        /// <summary>
        /// 更新数据项到数据表
        /// </summary>
        /// <param name="table"></param>
        /// <param name="dataCollection"></param>
        private static void FillDataTable<T>(DataTable table, IEnumerable<T> dataCollection)
        {
            foreach (var data in dataCollection)
            {
                UpdateDataRow(table, data);
            }
        }

        /// <summary>
        /// 更新数据项到数据行
        /// </summary>
        /// <param name="table"></param>
        /// <param name="data"></param>
        /// <param name="additionalInfos"></param>
        private static void UpdateDataRow<T>(DataTable table, T data, Dictionary<string, object> additionalInfos = null)
        {
            var dataRow = table.NewRow();
            table.Rows.Add(dataRow);

            var fields = SelectColumnNameDic(typeof(T), ignoreConflict: true);
            foreach (var field in fields)
                dataRow[field.Value] = PropertyFieldLoader.Instance.Load(data, field.Key) ?? DBNull.Value;

            if (additionalInfos == null) return;
            foreach (var key in additionalInfos.Keys)
                dataRow[key] = additionalInfos[key];
        }

        private static async Task WriteToServerAsync(DataTable dataTable, SqlConnection connection)
        {
            var bulkCopy = new SqlBulkCopy(connection.ConnectionString, SqlBulkCopyOptions.Default);
            bulkCopy.BatchSize = dataTable.Rows.Count;
            bulkCopy.DestinationTableName = dataTable.TableName;
            await bulkCopy.WriteToServerAsync(dataTable);
        }

        private static readonly ConcurrentDictionary<string, DataTable> DataTableCache =
            new ConcurrentDictionary<string, DataTable>();

        private static DataTable LoadDataTable(string tableName, SqlConnection conn)
        {
            var query = $"SELECT TOP 1 * FROM {tableName} WHERE 1 != 1";
            using (var adapter = new SqlDataAdapter(query, conn))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var set = new DataSet();
                adapter.Fill(set);
                return set.Tables[0];
            }
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="countSql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="param"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static async Task<IPaged<T>> SelectPagedAsync<T>(this SqlConnection conn,string selectSql,string countSql ,PageInfo pageInfo,object param = null, string orderBy = "")
        {
            try
            {
                var from = pageInfo.From();
                var to = pageInfo.To();
                var countTask =await conn.QueryFirstOrDefaultAsync<int>(countSql,param);
                var queryTask =await conn.QueryAsync<T>(selectSql, param);

                //await Task.WhenAll(countTask, queryTask);
                var list = queryTask.ToList();
                if (list.IsNullOrEmpty())
                {
                    return new EmptyPagedList<T>(countTask);
                }
                return new PagedList<T>(list, countTask, from, to);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }
    }
}