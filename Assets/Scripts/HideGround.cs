using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGround : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool editorMode = false;
    private bool printed = false;
    private MapCreator mapCreator;

    void Start()
    {
        mapCreator = GameObject.FindWithTag("mapCreator").GetComponent<MapCreator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    void Update()
    { 
        if (mapCreator.getEditorMode() != editorMode)
        {
            editorMode = !editorMode;
            if (editorMode)
            {
                spriteRenderer.enabled = true;
                printed = false;
            }
            else if (printed == false)
            {
                spriteRenderer.enabled = false;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = true;
            printed = true;
        }
    }
}
