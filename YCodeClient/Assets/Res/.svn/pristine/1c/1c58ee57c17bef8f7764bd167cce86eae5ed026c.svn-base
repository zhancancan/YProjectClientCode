using System;
using pure.utils.thread;

namespace main {
    internal class Start_Async : IStartRunner {
        bool IStartRunner.complete { get { return _c; } }
        internal Action action;
        private bool _c;

        void IStartRunner.Start() {
            ThreadSystem.RunAysn(RunCode);
        }

        private void RunCode() {
            action();
            _c = true;
        }
    }
}