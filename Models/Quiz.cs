using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}