using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Xml;
using UnityEngine .UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public static class Util
{

   

    

    public static long GetTime()
    {
        TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
        return (long)ts.TotalMilliseconds;
    }

    /// <summary>
    /// 搜索子物体组件-GameObject版
    /// </summary>
    public static T Get<T>(GameObject go, string subnode) where T : Component
    {
        if (go != null)
        {
            Transform sub = go.transform.Find(subnode);
            if (sub != null) return sub.GetComponent<T>();
        }
        return null;
    }



    /// <summary>
    /// 搜索子物体组件-Component版
    /// </summary>
    public static T Get<T>(Component go, string subnode) where T : Component
    {
        return go.transform.Find(subnode).GetComponent<T>();
    }

    


    /// <summary>
    /// 扩展方法 查找所有父物体
    /// 若未找到，返回-1
    /// 若找到，返回相隔层数（临近子物体于其父物体相隔1层）
    /// </summary>
    /// <param name="self"></param>
    /// <param name="parentName"></param>
    /// <returns></returns>
    public static int FindParents(this Transform self,string parentName)
    {
        Transform t = null;
        return self.FindParents(parentName, out t);
    }
   
    public static int FindParents(this Transform self, Transform parent)
    {
        Transform t = null;
        return self.FindParents(parent, out t);
    }
    public static int FindParents(this Transform self, Transform parent, out Transform NearChild)
    {
        NearChild = null;
        Transform t = self;
        int level = 0;
        while (t.parent != null)
        {
            level++;
            t = t.parent;
            if (t.Equals(parent))
            {
                return level;
            }
            NearChild = t;
        }
        return -1;
    }


    /// <summary>
    /// 查找子对象
    /// </summary>
    public static GameObject Child(GameObject go, string subnode)
    {
        return Child(go.transform, subnode);
    }

    /// <summary>
    /// 查找子对象
    /// </summary>
    public static GameObject Child(Transform go, string subnode)
    {
        Transform tran = go.Find(subnode);
        if (tran == null) return null;
        return tran.gameObject;
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    public static GameObject Peer(GameObject go, string subnode)
    {
        return Peer(go.transform, subnode);
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    public static GameObject Peer(Transform go, string subnode)
    {
        Transform tran = go.parent.Find(subnode);
        if (tran == null) return null;
        return tran.gameObject;
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <returns></returns>
    public static T Peer<T>(this Transform self)
    {
        foreach (Transform trans in self.parent)
        {
            T t = trans.GetComponent<T>();
            if (t != null)
            {
                return t;
            }
        }
        return default(T);
    }

    /// <summary>
    /// 手机震动
    /// </summary>
    public static void Vibrate()
    {
        //int canVibrate = PlayerPrefs.GetInt(Const.AppPrefix + "Vibrate", 1);
        //if (canVibrate == 1) iPhoneUtils.Vibrate();
    }



    

 

    /// <summary>
    /// HashToMD5Hex
    /// </summary>
    public static string HashToMD5Hex(string sourceStr)
    {
        byte[] Bytes = Encoding.UTF8.GetBytes(sourceStr);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] result = md5.ComputeHash(Bytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                builder.Append(result[i].ToString("x2"));
            return builder.ToString();
        }
    }

    

    /// <summary>
    /// 清除所有子节点
    /// </summary>
  







    /// <summary>
    /// 网络可用
    /// </summary>
    public static bool NetAvailable
    {
        get
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }

    /// <summary>
    /// 是否是无线
    /// </summary>
    public static bool IsWifi
    {
        get
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }
    }

    

    


    public static void Log(string str)
    {
        #if UNITY_EDITOR
                Debug.Log(str);
        #endif
    }

    public static void LogWarning(string str)
    {
        #if UNITY_EDITOR
                Debug.LogWarning(str);
        #endif
    }

    public static void LogError(string str)
    {
        Debug.LogError(str);
    }

    /// <summary>
    /// 是不是苹果平台
    /// </summary>
    /// <returns></returns>
    public static bool isApplePlatform
    {
        get
        {
            return Application.platform == RuntimePlatform.IPhonePlayer ||
                   Application.platform == RuntimePlatform.OSXEditor ||
                   Application.platform == RuntimePlatform.OSXPlayer;
        }
    }

    

    public static string DataPath
    {
        get { return Application.streamingAssetsPath; }
    }


    




    

    
}

public class ReadXml
{

    /// <summary>
    /// XML 文档实例
    /// </summary>
    XmlDocument xmlDocument;

    public ReadXml()
    {
        xmlDocument = new XmlDocument();
    }

    #region 数据加载
    /// <summary>
    /// 加载指定路径的xml文件
    /// </summary>
    public bool Load(string filePath)
    {
        if (xmlDocument == null)
        {
            throw new ArgumentNullException("xmlDocument");
        }

        if (filePath == null || filePath.Length == 0)
        {
            throw new ArgumentNullException("filePath");
        }
        try
        {
            xmlDocument.Load(filePath);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        return true;
    }

    /// <summary>
    /// 加载 XML的文本文件
    /// </summary>
    /// <param name="xmlFile"></param>
    public bool LoadXml(string xmlFile)
    {
        if (xmlDocument == null)
        {
            throw new ArgumentNullException("xmlDocument");
        }

        if (xmlFile == null || xmlFile.Length == 0)
        {
            throw new ArgumentNullException("xmlFile");
        }
        TextReader tr = new StringReader(xmlFile);
        xmlDocument.Load(tr);

        return true;
    }
    #endregion

    public void Remove()
    {
        xmlDocument.RemoveAll();
    }

    public XmlNodeList getNodeList(string path)
    {
        try
        {
            XmlNodeList childNodeList = xmlDocument.DocumentElement.SelectSingleNode(path).ChildNodes;
            return childNodeList;
        }
        catch (Exception ex)
        {
            Debug.LogError("error: getNodeList:" + path + "//" + ex);
            return null;
        }
    }

    public string getValue(string path)
    {
        string[] splitStr = path.Split('/');
        XmlNode childNode = null;
        try
        {
            foreach (string str in splitStr)
            {

                if (childNode == null)
                {
                    childNode = xmlDocument.DocumentElement.SelectSingleNode(str);
                }
                else
                {
                    childNode = childNode.SelectSingleNode(str);
                }
            }
            return childNode.InnerText;
        }
        catch (Exception ex)
        {
            Debug.LogError("error: getValue: " + path + "   " + ex);
            return "";
        }

    }
}

public static class IOExtension
{
    public static string GetUpLevelPath(string path)
    {
        int index1 = path.LastIndexOf('/');
        int index2 = path.LastIndexOf("\\");

        int index = (index1 > index2) ? index1 : index2;

        return path.Remove(index);

    }

    public static int GetPathDepth(string path)
    {
        int num1 = Regex.Matches(path, @"/").Count;
        int num2 = Regex.Matches(path, @"\").Count;

        return num1 + num2;
    }

    public static void GetFileSystemEntries(string path,ref string[] paths)
    {
        string[] strs = Directory.GetFileSystemEntries(path);
        paths = CollectionsTool.Merge<string>(paths, strs);
        foreach(string str in strs)
        {
            if(Directory.Exists(str))
            {
                GetFileSystemEntries(str,ref paths);
            }
        }
    }
}

