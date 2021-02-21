using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    public class PageContainer : MonoBehaviour
    {
        [SerializeField] private Transform pageParent;

        [SerializeField] private Button nextBtn;

        [SerializeField] private Button lastBtn;

        [SerializeField] private Text currentPageText;

        private int currentPage;

        private int maxPage;

        public UnityAction<int> onPageChanged;
        // Use this for initialization
        void Start()
        {
            maxPage = pageParent.childCount;

            nextBtn.onClick.AddListener(() => ActivePage(currentPage + 1));

            lastBtn.onClick.AddListener(() => ActivePage(currentPage - 1));
        }

        void ActivePage(int index)
        {
            index = Mathf.Clamp(index, 0, maxPage - 1);
            if (index == currentPage)
                return;
            pageParent.GetChild(currentPage).gameObject.SetActive(false);
            pageParent.GetChild(index).gameObject.SetActive(true);
            currentPage = index;
            if (onPageChanged != null)
                onPageChanged(currentPage);
            currentPageText.text = (currentPage + 1).ToString();
        }
    }
}
