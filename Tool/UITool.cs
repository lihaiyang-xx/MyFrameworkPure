using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

#if DoTween
using DG.Tweening;
#endif
#if TMPro
using TMPro;
#endif

namespace MyFrameworkPure
{
    public class UITool
    {
        #region UI Animation
#if DoTween
        public static void AnimationOut(GameObject go, float duration, UnityAction onComplete)
        {
            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    FadeOut(go, duration, onComplete);
                    break;
                case 1:
                    ScaleOut(go, duration, onComplete);
                    break;
                case 2:
                    MoveOut(go, duration, onComplete);
                    break;
            }
        }

        public static void AnimationIn(GameObject go, float duration, UnityAction onComplete)
        {
            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    FadeIn(go, duration, onComplete);
                    break;
                case 1:
                    ScaleIn(go, duration, onComplete);
                    break;
                case 2:
                    MoveIn(go, duration, onComplete);
                    break;
            }
        }

        public static void OnFillamount(GameObject go, float duration, UnityAction onComplete)
        {
            var fill = go.GetComponent<Image>();
            if (!fill) return;
            fill.fillAmount = 0;
            fill.DOFillAmount(1, duration).onComplete += () =>
            {
                if (onComplete != null) onComplete();
            };
        }

        public static void OutFillamount(GameObject go, float duration, UnityAction onComplete)
        {
            var fill = go.GetComponent<Image>();
            if (!fill) return;
            fill.fillAmount = 1;
            fill.DOFillAmount(0, duration).onComplete += () =>
            {
                if (onComplete != null) onComplete();
            };
        }

        public static void FadeOut(GameObject go, float duration, UnityAction onComplete)
        {
            CanvasGroup canvasGroup = go.GetComponent<CanvasGroup>();
            if (!canvasGroup)
                canvasGroup = go.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.interactable = false;
            canvasGroup.DOFade(0, duration).onComplete += (() =>
            {
                go.SetActive(false);
                if (onComplete != null) onComplete();
            });
        }

        public static void FadeIn(GameObject go, float duration, UnityAction onComplete)
        {
            CanvasGroup canvasGroup = go.GetComponent<CanvasGroup>();
            if (!canvasGroup)
                canvasGroup = go.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            go.SetActive(true);
            canvasGroup.DOFade(1, duration).onComplete += (() =>
            {
                canvasGroup.interactable = true;
                if (onComplete != null) onComplete();
            });
        }

        public static void ScaleOut(GameObject go, float duration, UnityAction onComplete)
        {
            CanvasGroup canvasGroup = go.GetComponent<CanvasGroup>();
            if (!canvasGroup)
                canvasGroup = go.AddComponent<CanvasGroup>();
            canvasGroup.interactable = false;

            Vector3 localScale = go.transform.localScale;
            Tweener tweener = go.transform.DOScale(Vector3.zero, duration);
            tweener.SetEase(Ease.Linear);
            tweener.onComplete = () =>
            {
                canvasGroup.interactable = true;

                go.transform.localScale = localScale;
                if (onComplete != null) onComplete();
            };
        }

        public static void ScaleIn(GameObject go, float duration, UnityAction onComplete)
        {
            CanvasGroup canvasGroup = go.GetComponent<CanvasGroup>();
            if (!canvasGroup)
                canvasGroup = go.AddComponent<CanvasGroup>();
            canvasGroup.interactable = false;

            Vector3 localScale = go.transform.localScale;
            go.transform.localScale = Vector3.zero;
            Tweener tweener = go.transform.DOScale(Vector3.one, duration);
            tweener.SetEase(Ease.OutBounce);
            tweener.onComplete = () =>
            {
                canvasGroup.interactable = true;

                go.transform.localScale = localScale;
                if (onComplete != null) onComplete();
            };

        }

        public static void MoveIn(GameObject go, float duration, UnityAction onComplete)
        {
            Canvas canvas = go.GetComponentInParent<Canvas>();
            if (!canvas)
            {
                Debug.Log(go.name + "非UI物体,动画不能工作");
                return;
            }


            go.transform.localPosition = Vector3.right * canvas.GetComponent<RectTransform>().sizeDelta.x;
            Tweener tweener = go.transform.DOLocalMove(Vector3.zero, duration);
            tweener.SetEase(Ease.OutBack);
            tweener.onComplete = () =>
            {
                if (onComplete != null) onComplete();
            };
        }
        public static void MoveOut(GameObject go, float duration, UnityAction onComplete)
        {
            Canvas canvas = go.GetComponentInParent<Canvas>();
            if (!canvas)
            {
                Debug.Log(go.name + "非UI物体,动画不能工作");
                return;
            }

            Vector3 targetPosition = Vector3.left * canvas.GetComponent<RectTransform>().sizeDelta.x;
            Tweener tweener = go.transform.DOLocalMove(targetPosition, duration);
            tweener.onComplete = () =>
            {
                if (onComplete != null) onComplete();
            };

        }
