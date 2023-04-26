using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string Result { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}