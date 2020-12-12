using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 2.0f;
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(Input.GetAxis("Horizontal"), rb2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}