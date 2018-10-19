using ASample.ExtensionMethod.Cache;
using System;
using System.Linq.Expressions;

namespace ASample.ExtensionMethod
{
    public class PropertyFieldLoader : CachedExpressionCompiler<string, Func<object, object>>
    {
        protected PropertyFieldLoader() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tObj"></param>
        /// <param name="type">将对象转换到指定的基类进行取值</param>
        /// <param name="propertyPath"></param>
        /// <returns></returns>
        public object Load(object tObj, Type type, string propertyPath)
        {
            return Compile(type, propertyPath)(tObj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tObj"></param>
        /// <param name="type">将对象转换到指定的基类进行取值</param>
        /// <param name="propertyPath"></param>
        /// <returns></returns>
        public T Load<T>(object tObj, Type type, string propertyPath)
            where T : class
        {
            return Load(tObj, type, propertyPath) as T;
        }

        public T Load<T>(object tObj, string propertyPath) where T : class
        {
            return Load<T>(tObj, tObj.GetType(), propertyPath);
        }

        public object Load(object tObj, string propertyPath)
        {
            return Load(tObj, tObj.GetType(), propertyPath);
        }

        public Func<object, object> Compile(Type type, string propertyPath)
        {
            var key = "Get_" + type.FullName + "_";
            var func = ConcurrentDic.GetOrAdd(key + "." + propertyPath, (string k) =>
            {
                ParameterExpression paramInstance = Expression.Parameter(typeof(object), "obj");
                Expression expression = Expression.Convert(paramInstance, type);
                foreach (var pro in propertyPath.Split('.'))
                    expression = Expression.PropertyOrField(expression, pro);
                expression = Expression.Convert(expression, typeof(object));
                var exp = Expression.Lambda<Func<object, object>>(expression, paramInstance);
                return exp.Compile();
            });
            return func;
        }

        public static PropertyFieldLoader Instance = new PropertyFieldLoader();
    }
}
