using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public MapCreator mapCreator;
    public PlayerMovement playerMovement;

    void Update()
    {
        if (transform.position.y < -50)
        {
            mapCreator.drawMap();
            playerMovement.switchToInitialPos();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "killPlayer")
        {
            mapCreator.drawMap();
            playerMovement.switchToInitialPos();
        }
    }
}
