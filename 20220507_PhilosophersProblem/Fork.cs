using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20220507_PhilosophersProblem
{
    class Fork
    {
        public Fork(string NameFork)
        {
            this.NameFork = NameFork;
        }

        public string NameFork { get; set; }

        public bool IsUse { get; set; }

        public Philosopher PhilosopherUseFork { get; set; }
    }
}
