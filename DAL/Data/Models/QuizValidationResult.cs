using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Data.Models
{
    public class QuizValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public int Score { get; set; }
    }
}
