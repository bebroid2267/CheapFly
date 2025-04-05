using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCountriesScreenView : MonoBehaviour, IUIScreen
{
    public List<CriterionDropdown> cretionsDropDown;

    public void Initialize()
    {
        foreach (var item in cretionsDropDown)
        {
            item.OnShowChoice += ShowChoicesCriterion;
        }
    }
    private void ShowChoicesCriterion(TypeCountrie countrie)
    {
        foreach (var criterion in cretionsDropDown)
        {
            criterion.ChoicesImage.gameObject.SetActive(criterion.typeCountrie == countrie);

            if (criterion.typeCountrie == countrie)
            {
                GameManager.Instance.ChoicesCountrie = countrie;
                StartCoroutine(ChoiceCoroutine(() =>
                {
                    GameManager.Instance.UIController.ShowScreen("MiniMap");
                }));
            }
        }
    }

    public void DeInitialize()
    {
        foreach (var item in cretionsDropDown)
        {
            item.OnShowChoice -= ShowChoicesCriterion;
        }
    }
    IEnumerator ChoiceCoroutine(Action onComplete)
    {
        float elapsed = 0f;
        while (elapsed < 2)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        onComplete?.Invoke();
    }

}
