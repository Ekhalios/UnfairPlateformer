using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGroundDidac : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = true;
        }
    }
}
