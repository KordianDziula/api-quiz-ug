using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace QuizAPI.Models
{
    [Table("Quiz")]
    public partial class Quiz
    {
        public Quiz()
        {
            QuizQuestions = new HashSet<QuizQuestion>();
        }

        public Quiz(int id, string name, ICollection<QuizQuestion> quizQuestions)
        {
            this.Id = id;
            this.Name = name;
            this.QuizQuestions = quizQuestions;
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(200)]
        public string Name { get; set; }

        [InverseProperty(nameof(QuizQuestion.Quiz))]
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
