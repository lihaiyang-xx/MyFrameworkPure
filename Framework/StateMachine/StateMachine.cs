
using UnityEngine;
using System.Collections;
/*
 *模块或类 ::     竞速模块->AI模块
 *作   用 ::      状态机实现
 *作   者 ::      李海洋
 *编写日期 ::     2015-10-14
 */
public class StateMachine<T> where T : class
{
    /// <summary>
    /// 游戏智能体
    /// </summary>
    private T mAI;

    /// <summary>
    /// 全局状态(可以在任意时间切换至全局状态)
    /// </summary>
    private State<T> mGlobalState;

    /// <summary>
    /// 当前状态
    /// </summary>
    private State<T> mCurrentState;

    /// <summary>
    /// 上一个状态
    /// </summary>
    private State<T> mPreviousState;

    public StateMachine(T ai)
    {
        mAI = ai;
    }

    /// <summary>
    /// 更新状态机
    /// </summary>
    public void UpdateState()
    {
        if (mGlobalState != null)
        {
            mGlobalState.Execute(mAI);
        }

        if (mCurrentState != null)
        {
            mCurrentState.Execute(mAI);
        }
    }

    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param equimpentName="newState">新状态</param>
    public void ChangeState(State<T> newState)
    {
        Debug.Assert(newState != null, "the new state to chanaged is null *** " + newState);
        mPreviousState = mCurrentState;
        mCurrentState.Exit(mAI);
        mCurrentState = newState;
        mCurrentState.Enter(mAI);
    }

    /// <summary>
    /// 反转状态至上一个状态
    /// </summary>
    public void RevertToPreviousState()
    {
        Debug.Assert(mPreviousState != null, "the previous to revert is null *** " + mPreviousState);

        ChangeState(mPreviousState);
    }

    public State<T> GlobalState
    {
        get { return mGlobalState; }
        set { mGlobalState = value; }
    }

    public State<T> CurrentState
    {
        get { return mCurrentState; }
        set { mCurrentState = value; }
    }

    public State<T> PreviousState
    {
        get { return mPreviousState; }
        set { mPreviousState = value; }
    }
}

