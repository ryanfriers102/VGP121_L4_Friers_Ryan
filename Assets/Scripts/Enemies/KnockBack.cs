using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public string kbTarget;
    public static float kbX;
    public static float kbY;

    static public bool hurt;

    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.x < tr.position.x)
                kbX = kbX * -1;
            else if (collision.gameObject.transform.position.x > tr.position.x)
                kbX = Mathf.Abs(kbX);
        }

        hurt = true;
        Vector2 impulse = new Vector2(kbX, kbY);

        if (collision.gameObject.CompareTag(kbTarget))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);
        }
    }
}
