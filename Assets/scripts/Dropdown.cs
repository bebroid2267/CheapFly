using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour
{
    public Image dropdownButton;
    public RectTransform dropdownPanel;
    public float animationDuration = 0.3f;

    private bool isOpen = false;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = dropdownPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = dropdownPanel.gameObject.AddComponent<CanvasGroup>();
        }

        dropdownPanel.localScale = Vector3.zero;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }


    public void ToggleDropdown()
    {
        if (isOpen)
            CloseDropdown();
        else
            OpenDropdown();
    }

    private void OpenDropdown()
    {
        isOpen = true;
        dropdownPanel.gameObject.SetActive(true);
        dropdownPanel.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutBack);
        canvasGroup.DOFade(1f, animationDuration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void CloseDropdown()
    {
        isOpen = false;
        dropdownPanel.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack);
        canvasGroup.DOFade(0f, animationDuration).OnComplete(() =>
        {
            dropdownPanel.gameObject.SetActive(false);
        });
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
