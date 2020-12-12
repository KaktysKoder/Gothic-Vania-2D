using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Set references in Inspector")]
    public BoxCollider2D boxCollider1;      // Defalut
    public BoxCollider2D boxCollider2;      // Crouch

    [Header("Set value in Inspector")]
    public float jumpForce = 2.0f;
    public float heightRestrictions = 0.5f;
    public float moveSpeed = 1.0f;

    private Rigidbody2D rb2D;
    private Animator anim;
    private SpriteRenderer sprite;

    private float moveInput;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");

        rb2D.velocity = new Vector2(moveInput * moveSpeed, rb2D.velocity.y);

        if (moveInput == 0)
        {
            anim.SetInteger("Anim", (int)AnimState.Idle);
        }

        if (moveInput > 0)
        {
            sprite.flipX = false;
            anim.SetInteger("Anim", (int)AnimState.Walk);
        }
        else if (moveInput < 0)
        {
            sprite.flipX = true;
            anim.SetInteger("Anim", (int)AnimState.Walk);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (transform.position.y > heightRestrictions) return;  // Временная решение.

            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Crouch();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            CrouchOff();
        }
    }

    private void Jump()
    {
        rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

        anim.SetInteger("Anim", (int)AnimState.Jump);
    }

    private void Crouch()
    {
        anim.SetInteger("Anim", (int)AnimState.Crouch);

        boxCollider1.enabled = false;
        boxCollider2.enabled = true;
    }

    private void CrouchOff()
    {
        boxCollider1.enabled = true;
        boxCollider2.enabled = false;
    }
}