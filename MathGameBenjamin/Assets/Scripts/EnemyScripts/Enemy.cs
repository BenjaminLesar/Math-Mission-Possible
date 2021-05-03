using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform groundCheckpos;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D myRigidBody;
    PolygonCollider2D damageCollider;
    Collider2D bodyCollider;
    bool isAlive = true;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        damageCollider = GetComponent<PolygonCollider2D>();
        bodyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isAlive)
        {
            return;
        }

        if (NeedToTurn())
        {
            Flip();
        }
        Move();
        Die();
    }

    void Move()
    {
        myRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }

    void Flip()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        moveSpeed *= -1;
    }

    bool NeedToTurn()
    {
        return bodyCollider.IsTouchingLayers(groundLayer)  // hitting wall
            || !Physics2D.OverlapCircle(groundCheckpos.position, 0.1f, groundLayer);  //facing edge
    }

    private void Die()
    {
        BoxCollider2D playerFeet = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        if (playerFeet.IsTouching(damageCollider))
        {
            FindObjectOfType<AudioController>().Play("Explosion");
            isAlive = false;
            Destroy(gameObject);
        }
    }


}
