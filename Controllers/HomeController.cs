using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

    public IActionResult Add([FromBody] Quiz obj)
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
                context.SaveChanges();
                if (obj.Questions.Count > 0)
                {
                    foreach (var question in obj.Questions)
                    {
                        quiz.Questions.Add(question);
                        if (question.Answers.Count > 0)
                        {
                            foreach (var answer in question.Answers)
                            {
                                question.Answers.Add(answer);
                            }
                            // context.SaveChanges();
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        return Ok("OK");
    }


    public IActionResult Index()
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
