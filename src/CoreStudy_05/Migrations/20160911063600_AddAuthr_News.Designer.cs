using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CoreStudy_05;

namespace CoreStudy_05.Migrations
{
    [DbContext(typeof(BloggingContext))]
    [Migration("20160911063600_AddAuthr_News")]
    partial class AddAuthr_News
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreStudy_05.Blogs", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("Url");

                    b.HasKey("BlogId")
                        .HasName("PK_Blogs");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("CoreStudy_05.News", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(20)")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25)
                        .HasAnnotation("SqlServer:ColumnType", "Nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("CoreStudy_05.Posts", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogId");

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("PostId")
                        .HasName("PK_Posts");

                    b.HasIndex("BlogId")
                        .HasName("IX_Posts_BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("CoreStudy_05.Posts", b =>
                {
                    b.HasOne("CoreStudy_05.Blogs", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
