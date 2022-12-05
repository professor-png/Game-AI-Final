using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xMove = 0f;
    bool canJump = true;
    float radiusCheck = 0.2f;

    public float xMoveSpeed = 5f;
    public float jumpSpeed = 525f;

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Horizontal
        xMove = Input.GetAxis("Horizontal") * xMoveSpeed;
        rb.velocity = new Vector2(xMove, rb.velocity.y);

        Collider2D col = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, ground);
        if (col == null)
            canJump = false;
        else
            canJump = true;

        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.AddRelativeForce(Vector2.up * jumpSpeed);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (canJump == false)
        {
            rb.velocity = Vector2.zero;

            if (Input.GetKey(KeyCode.Space))
            {
                canJump = false;
                Vector2 dir = Vector3.up + (collision.transform.position - transform.position);
                rb.AddRelativeForce(dir * jumpSpeed);
            }
        }
    }
}
