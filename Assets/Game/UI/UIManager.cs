using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Closes the game
    public void CloseGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
    
    // Used to open external links
    public void ExternalLink(string link)
    {
        Debug.Log("External link '" + link + "' opened");
        Application.OpenURL(link);
    }

    // Loads scenes
    public void LoadScene(string sceneName)
    {
        if (sceneName.Contains("this")) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

        SceneManager.LoadScene(sceneName);
        Debug.Log("Scene '" + sceneName + "' Loaded");
    }
}
