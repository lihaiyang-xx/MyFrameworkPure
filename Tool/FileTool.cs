/*
	功能描述：
	
	时间：
	
	作者：李海洋
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MyFrameworkPure
{
    public static class FileTool
    {
        /// <summary>
        /// 以UTF8编码读取文件内容
        /// </summary>
        /// <param equimpentName="path"></param>
        /// <returns></returns>
        public static string ReadAllText(string path)
        {
            return ReadAllText(path, Encoding.UTF8);
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        public static string ReadAllText(string path, Encoding encoding)
        {
            string text = "";
            try
            {
                text = File.ReadAllText(path, encoding);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return text;
        }

        public static void ReadTextFromUrl(string url, Encoding encoding,Action<string> action)
        {
            CoroutineTool.DoCoroutine(AsyncReadAllText(url, encoding, action));
        }

        public static IEnumerator AsyncReadAllText(string path, Encoding encoding, Action<string> action)
        {
            using (WWW www = new WWW(path))
            {
                yield return www;
                if (string.IsNullOrEmpty(www.error))
                {
                    string text = encoding.GetString(www.bytes);
                    if (action != null) action(text);
                }
                else
                {
                    Debug.LogError(www.error);
                }
            }
        }

        /// <summary>
        /// 写入文件,如果文件存在则覆盖,不存在则创建
        /// </summary>
        /// <param equimpentName="path"></param>
        /// <param equimpentName="contents"></param>
        public static void WriteAllText(string path, string contents)
        {
            try
            {
                File.WriteAllText(path, contents);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void ReadTextureFromUrl(string url, Action<Texture2D> action)
        {
            CoroutineTool.DoCoroutine(AsyncReadTextureFromUrl(url, action));
        }

        public static IEnumerator AsyncReadTextureFromUrl(string url, Action<Texture2D> action)
        {
            Debug.Log(url);
            using (WWW www = new WWW(url))
            {
                yield return www;
                if (string.IsNullOrEmpty(www.error))
                {
                    Texture2D texture = new Texture2D(0, 0);
                    texture.LoadImage(www.bytes);
                    if (action != null) action(texture);
                }
                else
                {
                    Debug.LogError(www.error);
                }
            }
        }

        /// <summary>
        /// 读取贴图文件
        /// </summary>
        /// <param equimpentName="path"></param>
        /// <returns></returns>
        public static Texture2D ReadTextureFromFile(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                Debug.LogError(path + "不存在！");
                return null;
            }
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(0, 0);
            texture.LoadImage(bytes);
            return texture;
        }

        public static void ReadAudioFromUrl(string url, Action<AudioClip> action)
        {
            CoroutineTool.DoCoroutine(AsyncReadAudioFromUrl(url, action));
        }

        public static IEnumerator AsyncReadAudioFromUrl(string url,Action<AudioClip> action)
        {
            using (WWW www = new WWW(url))
            {
                yield return www;
                if (string.IsNullOrEmpty(www.error))
                {
                    if (action != null) action(www.GetAudioClip(false));
                }
                else
                {
                    Debug.LogError(www.error);
                }
            }
        }

        /// <summary>
        /// 获取文件编码
        /// </summary>
        /// <param equimpentName="filename"></param>
        /// <returns></returns>
        public static Encoding GetEncoding(string filename)
        {
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            return Encoding.ASCII;
        }
    }

}

