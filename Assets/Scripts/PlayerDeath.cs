using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public MapCreator mapCreator;
    public PlayerMovement playerMovement;

    private Rigidbody2D rb;
    public AudioSource deathSoundEffect;
    private chronoScript chrono;
    private int numberDeath = 0;
    public TextMeshProUGUI death;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chrono = FindObjectOfType<chronoScript>();
        death.color = Color.black;
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            numberDeath++;
            chrono.resetChrono();
            deathSoundEffect.Play();
            mapCreator.drawMap();
            rb.velocity = new Vector3(0, 0, 0);
            playerMovement.switchToInitialPos();
            death.text = "Death: " + numberDeath.ToString();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "killPlayer")
        {
            numberDeath++;
            chrono.resetChrono();
            deathSoundEffect.Play();
            mapCreator.drawMap();
            rb.velocity = new Vector3(0, 0, 0);
            playerMovement.switchToInitialPos();
            death.text = "Death: " + numberDeath.ToString();
        }
    }
}
