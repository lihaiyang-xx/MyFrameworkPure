using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using MyFrameworkPure;
using UnityEngine.UI;

/// <summary>
/// 自动绑定UI
/// </summary>
public class AutoGenCode
{
    private static Dictionary<string,Type> dict = new Dictionary<string,Type>
    {
        {"img",typeof(Image)},
        {"btn",typeof(Button)},
        {"lab",typeof(Text)},
        {"tr",typeof(Transform)},
        {"sv",typeof(ScrollRect)},
        {"input",typeof(InputField)},
        {"go",typeof(GameObject)},
        {"tog",typeof(Toggle) },
        {"sld",typeof(Slider)}

    };

    private const string ViewTemplate = "Assets/3rdParts/MyFrameworkPure/Editor/AutoUICode/ViewTemplate.txt";
    private const string ControllerTemplate = "Assets/3rdParts/MyFrameworkPure/Editor/AutoUICode/ControllerTemplate.txt";
    private const string ViewScriptPath = "Assets/Scripts/AutoGen/View";
    private const string ControllerScriptPath = "Assets/Scripts/AutoGen/Controller";

    [MenuItem("GameObject/AutoGen/Create View", priority = 0)]
    static void CreateControllerAndView()
    {
        if (!Directory.Exists(ViewScriptPath))
            Directory.CreateDirectory(ViewScriptPath);
        if (!Directory.Exists(ControllerScriptPath))
            Directory.CreateDirectory(ControllerScriptPath);
        GameObject go = Selection.activeGameObject;
        if (go == null || !go.name.StartsWith("Panel_"))
        {
            Debug.Log("没有选中panel");
            return;
        }

        string hierarchy = go.transform.GetHierarchyPath();
        string content = File.ReadAllText(ViewTemplate);

        StringBuilder fieldSb = new StringBuilder();
        StringBuilder methodSb = new StringBuilder($"\t\ttransform = GameObjectTool.FindGameObjectQuick(\"{hierarchy}\").transform;\r\n");
        List<ComponentInfo> infos = new List<ComponentInfo>();
        GetComponentInfos(go.transform,infos,string.Empty);
        foreach (var info in infos)
        {
            fieldSb.AppendLine($"\tpublic {info.type} {info.name}{{get;set;}}");
            methodSb.AppendLine($"\t\t{info.name} = transform.Find(\"{info.path}\").GetComponent<{info.type}>();");
        }

        string panelClassName = go.name.Replace("Panel_", "")+"View";
        content = content.Replace("#CLASSNAME#", panelClassName).
            Replace("#FIELD#", fieldSb.ToString()).
            Replace("#METHOD#", methodSb.ToString());

        string path = $"{ViewScriptPath}/{panelClassName}.cs";
        Debug.Log(path);
        File.WriteAllText(path,content);

        //生成controller.cs
        string controllerClassName = go.name.Replace("Panel_", "")+"Controller";
        path = $"{ControllerScriptPath}/{controllerClassName}.cs";
        if (!File.Exists(path))
        {
            string field = $"\tpublic {panelClassName} view;";
            string method = $"\t\tview = new {panelClassName}();\r\n\t\tbaseView = view;\r\n";
            content = File.ReadAllText(ControllerTemplate);
            content = content.Replace("#CLASSNAME#", controllerClassName).
                Replace("#FIELD#", field).
                Replace("#METHOD#", method);
            File.WriteAllText(path,content);
        }
        
        AssetDatabase.Refresh();
    }

    static void GetComponentInfos(Transform tr,List<ComponentInfo> infos,string path)
    {
        foreach (Transform child in tr)
        {
            if(child.name.StartsWith("Panel_") && !string.IsNullOrEmpty(path))//不绑定子panel的元素
                continue;
            string childPath = string.IsNullOrEmpty(path) ? child.name : $"{path}/{child.name}";
            string prefix = child.name.GetPrefix('_');
            if (dict.ContainsKey(prefix))
            {
                ComponentInfo info = new ComponentInfo();
                info.path = childPath;
                info.type = dict[prefix].Name;
                info.name = child.name;
                infos.Add(info);
            }

            if (child.childCount > 0)
            {
                GetComponentInfos(child,infos,childPath);
            }
        }
    }
}

public class ComponentInfo
{
    public string path;
    public string type;
    public string name;
}