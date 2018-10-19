using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ExtensionMethod
{
    public static class LinqExtension
    {
        /// <summary>
        /// 基于参考序列的排序。给定一个已经排序好的集合，依次使用该集合中item作为测试元素，参考predicate进行匹配。
        /// 一旦匹配成功，则使用测试元素的排序值作为目标元素的排序值。
        /// 例如，对于abcdefg这个字符集合，使用gbc作为参考，同时使用char的equals作为测试表达式，那么结果就是gbcadef
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TP"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortedSource"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TS> OrderDepend<TS, TP>(this IEnumerable<TS> source,
            IEnumerable<TP> sortedSource, Func<TS, TP, bool> predicate)
        {
            var allItems = source.ToList();
            var temp = new List<TS>();
            foreach (var p in sortedSource)
            {
                var item = allItems.Where(i => predicate(i, p)).ToList();
                if (item.Count > 0)
                    temp.AddRange(item);
                allItems = allItems.Except(item).ToList();
            }
            if (allItems.Count > 0)
                temp.AddRange(allItems);
            return temp;
        }
    }
}
