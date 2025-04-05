using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniMapScreenView : MonoBehaviour, IUIScreen
{
    [SerializeField] private RectTransform mapContent;
    [SerializeField] RectTransform miniMapPanel;
    [SerializeField] List<RectTransform> targetPoint;
    [SerializeField] float zoom = 1f;

    public void CenterPoint(TypeCountrie countrie)
    {
        Canvas canvas = mapContent.GetComponentInParent<Canvas>();
        if (!canvas) return;

        targetPoint[(int)countrie].gameObject.SetActive(true);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mapContent,
            RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, targetPoint[(int)countrie].position),
            canvas.worldCamera,
            out localPoint
        );

        Vector2 panelCenter = new Vector2(miniMapPanel.rect.width / 2f, miniMapPanel.rect.height / 2f);
        Vector2 offset = localPoint - panelCenter;

        mapContent.anchoredPosition = -localPoint;
    }

    public void Initialize()
    {
        HideAllTargets();
        CenterPoint(GameManager.Instance.ChoicesCountrie);
    }
    public void DeInitialize()
    {
        
    }

    private void HideAllTargets()
    {
        foreach (var point in targetPoint)
        {
            point.gameObject.SetActive(false);
        }
    }

    public void GoToFullMapScreen()
    {
        GameManager.Instance.UIController.ShowScreen("FullMap");
    }
    public void GoToBackScreen()
    {
        GameManager.Instance.UIController.ShowScreen("ChooseCountries");
    }
}
