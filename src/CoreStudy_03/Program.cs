using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CoreStudy_03
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Test.json");


            Dictionary <string, string> source = new Dictionary<string, string>
            {
                ["format:dateTime:longDatePattern"] = "dddd, MMMM d, yyyy",
                ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
                ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
                ["format:dateTime:shortTimePattern"] = "h:mm tt",

                ["format:currencyDecimal:digits"] = "2",
                ["format:currencyDecimal:symbol"] = "$",
            };
            IConfiguration configuration = new ConfigurationBuilder()
                
                .AddJsonFile(path,true)
           .Add(new MemoryConfigurationSource { InitialData = source })
           .Build();

           // IServiceCollection service=new ServiceCollection();
          //  service.AddOptions();
           // service.Configure<FormatOptions>(configuration.GetSection("Format"));
           // IServiceProvider provider=service.BuildServiceProvider();

            var str=configuration["Data:ConnString"];//.GetSection("Data:ConnString");
            //var cprovider= 

            

          //  FormatOptions options = provider.GetService<IOptions<FormatOptions>>().Value;

            //FormatOptions options = new FormatOptions(configuration.GetSection("Format"));
           // DateTimeFormatOptions dateTime = options.DateTime;
           // CurrencyDecimalFormatOptions currencyDecimal = options.CurrencyDecimal;

            //Console.WriteLine("DateTime:");
            //Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
            //Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
            //Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
            //Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");

            //Console.WriteLine("CurrencyDecimal:");
            //Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
            //Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");

            Console.ReadLine();
        }
    }
    public class CurrencyDecimalFormatOptions
    {
        public int Digits { get; set; }
        public string Symbol { get; set; }

        //public CurrencyDecimalFormatOptions(IConfiguration config)
        //{
        //    this.Digits = int.Parse(config["Digits"]);
        //    this.Symbol = config["Symbol"];
        //}
    }
   
    public class DateTimeFormatOptions
    {
        //其他成员
        //public DateTimeFormatOptions(IConfiguration config)
        //{
        //    this.LongDatePattern = config["LongDatePattern"];
        //    this.LongTimePattern = config["LongTimePattern"];
        //    this.ShortDatePattern = config["ShortDatePattern"];
        //    this.ShortTimePattern = config["ShortTimePattern"];
        //}
        public string LongDatePattern { get; set; }
        public string LongTimePattern { get; set; }
        public string ShortDatePattern { get; set; }
        public string ShortTimePattern { get; set; }
    }
    public class FormatOptions
    {
        public DateTimeFormatOptions DateTime { get; set; }
        public CurrencyDecimalFormatOptions CurrencyDecimal { get; set; }

        //public FormatOptions(IConfiguration config)
        //{
        //    this.DateTime = new DateTimeFormatOptions(config.GetSection("DateTime"));
        //    this.CurrencyDecimal = new CurrencyDecimalFormatOptions(config.GetSection("CurrencyDecimal"));
        //}
    }
}
