using System;
using System.Collections.Generic;
using Cake.Core.IO;

namespace Cake.MkDocs.Tests.Fixtures
{
    public sealed class LongRunningFakeProcess : IProcess
    {
        private readonly object _lockObj = new object();
        private bool _killed;

        private bool Killed
        {
            get
            {
                lock (_lockObj)
                {
                    return _killed;
                }
            }
            set
            {
                lock (_lockObj)
                {
                    _killed = value;
                }
            }
        }

        private int _exitCode;
        private IEnumerable<string> _standardError;
        private IEnumerable<string> _standardOutput;

        public void Dispose()
        {
            Killed = true;
        }

        public void WaitForExit()
        {
            while (!WaitForExit(100))
            {
            }
        }

        public bool WaitForExit(int milliseconds)
        {
            if (Killed)
            {
                return true;
            }

            int clock = 0;
            while (!Killed && clock < milliseconds)
            {
                int wait = Math.Min(100, milliseconds - clock);
                System.Threading.Thread.Sleep(wait);

                clock += wait;
            }

            return Killed;
        }

        public int GetExitCode()
        {
            return _exitCode;
        }

        public IEnumerable<string> GetStandardError()
        {
            return _standardError;
        }

        public IEnumerable<string> GetStandardOutput()
        {
            return _standardOutput;
        }

        public void Kill()
        {
            Killed = true;
        }

        public void SetExitCode(int exitCode)
        {
            _exitCode = exitCode;
            Killed = true;
        }

        public void SetStandardError(IEnumerable<string> standardError)
        {
            _standardError = standardError;
        }

        public void SetStandardOutput(IEnumerable<string> standardOutput)
        {
            _standardOutput = standardOutput;
        }
    }
}
