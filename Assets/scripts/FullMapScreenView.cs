using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FullMapScreenView : MonoBehaviour,  IUIScreen
{
    [SerializeField] List<RectTransform> targetPoints; 

    [SerializeField] private RectTransform mapContent;
    [SerializeField] private float zoomSpeed = 0.05f;
    [SerializeField] private float minZoom = 0.5f;
    [SerializeField] private float maxZoom = 3f;
    [SerializeField] private float panSpeed = 1f;

    private Vector2 touchStartPos;
    private Vector2 contentStartPos;
    private float startPinchDistance;
    private Vector3 startScale;

    public void DeInitialize()
    {

    }

    public void Initialize()
    {
        HideAllTargets();
        targetPoints[(int)GameManager.Instance.ChoicesCountrie].gameObject.SetActive(true);
    }

    private void HideAllTargets()
    {
        foreach (var point in targetPoints)
        {
            point.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                contentStartPos = mapContent.anchoredPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - touchStartPos;
                mapContent.anchoredPosition = contentStartPos + delta * panSpeed;
            }
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                startPinchDistance = Vector2.Distance(touchZero.position, touchOne.position);
                startScale = mapContent.localScale;
            }
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                float currentPinchDistance = Vector2.Distance(touchZero.position, touchOne.position);

                if (Mathf.Approximately(startPinchDistance, 0f)) return;

                float zoomFactor = currentPinchDistance / startPinchDistance;
                float newScale = Mathf.Clamp(startScale.x * zoomFactor, minZoom, maxZoom);
                mapContent.localScale = Vector3.one * newScale;
            }
        }
    }
    public void BackToMiniMapScreen()
    {
        GameManager.Instance.UIController.ShowScreen("MiniMap");
    }
}
