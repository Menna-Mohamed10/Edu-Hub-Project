using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Data.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal WalletBalance { get; set; }

        public string Certificates { get; set; } // Store as JSON or comma-separated string, or create a separate Certificate model if needed

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<QuizResult> QuizResults { get; set; }
    }
}
