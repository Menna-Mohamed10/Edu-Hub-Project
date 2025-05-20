using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Data.Models
{
    public class QuestionAnswer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AnswerText { get; set; }

        public int QuestionId { get; set; }
        public QuizQuestion Question { get; set; }
    }
}
