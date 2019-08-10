using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class UITool
{
    #region UI Animation

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
        canvasGroup.DOFade(0, 1).onComplete += (() =>
        {
            if (onComplete != null) onComplete();
        });
    }

    public static void FadeIn(GameObject go, float duration, UnityAction onComplete)
    {
        CanvasGroup canvasGroup = go.GetComponent<CanvasGroup>();
        if (!canvasGroup)
            canvasGroup = go.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 1).onComplete += (() =>
          {
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
}
