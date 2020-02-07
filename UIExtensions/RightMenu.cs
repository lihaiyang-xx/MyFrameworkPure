/*
	功能描述：
	
	时间：
	
	作者：李海洋
*/

using MyFrameworkPure;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class RightMenu : MonoBehaviour
{

    private static Transform ktransform;

    void Awake()
    {
        ktransform = transform;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
            {
                Destroy(gameObject);
            }
        }
	}

    public static void Show(Vector3 position, string[] btnlist,float btnWidth,float btnHeight,params UnityAction[] dosomethings)
    {
        Debug.Assert(btnlist.Length == dosomethings.Length, "按钮数量同事件数量不匹配！");
        if(ktransform != null)
        {
            Destroy(ktransform.gameObject);
        }
        GameObject go = ResourceTool.Instantiate("UI/RightMenu", GameObject.Find("Overlay").transform);
        go.transform.position = position;
        Transform templateBtn = go.transform.GetChild(0);
        templateBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(btnWidth, btnHeight);
        int i = 0;
        foreach(string btn in btnlist)
        {
            Transform tr = Instantiate(go.transform.GetChild(0), ktransform);
            tr.gameObject.SetActive(true);
            tr.GetChild(0).GetComponent<Text>().text = btn;
            tr.GetComponent<Button>().onClick.AddListener(dosomethings[i++]);
            tr.GetComponent<Button>().onClick.AddListener(() => { Destroy(ktransform.gameObject); });
        }
    }
}
