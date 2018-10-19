using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Thirdparty.Tencent.SMS.Test
{
    public static class TestHelper
    {
        public static void DisplayInstance<T>(T instance)
        {
            //if (typeof(T).IsValueType || instance is string)
            //{
            //    Trace.WriteLine(instance);
            //    return;
            //}
            //var dic = ModelStringDicTransfer<T>.Instance.Compile("dispay_test_result")(instance);
            //var str = dic
            //    .SelectOrDefault(i => i.Key + ":" + i.Value)
            //    .Aggregate((c, n) => c + Environment.NewLine + n);
            //Trace.WriteLine(str);
        }
        
    }
}
