using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] AudioClip successSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        EndLevel();
    }

    private void Start()
    {
        DontDestroyOnLoad(successSound);
    }

    public void EndLevel()
    {
        StartCoroutine(LoadScene());
        FindObjectOfType<GameSession>().SetStartScore();
    }

    IEnumerator LoadScene()
    {        
        Time.timeScale = 0.1f;
        AudioSource.PlayClipAtPoint(successSound, Camera.main.transform.position, 2f);
        yield return new WaitForSeconds(0.2f);
        int currentSceneIndex;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++currentSceneIndex);
        Time.timeScale = 1f;
    }
}
