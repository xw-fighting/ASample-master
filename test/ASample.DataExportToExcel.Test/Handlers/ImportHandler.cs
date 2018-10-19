using ASample.DataExportToExcel.Test.Models;
using ASample.Web.Common.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ASample.DataExportToExcel.Test.Handlers
{
    public class ImportHandler
    {
        /// <summary>
        /// 从json文件中解析数据
        /// </summary>
        /// <returns></returns>
        public static List<OnceTransactGuideModel> ResolveDataFromJsonFile(string folderPath)
        {
            //获取文件夹中所有的文件
            var filenames = Directory.EnumerateFiles(folderPath).ToList();
            var filePaths = filenames.Where(e => ".json".Equals(Path.GetExtension(e)))
                .Select(e => Path.Combine(folderPath, e)).ToList();
            var models = LoadModels(filePaths.FirstOrDefault());
            return models;
        }

        private static List<OnceTransactGuideModel> LoadModels(string path)
        {
            var models = new ConcurrentBag<OnceTransactGuideModel>();
            try
            {
                var text = File.ReadAllText(path);
                //var item = Json.Default.Deserialize<TransactGuideModel>(text);
                var items = JsonConvert.DeserializeObject<List<OnceTransactGuideModel>>(text);
                var filename = Path.GetFileName(path);
                var ext = Path.GetExtension(path);
                foreach (var item in items)
                {
                    item.Key = filename.Replace(ext, string.Empty);
                    models.Add(item);
                }

                Console.WriteLine(path + "==> Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return models.ToList();
        }

        /// <summary>
        /// 将数据导出到excel中
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static string DataExportToExcel(string folderPath)
        {
            var dataLists = ResolveDataFromJsonFile(folderPath);
            // 2.设置单元格抬头
            // key：实体对象属性名称，可通过反射获取值
            // value：Excel列的名称
            Dictionary<string, string> cellheader = new Dictionary<string, string> {
                    { "Scope", "适用范围" },
                    { "Category", "事项类型" },
                    { "Source", "权力来源" },
                    { "AcceptOrg", "受理机构" },
                    { "DecisionOrg", "决定机构" },

                    { "Tel", "联系电话" },
                    { "DutyOffice", "责任处_科_室" },
                    { "ApplyType", "申请方式" },
                    { "InspectType", "事项审查类型" },
                    { "SuperviseTel", "监督投诉电话" },

                    { "ApprovalResult", "审批结果" },
                    { "CompletedTime", "办结时限" },
                    { "Delivery", "结果送达" },
                    { "TimePlace", "办公地址_时间" },
                    { "SpotTimes", "办事者到办事现场次数" },

                     { "Title", "事项标题" },
                    { "ComServiceDepartment", "服务单位" },
                    { "ComResponsibleDepartment", "主管部门" },
                    { "ComServiceTarget", "服务对象" },
                    { "ComChildCategory", "事项子类型" },

                     { "ComServiceContent", "服务内容" },
                    { "ComTransactTime", "办理时间" },
                    { "ComLimitTime", "办理时限" },
                    { "ComDeadLine", "法定期限" },
                    { "ComPromiseDeadLine", "承诺期限" },

                };
            var url = ExcelUtility.ConsoleExportExcel(cellheader, dataLists, "浙江省社保局办事指南","F:/");
            return url;
        }

        public static void ExcelExportToData()
        {
            // 2.设置单元格抬头
            // key：实体对象属性名称，可通过反射获取值
            // value：Excel列的名称
            var cellheader = new Dictionary<string, string> {
                    { "Scope", "适用范围" },
                    { "Category", "事项类型" },
                    { "Source", "权力来源" },
                    { "AcceptOrg", "受理机构" },
                    { "DecisionOrg", "决定机构" },

                    { "Tel", "联系电话" },
                    { "DutyOffice", "责任处_科_室" },
                    { "ApplyType", "申请方式" },
                    { "InspectType", "事项审查类型" },
                    { "SuperviseTel", "监督投诉电话" },

                    { "ApprovalResult", "审批结果" },
                    { "CompletedTime", "办结时限" },
                    { "Delivery", "结果送达" },
                    { "TimePlace", "办公地址_时间" },
                    { "SpotTimes", "办事者到办事现场次数" },

                     { "Title", "事项标题" },
                    { "ComServiceDepartment", "服务单位" },
                    { "ComResponsibleDepartment", "主管部门" },
                    { "ComServiceTarget", "服务对象" },
                    { "ComChildCategory", "事项子类型" },

                     { "ComServiceContent", "服务内容" },
                    { "ComTransactTime", "办理时间" },
                    { "ComLimitTime", "办理时限" },
                    { "ComDeadLine", "法定期限" },
                    { "ComPromiseDeadLine", "承诺期限" },

                };
            var errorMsg = new StringBuilder(); // 错误信息
            var onceTransactGuideModelList = ExcelUtility.ExcelToList<OnceTransactGuideModel>(cellheader, "F:\\浙江省社保局办事指南-20180425155951325.xls", out errorMsg).Skip(1).ToList();
            var sb = new StringBuilder();
            foreach(var item in onceTransactGuideModelList)
            {
                var jsonStr = JsonConvert.SerializeObject(item);
                sb.Append(jsonStr);
            }
            File.WriteAllText("D:/1.txt",sb.ToString());
        }
    }
}
