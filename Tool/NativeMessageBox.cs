using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MyFrameworkPure
{
    public enum NativeMessageBoxType
    {
        MB_OK = 0x00000000,
        MB_OKCANCEL = 0x00000001,
        MB_ABORTRETRYIGNORE = 0x00000002,
        MB_YESNOCANCEL = 0x00000003,
        MB_YESNO = 0x00000004,
        MB_RETRYCANCEL = 0x00000005,
        MB_CANCELTRYCONTINUE = 0x00000006,
        MB_HELP = 0x00004000
    }
    public class NativeMessageBox
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr handle, String message, String title, uint type);
        public static void ShowMessage(string title, string message, NativeMessageBoxType type)
        {
            MessageBox(new IntPtr(0), message, title, (uint)type);
        }
#endif
    }
}

