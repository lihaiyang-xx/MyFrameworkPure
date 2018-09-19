using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

public class FontTool
{
    private static string[] fontName_ch;

    private static string[] fontName_en;

    public static int GetIndexByFontName_en(string name)
    {
        for(int i = 0; i < FontName_EN.Length; i++)
        {
            if(FontName_EN[i] == name)
            {
                return i;
            }
        }
        return -1;
    }

    public static string[] FontName_CH
    {
        get
        {
            GetSystemFont();
            return fontName_ch;
        }
    }

    public static string[] FontName_EN
    {
        get
        {
            GetSystemFont();
            return fontName_en;
        }
    }

    public static UnityEngine.Font GetFontByName_ch(string name)
    {
        for(int i = 0;i < fontName_ch.Length; i++)
        {
            if (fontName_ch[i] == name)
            {
                return UnityEngine.Font.CreateDynamicFontFromOSFont(FontName_EN[i], 20);
            }
        }

        return null;
    }

    private static void GetSystemFont()
    {
        if (fontName_ch != null) return;
        List<string> list = new List<string>();
        fontName_en = UnityEngine.Font.GetOSInstalledFontNames();

        List<string> fne = new List<string>(fontName_en);

        for(int i = 0; i < fne.Count; i++)
        {
            try
            {
                FontFamily ff = new FontFamily(Path.GetFileNameWithoutExtension(fne[i]));
                list.Add(ff.Name);
            }
            catch
            {
                fne.RemoveAt(i);
                i--;
            }
        }
        fontName_en = fne.ToArray();
        fontName_ch = list.ToArray();
    }

}