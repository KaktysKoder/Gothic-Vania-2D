using UnityEngine;

/// <summary>
/// Сосотояния анимаций.
/// </summary>
public enum AnimState : int
{
    Idle = 0,
    Walk = 1,
    Jump = 2,
    Crouch = 3,
}

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 2.0f;

    public BoxCollider2D boxCollider1;      // enabled defalut or first  default collider
    public BoxCollider2D boxCollider2;      // crouch
    private SpriteRenderer sprite;
    private Animator anim;
    private Rigidbody2D rb2D;
    private bool facingRight;
    private float moveInput;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");

        rb2D.velocity = new Vector2(moveInput, rb2D.velocity.y);

        //if (facingRight == true && moveInput > 0)
        //{
        //    Flip();
        //}
        //else if (facingRight == false && moveInput < 0)
        //{
        //    Flip();
        //}

        if (moveInput == 0) // Если игрок ничего не делает.
        {
            anim.SetInteger("Anim", (int)AnimState.Idle);
        }

        if (moveInput > 0 || moveInput < 0)
        {
            anim.SetInteger("Anim", (int)AnimState.Walk);
        }// Если игрок не стоит на месте, значит двигается (Walk)

        if (moveInput > 0)
        {
            sprite.flipX = false;
        }
        else if(moveInput < 0)
        {
            sprite.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Crouch();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            CrouchOff();    // отпустили клавишу
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    private void Jump()
    {
        anim.SetInteger("Anim", (int)AnimState.Jump);

        rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scaler = transform.localScale;
        scaler.x *= -1;

        transform.localScale = scaler;
    }
}