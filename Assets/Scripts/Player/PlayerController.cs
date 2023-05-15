using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // Component references
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    // Movement variables
    public float speed = 5.0f;
    public float jumpForce = 300.0f;
    public float fallThreshold= -10f;

    // Groundcheck stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;

    float fall = 0.0f;
    float slam = 0.0f;
    public static bool power = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        

        if (speed <= 0)
        {
            speed = 5.0f;
            Debug.Log("Speed value incorrect, default value 5.0");
        }
        if (jumpForce <= 0) jumpForce = 300.0f;
        if (groundCheckRadius <= 0) groundCheckRadius = 0.02f;

        if (!groundCheck) groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");
        float fInput = Input.GetAxisRaw("Fire1");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (curPlayingClips.Length > 0)
        { 
            if (Input.GetButton("Fire1") && curPlayingClips[0].clip.name != "Fire")
                fInput = 1;
            else if (curPlayingClips[0].clip.name == "Fire")
                rb.velocity = Vector2.zero;
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
                fInput = 0;
            }
        }

        // Falling
        if ((rb.velocity.y < fallThreshold))
        {
            fall = 1.0f;
        }
        else
        {
            fall = 0.0f;
        }

        // Jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        // Slam input
        if (isGrounded == false && Input.GetButtonDown("Fire2"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.down * jumpForce * 1.25f);
        }

        if (Input.GetButton("Fire1") && power == true)
        {
            fInput = 1;
        }
        else
            fInput = 0;

        if (Input.GetButton("Fire2"))
        {
            slam = 1;
        }
        else
        {
            slam = 0;
        }

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("fInput", fInput);
        anim.SetFloat("slam", slam);
        anim.SetFloat("fall", fall);

        if (hInput != 0)
            sr.flipX = (hInput < 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            Destroy(collision.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
