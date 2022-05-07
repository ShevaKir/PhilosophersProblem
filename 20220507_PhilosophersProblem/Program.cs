using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20220507_PhilosophersProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fork forkOrange = new Fork("Orange");
            Fork forkBlack = new Fork("Black");
            Fork forkGreen = new Fork("Green");
            Fork forkBlue = new Fork("Blue");
            Fork forkRed = new Fork("Red");

            Philosopher platon = new Philosopher("Platon", forkOrange, forkBlack);
            Philosopher pifagor = new Philosopher("Pifagor", forkBlack, forkGreen);
            Philosopher sokrat = new Philosopher("Sokrat", forkGreen, forkBlue);
            Philosopher aristotel = new Philosopher("Aristotel", forkBlue, forkRed);
            Philosopher kant = new Philosopher("Kant", forkRed, forkOrange);
        }
    }
}
