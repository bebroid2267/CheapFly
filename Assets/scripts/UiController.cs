using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [System.Serializable]
    public class ScreenEntry
    {
        public string name;
        public GameObject screen;
    }

    public List<ScreenEntry> screens;

    private GameObject currentScreen;

    private void Start()
    {
        if (screens.Count > 0)
        {
            ShowScreen(screens[0].name);
        }
    }

    public void ShowScreen(string screenName)
    {
        foreach (var entry in screens)
        {
            bool isTarget = entry.name == screenName;
            entry.screen.SetActive(isTarget);
            if (entry.screen.activeSelf)
            {
                entry.screen.GetComponent<IUIScreen>().Initialize();
            }
            if (isTarget) currentScreen = entry.screen;
        }
    }

    public void HideAllScreens()
    {
        foreach (var entry in screens)
        {
            entry.screen.SetActive(false);
        }
        currentScreen = null;
    }

    public string GetCurrentScreenName()
    {
        var screen = screens.Find(s => s.screen == currentScreen);
        return screen != null ? screen.name : "";
    }
}
