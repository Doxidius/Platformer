using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{

    [SerializeField] int lives = 3;
    
    

    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] TextMeshProUGUI health;

    int startCoinsVal = 0;
    int coinsVal;

    private void Awake()
    {
            DontDestroyOnLoad(coins);

        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        coinsVal = 0;
        coins.text = coinsVal.ToString();
        health.text = lives.ToString();
    }

    public void SetStartScore()
    {
        startCoinsVal = coinsVal;
    }

    private void OnEnable()
    {
        Player.OnDeath += PlayerDeath;
    }

    private void OnDisable()
    {
        Player.OnDeath -= PlayerDeath;
    }

    public void ResetScore()
    {
        coinsVal = startCoinsVal;
        coins.text = coinsVal.ToString();
    }

    void PlayerDeath()
    {
        
        StartCoroutine(Death());
    }

    public void IncreaseCoins(int coinAmount)
    {
        coinsVal += coinAmount;
        coins.text = coinsVal.ToString();
    }

     IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
       
        if (lives > 0)
        {
            DecreaseLife();
            ResetScore();
            FindObjectOfType<SceneLoader>().RestartScene();
            // SceneManager.LoadScene(indexScene);
        }
        else
        {
             FindObjectOfType<SceneLoader>().LoadMenu();
           // SceneManager.LoadScene("Menu");
            Destroy(gameObject);
        }
        Debug.Log("Lives: " + lives);
    }

    public void DecreaseLife()
    {
        lives--;
        health.text = lives.ToString();
    }
    public void IncreaseLife()
    {
        lives++;
        health.text = lives.ToString();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
