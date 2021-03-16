using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crates : MonoBehaviour
{

    PolygonCollider2D damageCollider; 
    // Start is called before the first frame update
    void Start()
    {
        
        Crush();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Crush()
    {
        BoxCollider2D playerFeet = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        if (playerFeet.IsTouching(damageCollider))
        {
        
            Destroy(gameObject);
        }
    }
}
