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

        checkMove();
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
        int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerSize.y / 2 + 0.01f, groundLayerMask);
        if (hit.collider != null)
        {
            onGround = true;
        }
        Vector2 raycastOrigin = new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.center.y);
        RaycastHit2D hitHead = Physics2D.Raycast(raycastOrigin, Vector2.down, playerSize.x / 2 + 0.01f, groundLayerMask);
        if (hitHead.collider != null)
        {
            onGround = true;
        }
        Vector2 raycastOrigin1 = new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.center.y);
        RaycastHit2D hitFoot = Physics2D.Raycast(raycastOrigin1, Vector2.down, playerSize.x / 2 + 0.01f, groundLayerMask);
        if (hitFoot.collider != null)
        {
            onGround = true;
        }
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

    private void checkMove()
    {
        int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.left, playerSize.x / 2 + 0.1f, groundLayerMask);
        Debug.DrawRay(playerCollider.bounds.center, Vector2.left * (playerSize.y / 2 + 0.1f), Color.red);
        if (hit.collider != null)
        {
            canMoveLeft = false;
        }
        Vector2 raycastOrigin = new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.max.y);
        RaycastHit2D hitHead = Physics2D.Raycast(raycastOrigin, Vector2.left, playerSize.x / 2 + 0.1f, groundLayerMask);
        Debug.DrawRay(raycastOrigin, Vector2.left * (playerSize.y / 2 + 0.1f), Color.red);
        if (hitHead.collider != null)
        {
            canMoveLeft = false;
        }
        Vector2 raycastOrigin1 = new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.min.y);
        RaycastHit2D hitFoot = Physics2D.Raycast(raycastOrigin1, Vector2.left, playerSize.x / 2 + 0.1f, groundLayerMask);
        Debug.DrawRay(raycastOrigin1, Vector2.left * (playerSize.y / 2 + 0.1f), Color.red);
        if (hitFoot.collider != null)
        {
            canMoveLeft = false;
        }
        RaycastHit2D hit1 = Physics2D.Raycast(playerCollider.bounds.center, Vector2.right, playerSize.x / 2 + 0.1f, groundLayerMask);
        Debug.DrawRay(playerCollider.bounds.center, Vector2.right * (playerSize.y / 2 + 0.1f), Color.red);
        if (hit1.collider != null)
        {
            canMoveRight = false;
        }
        Vector2 raycastOrigin2 = new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.max.y);
        RaycastHit2D hitHead1 = Physics2D.Raycast(raycastOrigin2, Vector2.right, playerSize.x / 2 + 0.1f, groundLayerMask);
        Debug.DrawRay(raycastOrigin2, Vector2.right * (playerSize.y / 2 + 0.1f), Color.red);
        if (hitHead1.collider != null)
        {
            canMoveRight = false;
        }
        Vector2 raycastOrigin3 = new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.min.y);
        RaycastHit2D hitFoot1 = Physics2D.Raycast(raycastOrigin3, Vector2.right, playerSize.x / 2 + 0.1f, groundLayerMask);
        Debug.DrawRay(raycastOrigin3, Vector2.right * (playerSize.y / 2 + 0.1f), Color.red);
        if (hitFoot1.collider != null)
        {
            canMoveRight = false;
        }
        if (hit.collider == null && hitHead.collider == null && hitFoot.collider == null)
        {
            canMoveLeft = true;
        }
        if (hit1.collider == null && hitHead1.collider == null && hitFoot1.collider == null)
        {
            canMoveRight = true;
        }
    }

}
