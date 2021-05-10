using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
    public float myTestX;
    public Rigidbody2D myRB;
    public Rigidbody2D myRB2;
    public float tooFar = 4f;
    void Start()
    {
        myTestX = gameObject.transform.parent.gameObject.transform.position.x;
        myRB2 = gameObject.GetComponent<Rigidbody2D>();
        myRB = FindObjectOfType<Player>().GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameObject.transform.position.x < myTestX - 17)
        {
            
            myRB2.velocity = new Vector2(tooFar, tooFar);
        }

        if (gameObject.transform.position.x > myTestX + 17)
        {
            myRB2.velocity = new Vector2(-1 * tooFar, -1 * tooFar);
        }
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            myRB2.velocity = myRB.velocity / 4;
        }
    }
}
