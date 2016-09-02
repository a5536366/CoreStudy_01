using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CoreStudy_01
{
    public interface IFooBar { }
    public interface IFoo { }
    public interface IBar { }
    public class Foo : IFoo,IFooBar { }
   public class Bar : IBar,IFooBar { }

    public class Program
    {
        public static void Main(string[] args)
        {
            //提供注入
            IServiceCollection services = new ServiceCollection()
                .AddSingleton<IFooBar,Foo>()//单件注入
                .AddSingleton<IFooBar,Bar>()
                .AddSingleton<IFoo, Foo>()
                .AddSingleton<IBar>(new Bar());
            
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
