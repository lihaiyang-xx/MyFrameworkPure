using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

namespace MyFrameworkPure
{
    /// <summary>
    /// 消息发送和接收工具
    /// </summary>
    public static class MessageTool
    {
        public delegate void Handler(params object[] args);

        public static void Listen(Enum message, Handler action)
        {
            int messageId = message.GetHashCode();
            if (Listeners[messageId] is Handler actions)
            {
                Listeners[messageId] = actions + action;
            }
            else
            {
                Listeners[messageId] = action;
            }
        }

        public static void Remove(Enum message, Handler action)
        {
            int messageId = message.GetHashCode();

            if (Listeners[messageId] is Handler actions)
            {
                Listeners[messageId] = actions - action;
            }
        }

        public static void Clean(Enum message)
        {
            int messageId = message.GetHashCode();

            if (Listeners[messageId] is Handler actions)
            {
                Listeners[messageId] = null;
            }
        }

        public static void Send(Enum message, params object[] args)
        {
            int messageId = message.GetHashCode();

            if (Listeners[messageId] is Handler actions)
            {
                actions(args);
            }
        }

        private static readonly Hashtable Listeners = new Hashtable();
    }

}

