using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public MapCreator mapCreator;
    public PlayerMovement playerMovement;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.y < -50)
        {
            mapCreator.drawMap();
            rb.velocity = new Vector3(0, 0, 0);
            playerMovement.switchToInitialPos();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "killPlayer")
        {
            mapCreator.drawMap();
            rb.velocity = new Vector3(0, 0, 0);
            playerMovement.switchToInitialPos();
        }
    }
}
