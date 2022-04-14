using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HttpTool
{

    public static async Task<string> Get(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            await request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                return request.downloadHandler.text;
            }
        }

        return null;
    }

    public static async Task<Texture> GetTexture(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            await request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                return DownloadHandlerTexture.GetContent(request);
            }
        }

        return null;
    }

    public static async Task<string> Post(string url,string json)
    {
        using (UnityWebRequest request = new UnityWebRequest(url,UnityWebRequest.kHttpVerbPOST))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type","application/json");
            await request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                return request.downloadHandler.text;
            }
        }

        return null;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="form"></param>
    /// <param name="headers">
    /// 常用header
    /// request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    /// request.SetRequestHeader("access_token", token);
    ///</param>
    /// <returns></returns>
    public static async Task<string> Post(string url, WWWForm form,Dictionary<string,string> headers = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url,form))
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.SetRequestHeader(header.Key,header.Value);
                }
            }
            await request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                return request.downloadHandler.text;
            }
        }

        return null;
    }
}
