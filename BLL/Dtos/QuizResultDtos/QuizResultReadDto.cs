using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Dtos.QuizResultDtos
{
    public class QuizResultReadDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int Score { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
