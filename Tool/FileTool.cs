using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace MyFrameworkPure
{
    /// <summary>
    /// 文件处理工具
    /// </summary>
    public static class FileTool
    {
        /// <summary>
        /// 以UTF8编码读取文件内容
        /// </summary>
        /// <param equimpentName="path"></param>
        /// <returns></returns>
        public static string ReadAllText(string path)
        {
            if (path.StartsWith(Application.streamingAssetsPath) && Application.platform == RuntimePlatform.Android)
            {
                UnityWebRequest www = UnityWebRequest.Get(path);
                www.SendWebRequest();
                while (!www.isDone) { }
                return www.downloadHandler.text;
            }
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


        public static string[] ReadAllLines(string path)
        {
            string[] lines = new string[] { };
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (Exception e)
            {
               Debug.LogException(e);
            }
            return lines;
        }

        public static string[] SafeReadAllLines(String path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs,Encoding.UTF8))
            {
                List<string> file = new List<string>();
                
                while (!sr.EndOfStream)
                {
                    file.Add(sr.ReadLine());
                }
                return file.ToArray();
            }
        }

        

        /// <summary>
        /// 从远程地址读取文本信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="action"></param>
        public static void ReadTextFromUrl(string url, Encoding encoding,Action<string> action)
        {
            CoroutineTool.DoCoroutine(AsyncReadAllText(url, encoding, action));
        }

        /// <summary>
        /// 异步读取文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <param name="action"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 远程读取图片信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <param name="onError"></param>
        public static void ReadTextureFromUrl(string url, Action<Texture2D> action,Action<string> onError = null)
        {
            CoroutineTool.DoCoroutine(AsyncReadTextureFromUrl(url, action,onError));
        }

        /// <summary>
        /// 异步读取图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <param name="onError"></param>
        /// <returns></returns>
        public static IEnumerator AsyncReadTextureFromUrl(string url, Action<Texture2D> action,Action<string> onError = null)
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
                    onError?.Invoke(www.error);
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
                Debug.LogWarning(path + "不存在！");
                return null;
            }
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(0, 0);
            texture.LoadImage(bytes);
            return texture;
        }

        /// <summary>
        /// 读取远程音频信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        public static void ReadAudioFromUrl(string url, Action<AudioClip> action)
        {
            CoroutineTool.DoCoroutine(AsyncReadAudioFromUrl(url, action));
        }

        public static IEnumerator AsyncReadAudioFromUrl(string url,Action<AudioClip> action)
        {
            Debug.Log(url);
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

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="newFileName"></param>
        public static void Rename(string fileName, string newFileName)
        {
            FileInfo fi = new FileInfo(fileName);
            fi.MoveTo(newFileName);
        }
        public static string[,] ReadCsvData(string path)
        {
            string str = ReadAllText(path);
            string[] lines = str.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[,] data = ReadCsvFromLines(lines);
            return data;
        }

        public static string[,] ReadCsvData(byte[] bytes)
        {
            return ReadCsvData(bytes, Encoding.UTF8);
        }

        public static string[,] ReadCsvData(byte[] bytes,Encoding encoding)
        {
            string[,] data = new string[,] { };
            try
            {
                string str = encoding.GetString(bytes);
                string[] lines = str.Split(new []{"\r\n"}, StringSplitOptions.RemoveEmptyEntries); ;
                data = ReadCsvFromLines(lines);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            return data;
        }

        static string[,] ReadCsvFromLines(string[] lines)
        {
            Regex regex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            int row = lines.Length;
            int col = lines.Max(x => regex.Split(x).Length);
            string[,] data = new string[row, col];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] splits = regex.Split(lines[i]);
                for (int j = 0; j < splits.Length; j++)
                {
                    string str = splits[j];
                    if (str.StartsWith("\"") && str.EndsWith("\""))//去除分割后的双引号
                    {
                        str = str.Substring(1, str.Length - 2);
                    }
                    data[i, j] = str;
                }
            }

            return data;
        }
    }

}

