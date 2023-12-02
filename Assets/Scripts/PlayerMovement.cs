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
    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private Vector2 playerSize;
    private Collider2D playerCollider;
    public AudioSource jumpSoundEffect;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        playerSize = playerCollider.bounds.size;
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

        //checkMove();
        HandlePlayerMovement(inputX);
        HandleJump();
        FlipSprite(inputX);
    }

    private void HandlePlayerMovement(float inputX)
    {
        if (inputX > 0 && !canMoveRight)
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                Debug.Log("Player Velocity after set: " + rb.velocity);
            }
            return;
        }
        if (inputX < 0 && !canMoveLeft)
        {
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                Debug.Log("Player Velocity after set: " + rb.velocity);
            }
            return;
        }
        if ((inputX > 0 || transform.position.x > 0))
        {
            rb.velocity = new Vector2(speed.x * inputX, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            Debug.Log("JUUUUUUUUUUUUUUUUUUUUUMP");
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
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                float playerTop = transform.position.y + playerSize.y / 2;
                float playerBottom = transform.position.y - playerSize.y / 2;
                float playerLeft = transform.position.x - playerSize.y / 2 + 0.1f;
                float playerRight = transform.position.x + playerSize.y / 2 - 0.1f;
                if (contact.normal.y >= 1 && contact.point.x > playerLeft && contact.point.x < playerRight)
                {
                    onGround = true;
                }
                if (contact.normal.x < 0 && contact.point.y > playerBottom && contact.point.y < playerTop)
                {
                    canMoveRight = false;
                }
                else if (contact.normal.x > 0 && contact.point.y > playerBottom && contact.point.y < playerTop)
                {
                    canMoveLeft = false;
                }
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
       canMoveRight = true;
       canMoveLeft = true;
    }

}
