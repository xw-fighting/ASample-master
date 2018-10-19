using ASample.Web.Common.Excel;
using ASample.Web.Common.Excel.Models;
using ASample.Web.Common.Excel.Values;
using ASample.WebSite.Models;
using ASample.WebSite.Models.DataExport;
using ASample.WebSite.Models.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASample.WebSite.Controllers
{
    /// <summary>
    /// 将数据导出到Excel
    /// </summary>
    public class DataExportController : Controller
    {
        // GET: DataExport
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 导出数据到Excel中
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> ExportDataToExcel()
        {
            //查询出的数据
            var list = new List<TestDataViewModel>();

            list.Add(new TestDataViewModel
            {
                PlanSubject = "test1",
                MobileNo = "13577778888",
                TelcoOperatorText = "移动"
            });
            list.Add(new TestDataViewModel
            {
                PlanSubject = "test2",
                MobileNo = "13577779999",
                TelcoOperatorText = "移动"
            });
            list.Add(new TestDataViewModel
            {
                PlanSubject = "test3",
                MobileNo = "8077778888",
                TelcoOperatorText = "电信"
            });
            //var model = await DataTrafficGiftRecordQueryEntry.SelectAsync();

            //已经领取了流量
            //var hasGetList = model.Where(m => !string.IsNullOrWhiteSpace(m.MobileNo) && m.MobileNo.Length == 11);
            //var result = hasGetList.MapList<DataTrafficGiftRecordViewModel>().ToList();

            var setting = new ExcelExportOption<TestDataViewModel>
            {
                FileName = "客户领取流量记录",
                DefaultRowHeight = 270
            };
            if (list.Count() >= 65535)
                setting.Description = "结果数据总数:" + list.Count() + "但Excel最多支持65535条数据, 请分开条件导出";
            setting.Source = list.Take(65534);
            var colums = new List<ExcelColum<TestDataViewModel>>
            {
                new ExcelColum<TestDataViewModel>
                {
                    Name = "赠送计划标题",
                    Width = 7000,
                    ColumType = ExcelColumType.String,
                    ResultFunc = x => x.PlanSubject
                },
                new ExcelColum<TestDataViewModel>
                {
                    Name = "手机号码",
                    Width = 7000,
                    ColumType = ExcelColumType.String,
                    ResultFunc = x => x.MobileNo
                },

                 new ExcelColum<TestDataViewModel>
                {
                    Name = "手机运营商",
                    Width = 7000,
                    ColumType = ExcelColumType.String,
                    ResultFunc = x => x.TelcoOperatorText
                },
            };

            setting.Colums = colums;

            //发送文件路径到客户端
            var steam = ExcelUtility.ExportToStream(setting);
            var filename = HttpUtility.UrlEncode(setting.FileName + DateTime.Now.ToString("_yyyyMMdd-HHmmss") + ".xls", System.Text.Encoding.UTF8);
            return File(steam, "application/vnd.ms-excel", filename);
            //Response.AddHeader("Content-Disposition", $"attachment;filename={filename}");
            //Response.Clear();
            //Response.BinaryWrite(steam.GetBuffer());
            //Response.End();
        }

        /// <summary>
        /// 导出数据到Excel中
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task ExportDataToExcel2()
        {
            //查询出的数据
            // 1.获取数据集合
            var list = new List<UserViewModel>() {
                    new UserViewModel{Name="刘一",Age=22,Gender="Male",TranscriptsEn=new TranscriptsEntity{ChineseScores=80,MathScores=90}},
                    new UserViewModel{Name="陈二",Age=23,Gender="Male",TranscriptsEn=new TranscriptsEntity{ChineseScores=81,MathScores=91} },
                    new UserViewModel{Name="张三",Age=24,Gender="Male",TranscriptsEn=new TranscriptsEntity{ChineseScores=82,MathScores=92} },
                    new UserViewModel{Name="李四",Age=25,Gender="Male",TranscriptsEn=new TranscriptsEntity{ChineseScores=83,MathScores=93} },
                    new UserViewModel{Name="王五",Age=26,Gender="Male",TranscriptsEn=new TranscriptsEntity{ChineseScores=84,MathScores=94} },
                };

            var setting = new ExcelExportOption<UserViewModel>
            {
                FileName = "客户领取流量记录",
                DefaultRowHeight = 270
            };
            if (list.Count() >= 65535)
                setting.Description = "结果数据总数:" + list.Count() + "但Excel最多支持65535条数据, 请分开条件导出";
            setting.Source = list.Take(65534);


            // 2.设置单元格抬头
            // key：实体对象属性名称，可通过反射获取值
            // value：Excel列的名称
            Dictionary<string, string> cellheader = new Dictionary<string, string> {
                    { "Name", "姓名" },
                    { "Age", "年龄" },
                    { "GenderName", "性别" },
                    { "TranscriptsEn.ChineseScores", "语文成绩" },
                    { "TranscriptsEn.MathScores", "数学成绩" },
                };

            //发送文件路径到客户端
            var urlPath = ExcelUtility.ExportExcel(cellheader, list, "学生成绩");
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            HttpContext.Response.ContentType = "text/plain";
            HttpContext.Response.Write(js.Serialize(urlPath)); // 返回Json格式的内容
            //var column = new List<ExcelColum<UserViewModel>>
            //{
            //    new ExcelColum<UserViewModel>
            //    {
            //        Name = "姓名",
            //        Width = 7000,
            //        ColumType = ExcelColumType.String,
            //        ResultFunc = x => x.Name
            //    },
            //    new ExcelColum<UserViewModel>
            //    {
            //        Name = "年龄",
            //        Width = 7000,
            //        ColumType = ExcelColumType.String,
            //        ResultFunc = x => x.Age
            //    },
            //    new ExcelColum<UserViewModel>
            //    {
            //        Name = "性别",
            //        Width = 7000,
            //        ColumType = ExcelColumType.String,
            //        ResultFunc = x => x.GenderName
            //    },
            //    new ExcelColum<UserViewModel>
            //    {
            //        Name = "语文成绩",
            //        Width = 7000,
            //        ColumType = ExcelColumType.String,
            //        ResultFunc = x => x.TranscriptsEn.ChineseScores
            //    },
            //     new ExcelColum<UserViewModel>
            //    {
            //        Name = "数学成绩",
            //        Width = 7000,
            //        ColumType = ExcelColumType.String,
            //        ResultFunc = x => x.TranscriptsEn.MathScores
            //    },
            //};
            //var option = new ExcelExportOption<UserViewModel>
            //{
            //    Title = "学生成绩",
            //    Source = list,
            //    Colums = column
            //};
            //var filename = $"民办初中-{DateTime.Now:yyyyMMddHHmmssfff}.xls";
            //var file = ExcelUtility.ExportToStream<UserViewModel>(option);
            //return File(file, "application/vnd.ms-excel", filename);
        }


        /// <summary>
        /// 将Excel数据导入到List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task ExcelToDataBase()
        {
            var errorMsg = new StringBuilder(); // 错误信息
            try
            {
                // 1.1存放Excel文件到本地服务器
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase filePost = files["filed"];
                string filePath = ExcelUtility.SaveExcelFile(filePost); // 保存文件并获取文件路径

                // 单元格抬头
                // key：实体对象属性名称，可通过反射获取值
                // value：属性对应的中文注解
                Dictionary<string, string> cellheader = new Dictionary<string, string> {
                    //{ "PlanSubject", "赠送计划标题" },
                    //{ "MobileNo", "手机号码" },
                    //{ "TelcoOperatorText", "手机运营商" },
                    { "Name", "姓名" },
                    { "Age", "年龄" },
                    { "Gender", "性别" },
                    { "Chiness", "语文成绩" },
                    { "Math", "数学成绩" },
                };

                // 1.2解析文件，存放到一个List集合里
                var enlist = ExcelUtility.ExcelToList<StudentViewModel>(cellheader, filePath, out errorMsg);


                #region 2.对List集合进行有效性校验

                #region 2.1检测必填项是否必填

                for (int i = 0; i < enlist.Count; i++)
                {
                    var en = enlist[i];
                    string errorMsgStr = "第" + (i + 1) + "行数据检测异常：";
                    bool isHaveNoInputValue = false; // 是否含有未输入项
                    if (string.IsNullOrEmpty(en.Name))
                    {
                        errorMsgStr += "姓名列不能为空；";
                        isHaveNoInputValue = true;
                    }
                    if (isHaveNoInputValue) // 若必填项有值未填
                    {
                        //en.IsExcelVaildateOK = false;
                        errorMsg.AppendLine(errorMsgStr);
                    }
                }

                #endregion

                #region 2.2检测Excel中是否有重复对象

                for (int i = 0; i < enlist.Count; i++)
                {
                    var enA = enlist[i];
                    //if (enA.IsExcelVaildateOK == false) // 上面验证不通过，不进行此步验证
                    //{
                    //    continue;
                    //}

                    for (int j = i + 1; j < enlist.Count; j++)
                    {
                        var enB = enlist[j];
                        // 判断必填列是否全部重复
                        if (enA.Name == enB.Name)
                        {
                            //enA.IsExcelVaildateOK = false;
                            //enB.IsExcelVaildateOK = false;
                            errorMsg.AppendLine("第" + (i + 1) + "行与第" + (j + 1) + "行的必填列重复了");
                        }
                    }
                }

                #endregion

                // TODO：其他检测

                #endregion

                // 3.TODO：对List集合进行持久化存储操作。如：存储到数据库

                // 4.返回操作结果
                bool isSuccess = false;
                if (errorMsg.Length == 0)
                {
                    isSuccess = true; // 若错误信息成都为空，表示无错误信息
                }
                var rs = new { success = isSuccess, msg = errorMsg.ToString(), data = enlist };
                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                Response.ContentType = "text/plain";
                Response.Write(js.Serialize(rs)); // 返回Json格式的内容
            }
            catch (Exception)
            {
                throw;
            }


        }

        /// <summary>
        /// 将Excel数据导入到List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task ExcelToDataBase2()
        {
            var errorMsg = new StringBuilder(); // 错误信息
            try
            {
                // 1.1存放Excel文件到本地服务器
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase filePost = files["filed"];
                string filePath = ExcelUtility.SaveExcelFile(filePost); // 保存文件并获取文件路径

                // 单元格抬头
                // key：实体对象属性名称，可通过反射获取值
                // value：属性对应的中文注解
                Dictionary<string, string> cellheader = new Dictionary<string, string> {
                    //{ "PlanSubject", "赠送计划标题" },
                    //{ "MobileNo", "手机号码" },
                    //{ "TelcoOperatorText", "手机运营商" },
                    //{ "Name", "姓名" },
                    //{ "Age", "年龄" },
                    //{ "Gender", "性别" },
                    //{ "Chiness", "语文成绩" },
                    //{ "Math", "数学成绩" },
                    { "KidName", "姓名" },
                    { "Gender", "性别" },
                    {"KidNumber", "公民身份号码" },
                    {"Nation", "民族"},
                    {"Birthday", "出生日期"},
                    {"PoliceStation", "派出所"},
                    {"TownOrStreet", "乡镇（街道）"},
                    {"Committee", "居委会"},
                    {"OtherAddress", "其他详址"},
                    {"Road", "街路巷"},
                    {"HouseNumber", "门楼牌号"},
                    {"HouseAddress", "门楼详址"},
                    {"HouseOwner", "户主姓名"},
                    {"RelationShip", "与户主关系"},
                    {"Phone", "电话号码"},
                    {"MoveCityDate", "何时迁来"},
                    {"MoveCityReason", "何因来本市"},
                    {"MoveAddressDate", "何时来本址"},
                    {"MoveAddressReason", "何因来本址"},
                    {"GuardianOneName", "监护人一姓名"},
                    {" GuardianOneCardNumber", "监护人一身份号码"},
                    {"GuardianOneRelation", "监护人一关系"},
                    {"GuardianTwoName", "监护人二姓名"},
                    {"GuardianTwoCardNumber", "监护人二身份号码"},
                    {" GuardianTwoRelation", "监护人二关系"},
                    
                };

                // 1.2解析文件，存放到一个List集合里
                var enlist = ExcelUtility.ExcelToList<LdGaKidInfo>(cellheader, filePath, out errorMsg);


                #region 2.对List集合进行有效性校验

                #region 2.1检测必填项是否必填

                for (int i = 0; i < enlist.Count; i++)
                {
                    var en = enlist[i];
                    string errorMsgStr = "第" + (i + 1) + "行数据检测异常：";
                    bool isHaveNoInputValue = false; // 是否含有未输入项
                    if (string.IsNullOrEmpty(en.KidName))
                    {
                        errorMsgStr += "姓名列不能为空；";
                        isHaveNoInputValue = true;
                    }
                    if (isHaveNoInputValue) // 若必填项有值未填
                    {
                        //en.IsExcelVaildateOK = false;
                        errorMsg.AppendLine(errorMsgStr);
                    }
                }

                #endregion

                #region 2.2检测Excel中是否有重复对象

                for (int i = 0; i < enlist.Count; i++)
                {
                    var enA = enlist[i];
                    //if (enA.IsExcelVaildateOK == false) // 上面验证不通过，不进行此步验证
                    //{
                    //    continue;
                    //}

                    for (int j = i + 1; j < enlist.Count; j++)
                    {
                        var enB = enlist[j];
                        // 判断必填列是否全部重复
                        if (enA.KidName == enB.KidName)
                        {
                            //enA.IsExcelVaildateOK = false;
                            //enB.IsExcelVaildateOK = false;
                            errorMsg.AppendLine("第" + (i + 1) + "行与第" + (j + 1) + "行的必填列重复了");
                        }
                    }
                }

                #endregion

                // TODO：其他检测

                #endregion

                // 3.TODO：对List集合进行持久化存储操作。如：存储到数据库

                // 4.返回操作结果
                bool isSuccess = false;
                if (errorMsg.Length == 0)
                {
                    isSuccess = true; // 若错误信息成都为空，表示无错误信息
                }
                var rs = new { success = isSuccess, msg = errorMsg.ToString(), data = enlist };
                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                Response.ContentType = "text/plain";
                Response.Write(js.Serialize(rs)); // 返回Json格式的内容
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}