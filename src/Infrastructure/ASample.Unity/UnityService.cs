using ASample.Unity.Models;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Unity;
using Unity.Lifetime;
using Unity.Resolution;

namespace ASample.Unity
{
    public class UnityService
    {
        private static IUnityContainer _UnityContainer = new UnityContainer();

        /// <summary>
        /// 获取依赖注入容器(单例模式)
        /// </summary>
        public static IUnityContainer Current
        {
            get
            {
                return _UnityContainer;
            }
        }

        private UnityService() { }

        /// <summary>
        /// 获取指定目录及其子目录的所有DLL文件路径集合
        /// </summary>
        /// <param name="assemblyDirectory"></param>
        /// <returns></returns>
        private static List<string> GetAssemblyFiles()
        {
            string assemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (HttpContext.Current != null)
            {
                assemblyDirectory = Path.Combine(assemblyDirectory, "Bin");
            }
            string[] notFiles = new string[]
            {
                "EntityFramework.dll",
                "ICSharpCode.SharpZipLib.dll",
                "Ionic.Zip.dll",
                "log4net.dll",
                "Microsoft.ApplicationBlocks.Data.dll",
                "Microsoft.Practices.ServiceLocation.dll",
                "Microsoft.Practices.Unity.Configuration.dll",
                "Microsoft.Practices.Unity.dll",
                "Newtonsoft.Json.dll"
            };
            //获取DLL文件
            List<string> assemblyFiles = Directory.GetFiles(assemblyDirectory, "*.dll").Select(path => Path.GetFileName(path)).ToList();
            //EXE可执行文件
            assemblyFiles.AddRange(Directory.GetFiles(assemblyDirectory, "*.exe").Select(path => Path.GetFileName(path)));
            assemblyFiles = assemblyFiles.Where(f => !notFiles.Contains(f)).ToList();
            return assemblyFiles;
        }

        /// <summary>
        /// 从当前应用程序域获取已加载的程序集
        /// </summary>
        /// <param name="assemblyFiles"></param>
        /// <returns></returns>
        public static List<Assembly> LoadAssembly()
        {
            List<string> assemblyFiles = GetAssemblyFiles();

            //加载程序集不能使用Assembly.LoadFile()方法，该方法会导致DLL文件占用无法释放，改为文件流加载方式
            //return assemblyFiles.Select(assemblyFile => Assembly.Load(File.ReadAllBytes(assemblyFile))).ToList();

            //放弃使用Assembly.Load方法加载程序集
            //Assembly.Load方法返回的程序集和当前应用程序域运行的程序集是相互独立
            //当使用Load方法加载程序集Assembly1并加载类型T1时，然后从应用程序中的程序集Assembly1加载一个类型T1，
            //尽管这两个类型看上去是完全相同的而且也确实是完全相同的，但这两个T1类型却不相同，因为这两个都叫T1的类型分属两个不同的Assembly
            //只有一种情况下才会相同即你使用的这个类型有一个接口，并且这个接口定义在其它Assembly中
            //因此为了程序的兼容性这里采取从当前应用程序域获取程序集
            return AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assemblyFiles.Contains(assembly.ManifestModule.ScopeName)).ToList();
        }

        /// <summary>
        /// 从程序集加载所有类(不包含接口、抽象类)
        /// </summary>
        /// <param name="assemblys"></param>
        /// <returns></returns>
        private static List<Type> GetClassTypes(List<Assembly> assemblys)
        {
            List<Type> types = new List<Type>();
            assemblys.ForEach(assembly =>
            {
                try
                {
                    types.AddRange(assembly.GetTypes().Where(t => t.IsClass && !t.IsInterface && !t.IsAbstract));
                }
                catch (ReflectionTypeLoadException ex)
                {
                    //处理类型加载异常，一般为缺少引用的程序集导致
                }

            });
            return types;
        }


