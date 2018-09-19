using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetworkTool
{
    /// <summary>
    /// 获取本机Ipv4地址,如果存在多个,获取第一个
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once InconsistentNaming
    public static string GetLocalIP()
    {
        string localIP = string.Empty;
        try
        {
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipe.AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            localIP = ipAddr.ToString();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            localIP = string.Empty;
        }
        
        return localIP;
    }
}
