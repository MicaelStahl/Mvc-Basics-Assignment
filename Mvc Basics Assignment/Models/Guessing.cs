using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc_Basics_Assignment.Models
{
    public class Guessing
    {
        public string Guess(int Number, int Rnd)
        {
            string Answer = "";

            if (Number == Rnd)
            {
                Answer = "Congratulations! You guessed correctly!";
            }
            else if (Number < Rnd)
            {
                Answer = "Your guess was too small! Please try again";
            }
            else
            {
                Answer = "Your guess was too big! please try again";
            }
            return Answer;
        }

        public int RndGeneratedNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101);
            return randomNumber;
        }
    }
}
