using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// ʱ����Ȩ����
    /// </summary>
    public class TimeAuthorization : MonoBehaviour
    {
        [SerializeField] private string expiryTime = "2099-12-31";
        private string filePath;
        private float checkInterval = 3f; // ʱ����������λΪ��
        private float nextCheckTime;

        private void Start()
        {
            filePath = Application.persistentDataPath + "/time.txt"; // �ļ�·��

            // ������ʱ����ʱ����
            CheckSystemTime();

            if (!AuthorizeTool.IsValid(expiryTime))
            {
                Debug.Log("��Ȩ����!");
                CloseApplication();
            }
        }

        private void Update()
        {
            // ����Ƿ񵽴���һ��ʱ�����ʱ��
            if (Time.time >= nextCheckTime)
            {
                CheckSystemTime();
                nextCheckTime = Time.time + checkInterval;
            }
        }

        private void CheckSystemTime()
        {
            // ��ȡ��ǰϵͳʱ��
            DateTime currentTime = DateTime.Now;

            // д�뵱ǰϵͳʱ�䵽�ļ�
            WriteTimeToFile(currentTime);

            // ��ȡ�ļ��е�ʱ���¼
            DateTime lastRecordedTime = ReadTimeFromFile();

            // ���ϵͳʱ���Ƿ񱻸���
            bool isTimeChanged = (currentTime <= lastRecordedTime);

            if (isTimeChanged)
            {
                Debug.Log("ϵͳʱ���ѱ����ģ�");
                CloseApplication();
            }
        }

        private void WriteTimeToFile(DateTime time)
        {
            // ׷�Ӽ�¼���ļ�
            File.AppendAllText(filePath, time.ToString() + Environment.NewLine);
        }

        private DateTime ReadTimeFromFile()
        {
            if (File.Exists(filePath))
            {
                // ��ȡ�ļ��е����м�¼
                string[] timeRecords = File.ReadAllLines(filePath);

                // ��ȡ���µ�ʱ���¼
                DateTime lastRecordTime = timeRecords.Length > 0 ? timeRecords.Select(DateTime.Parse).Max() : DateTime.Now;

                return lastRecordTime;
            }

            // ����ļ������ڻ��ȡʧ�ܣ��򷵻�һ��Ĭ��ʱ��
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
