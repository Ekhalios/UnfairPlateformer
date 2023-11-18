using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGround : MonoBehaviour
{
    public float fallingGravityScale;
    private bool falling = false;
    public Sprite editorModeSpite;
    public Sprite gameModeSprite;
    private SpriteRenderer spriteRenderer;
    private bool editorMode = false;
    private MapCreator mapCreator;

    void Start()
    {
        mapCreator = GameObject.FindWithTag("mapCreator").GetComponent<MapCreator>();
    }
    void Update()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (mapCreator.getEditorMode() != editorMode)
        {
            editorMode = !editorMode;
            Debug.Log("SWITCH TEXT");
            if (editorMode)
            {
                spriteRenderer.sprite = editorModeSpite;
            } else
            {
                spriteRenderer.sprite = gameModeSprite;
            }
        }
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && falling == false)
        {
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = fallingGravityScale;
            falling = true;
        }
    }

}
