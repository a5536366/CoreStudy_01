using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace CoreStudy_04
{
    /// <summary>
    /// 分布式缓存学习
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            //注册编码，控制台才可以输出中文
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "AppSetting.json");
            //设置配置
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(path, true)
                .Build();
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDistributedRedisCache(options =>
            {
                //从配置读取地址
                options.Configuration = configuration.GetConnectionString("RedisConnection");
                //实例名称
                options.InstanceName = "master";
            }); //注册分布式缓存
            IFileProvider fileProvider = new PhysicalFileProvider(@"c:\test");
            //监测文件变化，回调执行
            ChangeToken.OnChange(() => fileProvider.Watch("text.txt"), () => { Console.WriteLine("Change"); });
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IDistributedCache _distributedCache = serviceProvider.GetService<IDistributedCache>();
            //字符串缓存，可缓存 json
            _distributedCache.SetString("CoreTest","CoreTest"+DateTime.Now);
            //二进制数据缓存
            _distributedCache.Set("CoreTest_byte", Encoding.UTF8.GetBytes("CoreTest_byte"));

            var s = Encoding.UTF8.GetString(_distributedCache.Get("CoreTest_byte"));
            string str=_distributedCache.GetString("CoreTest");
            Console.WriteLine(str);
            Console.WriteLine(s);
            //滑动过期
            _distributedCache.SetString("CoreTest_Token","CoreTest_Token",new DistributedCacheEntryOptions() {SlidingExpiration = TimeSpan.FromSeconds(2000)});
            Console.Read();
        }
    }
}
