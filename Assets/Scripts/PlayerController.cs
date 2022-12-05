using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xMove = 0f;
    bool canJump = true;
    int maxJumps = 2;
    int numJumps = 0;
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
    void Update()
    {
        //Horizontal
        xMove = Input.GetAxis("Horizontal") * xMoveSpeed;
        rb.velocity = new Vector2(xMove, rb.velocity.y);

        Collider2D col = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, ground);
        if (col == null)
            canJump = false;
        else
        {
            canJump = true;
            numJumps = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && numJumps < maxJumps)
        {
            numJumps++;
            rb.AddRelativeForce(Vector2.up * jumpSpeed * 2f);

            if (numJumps > maxJumps)
                canJump = false;
        }
    }
}
