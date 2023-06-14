using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyFrameworkPure;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SplitPagePanel : MonoBehaviour
{
    [SerializeField] private int numPerPage = 12;
    [SerializeField] private Transform content;
    [SerializeField] private Transform pageNumBar;
    [SerializeField] private Transform pageNumTemplate;
    [SerializeField] private Transform selectBg;
    [SerializeField] private Button nextPageBtn;
    [SerializeField] private Button prePageBtn;

    private int totalItemNum;
    private int totalPage;
    private int currentPage = 1;

    private object[] dataSource;

    public UnityAction<object[]> onRefreshContent;

    public Transform Content => content;

    public void SetDataSource(object[] source,bool resetBar = false)
    {
        dataSource = source;
        totalItemNum = dataSource.Length;
        RefreshBar(resetBar);
        RefreshContent();
    }

    void Start()
    {
        prePageBtn?.onClick.AddListener(()=> 
        { 
            SetCurrentPage(currentPage - 1);
        });

        nextPageBtn?.onClick.AddListener(() =>
        {
            SetCurrentPage(currentPage + 1);
        });
    }

    void RefreshBar(bool reset)
    {
        pageNumBar.TraversalChild(x=>x.SetActive(false));
        totalPage = (totalItemNum - 1) / numPerPage + 1;
        currentPage = reset?1:Mathf.Clamp(currentPage,1,totalPage);
        for (int i = 1; i <= totalPage; i++)
        {
            InstantiatePageBtn(i);
        }
        pageNumBar.Find(currentPage.ToString()).GetComponent<Button>().onClick.Invoke();
    }

    public void RefreshContent()
    {
        int startIndex = (currentPage - 1) * numPerPage;
        int count = startIndex + numPerPage <= totalItemNum ? numPerPage : totalItemNum % numPerPage;
        object[] source = dataSource.GetRange(startIndex, count);
        onRefreshContent?.Invoke(source);
    }

    void InstantiatePageBtn(int pageNum)
    {
        string pageName = pageNum.ToString();
        Transform btnTransform = pageNumBar.Find(pageName);
        if (!btnTransform)
        {
            btnTransform = Instantiate(pageNumTemplate, pageNumBar);
            btnTransform.SetSiblingIndex(pageNum);
            btnTransform.name = pageNum.ToString();
            btnTransform.GetComponentInChildren<Text>().text = pageName;
            Button btn = btnTransform.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                SetCurrentPage(pageNum);
            });
        }
        btnTransform.SetActive(true);
    }

    void SetCurrentPage(int page)
    {
        currentPage = Mathf.Clamp(page,1,totalPage);
        selectBg.SetParent(pageNumBar.Find(currentPage.ToString()));
        selectBg.SetAsFirstSibling();
        selectBg.localPosition = Vector3.zero;
        selectBg.SetActive(true);
        RefreshContent();
    }
}

