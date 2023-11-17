using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -100)
        {
            Debug.Log("PLAYER DEATH");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "killPlayer")
        {
            Debug.Log("PLAYER DEATH");
        }
    }
}
