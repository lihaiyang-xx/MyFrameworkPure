using UnityEngine;
using System.Collections;

namespace MyFrameworkPure
{
    internal class MonoManagerMonoBehaviour : MonoBehaviour
    {
    }

    public static class CoroutineTool
    {
        private static MonoManagerMonoBehaviour _monoManagerMonoBehaviour;
        private static MonoManagerMonoBehaviour _monoManagerMonoBehaviourNormal;
        static CoroutineTool()
        {
            Init();
        }

        public static Coroutine DoCoroutine(IEnumerator routine)
        {
            return _monoManagerMonoBehaviour.StartCoroutine(routine);
        }

        public static Coroutine DoCoroutine_Normal(IEnumerator routine)
        {
            if (_monoManagerMonoBehaviourNormal == null)
            {
                var go1 = new GameObject { name = "CoroutineManager_Normal" };
                _monoManagerMonoBehaviourNormal = go1.AddComponent<MonoManagerMonoBehaviour>();
            }
            return _monoManagerMonoBehaviourNormal.StartCoroutine(routine);
        }
        private static void Init()
        {
            var go = new GameObject { name = "CoroutineTool" };
            _monoManagerMonoBehaviour = go.AddComponent<MonoManagerMonoBehaviour>();
            Object.DontDestroyOnLoad(go);

            var go1 = new GameObject { name = "CoroutineManager_Normal" };
            _monoManagerMonoBehaviourNormal = go1.AddComponent<MonoManagerMonoBehaviour>();
        }
    }

}

