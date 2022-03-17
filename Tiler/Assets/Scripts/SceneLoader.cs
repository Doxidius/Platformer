using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextLevel()
    {
        int currentSceneIndex;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++currentSceneIndex);
    }

    public void RestartScene()
    {
        var indexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexScene);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
