using System.Collections;
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

    public AudioSource jumpSoundEffect;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switchToInitialPos();
    }

    private void Update()
    {
        if (!controlable)
        {
            return;
        }

        float inputX = Input.GetAxisRaw("Horizontal");

        if (mapCreator.getEditorMode())
        {
            if (transform.position.x <= 8.5f && inputX < 0)
            {
                return;
            }

            rb.velocity = new Vector2(speed.x * inputX, rb.velocity.y);
            return;
        }

        HandlePlayerMovement(inputX);
        HandleJump();
        FlipSprite(inputX);
    }

    private void HandlePlayerMovement(float inputX)
    {
        if (inputX > 0 || transform.position.x > 0)
        {
            rb.velocity = new Vector2(speed.x * inputX, rb.velocity.y);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            jumpSoundEffect.Play();
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
    }

    private void FlipSprite(float inputX)
    {
        if (inputX > 0 && !reverse)
        {
            ReverseSprite(false);
        }

        if (inputX < 0 && reverse)
        {
            ReverseSprite(true);
        }
    }

    private void ReverseSprite(bool flipX)
    {
        reverse = !reverse;
        spriteRenderer.flipX = flipX;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        foreach (ContactPoint2D contact in col.contacts)
        {
            if (contact.point.y < transform.position.y)
            {
                onGround = true;
            }
        }
    }
}
