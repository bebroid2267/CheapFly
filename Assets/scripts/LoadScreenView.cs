using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts;
using System;

public class LoadUI : MonoBehaviour, IUIScreen
{
    public Image loader;
    public float duration = 5f;
    public float rotationSpeed = 360f;
    float elapsed = 0f;

    public void Initialize()
    {
        StartCoroutine(LoaderCoroutine(() =>
        {
            GameManager.Instance.UIController.ShowScreen("ChooseCountries");
        }));
    }
    public void DeInitialize()
    {
        StopAllCoroutines();
    }
    IEnumerator LoaderCoroutine(Action onComplete)
    {

        while (elapsed < duration)
        {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            loader.rectTransform.Rotate(0f, 0f, -rotationThisFrame);
            elapsed += Time.deltaTime;
            yield return null;
        }
        onComplete?.Invoke();
    }
}
