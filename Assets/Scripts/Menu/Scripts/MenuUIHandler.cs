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
    public Text text;
    public InputField iField;

    private void OnEnable()
    {
        if (MainManagerII.Instance.PlayerName == null || MainManagerII.Instance.PlayerName == string.Empty)
            iField.onValueChanged.AddListener(UpdateText);
        else
        {
            iField.text = MainManagerII.Instance.PlayerName;
            text.text = "Best Score : " + MainManagerII.Instance.PlayerName + " : " + MainManagerII.Instance.PlayerScore;
        }
    }

    private void UpdateText(string _)
    {
        if (MainManagerII.Instance.PlayerScore != "0")
            text.text = "Best Score : : " + MainManagerII.Instance.PlayerScore;
        else
            text.text = "Best Score : : 0";
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
