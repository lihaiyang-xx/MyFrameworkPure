
using UnityEngine;
using System.Collections;

namespace MyFrameworkPure
{
    //状态机实现
    public class StateMachine
    {
        public StateMachine()
        {
            MonoBehaviorTool.Instance.RegisterUpdate(UpdateState);
        }
        /// <summary>
        /// 更新状态机
        /// </summary>
        public void UpdateState()
        {
            GlobalState?.Update();

            CurrentState?.Update();
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        /// <param name="newState"></param>
        public void EnterStatus(State newState)
        {
            Debug.Assert(newState != null, "the new state to chanaged is null *** " + newState);
            PreviousState = CurrentState;
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        /// <summary>
        /// 反转状态至上一个状态
        /// </summary>
        public void RevertToPreviousState()
        {
            if(PreviousState != null)
                EnterStatus(PreviousState);
        }

        /// <summary>
        /// 全局状态(可以在任意时间切换至全局状态)
        /// </summary>
        public State GlobalState { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public State CurrentState { get; set; }

        /// <summary>
        /// 上一个状态
        /// </summary>
        public State PreviousState { get; set; }
    }
}


