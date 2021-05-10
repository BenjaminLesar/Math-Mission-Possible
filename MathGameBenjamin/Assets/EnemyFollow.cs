using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject Player;
    private Transform playerPos;
    private Vector2 currentPos;
    public float distance;
    public float speedEnemy;

    BoxCollider2D myBody;
    void Start()
    {
        playerPos = Player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
        myBody = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerPos.position) < distance )//&& myBody.IsTouchingLayers(LayerMask.GetMask("Water")))
        {

            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);
        }

        else
        {
            if (Vector2.Distance(transform.position, currentPos) <= 0)
            {

            }

            else
            {
                transform.position =  Vector2.MoveTowards(transform.position,currentPos,speedEnemy * Time.deltaTime);
            }
           
        }
        
    }
}
