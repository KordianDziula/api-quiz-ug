using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace QuizAPI.Models
{
    [Table("QuizQuestionAnswer")]
    public partial class QuizQuestionAnswer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("quiz_question_id")]
        public int? QuizQuestionId { get; set; }
        [Column("answer")]
        public string Answer { get; set; }
        [Column("is_correct")]
        public bool? IsCorrect { get; set; }

        [ForeignKey(nameof(QuizQuestionId))]
        [InverseProperty("QuizQuestionAnswers")]
        public virtual QuizQuestion QuizQuestion { get; set; }
    }
}
