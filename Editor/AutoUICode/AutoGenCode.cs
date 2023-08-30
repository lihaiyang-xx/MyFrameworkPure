using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using MyFrameworkPure;
using TMPro;
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
        {"sld",typeof(Slider)},
        {"dd",typeof(Dropdown)},
        {"tlab",typeof(TMP_Text)},
        {"tinput",typeof(TMP_InputField)},
        {"tdd",typeof(TMP_Dropdown)}
    };

    private const string ViewTemplate = "Assets/3rdParts/MyFrameworkPure/Editor/AutoUICode/ViewTemplate.txt";
    private const string ControllerTemplate = "Assets/3rdParts/MyFrameworkPure/Editor/AutoUICode/ControllerTemplate.txt";
    private const string LogicTemplate = "Assets/3rdParts/MyFrameworkPure/Editor/AutoUICode/LogicTemplate.txt";
    private const string ViewScriptPath = "Assets/Scripts/AutoGen/View";
    private const string ControllerScriptPath = "Assets/Scripts/AutoGen/Controller";
    private const string LogicScriptPath = "Assets/Scripts/AutoGen/Logic";
    private const string UsingTMP = "using TMPro;";

    [MenuItem("GameObject/AutoGen/Create View", priority = 0)]
    static void CreateControllerAndView()
    {
        if (!Directory.Exists(ViewScriptPath))
            Directory.CreateDirectory(ViewScriptPath);
        if (!Directory.Exists(ControllerScriptPath))
            Directory.CreateDirectory(ControllerScriptPath);
        if (!Directory.Exists(LogicScriptPath))
            Directory.CreateDirectory(LogicScriptPath);
        GameObject[] gos = Selection.gameObjects;
        foreach (GameObject go in gos)
        {
            string goName = go != null ? go.name : string.Empty;
            if (go == null || !(goName.StartsWith("Panel_") || goName.StartsWith("LP_")))
            {
                EditorUtility.DisplayDialog("提示", $"没有选中panel,选中的物体名称为{goName}", "确定");
                Debug.Log("没有选中panel");
                continue;
            }

            string className = goName.StartsWith("Panel_")
                ? goName.Replace("Panel_", "")
                : goName.Replace("LP_", string.Empty);

            //生成view.cs
            string viewClassName = className + "View";
            string hierarchy = go.transform.GetHierarchyPath();
            string content = File.ReadAllText(ViewTemplate);
            StringBuilder fieldSb = new StringBuilder();
            StringBuilder methodSb = new StringBuilder($"\t\ttransform = GameObjectTool.FindGameObjectQuick(\"{hierarchy}\").transform;\r\n");
            StringBuilder usingSb = new StringBuilder();
            List<ComponentInfo> infos = new List<ComponentInfo>();
            GetComponentInfos(go.transform, infos, string.Empty);
            foreach (var info in infos)
            {
                fieldSb.AppendLine($"\tpublic {info.type} {info.name}{{get;set;}}");
                methodSb.AppendLine($"\t\t{info.name} = transform.Find(\"{info.path}\").GetComponent<{info.type}>();");
                if (info.type.Contains("TMP_") && !usingSb.ToString().Contains(UsingTMP))
                    usingSb.AppendLine(UsingTMP);
            }
            content = content.Replace("#CLASSNAME#", viewClassName).
                Replace("#FIELD#", fieldSb.ToString()).
                Replace("#METHOD#", methodSb.ToString()).
                Replace("#USING#", usingSb.ToString());

            string path = $"{ViewScriptPath}/{viewClassName}.cs";
            Debug.Log(path);
            File.WriteAllText(path, content);

            //生成controller.cs
            if (goName.StartsWith("Panel_"))
            {
                string controllerClassName = className + "Controller";
                path = $"{ControllerScriptPath}/{controllerClassName}.cs";
                if (!File.Exists(path))
                {
                    string field = $"\tpublic {viewClassName} view;";
                    string method = $"\t\tview = new {viewClassName}();\r\n\t\tbaseView = view;\r\n";
                    content = File.ReadAllText(ControllerTemplate);
                    content = content.Replace("#CLASSNAME#", controllerClassName).
                        Replace("#FIELD#", field).
                        Replace("#METHOD#", method);
                    File.WriteAllText(path, content);
                }
            }
            else if (goName.StartsWith("LP_"))
            {
                string logicClassName = className + "Logic";
                path = $"{LogicScriptPath}/{logicClassName}.cs";
                if (!File.Exists(path))
                {
                    string field = $"\tpublic {viewClassName} view;";
                    string method = $"\t\tview = new {viewClassName}();\r\n";
                    content = File.ReadAllText(LogicTemplate);
                    content = content.Replace("#CLASSNAME#", logicClassName).
                        Replace("#FIELD#", field).
                        Replace("#METHOD#", method);
                    File.WriteAllText(path, content);
                }
            }
        }
        AssetDatabase.Refresh();
    }

    static void GetComponentInfos(Transform tr,List<ComponentInfo> infos,string path)
    {
        foreach (Transform child in tr)
        {
            if((child.name.StartsWith("Panel_") || child.name.StartsWith("LP_")) && !string.IsNullOrEmpty(path))//不绑定子panel的元素
                continue;
            string childPath = string.IsNullOrEmpty(path) ? child.name : $"{path}/{child.name}";
            string prefix = child.name.GetPrefix('_');
            if (dict.ContainsKey(prefix))
            {
                ComponentInfo info = new ComponentInfo();
                info.path = childPath;

                string type = dict[prefix].Name;
                if (prefix == "lab" || prefix == "input" || prefix == "dd")
                {
                    if (child.GetComponent("TMP_"+type) != null)
                        type = "TMP_" + type;
                }
                info.type = type;
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