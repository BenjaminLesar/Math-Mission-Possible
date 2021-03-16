using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    PolygonCollider2D damageCollider;
    bool isAlive = true;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        damageCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isAlive)
        {
            return;
        }

        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }

        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
        
        bool IsFacingRight()
        {
            return transform.localScale.x > 0;
        }
        Die();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    private void Die()
    {
        BoxCollider2D playerFeet = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        if (playerFeet.IsTouching(damageCollider))
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }
}
