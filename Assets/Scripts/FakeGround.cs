using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGround : MonoBehaviour
{
    public Rigidbody2D rb;
    public float fallingGravityScale;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            rb.gravityScale = fallingGravityScale;
        }
    }
}
