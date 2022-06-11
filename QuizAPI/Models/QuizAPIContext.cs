using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace QuizAPI.Models
{
    public partial class QuizAPIContext : DbContext
    {
        public QuizAPIContext()
        {
        }

        public QuizAPIContext(DbContextOptions<QuizAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }
        public virtual DbSet<QuizQuestionAnswer> QuizQuestionAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ZaliczenieAPI;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<QuizQuestion>(entity =>
            {
                entity.Property(e => e.Question).IsUnicode(false);

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizQuestions)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK__QuizQuest__quiz___267ABA7A");
            });

            modelBuilder.Entity<QuizQuestionAnswer>(entity =>
            {
                entity.Property(e => e.Answer).IsUnicode(false);

                entity.HasOne(d => d.QuizQuestion)
                    .WithMany(p => p.QuizQuestionAnswers)
                    .HasForeignKey(d => d.QuizQuestionId)
                    .HasConstraintName("FK__QuizQuest__quiz___29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
