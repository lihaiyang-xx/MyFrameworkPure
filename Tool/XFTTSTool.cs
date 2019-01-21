#if UNITY_EDITOR || UNITY_STANDALONE_WIN


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using UnityEngine;
using XFTTS;

public class XFTTSTool
{
    /// <summary>
    /// 登录讯飞
    /// </summary>
    /// <param name="config">讯飞上注册的ID</param>
    /// <returns></returns>
    public static int Login(string config = "appid = 5c05f7b4")
    {
        int ret = TTS.MSPLogin(null, null, config);//第一个参数为用户名，第二个参数为密码，第三个参数是登录参数
        Debug.Log(ret == (int)ErrorCode.MSP_SUCCESS?"登录成功":"登录失败:" + ret);
        return ret;
    }

    /// <summary>
    /// 文字转语音
    /// </summary>
    /// <param name="text">文字</param>
    /// <param name="path">存储语音的路径</param>
    /// <param name="args">语音格式</param>
    public static void TextToSpeech(string text, string path, string args = "voice_name = xiaoyan, text_encoding = utf8, sample_rate = 16000, speed = 50, volume = 50, pitch = 50, rdn = 2")
    {
        IntPtr sessionId = IntPtr.Zero;
        try
        {
            int ret = 0;
            sessionId = TTS.QTTSSessionBegin(args, ref ret);
            if (ret != (int) ErrorCode.MSP_SUCCESS)
            {
                Debug.Log("QTTSSessionBegin--错误码:" + ret);
                return;
            }

            ret = TTS.QTTSTextPut(Ptr2Str(sessionId), text, (uint) Encoding.UTF8.GetByteCount(text), string.Empty);
            if (ret != (int) ErrorCode.MSP_SUCCESS)
            {
                Debug.Log("QTTSTextPut--错误码:" + ret);
                return;
            }

            uint audioLen = 0;
            SynthStatus synthStatus = SynthStatus.MSP_TTS_FLAG_STILL_HAVE_DATA;

            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(new byte[44], 0, 44);
            while (true)
            {
                IntPtr source = TTS.QTTSAudioGet(Ptr2Str(sessionId), ref audioLen, ref synthStatus, ref ret);
                if (source == IntPtr.Zero)
                    continue;
                byte[] array = new byte[(int) audioLen];
                if (audioLen > 0)
                {
                    Marshal.Copy(source, array, 0, (int) audioLen);
                }

                memoryStream.Write(array, 0, array.Length);
                //Thread.Sleep(10);
                if (synthStatus == SynthStatus.MSP_TTS_FLAG_DATA_END || ret != 0)
                    break;
            }

            WavHeader wavHeader = GetWavHeader((int) memoryStream.Length - 44);
            byte[] array2 = StructToBytes(wavHeader);
            memoryStream.Position = 0L;
            memoryStream.Write(array2, 0, array2.Length);
            memoryStream.Position = 0L;

            if (path != null)
            {
                FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                memoryStream.WriteTo(fileStream);
                memoryStream.Close();
                fileStream.Close();
            }

            Debug.Log("转换完成!文本内容:" + text);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            TTS.QTTSSessionEnd(Ptr2Str(sessionId), "");
        }
        
    }

    /// <summary>
    /// 退出讯飞
    /// </summary>
    /// <returns></returns>
    public static int Logout()
    {
        int ret = TTS.MSPLogout();
        Debug.Log(ret == 0?"退出成功": "退出失败:" + ret);
        return ret;
    }

    private static byte[] StructToBytes(object structure)
    {
        int num = Marshal.SizeOf(structure);
        IntPtr intPtr = Marshal.AllocHGlobal(num);
        byte[] result;
        try
        {
            Marshal.StructureToPtr(structure, intPtr, false);
            byte[] array = new byte[num];
            Marshal.Copy(intPtr, array, 0, num);
            result = array;
        }
        finally
        {
            Marshal.FreeHGlobal(intPtr);
        }
        return result;
    }

    /// <summary>
    /// 结构体初始化赋值
    /// </summary>
    /// <param name="dataLen"></param>
    /// <returns></returns>
    private static  WavHeader GetWavHeader(int dataLen)
    {
        return new WavHeader
        {
            RIFF_ID = 1179011410,
            File_Size = dataLen + 36,
            RIFF_Type = 1163280727,
            FMT_ID = 544501094,
            FMT_Size = 16,
            FMT_Tag = 1,
            FMT_Channel = 1,
            FMT_SamplesPerSec = 16000,
            AvgBytesPerSec = 32000,
            BlockAlign = 2,
            BitsPerSample = 16,
            DATA_ID = 1635017060,
            DATA_Size = dataLen
        };
    }

    private static string Ptr2Str(IntPtr p)
    {
        return Marshal.PtrToStringAnsi(p);
    }

    /// <summary>
    /// 语音音频头
    /// </summary>
    private struct WavHeader
    {
        public int RIFF_ID;
        public int File_Size;
        public int RIFF_Type;
        public int FMT_ID;
        public int FMT_Size;
        public short FMT_Tag;
        public ushort FMT_Channel;
        public int FMT_SamplesPerSec;
        public int AvgBytesPerSec;
        public ushort BlockAlign;
        public ushort BitsPerSample;
        public int DATA_ID;
        public int DATA_Size;
    }
}
#endif