        /// <summary>
        /// 获取类型的所有集成、实现的接口抽象类
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        public static List<Type> GetBaseTypes(Type classType)
        {
            HashSet<string> ignoreInterface = new HashSet<string>
            {
                typeof(ISingleton).ToString(),
                typeof(IWeakReference).ToString()
            };
            List<Type> baseTypes = classType.GetInterfaces().Where(t => !IsSystemNamespace(t.Namespace) && !ignoreInterface.Contains(t.FullName)).ToList();
            GetAbstructTypes(classType, baseTypes);
            return baseTypes;
        }

        /// <summary>
        /// 获取类型所有的抽象基类
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="abstructTypes"></param>
        public static void GetAbstructTypes(Type classType, List<Type> abstructTypes)
        {
            Type baseType = classType.BaseType;
            if (baseType != typeof(object) && baseType.IsAbstract && !IsSystemNamespace(baseType.Namespace))
            {
                abstructTypes.Add(baseType);
                GetAbstructTypes(baseType, abstructTypes);
            }
        }

        /// <summary>
        /// 判断接口或抽象类是否为系统的命名空间
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        private static bool IsSystemNamespace(string ns)
        {
            //常用系统命名空间
            HashSet<string> sysNamespace = new HashSet<string>
            {
                "Microsoft.Xml",
                "System",
                "System.Collections",
                "System.ComponentModel",
                "System.Configuration",
                "System.Data",
                "System.IO",
                "System.Runtime",
                "System.ServiceModel",
                "System.Text",
                "System.Web",
                "System.Xml"
            };
            return sysNamespace.Contains(string.Join(".", ns.Split('.').Take(2)));
        }

        /// <summary>
        /// 从指定的类型集合中过滤出从指定类或接口派生的类
        /// </summary>
        /// <typeparam name="T">基类或接口</typeparam>
        /// <param name="classTypes">类型集合</param>
        /// <returns></returns>
        private static List<Type> GetDerivedClass<T>(List<Type> classTypes) where T : class
        {
            return classTypes.AsParallel().Where(t => t.GetInterface(typeof(T).ToString()) != null).ToList();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="types"></param>
        /// <param name="lifetimeManager"></param>
        private static void RegisterType<T>(List<Type> types) where T : LifetimeManager, new()
        {
            types.AsParallel().ForAll(classType =>
            {
                List<Type> baseTypes = GetBaseTypes(classType).ToList();
                foreach (Type baseType in baseTypes)
                {
                    Current.RegisterType(baseType, classType, new T());
                    Current.RegisterType(baseType, classType, classType.FullName, new T());
                }
            });
        }

        /// <summary>
        /// 初始化依赖注入
        /// 注册所有实现了ISingleton和IWeakReference接口的类型到IUnityContainer容器
        /// </summary>
        public  static void Init()
        {
            //默认的情况下使用TransientLifetimeManager管理对象的生命周期,它不会在container中保存对象的引用，简而言之每当调用Resolve或ResolveAll方法时都会实例化一个新的对象
            //ContainerControlledLifetimeManager 单例模式,每次调用Resolve或ResolveAll方法都会调用同一个对象的引用
            //ExternallyControlledLifetimeManager 弱引用

            List<Assembly> assemblys = LoadAssembly();

            List<Type> classTypes = GetClassTypes(assemblys);

            //所有接口默认注册为弱引用
            RegisterType<ExternallyControlledLifetimeManager>(classTypes);

            //先注册单例，再注册若引用，确保若同时实现了ISingleton和IWeakReference接口，则注册为弱引用

            //实现ISingleton接口的类型集合注册为单例模式
            List<Type> singletonTypeList = GetDerivedClass<ISingleton>(classTypes);
            //注册单例
            RegisterType<ContainerControlledLifetimeManager>(singletonTypeList);

            //实现IWeakReference接口的类型集合注册为弱引用
            List<Type> weakReferenceTypeList = GetDerivedClass<IWeakReference>(classTypes);
            //注册弱引用
            RegisterType<ExternallyControlledLifetimeManager>(weakReferenceTypeList);

        }

        /// <summary>
        /// 注册依赖注入
        /// </summary>
        public static void RegisterDependency()
        {
            Init();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(Current));
        }

    }
}
