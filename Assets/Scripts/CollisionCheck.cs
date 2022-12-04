using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private bool collision = false;
    private BoxCollider2D box;

    // Start is called before the first frame update
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D()
    {
        //Debug.Log("hit");
        collision = true;
    }

    public bool GetCollision()
    {
        //Collider2D tmp = Physics2D.OverlapBox(transform.position, new Vector3(transform.localScale.x, transform.localScale.y), 0f);
        //return tmp;
        
        return collision;
    }

    public void SetTrigger(bool set)
    {
        //box.isTrigger = set;
        if (box != null)
            box.enabled = set;
    }
}
