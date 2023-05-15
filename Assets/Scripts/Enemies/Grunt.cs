using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour
{
    public float speed;
    public float timerTimeout;
    public float directionX;
    private float xChange = 1;

    Rigidbody2D rb;
    Animator anim;
    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (xChange == 1)
        {
            Vector2 targetVelocity = new Vector2(directionX, 0);
            rb.velocity = targetVelocity * speed;
        }

        if (xChange == -1)
        {
            Vector2 targetVelocity1 = new Vector2(-directionX, 0);
            rb.velocity = targetVelocity1 * speed;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DirectionTrigger"))
            xChange = xChange * -1;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.x < tr.position.x)
                xChange = 1;
            else if (collision.gameObject.transform.position.x > tr.position.x)
                xChange = -1;
        }
    }  
}