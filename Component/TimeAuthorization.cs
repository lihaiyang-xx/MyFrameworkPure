using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 时间授权管理
    /// </summary>
    public class TimeAuthorization : MonoBehaviour
    {
        [SerializeField] private string expiryTime = "2099-12-31";
        private string filePath;
        private float checkInterval = 3f; // 时间检测间隔，单位为秒
        private float nextCheckTime;

        private void Start()
        {
            filePath = Application.persistentDataPath + "/time.txt"; // 文件路径

            // 在启动时进行时间检测
            CheckSystemTime();

            if (!AuthorizeTool.IsValid(expiryTime))
            {
                Debug.Log("授权过期!");
                CloseApplication();
            }
        }

        private void Update()
        {
            // 检查是否到达下一次时间检测的时间
            if (Time.time >= nextCheckTime)
            {
                CheckSystemTime();
                nextCheckTime = Time.time + checkInterval;
            }
        }

        private void CheckSystemTime()
        {
            // 获取当前系统时间
            DateTime currentTime = DateTime.Now;

            // 写入当前系统时间到文件
            WriteTimeToFile(currentTime);

            // 读取文件中的时间记录
            DateTime lastRecordedTime = ReadTimeFromFile();

            // 检查系统时间是否被更改
            bool isTimeChanged = (currentTime <= lastRecordedTime);

            if (isTimeChanged)
            {
                Debug.Log("系统时间已被更改！");
                CloseApplication();
            }
        }

        private void WriteTimeToFile(DateTime time)
        {
            // 追加记录到文件
            File.AppendAllText(filePath, time.ToString() + Environment.NewLine);
        }

        private DateTime ReadTimeFromFile()
        {
            if (File.Exists(filePath))
            {
                // 读取文件中的所有记录
                string[] timeRecords = File.ReadAllLines(filePath);

                // 获取最新的时间记录
                DateTime lastRecordTime = timeRecords.Length > 0 ? timeRecords.Select(DateTime.Parse).Max() : DateTime.Now;

                return lastRecordTime;
            }

            // 如果文件不存在或读取失败，则返回一个默认时间
            return DateTime.Now;
        }

        private void CloseApplication()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
