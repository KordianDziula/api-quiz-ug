using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace QuizAPI.Models
{
    [Table("QuizQuestion")]
    public partial class QuizQuestion
    {
        public QuizQuestion()
        {
            QuizQuestionAnswers = new HashSet<QuizQuestionAnswer>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("quiz_id")]
        public int? QuizId { get; set; }
        [Column("question")]
        public string Question { get; set; }

        [ForeignKey(nameof(QuizId))]
        [InverseProperty("QuizQuestions")]
        public virtual Quiz Quiz { get; set; }
        [InverseProperty(nameof(QuizQuestionAnswer.QuizQuestion))]
        public virtual ICollection<QuizQuestionAnswer> QuizQuestionAnswers { get; set; }
    }
}
