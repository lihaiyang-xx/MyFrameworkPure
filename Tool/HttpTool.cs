using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class HttpTool
{
#if AsyncAwaitUtil
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

    public static async Task<byte[]> GetBytes(string url)
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
                return request.downloadHandler.data;
            }
        }

        return null;
    }

    //public static async Task<byte[]> GetAssetbundle(string url)
    //{
    //    using (UnityWebRequest request = UnityWebRequest.Get(url))
    //    {
    //        await request.SendWebRequest();
    //        if (request.isNetworkError || request.isHttpError)
    //        {
    //            Debug.Log(request.error);
    //        }
    //        else
    //        {
    //            return request.downloadedBytes.;
    //        }
    //    }

    //    return null;
    //}
#endif

    public static string GetNative(string uri)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }

    public static IEnumerator Get(string url, UnityAction<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string text = request.downloadHandler.text;
                callback?.Invoke(text);
            }
        }
    }

    public static IEnumerator GetBytes(string url, UnityAction<byte[]> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                byte[] bytes = request.downloadHandler.data;
                callback?.Invoke(bytes);
            }
        }
    }

    public static IEnumerator GetTexture(string url, UnityAction<Texture2D> callback)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                var texture = DownloadHandlerTexture.GetContent(request);
                callback?.Invoke(texture);
            }
        }
    }


    public static IEnumerator Post(string url, string json, UnityAction<string> callback)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string text = request.downloadHandler.text;
                callback?.Invoke(text);
            }
        }
    }

    public static IEnumerator Post(string url, WWWForm form, UnityAction<string> callback, Dictionary<string, string> headers = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string text = request.downloadHandler.text;
                callback?.Invoke(text);
            }
        }
    }
}
