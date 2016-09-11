using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreStudy_05
{
    public partial class MyDatabase_CoreStudy_05Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=.;Database=MyDatabase_CoreStudy_05;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blogs>(entity =>
            {
                entity.HasKey(e => e.BlogId)
                    .HasName("PK_Blogs");

                entity.Property(e => e.Author).HasMaxLength(25);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PK_Posts");

                entity.HasIndex(e => e.BlogId)
                    .HasName("IX_Posts_BlogId");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BlogId);
            });
        }

        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
    }
}