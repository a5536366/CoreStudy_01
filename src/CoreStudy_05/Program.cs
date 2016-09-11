using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace CoreStudy_05
{
    /// <summary>
    /// EF Core 学习
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            //using (var db = new BloggingContext())
            //{
            //    var entity = db.Blogs

            //        .OrderBy(b => b.Url)
            //        .FirstOrDefault();
            //    entity.Url = "luckyearth.cn";

            //    db.Blogs.Update(entity);
            //    db.SaveChanges();
            //}
            //using (BloggingContext context=new BloggingContext())
            //{
            //    var entity=context.Blogs.Add(new Blog()
            //    {
                    
            //        Url = "d.luckyhr.com",
                    
            //    }).Entity;
               
            //    context.SaveChanges();
            //}
        }
    }
   

}

