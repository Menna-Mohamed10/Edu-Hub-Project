using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Data.Models
{
    public class QuizQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        public ICollection<QuestionAnswer> Answers { get; set; } = new List<QuestionAnswer>();

        [Required]
        public string CorrectAnswer { get; set; }  

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
