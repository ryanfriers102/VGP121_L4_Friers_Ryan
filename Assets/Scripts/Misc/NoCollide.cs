using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCollide : MonoBehaviour
{
    public string tg;
    private void Start()
    {
        GameObject colTag = GameObject.FindGameObjectWithTag(tg);
        Physics2D.IgnoreCollision(colTag.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == tg)
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
