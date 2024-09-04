using Azure;
using DevStart_DataAccsess.Identity;
using DevStart_Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_DataAccsess.Contexts
{
    public class DevStartDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DevStartDbContext(DbContextOptions<DevStartDbContext> options) : base(options) { } //constructor tanımlıyoruz..
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSale> CoursesSales { get; set; }
        public DbSet<CourseSaleDetail> CoursesSalesDetails { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Fluent API Validation
            builder.Entity<Course>().Property("CourseTitle").IsRequired().HasMaxLength(200);
            builder.Entity<Course>().Property("CourseDescription").IsRequired().HasMaxLength(500);
            builder.Entity<Lesson>().Property("LessonTitle").IsRequired().HasMaxLength(200);
            builder.Entity<Lesson>().Property("LessonContent").IsRequired().HasMaxLength(500);
            builder.Entity<Review>().Property("ReviewComment").IsRequired().HasMaxLength(500);
            builder.Entity<Category>().Property("CategoryName").IsRequired().HasMaxLength(50);
            builder.Entity<Category>().Property("CategoryDescription").IsRequired().HasMaxLength(500);
            builder.Entity<Tag>().Property("TagName").IsRequired().HasMaxLength(50);
            builder.Entity<Tag>().Property("TagDescription").IsRequired().HasMaxLength(200);
            builder.Entity<Video>().Property("VideoLink").IsRequired().HasMaxLength(500);


            //bu yapı, Course ile tag arasında many to many ilişkisi. Course birden fazla tag içerebilir, aynı şekilde bir tag birden fazla course ile ilişkili olabilir.
            builder.Entity<Course>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Courses)
                .UsingEntity<Dictionary<string, object>>
                ("CourseTag",
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"));

            base.OnModelCreating(builder);


        }
    }
}
