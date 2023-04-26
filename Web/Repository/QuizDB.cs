using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;

namespace QuizProject.Repository
{
    public class QuizDB : DbContext
    {
        public QuizDB()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS02;Database=QuizDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>().Navigation(e => e.Questions).AutoInclude();
            modelBuilder.Entity<Question>().Navigation(e => e.Answers).AutoInclude();
        }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}