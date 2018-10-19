using ASample.Npoi.Config;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Npoi.Core
{
    public static class Extension
    {
        public static T Extract<T>(this ICell cell)
        {
            //return (T) Extract(cell, typeof (T));
            //System.ComponentModel.TypeDescriptor.GetConverter(pinfo.PropertyType).ConvertFrom(b.ToString());
            if (cell.CellType == CellType.Numeric)
            {
                return (T)(object)cell.NumericCellValue;
            }
            if (cell.CellType == CellType.String)
            {
                //return (T)cell.StringCellValue.Convert(typeof(T));
                return default(T);
            }
            return default(T);
        }

        public static object Extract(this ICell cell, Type valueType)
        {
            if (valueType == typeof(string))
            {
                return cell.StringCellValue;
            }
            if (valueType == typeof(DateTime))
            {
                return cell.DateCellValue;
            }
            if (valueType == typeof(double))
            {
                return cell.NumericCellValue;
            }
            if (valueType == typeof(int))
            {
                return (int)cell.NumericCellValue;
            }
            if (valueType == typeof(IRichTextString))
            {
                return cell.RichStringCellValue;
            }
            if (valueType == typeof(bool))
            {
                return cell.BooleanCellValue;
            }
            if (valueType == typeof(Guid))
            {
                return new Guid(cell.StringCellValue);
            }
            throw new NotSupportedException();
        }

        public static TableConfig<T> Map<T>(this TableConfig<T> config,
            Expression<Func<T, object>> member, string cellHeadeName,
            Func<ICell, object> readConversion = null,
            Func<object, object> writeConversion = null)
        {
            var propertyInfo = ReadMemberInfo(member);
            var cellConfig = new CellConfig
            {
                CellHeaderName = cellHeadeName,
                ModelPropertyName = propertyInfo.Name,
                ModelPropertyType = propertyInfo.PropertyType,
                ReadConvertion = readConversion,
                WriteConversion = writeConversion
            };
            config.AddCellConfig(cellConfig);
            return config;
        }

        public static PropertyInfo ReadMemberInfo<T>(Expression<Func<T, object>> expression)
        {
            var body = expression.Body;
            /*这里需要考虑转型表达式*/
            if (body.NodeType == ExpressionType.Convert)
                body = ((UnaryExpression)body).Operand;
            Trace.Assert(body.NodeType == ExpressionType.MemberAccess, "表达式必须是成员访问或者是带转型的成员访问");
            var accessMember = (MemberExpression)body;
            return (PropertyInfo)accessMember.Member;
        }

        public static MemoryStream WriteToMemoryStream(this IWorkbook workbook)
        {
            var mStream = new MemoryStream();
            workbook.Write(mStream);
            mStream.Seek(0, SeekOrigin.Begin);
            return mStream;
        }
    }
}
