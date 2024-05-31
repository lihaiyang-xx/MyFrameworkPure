using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerAuthorization : MonoBehaviour
{
    [SerializeField] private string appName;
    // Start is called before the first frame update
    IEnumerator  Start()
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"http://106.14.190.213:8090/active/app.php?app={appName}"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string result = request.downloadHandler.text;
                if (result == "fail")
                {
                    Debug.Log("start failed by some reason!");
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
                else
                {
                    //Debug.Log("服务器授权成功!");
                }
            }
            else
            {
                yield break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
