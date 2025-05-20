using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.DAL.Data
{
    public class LMSContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public LMSContext(DbContextOptions<LMSContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }

        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>()
                        .HasKey(e => new { e.UserId, e.CourseId });

            //modelBuilder.Entity<Enrollment>()
            //            .HasOne(e => e.User)
            //            .WithMany(u => u.Enrollments)
            //            .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Enrollment>()
                        .HasOne(e => e.Course)
                        .WithMany(c => c.Enrollments)
                        .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<QuizResult>()
                        .HasKey(q => new { q.UserId, q.QuizId });

            //modelBuilder.Entity<QuizResult>()
            //            .HasOne(q => q.User)
            //            .WithMany(u => u.QuizResults)
            //            .HasForeignKey(q => q.UserId);

            modelBuilder.Entity<QuizResult>()
                        .HasOne(qr => qr.Quiz)
                        .WithMany(q => q.QuizResults)
                        .HasForeignKey(qr => qr.QuizId);

            modelBuilder.Entity<Student>()
            .HasOne(s => s.User)
            .WithOne(u => u.Student)
            .HasForeignKey<Student>(s => s.UserId);

            modelBuilder.Entity<Professor>()
                .HasOne(p => p.User)
                .WithOne(u => u.Professor)
                .HasForeignKey<Professor>(p => p.UserId);

            modelBuilder.Entity<Enrollment>()
             .HasKey(e => new { e.UserId, e.CourseId });


        }

    }
}
