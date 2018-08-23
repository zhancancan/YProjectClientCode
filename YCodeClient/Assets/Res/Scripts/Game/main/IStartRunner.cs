using System;

namespace main {
    internal interface IStartRunner { 
        void Start();
        bool complete { get; }
    }
}