#endif
        #endregion

        /// <summary>
        /// 判断鼠标是否在UI界面上
        /// </summary>
        /// <returns></returns>
        public static bool WereAnyUiElementsHovered()
        {
            if (EventSystem.current == null) return false;

            Vector2 inputDevPos = Input.mousePosition;

            PointerEventData eventDataCurrentPosition =
                new PointerEventData(EventSystem.current) { position = new Vector2(inputDevPos.x, inputDevPos.y) };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            return results.Count != 0;
        }

        /// <summary>
        /// 获取鼠标位置的ui物体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject GetUiFromMousePosition(string name)
        {
            if (EventSystem.current == null) return null;

            Vector2 inputDevPos = Input.mousePosition;

            PointerEventData eventDataCurrentPosition =
                new PointerEventData(EventSystem.current) { position = new Vector2(inputDevPos.x, inputDevPos.y) };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            RaycastResult result = results.FirstOrDefault(x => x.gameObject.name == name);
            return result.gameObject;

        }

        /// <summary>
        /// 设置ui的交互状态,包括子物体
        /// </summary>
        /// <param name="uiGameObject"></param>
        /// <param name="interactable"></param>
        public static void SetInteractable(GameObject uiGameObject, bool interactable)
        {
            if (uiGameObject.GetComponent<RectTransform>() == null)
            {
                Debug.LogError("SetInteractable方法仅作用于UI物体");
                return;
            }

            Selectable[] selectables = uiGameObject.GetComponentsInChildren<Selectable>();
            selectables.ForEach(x => x.interactable = interactable);
        }

        /// <summary>
        /// 获取激活toggle的索引
        /// </summary>
        /// <param name="toggles"></param>
        /// <returns></returns>
        public static int[] GetActiveToggleIndex(Toggle[] toggles)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn)
                    list.Add(i);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 模拟鼠标点击ui
        /// </summary>
        /// <param name="go"></param>
        public static void SimulateClick(GameObject go)
        {
            ExecuteEvents.Execute<IPointerClickHandler>(go, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }

        /// <summary>
        /// 绑定滑动条和文本输入框
        /// </summary>
        /// <param name="scrollbar"></param>
        /// <param name="inputField"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public static void Bind(Scrollbar scrollbar, InputField inputField, float minValue, float maxValue)
        {
            inputField.contentType = InputField.ContentType.DecimalNumber;
            inputField.onEndEdit.AddListener(s =>
            {
                bool success = float.TryParse(s, out float result);
                if (!success)
                {
                    MessageBox.Show("数值类型输入不合法!", () => inputField.text = Mathf.Lerp(minValue, maxValue, scrollbar.value).ToString("F2"));
                    return;
                }

                if (result < minValue || result > maxValue)
                {
                    MessageBox.Show("数值范围输入不合法!", () => inputField.text = Mathf.Lerp(minValue, maxValue, scrollbar.value).ToString("F2"));
                    return;
                }
                scrollbar.value = (result - minValue) / (maxValue - minValue);
            });

            scrollbar.numberOfSteps = 0;
            scrollbar.onValueChanged.AddListener(v => inputField.text = Mathf.Lerp(minValue, maxValue, v).ToString("F2"));
            scrollbar.onValueChanged.Invoke(scrollbar.value);
        }

#if TMPro
    public static void Bind(Scrollbar scrollbar, TMP_InputField inputField, float minValue, float maxValue)
    {
        inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
        inputField.onEndEdit.AddListener(s =>
        {
            bool success = float.TryParse(s, out float result);
            if (!success)
            {
                MessageBox.Show("数值类型输入不合法!", () => inputField.text = Mathf.Lerp(minValue, maxValue, scrollbar.value).ToString("F2"));
                return;
            }

            if (result < minValue || result > maxValue)
            {
                MessageBox.Show("数值范围输入不合法!", () => inputField.text = Mathf.Lerp(minValue, maxValue, scrollbar.value).ToString("F2"));
                return;
            }
            scrollbar.value = (result - minValue) / (maxValue - minValue);
        });

        scrollbar.numberOfSteps = 0;
        scrollbar.onValueChanged.AddListener(v => inputField.text = Mathf.Lerp(minValue, maxValue, v).ToString("F2"));
        scrollbar.onValueChanged.Invoke(scrollbar.value);
    }
#endif
        /// <summary>
        /// 绑定滑动条和文本输入框
        /// </summary>
        /// <param name="scrollbar"></param>
        /// <param name="inputField"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public static void Bind(Scrollbar scrollbar, InputField inputField, int minValue, int maxValue)
        {
            inputField.contentType = InputField.ContentType.IntegerNumber;
            inputField.onEndEdit.AddListener(s =>
            {
                bool success = int.TryParse(s, out int result);
                if (!success)
                {
                    MessageBox.Show("数值类型输入不合法!", () => inputField.text = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, scrollbar.value)).ToString("G"));
                    return;
                }

                if (result < minValue || result > maxValue)
                {
                    MessageBox.Show("数值范围输入不合法!", () => inputField.text = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, scrollbar.value)).ToString("G"));
                    return;
                }

                scrollbar.value = (result - minValue) * 1.0f / (maxValue - minValue);
            });
            scrollbar.numberOfSteps = maxValue - minValue;
            scrollbar.onValueChanged.AddListener(v => inputField.text = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, v)).ToString("G"));
            scrollbar.onValueChanged.Invoke(scrollbar.value);
        }

#if TMPro
    public static void Bind(Scrollbar scrollbar, TMP_InputField inputField, int minValue, int maxValue)
    {
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        inputField.onEndEdit.AddListener(s =>
        {
            bool success = int.TryParse(s, out int result);
            if (!success)
            {
                MessageBox.Show("数值类型输入不合法!", () => inputField.text = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, scrollbar.value)).ToString("G"));
                return;
            }

            if (result < minValue || result > maxValue)
            {
                MessageBox.Show("数值范围输入不合法!", () => inputField.text = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, scrollbar.value)).ToString("G"));
                return;
            }

            scrollbar.value = (result - minValue) * 1.0f / (maxValue - minValue);
        });
        scrollbar.numberOfSteps = maxValue - minValue;
        scrollbar.onValueChanged.AddListener(v => inputField.text = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, v)).ToString("G"));
        scrollbar.onValueChanged.Invoke(scrollbar.value);
    }
#endif
        public static void Bind(Toggle toggle, Selectable selectable)
        {
            toggle.onValueChanged.AddListener(v => selectable.interactable = v);
            toggle.onValueChanged.Invoke(toggle.isOn);
        }
    }
}

