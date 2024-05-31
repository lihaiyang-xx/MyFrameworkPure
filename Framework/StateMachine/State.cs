using UnityEngine;
using System.Collections;

namespace MyFrameworkPure
{
    /// <summary>
    /// 状态基类
    /// </summary>
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}


