using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CriterionDropdown : MonoBehaviour
{
    public TypeCountrie typeCountrie;
    public Image ChoicesImage;

    public Action<TypeCountrie> OnShowChoice;

    public void Choice()
    {
        OnShowChoice.Invoke(typeCountrie);
    }

    public void HideChoicesImage() 
    {
        ChoicesImage.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum TypeCountrie
{
    France,
    Egypt,
    Japan,
    US
}