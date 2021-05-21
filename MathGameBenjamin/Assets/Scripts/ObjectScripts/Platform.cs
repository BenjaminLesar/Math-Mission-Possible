using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = pos1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pos1.position){

            nextPos = pos2.position;
            if(gameObject.layer == 12)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

        }     

        if(transform.position == pos2.position){

            nextPos = pos1.position;
            if (gameObject.layer == 12)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }  

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

        
    }

private void OnDrawGizmos(){

    Gizmos.DrawLine(pos1.position, pos2.position);
}

}
