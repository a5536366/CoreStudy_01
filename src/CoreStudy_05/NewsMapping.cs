using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreStudy_05
{
    public sealed class NewsMapping
    {
        public static void Map(EntityTypeBuilder<News> builder)
        {
            
            builder.ToTable("News");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).UseSqlServerIdentityColumn();
            builder.Property(t => t.Title).IsRequired().HasMaxLength(25).ForSqlServerHasColumnType("Nvarchar(25)");
            builder.Property(t => t.Author).HasMaxLength(20).HasColumnType("nvarchar(20)");
            builder.Property(t => t.CreateTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");
            builder
                .Property(o => o.OrderNo)
                .HasDefaultValueSql("NEXT VALUE FOR shared.OrderNumbers");
        }
    }
}
