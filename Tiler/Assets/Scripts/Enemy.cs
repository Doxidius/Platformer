using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;

    LayerMask _groundMask;

    Vector2 direction;

    bool isLooingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.right;
        _groundMask = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);        
    }

  

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isLooingRight)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            direction = Vector2.left;
            isLooingRight = false;
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            direction = Vector2.right;
            isLooingRight = true;
        }
    }
    

}
