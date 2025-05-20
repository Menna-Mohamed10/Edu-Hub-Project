using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Dtos
{
    public class QuizSubmissionDto
    {
        public int QuizId { get; set; }
        public Dictionary<int, string> Answers { get; set; }
    }
}
