using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public PlayerController player;

    public float followDistance;
    public float maxDistance;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Transform groundCheck;
    [HideInInspector]
    public GameObject standingOn;
    public LayerMask ground, playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Collider2D col = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        if (standingOn != player.standingOn && player.standingOn != null)
        {
            standingOn = player.standingOn;
            CheckStandingOn();
        }

        if (player.standingOn != null && Vector3.Distance(player.transform.position, transform.position) > followDistance)
        {
            Vector2 dir = player.transform.position - transform.position;
            dir.y = 0;
            rb.velocity = dir.normalized * moveSpeed;
        }

        if (Vector3.Distance(player.transform.position, transform.position) > maxDistance && player.standingOn != null)
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    void CheckStandingOn()
    {
        if (Mathf.Abs(groundCheck.position.y - player.groundCheck.position.y) > 0.1f)
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }
}
