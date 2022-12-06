using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xMove = 0f;
    int maxJumps = 2;
    int numJumps = 0;
    float radiusCheck = 0.2f;

    public float xMoveSpeed = 5f;
    public float jumpSpeed = 525f;

    public Rigidbody2D rb;
    public Transform groundCheck;
    [HideInInspector]
    public GameObject standingOn;
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
        {
            numJumps = 0;
            standingOn = null;
        }
        else
            standingOn = col.gameObject;

        Collider2D wallCol = Physics2D.OverlapCircle(new Vector3(transform.position.x + Input.GetAxis("Horizontal"), transform.position.y, transform.position.z), radiusCheck, ground);

        if (wallCol == null)
        {
            numJumps = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && numJumps < maxJumps)
        {
            numJumps++;
            rb.velocity = Vector2.up * jumpSpeed;
        }
    }
}
