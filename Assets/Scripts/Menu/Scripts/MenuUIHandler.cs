using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public InputField iField;

    private void OnEnable()
    {
        if (MainManagerII.Instance.PlayerName != null)
            iField.text = MainManagerII.Instance.PlayerName;
    }

    public void StartNew()
    {
        MainManagerII.Instance.SaveName(iField.text);
        StartCoroutine(SkipAFrameBeforeLoadingScene());
    }

    private IEnumerator SkipAFrameBeforeLoadingScene()
    {
        yield return new WaitForFixedUpdate();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
