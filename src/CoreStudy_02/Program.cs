using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace CoreStudy_02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //注册编码，控制台才可以输出中文
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            IServiceCollection serviceCollection=new ServiceCollection();
            serviceCollection.AddSingleton<ISignal, Signal>();
            serviceCollection.AddMemoryCache();
            serviceCollection.AddDistributedMemoryCache();

            
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            MemcacheGetSet(serviceProvider);
            var cancellationTokenSource = new CancellationTokenSource();
            var changeToken = new CancellationChangeToken(cancellationTokenSource.Token);
            DateTime t= MemcacheGetOrCreate(serviceProvider, changeToken);
            Console.WriteLine(Guid.NewGuid().ToString().Replace("-", "").Substring(0, 7));

            Console.WriteLine($"从缓存获取:{t}");
            Console.WriteLine($"睡一会........！");
            Thread.Sleep(1000*3);
            Console.WriteLine($"睡一会后........！当前时间:{DateTime.Now}");
            t = MemcacheGetOrCreate(serviceProvider, changeToken);
            Console.WriteLine($"再次从缓存获取:{t}");
            //手动设置过期
            cancellationTokenSource.Cancel();
            t = MemcacheGetOrCreate(serviceProvider, changeToken);
            Console.WriteLine($"缓存失效后从缓存获取:{t}");
            //时间过期
            t = MemcacheAbsoluteExpiration(serviceProvider);
            Console.WriteLine($"从缓存获取:{t}");
            Thread.Sleep(1000 * 5);
            Console.WriteLine($"睡一会后........！当前时间:{DateTime.Now}");
            t = MemcacheAbsoluteExpiration(serviceProvider);
            Console.WriteLine($"再次从缓存获取:{t}");
            //信号过期
            t = MemcacheSignal(serviceProvider);
            Console.WriteLine($"从信号缓存中获取:{t}");
            serviceProvider.GetService<ISignal>().SignalToken("GetOrCreateTest");
            Thread.Sleep(1000 * 5);
            t = MemcacheSignal(serviceProvider);
            Console.WriteLine($"信号过期后缓存中获取:{t}");


            
            Console.Read();
        }
        /// <summary>
        /// 缓存的 Get Set
        /// </summary>
        /// <param name="provider"></param>
        public static void MemcacheGetSet(IServiceProvider provider)
        {
            IMemoryCache _cache = provider.GetService<IMemoryCache>();
            DateTime t=DateTime.Now;
            Console.WriteLine($"存储时间为:{t}");
            Thread.Sleep(1000*3);//模拟睡眠
            Console.WriteLine($"当前时间:{DateTime.Now}");
            _cache.Set("Test", t);
            Console.WriteLine($"获取时间为：{_cache.Get("Test")}");
        }
        /// <summary>
        /// 手动设置过期
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="token"></param>
        public static DateTime MemcacheGetOrCreate(IServiceProvider provider, CancellationChangeToken token)
        {
            IMemoryCache _cache = provider.GetService<IMemoryCache>();
            return _cache.GetOrCreate("GetOrCreateTest", (entry) =>
            {
                entry.ExpirationTokens.Add(token);
                //缓存过期回调
                entry.RegisterPostEvictionCallback(((key, value, reason, state) =>
                {
                    Console.WriteLine($"键{key}值{value}改变，因为{reason}");
                }));
                return DateTime.Now;
            });
        }
        /// <summary>
        /// 绝对时间过期
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static DateTime MemcacheAbsoluteExpiration(IServiceProvider provider)
        {
            IMemoryCache _cache = provider.GetService<IMemoryCache>();
            return _cache.GetOrCreate("GetOrCreateTest", (entry) =>
            {
                entry.AbsoluteExpiration=DateTimeOffset.Now.AddSeconds(3);
                return DateTime.Now;
            });
        }
        /// <summary>
        /// 滑动时间过期
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static DateTime MemcacheDateTime(IServiceProvider provider)
        {
            IMemoryCache _cache = provider.GetService<IMemoryCache>();
            return _cache.GetOrCreate("GetOrCreateTest", (entry) =>
            {
                entry.SlidingExpiration =TimeSpan.FromSeconds(3);
                return DateTime.Now;
            });
        }
        /// <summary>
        /// 信号过期
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static DateTime MemcacheSignal(IServiceProvider provider)
        {
            IMemoryCache _cache = provider.GetService<IMemoryCache>();
            ISignal _signal = provider.GetService<ISignal>();
            return _cache.GetOrCreate("GetOrCreateTest", (entry) =>
            {
                entry.ExpirationTokens.Add(_signal.GetToken("GetOrCreateTest"));
                return DateTime.Now;
            });
        }
    }
}
