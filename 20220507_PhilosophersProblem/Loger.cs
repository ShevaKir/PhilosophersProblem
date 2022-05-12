using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _20220507_PhilosophersProblem
{
    class Loger : ILoger, IDisposable
    {
        private const string FILE_NAME = "log.txt";

        private bool disposedValue;

        private StreamWriter _writer;
        private BufferedStream _buffer;

        private object _sync = new object();

        public Loger(string fileName = FILE_NAME)
        {
            _writer = new StreamWriter(fileName, true);
            _buffer = new BufferedStream(_writer.BaseStream, int.MaxValue);
        }


        public void Write(string message)
        {
            lock (_sync)
            {
                _writer.WriteLine("{0} ----- {1}", DateTime.Now.ToLocalTime(), message);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _buffer.Flush();
                    _writer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~Loger()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
