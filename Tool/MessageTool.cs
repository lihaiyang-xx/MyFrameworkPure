using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Message
{

}

public static class MessageTool
{
    public delegate void Handler(params object[] args);

    public static void Listen(Message message, Handler action)
    {
        var actions = Listeners[message] as Handler;
        if (actions != null)
        {
            Listeners[message] = actions + action;
        }
        else
        {
            Listeners[message] = action;
        }
    }

    public static void Remove(Message message, Handler action)
    {
        var actions = Listeners[message] as Handler;
        if (actions != null)
        {
            Listeners[message] = actions - action;
        }
    }

    public static void Clean(Message message)
    {
        var actions = Listeners[message] as Handler;
        if (actions != null)
        {
            Listeners[message] = null;
        }
    }

    public static void Send(Message message, params object[] args)
    {
        Debug.Log("广播消息：" + message);
        var actions = Listeners[message] as Handler;
        if (actions != null)
        {
            actions(args);
        }
    }

    private static readonly Hashtable Listeners = new Hashtable();
}
