using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc_Basics_Assignment.Models;

namespace Mvc_Basics_Assignment.Controllers
{
    public class GuessingGameController : Controller
    {
        public const string SessionKeyRndNumber = "_RandomNumber";
        public const string SessionKeyRndGuess = "_Guess";
        public const string SessionKeyResetSession = "_ResetSession";

        private string ResetSession { get; set; }

        private int Rnd { get; set; }

        Guessing guessing = new Guessing();

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.NewGuess = "Insert a number to keep on guessing!";
            //string test = HttpContext.Session.GetString("_ResetSession");

            //if (test != null)
            //    if (test.Contains("FinishThis"))
            //    {
            HttpContext.Session.Clear();
            //    return RedirectToAction("Index", "GuessingGame");
            //}


            int RandomNumber = guessing.RndGeneratedNumber();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("_RandomNumber")))
            {
                HttpContext.Session.SetInt32("_RandomNumber", RandomNumber);
            }

            return View();
        }
        [HttpPost]
        public IActionResult Index(int number)
        {
            if (number != 0)
            {
                string test = HttpContext.Session.GetString("_ResetSession");

                if (test != null)
                {
                    if (test.Contains("FinishThis"))
                    {
                        return RedirectToAction("Index", "GuessingGame");
                    }
                }

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("_Guess")))
                {
                    HttpContext.Session.SetInt32("_Guess", number);
                }

                Rnd = (int)HttpContext.Session.GetInt32("_RandomNumber");

                // gets the answer from guessing.Guess
                ViewBag.Guess = guessing.Guess(number, Rnd);

                ViewBag.YourGuess = $"Your guess was {number}!";

                // the random value generated once.
                ViewBag.Random = Rnd;

                if (number == HttpContext.Session.GetInt32("_RandomNumber"))
                {
                    ResetSession = "FinishThis";
                    HttpContext.Session.SetString("_ResetSession", ResetSession);
                }
            }
            else
            {
                ViewBag.Blank = "Please enter a value before pressing the button.";
            }
            return View();
        }
    }
}