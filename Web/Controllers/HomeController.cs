using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizProject.Models;
using QuizProject.Repository;

namespace QuizProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Create([FromBody] Quiz obj)
    {
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
        return Ok("OK");
    }

    [HttpPost]
    public IActionResult Update([FromBody] Quiz obj)
    {
        using (var context = new QuizDB())
        {
            var quiz = context.Quizzes.Find(obj.Id);
            if (quiz != null)
            {
                context.Questions.RemoveRange(quiz.Questions);
                context.SaveChanges();

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
        return Ok("OK");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        using (var context = new QuizDB())
        {
            var quiz = context.Quizzes.Find(id);
            context.Quizzes.Remove(quiz);
            context.SaveChanges();
        }
        return Ok("OK");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var quiz = new Quiz();
        using (var context = new QuizDB())
        {
            quiz = context.Quizzes.Find(id);
        }
        return View(quiz);
    }

    public IActionResult Excercise(string url)
    {
        var quiz = new Quiz();
        using (var context = new QuizDB())
        {
            quiz = context.Quizzes.FirstOrDefault(s => s.Url == url);
        }
        return View(quiz);
    }

    public IActionResult Get()
    {
        IEnumerable<Quiz> lstQuiz = new List<Quiz>();
        using (var context = new QuizDB())
        {
            lstQuiz = context.Quizzes.ToList();
        }
        return View(lstQuiz);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Add()
    {
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    private static Random random = new Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
