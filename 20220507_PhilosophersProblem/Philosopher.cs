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
        private Random _random;
        private readonly int _maxThinkDuration = 1000;
        private readonly int _minThinkDuration = 50;

        private object _sync = new object();

        private Fork _left;
        private Fork _right;
        private ActionPhilosopher _action = ActionPhilosopher.Thinking;
        private readonly List<Philosopher> _allPhilosophers;

        static SemaphoreSlim allowedToEat = new SemaphoreSlim(2);

        public Philosopher(string name, Fork left, Fork right/*, List<Philosopher> allPhilosophers*/)
        {
            Name = name;
            _left = left;
            _right = right;
            //_allPhilosophers = allPhilosophers;
        }

        public string Name { get; }

        private void Eat()
        {
            var eatingDuration = _random.Next(_maxThinkDuration) + _minThinkDuration;

            Console.WriteLine("Philosopher {0} is eating.", Name);
            Thread.Sleep(eatingDuration);
            Console.WriteLine("Philosopher {0} is thinking after eating.", Name);
        }

        private bool IsForkAvailable()
        {
            bool isAvailable = true;

            lock (_sync) 
            {
                if (_left.IsUse)
                {
                    Console.WriteLine("Philosopher {0} cannot eat. Left fork is in use", Name);
                    isAvailable = false;
                }

                if (_right.IsUse)
                {
                    Console.WriteLine("Philosopher {0} cannot eat. Left fork is in use", Name);
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

                Console.WriteLine("Philosopher {0} acquired forks: {1}, {2}.", 
                        Name, _left.NameFork, _right.NameFork);
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

                Console.WriteLine("Philosopher {0} released forks: {1}, {2}.",
                        Name, _left.NameFork, _right.NameFork);
            }
        }
    }
}
