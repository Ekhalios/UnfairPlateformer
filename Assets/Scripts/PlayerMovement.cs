using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2(5, 5);
    public Vector2 initialPosition = new Vector2(9, 1);
    public Rigidbody2D rb;
    private bool onGround = true;
    private SpriteRenderer spriteRenderer;
    private bool reverse = false;
    public MapCreator mapCreator;

    private bool controlable = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switchToInitialPos();
    }

    void Update()
    {
        if (!controlable)
        {
            return;
        }
        float inputX = Input.GetAxis("Horizontal");

        if (mapCreator.getEditorMode())
        {
            if (transform.position.x <= 8.5f && inputX < 0)
            {
                return;
            }
            rb.velocity = new Vector2(speed.x * inputX, rb.velocity.y);
            return;
        }
        if (inputX > 0 || transform.position.x > 0) {
            rb.velocity = new Vector2(speed.x * inputX, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            onGround = false;
        }
        if (rb.velocity.y < 0)
        {
            onGround = false;
        }
        if (rb.velocity.y < -15)
        {
            rb.velocity = new Vector3(rb.velocity.x, -15, 0);
        }
        if (inputX > 0 && !reverse)
        {
            reverse = !reverse;
            spriteRenderer.flipX = false;
        }
        if (inputX < 0 && reverse)
        {
            reverse = !reverse;
            spriteRenderer.flipX = true;
        }
    }

    public void Editor()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
        rb.isKinematic = !rb.isKinematic;
    }

    public void switchToInitialPos()
    {
        transform.position = initialPosition;
    }

    public void switchControlable()
    {
        controlable = !controlable;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float colliderYPosition = col.contacts[0].point.y;

        foreach (ContactPoint2D contact in col.contacts)
        {
            if (contact.point.y < transform.position.y)
            {
                onGround = true;
            }
        }
    }
}
