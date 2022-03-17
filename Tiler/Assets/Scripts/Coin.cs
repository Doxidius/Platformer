using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    [SerializeField] int coinAmount = 1;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().IncreaseCoins(coinAmount);
        
        AudioSource.PlayClipAtPoint(clip, gameObject.transform.position, 0.5f);
        
        Destroy(gameObject);
    }
}
