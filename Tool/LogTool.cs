using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MyFrameworkPure;
using UnityEngine;

public static class LogTool
{
    private static FileStream fs;

    public static void Init(string logDirectoryPath)
    {
        if (!Directory.Exists(logDirectoryPath))
            Directory.CreateDirectory(logDirectoryPath);
        string logFilePath = Path.Combine(logDirectoryPath, $"{TimeTool.GetTimeStr("yyyy-MM-dd HH-mm-ss")}.txt");
        fs = new FileStream(logFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        Application.logMessageReceived += WriteLogToFile;
    }

    static void WriteLogToFile(string condition, string stackTrace, LogType type)
    {
        string str = $"[{TimeTool.GetTimeStr("HH:mm:ss")}] [{type}] {condition} \r\n";
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        fs.Write(bytes, 0, bytes.Length);
        fs.Flush(true);
    }

    public static void Destroy()
    {
        fs?.Close();
    }
}
