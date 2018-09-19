using UnityEngine;
using System.Collections;

internal class MonoManagerMonoBehaviour : MonoBehaviour {
}

public class CoroutineTool {
	private static MonoManagerMonoBehaviour _MonoManagerMonoBehaviour;
    private static MonoManagerMonoBehaviour _MonoManagerMonoBehaviour_Normal;
	static CoroutineTool () {
		Init();
	}

	public static Coroutine DoCoroutine (IEnumerator routine) {
		return _MonoManagerMonoBehaviour.StartCoroutine(routine);
	}

    public static Coroutine DoCoroutine_Normal(IEnumerator routine)
    {
        if (_MonoManagerMonoBehaviour_Normal == null)
        {
            var go1 = new GameObject();
            go1.name = "CoroutineManager_Normal";
            _MonoManagerMonoBehaviour_Normal = go1.AddComponent<MonoManagerMonoBehaviour>();
        }
        return _MonoManagerMonoBehaviour_Normal.StartCoroutine(routine);
    }
	private static void Init () {
		var go = new GameObject();
		go.name = "CoroutineTool";
		_MonoManagerMonoBehaviour = go.AddComponent<MonoManagerMonoBehaviour>();
		GameObject.DontDestroyOnLoad(go);

        var go1 = new GameObject();
        go1.name = "CoroutineManager_Normal";
        _MonoManagerMonoBehaviour_Normal = go1.AddComponent<MonoManagerMonoBehaviour>();
	}
}
