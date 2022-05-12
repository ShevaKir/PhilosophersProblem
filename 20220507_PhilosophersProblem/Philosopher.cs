using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _20220507_PhilosophersProblem
{
    

    class Philosopher
    {
        public const int ITERATIONS_COUNT = 5;

        private Random _random;
        private object _sync = new object();
        ILoger _loger;

        private Fork _left;
        private Fork _right;
        private ActionPhilosopher _action = ActionPhilosopher.Thinking;

        public Philosopher(string name, Fork left, Fork right, ILoger logFile)
        {
            Name = name;
            _left = left;
            _right = right;
            _random = new Random();
            _loger = logFile;
        }

        public string Name { get; }

        private void Eat()
        {
            var eatingDuration = _random.Next(0, 500);
            
            _loger.Write(string.Format("Philosopher {0} is eating.", Name));
            Thread.Sleep(eatingDuration);
            _loger.Write(string.Format("Philosopher {0} is thinking after eating.", Name));
        }

        private bool IsForkAvailable()
        {
            bool isAvailable = true;

            lock (_sync) 
            {
                if (_left.IsUse)
                {
                    _loger.Write(string.Format("Philosopher {0} cannot eat. Left fork is in use", Name));
                    isAvailable = false;
                }

                if (_right.IsUse)
                {
                    _loger.Write(string.Format("Philosopher {0} cannot eat. Right fork is in use", Name));
                    isAvailable = false;
                }
            }

            return isAvailable;
        }

        private void AquireForks()
        {
            lock (_sync)
            {
                _left.IsUse = true;
                _left.PhilosopherUseFork = this;
                _right.IsUse = true;
                _right.PhilosopherUseFork = this;
                _action = ActionPhilosopher.Eating;

                _loger.Write(string.Format("Philosopher {0} acquired forks: {1}, {2}. Philosopher {3}",
                        Name, _left.NameFork, _right.NameFork, _action.ToString()));
            }
        }

        private void ReleaseForks()
        {
            lock (_sync)
            {
                _left.IsUse = false;
                _left.PhilosopherUseFork = null;
                _right.IsUse = false;
                _right.PhilosopherUseFork = null;
                _action = ActionPhilosopher.Thinking;

                _loger.Write(string.Format("Philosopher {0} release forks: {1}, {2}. Philosopher {3}",
                        Name, _left.NameFork, _right.NameFork, _action.ToString()));
            }
        }

        public void Run()
        {
            int count = 0;

            while (count < ITERATIONS_COUNT)
            {
                bool isOkToEat;
                lock(_sync)
                {
                    isOkToEat = IsForkAvailable();
                    if (isOkToEat)
                    {
                        AquireForks();
                    }
                }


                if (isOkToEat)
                { 
                    Eat();
                    ReleaseForks();
                }
                count++;
            }

        }
    }
}
