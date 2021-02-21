using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace MyFrameworkPure
{
    /// <summary>
    /// 网络工具
    /// </summary>
    public class NetworkTool
    {
        /// <summary>
        /// 获取本机Ipv4地址,如果存在多个,获取第一个
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static string GetLocalIP()
        {
            return GetAllIP().FirstOrDefault();
        }

        /// <summary>
        /// 获取本机所有网卡的ipv4地址
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllIP()
        {
            try
            {
                IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                return ipe.AddressList.Where(x=>x.AddressFamily == AddressFamily.InterNetwork).Select(x => x.ToString()).ToArray();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return new string[]{};
        }

        /// <summary>
        /// 获取mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(x => x.OperationalStatus == OperationalStatus.Up)
                .Select(x => x.GetPhysicalAddress().ToString()).FirstOrDefault();
        }

        /// <summary>
        /// 获取公网ip地址
        /// </summary>
        /// <param name="onGet"></param>
        /// <returns></returns>
        public static IEnumerator GetPublicIp(UnityAction<string> onGet)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get("http://pv.sohu.com/cityjson?ie=utf-8"))
            {
                yield return webRequest.SendWebRequest();
                string result = string.Empty;
                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    Debug.Log(webRequest.error);
                }
                else
                {
                    result = webRequest.downloadHandler.text;
                    int first = result.IndexOf('{');
                    int end = result.IndexOf('}');
                    result = result.Substring(first, end - first + 1);
                }
                onGet?.Invoke(result);
            }
        }
    }
}

