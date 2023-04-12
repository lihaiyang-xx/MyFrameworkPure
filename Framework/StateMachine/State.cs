using UnityEngine;
using System.Collections;
/*
 *模块或类 ::     状态机
 *作   用 ::      状态基类
 *作   者 ::      李海洋
 *编写日期 ::     2015-10-14
 */

namespace MyFrameworkPure
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}


