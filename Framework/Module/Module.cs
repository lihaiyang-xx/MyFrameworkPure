using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public abstract class Module
    {
        public abstract void Init();
        public abstract void Run();
        public abstract void Destroy();
    }
}

