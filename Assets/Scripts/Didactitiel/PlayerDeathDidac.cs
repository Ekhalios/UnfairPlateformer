using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDeathDidac : MonoBehaviour
{
    public PlayerMovementDidac playerMovement;

    private Rigidbody2D rb;
    public AudioSource deathSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            deathSoundEffect.Play();
            rb.velocity = new Vector3(0, 0, 0);
            playerMovement.switchToInitialPos();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "killPlayer")
        {
            deathSoundEffect.Play();
            rb.velocity = new Vector3(0, 0, 0);
            playerMovement.switchToInitialPos();
        }
    }
}
