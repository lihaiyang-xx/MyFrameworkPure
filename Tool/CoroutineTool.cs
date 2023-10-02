using UnityEngine;
using System.Collections;

namespace MyFrameworkPure
{
    internal class MonoManagerMonoBehaviour : MonoBehaviour
    {
    }

    /// <summary>
    /// 协程工具类
    /// </summary>
    public static class CoroutineTool
    {
        private static MonoManagerMonoBehaviour _monoManagerMonoBehaviour;
        static CoroutineTool()
        {
            Init();
        }

        public static Coroutine DoCoroutine(IEnumerator routine)
        {
            return _monoManagerMonoBehaviour.StartCoroutine(routine);
        }

        public static void StopCoroutine(IEnumerator routine)
        {
            _monoManagerMonoBehaviour.StopCoroutine(routine);
        }

        public static void StopCoroutine(Coroutine coroutine)
        {
            _monoManagerMonoBehaviour.StopCoroutine(coroutine);
        }

        private static void Init()
        {
            var go = new GameObject { name = "CoroutineTool" };
            _monoManagerMonoBehaviour = go.AddComponent<MonoManagerMonoBehaviour>();
            Object.DontDestroyOnLoad(go);
        }
    }
}

