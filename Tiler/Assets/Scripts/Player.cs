using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float ladderSpeed;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip deathSound;

    Rigidbody2D rb;
    Collider2D playerCollider;

    Vector2 deathKick;

    float gravityScale;

    LayerMask _groundMask;
    LayerMask _ladderMask;

    Animator animator;

    bool _isRight = true;
    bool _onGround = true;
    bool _jump;
    bool _isDead = false;

    RaycastHit2D hit;

    
    public delegate void death();
    public static event death OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        deathKick = new Vector2(Random.Range(-25f, 25f), Random.Range(0f, 25f));
        _groundMask = LayerMask.GetMask("Ground");
        _ladderMask = LayerMask.GetMask("Ladder");
        playerCollider = transform.GetComponent<Collider2D>();
        RaycastHit2D hit;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        if (jumpSound != null && deathSound != null)
        {
            DontDestroyOnLoad(jumpSound);
            DontDestroyOnLoad(deathSound);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) { return; }
        
            Run();
            Ladder();
            if (Input.GetKeyDown(KeyCode.W) && _onGround)
            {
                _jump = true;
            }
        //Debug.Log("Velocity: " + rb.velocity);
        
        

    }
    private void FixedUpdate()
    {
        if (_isDead) { return; }
        
            Death();
            _onGround = true;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.7f, _groundMask);
            Debug.DrawRay(transform.position, -Vector2.up * 0.8f, Color.green);
            if (!hit)//!hit.collider.IsTouchingLayers(_groundMask))
            {
               // Debug.Log("OFF");
                _onGround = false;
            }
            if (_jump && _onGround && !playerCollider.IsTouchingLayers(_ladderMask))
            {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            var jumpVel = new Vector2(0, jumpForce);
                rb.velocity += jumpVel;
                _jump = false;

            }
        
        
    }

    public void Death()
    {
        if (rb.IsTouchingLayers(LayerMask.GetMask("Enemy"))||rb.IsTouchingLayers(LayerMask.GetMask("Spike")) || rb.IsTouchingLayers(LayerMask.GetMask("Water")) && !_isDead)
        {
            _isDead = true;            
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            animator.SetBool("isDead", true);

            rb.velocity = deathKick;
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            OnDeath?.Invoke();


        }
        
        
    }

    private void Ladder()
    {
        if (playerCollider.IsTouchingLayers(_ladderMask))
        {
            float vet = Input.GetAxis("Vertical");

            if (vet == 0 && _onGround == false)
            {
               // Debug.Log("PRIZOK");
                animator.enabled = false;
                
                rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            }
            else if (vet != 0)
            {
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                animator.enabled = true;
                animator.SetBool("isClimbing", true);
                Debug.Log(vet);
                Vector2 ladderVelocity = new Vector2(rb.velocity.x, ladderSpeed * vet);
                rb.velocity = ladderVelocity;
            }

        }
        else animator.SetBool("isClimbing", false);
    }

    private void Run()
    {
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        animator.enabled = true;
        float hor = Input.GetAxis("Horizontal");
        animator.SetBool("isRunning", true);
        FlipScale(hor);
        Vector2 playerVelocity = new Vector2(hor * speed, rb.velocity.y);
        rb.velocity = playerVelocity;
        
        
    }    

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            
            var jumpVel = new Vector2(0, jumpForce);
            Debug.Log("Frame " + jumpVel * Time.deltaTime);

            rb.velocity += jumpVel;
            
        }
    }

    private void FlipScale(float hor)
    {
        if (hor > 0 && !_isRight)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            _isRight = true;
           // animator.SetBool("isRunning", true);
        }
        else if (hor < 0 && _isRight)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            _isRight = false;
           // animator.SetBool("isRunning", true);
        }
        else if(hor == 0)
        {
            animator.SetBool("isRunning", false);
        }
       
    }
}
 