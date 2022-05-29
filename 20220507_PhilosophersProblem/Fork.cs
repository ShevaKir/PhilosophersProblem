using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20220507_PhilosophersProblem
{
    class Fork
    {
        private object _sync = new object();
        private bool _isUse = false;

        public Fork(string NameFork)
        {
            this.NameFork = NameFork;
            IsUse = false;
        }

        public string NameFork { get; }

        public bool IsUse
        {
            get
            {
                return _isUse;
            }
            set
            {
                lock (_sync)
                {
                    _isUse = value;
                }
            }
        }

        public Philosopher PhilosopherUseFork { get; set; }
    }
}
