using ASample.Web.Common.Excel;
using ASample.Web.Common.Excel.Models;
using ASample.Web.Common.Excel.Values;
using ASample.WebSite.Models;
using ASample.WebSite.Models.DataExport;
using ASample.WebSite.Models.UserEntity;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
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
                    { "TranscriptsEn", "成绩" },
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
        [System.Web.Mvc.HttpPost]
        public void ExportData3()
        {
            try
            {
                var workbook = new XSSFWorkbook();
                var sheet = workbook.CreateSheet("Commission");
                var row = sheet.CreateRow(0);

                var cellStyleBorder = workbook.CreateCellStyle();
                cellStyleBorder.BorderBottom = BorderStyle.Thin;
                cellStyleBorder.BorderLeft = BorderStyle.Thin;
                cellStyleBorder.BorderRight = BorderStyle.Thin;
                cellStyleBorder.BorderTop = BorderStyle.Thin;
                cellStyleBorder.Alignment = HorizontalAlignment.Center;
                cellStyleBorder.VerticalAlignment = VerticalAlignment.Center;

                var cellStyleBorderAndColorGreen = workbook.CreateCellStyle();
                cellStyleBorderAndColorGreen.CloneStyleFrom(cellStyleBorder);
                cellStyleBorderAndColorGreen.FillPattern = FillPattern.SolidForeground;
                ((XSSFCellStyle)cellStyleBorderAndColorGreen).SetFillForegroundColor(new XSSFColor(new byte[] { 198, 239, 206 }));

                var cellStyleBorderAndColorYellow = workbook.CreateCellStyle();
                cellStyleBorderAndColorYellow.CloneStyleFrom(cellStyleBorder);
                cellStyleBorderAndColorYellow.FillPattern = FillPattern.SolidForeground;
                ((XSSFCellStyle)cellStyleBorderAndColorYellow).SetFillForegroundColor(new XSSFColor(new byte[] { 255, 235, 156 }));

                row.CreateCell(0);
                row.CreateCell(1);
                row.CreateCell(2);
                row.CreateCell(3);

                var r2 = sheet.CreateRow(1);
                r2.CreateCell(0, CellType.String).SetCellValue("Name");
                r2.Cells[0].CellStyle = cellStyleBorderAndColorGreen;
                r2.CreateCell(1, CellType.String).SetCellValue("Address");
                r2.Cells[1].CellStyle = cellStyleBorderAndColorGreen;
                r2.CreateCell(2, CellType.String).SetCellValue("city");
                r2.Cells[2].CellStyle = cellStyleBorderAndColorYellow;
                r2.CreateCell(3, CellType.String).SetCellValue("state");
                r2.Cells[3].CellStyle = cellStyleBorderAndColorYellow;
                var cra = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 1);
                var cra1 = new NPOI.SS.Util.CellRangeAddress(0, 0, 2, 3);
                sheet.AddMergedRegion(cra);
                sheet.AddMergedRegion(cra1);

                ICell cell = sheet.GetRow(0).GetCell(0);
                cell.SetCellType(CellType.String);
                cell.SetCellValue("Supplier Provided Data");
                cell.CellStyle = cellStyleBorderAndColorGreen;
                sheet.GetRow(0).GetCell(1).CellStyle = cellStyleBorderAndColorGreen;

                ICell cell1 = sheet.GetRow(0).GetCell(2);
                cell1.SetCellType(CellType.String);
                cell1.SetCellValue("Deal Provided Data");
                cell1.CellStyle = cellStyleBorderAndColorYellow;
                sheet.GetRow(0).GetCell(3).CellStyle = cellStyleBorderAndColorYellow;

                using (FileStream fs = new FileStream(@"c:\temp\excel\test.xlsx", FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExportData4()
        {
            try
            {
                var sheets = new List<string>
                {
                    "1月","2月","3月","4月","5月","6月","7月","8月","9月","10月","11月","12月"
                };

                var airlineNames = new List<string>
                {
                    "飞院本场","飞院转场","容商跳伞","四川驼峰"
                };

                var dataList = new List<ExportDataDto>
                {
                    new ExportDataDto
                    {
                        Month = "1月",
                        AirlineName = "飞院本场",
                        TaskDate = "2020-01-01",
                        TotalHours = "10.7",
                        TotalSorties = "5"
                    },
                    new ExportDataDto
                    {
                        Month = "1月",
                        AirlineName = "飞院转场",
                        TaskDate = "2020-01-05",
                        TotalHours = "10.7",
                        TotalSorties = "5"
                    },
                    new ExportDataDto
                    {
                        Month = "1月",
                        AirlineName = "容商跳伞",
                        TaskDate = "2020-01-05",
                        TotalHours = "6.7",
                        TotalSorties = "3"
                    }
                };
                var workbook = new XSSFWorkbook();

                foreach (var sheetName in sheets)
                {
                    var sheet = workbook.CreateSheet(sheetName);
                    var row = sheet.CreateRow(0);
                    var craFirst = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, airlineNames.Count*2);
                    sheet.AddMergedRegion(craFirst);

                    var cellStyleBorder = workbook.CreateCellStyle();
                    cellStyleBorder.BorderBottom = BorderStyle.Thin;
                    cellStyleBorder.BorderLeft = BorderStyle.Thin;
                    cellStyleBorder.BorderRight = BorderStyle.Thin;
                    cellStyleBorder.BorderTop = BorderStyle.Thin;
                    cellStyleBorder.Alignment = HorizontalAlignment.Center;
                    cellStyleBorder.VerticalAlignment = VerticalAlignment.Center;

                    var cellStyleBorderAndColorGreen = workbook.CreateCellStyle();
                    cellStyleBorderAndColorGreen.CloneStyleFrom(cellStyleBorder);
                    cellStyleBorderAndColorGreen.FillPattern = FillPattern.SolidForeground;
                    ((XSSFCellStyle)cellStyleBorderAndColorGreen).SetFillForegroundColor(new XSSFColor(new byte[] { 198, 239, 206 }));

                    var cellStyleBorderAndColorYellow = workbook.CreateCellStyle();
                    cellStyleBorderAndColorYellow.CloneStyleFrom(cellStyleBorder);
                    cellStyleBorderAndColorYellow.FillPattern = FillPattern.SolidForeground;
                    ((XSSFCellStyle)cellStyleBorderAndColorYellow).SetFillForegroundColor(new XSSFColor(new byte[] { 255, 235, 156 }));

                    //创建第一行的列
                    for (int i = 0; i < airlineNames.Count * 2 + 1; i++)
                    {
                        row.CreateCell(i);
                    }
                    row.CreateCell(0, CellType.String).SetCellValue($"自贡凤鸣通用机场飞行日统计（2020年{sheetName}）");

                    var r2 = sheet.CreateRow(1);
                    r2.CreateCell(0, CellType.String).SetCellValue("单位");

                    var r3 = sheet.CreateRow(2);
                    r3.CreateCell(0, CellType.String).SetCellValue("日期/架次、小时");

                    //创建第二行的列
                    for (int i = 1; i < airlineNames.Count * 2+1; i++)
                    {
                        r2.CreateCell(i, CellType.String);
                        r3.CreateCell(i, CellType.String);
                        var cellVale = string.Empty;
                        if (i % 2 == 0)
                        {
                            r3.Cells[i].CellStyle = cellStyleBorderAndColorGreen;
                            cellVale = "架次(个)";
                        }
                        else
                        {
                            r3.Cells[i].CellStyle = cellStyleBorderAndColorYellow;
                            cellVale = "时间(小时)";
                        }
                        r3.CreateCell(i, CellType.String).SetCellValue(cellVale);
                    }

                    //创建第三行的列
                    for (int i = 1; i < airlineNames.Count+1; i++)
                    {
                        var j = 1;
                        ICellStyle cellStyle;
                        if (i % 2 == 0)
                        {
                            j = i + 1;
                            cellStyle = cellStyleBorderAndColorGreen;
                        }
                        else
                        {
                            j = i;
                            cellStyle = cellStyleBorderAndColorYellow;
                        }
                        var cra = new NPOI.SS.Util.CellRangeAddress(1, 1, i*2-1, i * 2);
                        sheet.AddMergedRegion(cra);
                        ICell cell = sheet.GetRow(1).GetCell(i*2-1);
                        cell.SetCellType(CellType.String);
                        cell.SetCellValue(airlineNames[i-1]);
                        cell.CellStyle = cellStyle;
                        sheet.GetRow(1).GetCell(i*2).CellStyle = cellStyle;
                    }

                    //创建数据列
                    var date = DateTime.Now;
                    var month = Convert.ToInt32(sheetName.Replace("月", ""));
                    var days = DateTime.DaysInMonth(date.Year, month);
                    var daysDatas = dataList.Where(c => c.Month == sheetName);
                    var monthStr = string.Empty;
                    if (month < 10)
                        monthStr = "0" + month;
                    else
                        monthStr = month.ToString();

                    for (int i = 1; i < days; i++)
                    {
                        var dataRow = sheet.CreateRow(i + 2);
                        
                        var dayStr = string.Empty;
                        if(i < 10)
                            dayStr = "0" + i;
                        else
                            dayStr = i.ToString();

                        var dateStr = date.Year +"-"+ monthStr + "-"+ dayStr;
                        dataRow.CreateCell(0, CellType.String).SetCellValue(dateStr);
                        var dayDatas = daysDatas.Where(c => c.TaskDate == dateStr);

                        for (int j = 0; j < airlineNames.Count; j++)
                        {
                            dataRow.CreateCell(j * 2 + 1, CellType.String);
                            dataRow.CreateCell(j * 2 + 2, CellType.String);
                            if (dayDatas == null || dayDatas.Count() <= 0)
                                continue;
                            foreach (var dayData in dayDatas)
                            {
                                if (dayData == null)
                                    continue;
                                if (dayData.AirlineName.Contains(airlineNames[j]))
                                {
                                    dataRow.CreateCell(j * 2 + 1, CellType.String).SetCellValue(dayData.TotalHours);
                                    dataRow.CreateCell(j * 2 + 2, CellType.String).SetCellValue(dayData.TotalSorties);
                                }
                            }
                        }
                    }
                    using (FileStream fs = new FileStream(@"c:\temp\excel\test6.xlsx", FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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