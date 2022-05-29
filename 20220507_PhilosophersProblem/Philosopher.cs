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
        public const int ITERATIONS_COUNT = 50;

        private Random _random;

        ILoger _loger;

        private Fork _left;
        private Fork _right;

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
            var eatingDuration = _random.Next(0, 300);

            _loger.Write(string.Format("Philosopher {0} have two forks {1} --- {2}.", Name, _left.NameFork, _right.NameFork));
            _loger.Write(string.Format("Philosopher {0} is eating.", Name));
            Thread.Sleep(eatingDuration);

        }

        private void Think()
        {
            var thinkingDuration = _random.Next(0, 100);

            _loger.Write(string.Format("Philosopher {0} is thinking.", Name));
            Thread.Sleep(thinkingDuration);
        }

        private bool IsForkAvailable()
        {
            bool isAvailable = true;

            return isAvailable;
        }

        private void AquireForks()
        {
            _left.IsUse = true;
            _right.IsUse = true;

            _loger.Write(string.Format("Philosopher {0} acquired forks: {1}, {2}.",
                    Name, _left.NameFork, _right.NameFork));
        }

        private void ReleaseForks()
        {
            _left.IsUse = false;
            _left.PhilosopherUseFork = null;
            _right.IsUse = false;
            _right.PhilosopherUseFork = null;

            _loger.Write(string.Format("Philosopher {0} release forks: {1}, {2}.",
                    Name, _left.NameFork, _right.NameFork));
        }

        public void Run()
        {
            int count = 0;

            for( ; ; )
            {
                if (!_left.IsUse)
                {
                    _left.IsUse = true;
                    _left.PhilosopherUseFork = this;
                    _loger.Write(string.Format("Philosopher {0} acquired fork: {1}", Name, _left.NameFork));
                }
                else
                {
                    if (!_right.IsUse)
                    {
                        _right.IsUse = true;
                        _right.PhilosopherUseFork = this;
                        _loger.Write(string.Format("Philosopher {0} acquired fork: {1}", Name, _right.NameFork));
                    }
                    else
                    {
                        Think();
                        count++;
                        if(count == 10)
                        {
                            _left.IsUse = false;
                            _left.PhilosopherUseFork = null;
                            _right.IsUse = false;
                            _right.PhilosopherUseFork = null;
                            count = 0;
                        }
                    }
                }

                if (ReferenceEquals(_left.PhilosopherUseFork, this) && ReferenceEquals(_right.PhilosopherUseFork, this))
                {
                    Eat();
                    ReleaseForks();
                    Think();
                    count = 0;
                }
            }

        }
    }
}

