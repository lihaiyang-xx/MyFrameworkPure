using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

public class FileDialogTool
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;

    public void SetSaveType(string defExt, string filter = "All Files\0*.*\0\0")
    {
        this.structSize = Marshal.SizeOf(this);

        this.filter = filter;

        this.file = new string(new char[256]);

        this.maxFile = this.file.Length;

        this.fileTitle = new string(new char[64]);

        this.maxFileTitle = this.fileTitle.Length;

        this.initialDir = UnityEngine.Application.dataPath;//默认路径  

        this.title = "Open Project";

        this.defExt = defExt;//显示文件的类型  
                            //注意 一下项目不一定要全选 但是0x00000008项不要缺少  
        this.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  
    }

    public void SetOpenType(string defExt, string filter = "All Files\0*.*\0\0")
    {
        this.structSize = Marshal.SizeOf(this);

        this.filter = filter;

        this.file = new string(new char[256]);

        this.maxFile = this.file.Length;

        this.fileTitle = new string(new char[64]);

        this.maxFileTitle = this.fileTitle.Length;

        this.initialDir = UnityEngine.Application.dataPath;//默认路径  

        this.title = "Save Project";

        this.defExt = defExt;//显示文件的类型  
                             //注意 一下项目不一定要全选 但是0x00000008项不要缺少  
        this.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  
    }
}

public class FileDialog
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] FileDialogTool ofn);


    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] FileDialogTool ofn);

    public static bool OpenImage(out FileDialogTool ofn)
    {
        ofn = new FileDialogTool();
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "All Files\0*.*\0\0";
        ofn.file = new string(new char[256]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = Application.dataPath;//默认路径  
        ofn.title = "打开图像";
        //ofn.defExt = "JPG";
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        return FileDialog.GetOpenFileName(ofn);
    }


}