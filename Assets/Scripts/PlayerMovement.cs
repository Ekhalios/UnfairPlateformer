using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2(5, 5);
    public Vector2 initialPosition = new Vector2(9, 1);
    public Rigidbody2D rb;
    public float gravityScale = 2;
    public float fallingGravityScale = 7.5f;
    private bool onGround = true;
    private SpriteRenderer spriteRenderer;
    private bool reverse = false;
    public MapCreator mapCreator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switchToInitialPos();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (mapCreator.getEditorMode())
        {
            if (transform.position.x <= 8.5f && inputX < 0)
            {
                return;
            }
            Vector3 movementEditor = new Vector3(speed.x * inputX, 0, 0);
            movementEditor *= Time.deltaTime;
            transform.Translate(movementEditor);
            return;
        }
        if (inputX > 0 || transform.position.x > 0) {
            Vector3 movement = new Vector3(speed.x * inputX, 0, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
        }
        if (rb.velocity.y == 0)
        {
            onGround = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector2.up * speed.y, ForceMode2D.Impulse);
            onGround = false;
        }
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = gravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallingGravityScale;
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

}
