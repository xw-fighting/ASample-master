using ASample.ExtensionMethod.Cache;
using System;
using System.Linq.Expressions;

namespace ASample.ExtensionMethod
{
    public class PropertyFieldSetter : CachedExpressionCompiler<string, Action<object, object>>
    {
        protected PropertyFieldSetter() { }

        public void Set(object obj, string propertyPath, object value)
        {
            var tObj = obj.GetType();
            var tValue = value.GetType();
            var act = Compile(tObj, tValue, propertyPath);
            act(obj, value);
        }

        public static PropertyFieldSetter Instance = new PropertyFieldSetter();

        public Action<object, object> Compile(Type typeObj, Type typeValue, string propertyPath)
        {
            var key = "Set_" + typeObj.FullName + "_" + typeValue.FullName;
            var act = ConcurrentDic.GetOrAdd(key + "." + propertyPath, s =>
            {
                ParameterExpression paramInstance = Expression.Parameter(typeof(object), "obj");
                ParameterExpression paramValue = Expression.Parameter(typeof(object), "value");
                Expression expression = Expression.Convert(paramInstance, typeObj);
                foreach (var pro in propertyPath.Split('.'))
                    expression = Expression.PropertyOrField(expression, pro);
                var value = Expression.Convert(paramValue, typeValue);
                expression = Expression.Assign(expression, value);
                var exp = Expression.Lambda<Action<object, object>>(expression, paramInstance, paramValue);
                return exp.Compile();
            });
            return act;
        }
    }
}
