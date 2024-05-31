using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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

    public static IEnumerator Get(string url, UnityAction<string> onSuccess,UnityAction<string> onError)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string text = request.downloadHandler.text;
                onSuccess?.Invoke(text);
            }
            else
            {
                Debug.Log(request.error);
                onError?.Invoke(request.error);
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


    public static IEnumerator Post(string url, string json, UnityAction<string> onSuccess,UnityAction<string> onError, Dictionary<string, string> headers = null)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string text = request.downloadHandler.text;
                onSuccess?.Invoke(text);
            }
            else
            {
                onError?.Invoke(request.error);
            }
        }
    }

    public static IEnumerator Post(string url, WWWForm form, UnityAction<string> onSuccess, Dictionary<string, string> headers = null)
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
                onSuccess?.Invoke(request.error);
            }
            else
            {
                string text = request.downloadHandler.text;
                onSuccess?.Invoke(text);
            }
        }
    }

    //public static IEnumerator Require(
    //    string url,string method = "POST",
    //    Dictionary<string, string> queryParams = null,
    //    Dictionary<string, string> headers = null,
    //    WWWForm form = null,
    //    string jsonData = null,
    //    UnityAction<string> onSuccess = null,
    //    UnityAction<string> onFail = null)
    //{
    //    if (queryParams != null && queryParams.Count > 0)
    //    {
    //        url += "?";
    //        foreach (var param in queryParams)
    //        {
    //            url += param.Key + "=" + param.Value + "&";
    //        }
    //        url = url.TrimEnd('&');
    //    }

    //    UnityWebRequest www = new UnityWebRequest(url, method);
    //    www.downloadHandler = new DownloadHandlerBuffer();

    //    // Headers
    //    if (headers != null)
    //    {
    //        foreach (var header in headers)
    //        {
    //            www.SetRequestHeader(header.Key, header.Value);
    //        }
    //    }

    //    if (form != null)
    //    {
    //        www.uploadHandler = new UploadHandlerRaw(form.data);
    //        www.SetRequestHeader("Content-Type", form.headers["Content-Type"]);
    //    }
    //    else if (!string.IsNullOrEmpty(jsonData))
    //    {
    //        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
    //        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //        www.SetRequestHeader("Content-Type", "application/json");
    //    }

    //    yield return www.SendWebRequest();

    //    if (www.result != UnityWebRequest.Result.Success)
    //    {
    //        onFail?.Invoke(string.IsNullOrEmpty(www.downloadHandler.text) ? www.error : www.downloadHandler.text);
    //    }
    //    else
    //    {
    //        onSuccess?.Invoke(www.downloadHandler.text);
    //    }
    //}

    public static IEnumerator GetAudio(string url,AudioType audioType,UnityAction<AudioClip> onSuccess = null,UnityAction<string> onFail=null)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                onSuccess?.Invoke(audioClip);
            }
            else
            {
                onFail?.Invoke(www.error);
            }
        }
    }

#if UNITASK
    /// <summary>
    /// 异步获取音频
    /// </summary>
    /// <param name="url"></param>
    /// <param name="audioType"></param>
    /// <returns></returns>
    public static async UniTask<AudioClip> GetAudio(string url, AudioType audioType)
    {
        try
        {
            using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType);
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"get audio fail! url:{url} error:{www.error}");
                return null;
            }

            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
            return audioClip;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return null;
    }

    public static async UniTask<string> Require(
        string url, string method = "POST",
        Dictionary<string, string> queryParams = null,
        Dictionary<string, string> headers = null,
        WWWForm form = null,
        string jsonData = null)
    {
        if (queryParams != null && queryParams.Count > 0)
        {
            url += "?";
            foreach (var param in queryParams)
            {
                url += param.Key + "=" + param.Value + "&";
            }
            url = url.TrimEnd('&');
        }

        UnityWebRequest www = new UnityWebRequest(url, method);
        www.downloadHandler = new DownloadHandlerBuffer();

        // Headers
        if (headers != null)
        {
            foreach (var header in headers)
            {
                www.SetRequestHeader(header.Key, header.Value);
            }
        }

        if (form != null)
        {
            www.uploadHandler = new UploadHandlerRaw(form.data);
            www.SetRequestHeader("Content-Type", form.headers["Content-Type"]);
        }
        else if (!string.IsNullOrEmpty(jsonData))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.SetRequestHeader("Content-Type", "application/json");
        }

        try
        {
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                return string.IsNullOrEmpty(www.downloadHandler.text) ? www.error : www.downloadHandler.text;
            }

            return www.downloadHandler.text;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return string.Empty;
    }
#endif
}
