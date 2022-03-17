using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] AudioClip pickUpSound;

    HeartFinder heart;

    private bool _isTriggered = false;
    float smoothing = 1f;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        
    }
    private void Start()
    {
        heart = FindObjectOfType<HeartFinder>();
    }
    Vector3 DestinationPoint()
    {
        Vector3 toDestinationInWorldSpace;
        if (heart != null)
        {
            toDestinationInWorldSpace = Camera.main.ScreenToWorldPoint(heart.GetPosition());
            return toDestinationInWorldSpace;
        }
        else
        {
            toDestinationInWorldSpace = new Vector3(transform.position.x, transform.position.y + 2f);
            return toDestinationInWorldSpace;
        }
    }
    private void Update()
    {
        var endPos = DestinationPoint();
        if (endPos == transform.position)
        {
            Debug.Log("fdfdf");
        }
    }
    IEnumerator MoveCoroutine()
    {
        
        
        
        while (true)//Mathf.Approximately(transform.position.y,endPos.y))
        {
            var endPos = DestinationPoint();
            if (Mathf.Abs(transform.position.y - endPos.y)<0.1)
            {
                Debug.Log("hui");
                break;
                
            }
            Debug.Log("Curpos: " + Mathf.Abs(transform.position.y - endPos.y));
            transform.position = Vector3.Lerp(transform.position, endPos, smoothing * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        _isTriggered = true;
        FindObjectOfType<GameSession>().IncreaseLife();
        AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(MoveCoroutine());

    }
}
