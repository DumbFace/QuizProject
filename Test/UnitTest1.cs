using QuizProject.Models;
using QuizProject.Repository;

namespace Test1;

public class UnitTest1
{
    [Fact]
    public void CheckSave_ShouldWork()
    {
        var obj = new Quiz
        {
            Questions = new List<Question>{
                new Question{
                    Title = "1",
                    Answers = new List<Answer>{
                        new Answer{ Title = "A" , IsCorrect =true},
                        new Answer{ Title = "B" , IsCorrect =false},
                    },
                },
                new Question{
                   Title = "2",
                    Answers = new List<Answer>{
                        new Answer{ Title = "C" , IsCorrect =true},
                    },
                },
            },
        };

        if (obj != null)
        {
            using (var context = new QuizDB())
            {
                Quiz quiz = new Quiz
                {
                    Url = RandomString(10)
                };
                context.Add(quiz);
                if (obj.Questions.Count > 0)
                {
                    foreach (var question in obj.Questions)
                    {
                        quiz.Questions.Add(question);
                    }
                    context.SaveChanges();
                }
            }
        }

    }

    [Fact]
    public void GetData_ShouldWork()
    {
        var answer = new Answer
        {
            Title = "A",
            IsCorrect = true
        };

        var question = new Question
        {
            Title = "1",
            Result = "A"
        };

        using (var context = new QuizDB())
        {
            var quiz = new Quiz();
            context.Add(quiz);
            context.SaveChanges();

            question.Answers.Add(answer);

            quiz.Questions.Add(question);

            context.SaveChanges();
        }
    }

    private static Random random = new Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}