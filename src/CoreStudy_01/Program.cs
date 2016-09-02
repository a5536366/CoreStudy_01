using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CoreStudy_01
{
    public interface IDog
    {
       
    }
    public class Dog : IDog { }
    public interface IFooBar { }
    public interface IFoo { }
    public interface IBar { }
    public class Foo : IFoo,IFooBar { }

    public class Bar : IBar, IFooBar
    {
        public Bar(IServiceProvider provider)//系统默认提供注入功能
        {
            Console.WriteLine("Provider:{0}",provider.GetService<IDog>());
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            //注册编码，控制台才可以输出中文
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine("我是中文可以输出!");
            // Singleton：ServiceProvider创建的服务实例保存在作为根节点的ServiceProvider上，所有具有同一根节点的所有ServiceProvider提供的服务实例均是同一个对象。 
            //Scoped：ServiceProvider创建的服务实例由自己保存，所以同一个ServiceProvider对象提供的服务实例均是同一个对象。 
            //Transient：针对每一次服务提供请求，ServiceProvider总是创建一个新的服务实例。 
            //提供注入
            IServiceCollection services = new ServiceCollection()
                .AddSingleton<IDog,Dog>()
                .AddSingleton<IFooBar,Foo>()//单件注入
                .AddSingleton<IFooBar,Bar>()
                .AddSingleton<IFoo, Foo>()
                .AddSingleton<IBar,Bar>();
            
            //对注入提取
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            Console.WriteLine("serviceProvider.GetService<IFoo>(): {0}", serviceProvider.GetService<IFoo>());
            Console.WriteLine("serviceProvider.GetService<IBar>(): {0}", serviceProvider.GetService<IBar>());
            //只提取最后一个注入
            Console.WriteLine("serviceProvider.GetService<IFoobar>(): {0}", serviceProvider.GetService<IFooBar>());
            //对IServiceProvider 自身的支持为 ServiceProvider
            Console.WriteLine("serviceProvider.GetService<IServiceProvider>(): {0}", serviceProvider.GetService<IServiceProvider>());
            //多继承获取
            IEnumerable<IFooBar> _services = serviceProvider.GetServices<IFooBar>();
            int index = 1;
            foreach (IFooBar service in _services)
            {
                Console.WriteLine("{0}: {1}", index++, service);
            }
            Console.ReadLine();
        }
    }
}
