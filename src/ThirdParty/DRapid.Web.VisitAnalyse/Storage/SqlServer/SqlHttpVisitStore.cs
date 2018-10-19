using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Core;
using Microsoft.Extensions.Options;
using DRapid.Utility.Linq.Enumerable;
using System.Text;
using Dapper;

namespace DRapid.Web.VisitAnalyse.Storage.SqlServer
{
    public class SqlHttpVisitStore : IHttpVisitStore
    {
        public SqlHttpVisitStore(IOptions<SqlHttpVisitStoreOptions> options)
        {
            Options = options.Value;
        }

        public SqlHttpVisitStoreOptions Options { get; }

        public Task<HttpVisit> TryGetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(HttpVisit visit)
        {
            using (var conn = new SqlConnection(Options.ConnectionString))
            {
                var table = visit.AutoMapTo<SqlHttpVisitTable>(BindingFlags.Public | BindingFlags.Instance);
                table.AuthenticationType = visit.Identity?.AuthenticationType;
                table.IsAuthenticated = visit.Identity?.IsAuthenticated ?? false;
                table.UserName = visit.Identity?.Name;

                var infoItems = new List<SqlHttpVisitInfoTable>();
                if (!visit.Inforamtion.IsNullOrEmpty())
                {
                    foreach (var item in visit.Inforamtion)
                    {
                        var info = new SqlHttpVisitInfoTable
                        {
                            Id = Guid.NewGuid(),
                            Key = item.Key,
                            Value = item.Value,
                            HttpVisitId = visit.Id,
                            Time = visit.Time
                        };
                        infoItems.Add(info);
                    }
                }

                await conn.AddAsync(table);
                await conn.AddManyAsync(infoItems);
            }
        }

        public async Task AddAsync(IEnumerable<HttpVisit> visit)
        {
            var main = new List<SqlHttpVisitTable>();
            var sub = new List<SqlHttpVisitInfoTable>();

            foreach (var item in visit)
            {
                var table = item.AutoMapTo<SqlHttpVisitTable>(BindingFlags.Public | BindingFlags.Instance);
                table.AuthenticationType = item.Identity?.AuthenticationType;
                table.IsAuthenticated = item.Identity?.IsAuthenticated ?? false;
                table.UserName = item.Identity?.Name;
                main.Add(table);
                foreach (var infoItem in item.Inforamtion)
                {
                    var info = new SqlHttpVisitInfoTable
                    {
                        Id = Guid.NewGuid(),
                        Key = infoItem.Key,
                        Value = infoItem.Value,
                        HttpVisitId = item.Id,
                        Time = item.Time
                    };

                    sub.Add(info);
                }
            }

            using (var conn = new SqlConnection(Options.ConnectionString))
            {
                await conn.AddManyAsync(main);
                await conn.AddManyAsync(sub);
            }
        }

        public Task<IEnumerable<HttpVisit>> SelectAsync(HttpVisitFilter filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页查询系统访问记录
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public async Task<IPaged<SqlHttpVisitTable>> SelectPagedAsync(PageInfo pageInfo, HttpVisitFilter filter = null)
        {
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(filter.Url))
            {
                condition.Append($" And [{nameof(SqlHttpVisitTable.Url)}] like '%'+@Url+'%' ");
                parameters.Add("Url", filter.Url);
            }
            if (filter.BeginTime.HasValue)
            {
                condition.Append($" And [{nameof(SqlHttpVisitTable.Time)}] >= @BeginTime ");
                parameters.Add("BeginTime", filter.BeginTime);
            }
            if (filter.EndTime.HasValue)
            {
                condition.Append($" And [{nameof(SqlHttpVisitTable.Time)}] <= @EndTime ");
                parameters.Add("EndTime", filter.EndTime);
            }
            if (!string.IsNullOrEmpty(filter.Method))
            {
                condition.Append(
                    $" And [{nameof(SqlHttpVisitTable.Method)}] = @Method ");
                parameters.Add("Method", filter.Method);
            }
            if (!string.IsNullOrEmpty(filter.StatusCode))
            {
                condition.Append($" And [{nameof(SqlHttpVisitTable.StatusCode)}] = @StatusCode ");
                parameters.Add("StatusCode", filter.StatusCode);
            }

            if (filter.MinExpires >= 0)
            {
                condition.Append($" And [{nameof(SqlHttpVisitTable.Expires)}] >= @MinExpires ");
                parameters.Add("MinExpires", filter.MinExpires);
            }
            if (filter.MaxExpires > 0)
            {
                condition.Append($" And [{nameof(SqlHttpVisitTable.Expires)}] <= @MaxExpires ");
                parameters.Add("MaxExpires", filter.MaxExpires);
            }
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                condition.Append($" And [{nameof(SqlHttpVisitTable.UserName)}] = @UserName ");
                parameters.Add("UserName", filter.UserName);
            }

            using (var conn = new SqlConnection(Options.ConnectionString))
            {
                //分页查询
                var selectSql = $@"SELECT * FROM (
                                SELECT ROW_NUMBER() OVER(ORDER BY [Time] DESC) AS num,
  	                            * FROM [HttpVisit] WHERE 1=1 {condition.ToString()} )as tem
                                WHERE tem.num BETWEEN (({pageInfo.PageIndex} - 1)*{pageInfo.PageSize} + 1) AND ({pageInfo.PageIndex}*{pageInfo.PageSize})";

                //查询总数
                var countSql = $@"SELECT COUNT(Id) FROM HttpVisit WHERE 1=1 {condition.ToString()}";

                selectSql = $"{selectSql} ORDER BY [{nameof(SqlHttpVisitTable.Time)}] DESC";

                var result = await conn.SelectPagedAsync<SqlHttpVisitTable>(selectSql, countSql, pageInfo, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通过HttpVisit 编号查询系统访问记录详细信息
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public async Task<IPaged<SqlHttpVisitInfoTable>> SelectVisitInfoPagedByIdAsync(Guid visitId,PageInfo pageInfo)
        {
            using (var conn = new SqlConnection(Options.ConnectionString))
            {
                //分页查询
                var selectSql = $@"SELECT * FROM (
                                SELECT ROW_NUMBER() OVER(ORDER BY [Time] DESC) AS num,
  	                            * FROM [HttpVisitInfo] WHERE HttpVisitId = '{visitId}') as tem
                                WHERE  tem.num BETWEEN (({pageInfo.PageIndex} - 1)*{pageInfo.PageSize} + 1) AND ({pageInfo.PageIndex}*{pageInfo.PageSize})";
                //查询总数
                var countSql = $@"SELECT COUNT(Id) FROM HttpVisitInfo WHERE HttpVisitId = '{visitId}'";

                var result = await conn.SelectPagedAsync<SqlHttpVisitInfoTable>(selectSql, countSql, pageInfo);
                return result;
            }

        }
    }
}