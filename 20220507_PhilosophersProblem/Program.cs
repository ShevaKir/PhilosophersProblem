using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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


            List<Philosopher> all = new List<Philosopher>(5);

            using (Loger log = new Loger())
            {
                Philosopher platon = new Philosopher("1) Platon", forkOrange, forkBlack, log);
                Philosopher sokrat = new Philosopher("2) Sokrat", forkBlack, forkGreen, log);
                Philosopher pifagor = new Philosopher("3) Pifagor", forkGreen, forkBlue, log);
                Philosopher aristotel = new Philosopher("4) Aristotel", forkBlue, forkRed, log);
                Philosopher kant = new Philosopher("5) Kant", forkRed, forkOrange, log);

                all.Add(platon);
                all.Add(sokrat);
                all.Add(pifagor);
                all.Add(aristotel);
                all.Add(kant);


                log.Write("Dinner is starting!");

                List<Thread> philosopherThreads = new List<Thread>();
                foreach (Philosopher philosopher in all)
                {
                    var philosopherThread = new Thread(new ThreadStart(philosopher.Run));
                    philosopherThread.IsBackground = true;
                    philosopherThreads.Add(philosopherThread);
                }

                foreach (Thread thread in philosopherThreads)
                {
                    thread.Start();
                }

                //foreach (Thread thread in philosopherThreads)
                //{
                //    thread.Join();
                //}

                
                Console.ReadKey();
                Console.Write("Dinner is over!");
                //Console.ReadKey();
            }
            Console.Write("end");
        }
    }
}
