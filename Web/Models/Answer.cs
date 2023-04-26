using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public string Title { get; set; }
        public int ClickCount { get; set; }
        public bool IsCorrect { get; set; }
    }
}