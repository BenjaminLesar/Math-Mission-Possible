using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBlock : MonoBehaviour
{
    public float speed;
    public Vector3 startpos;
    public Vector3 targetpos;
    public Vector3 settingpos;
    public GameObject enemy;

    PolygonCollider2D damageCollider; 


    void Start()
    {
        startpos = enemy.transform.position;
        targetpos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 3, 0);
        settingpos = targetpos;

        damageCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 poss = transform.position;
        if(poss!=targetpos)
        {
            transform.position = Vector3.MoveTowards(poss, targetpos, speed * Time.deltaTime);

        }

        else
        {
            targetpos = startpos;
            if (poss == startpos){

                targetpos = settingpos;
            }
        }
        Die();
    }

    private void Die()
    {
        BoxCollider2D playerFeet = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        
        if (playerFeet.IsTouching(damageCollider))
        {
        
            Destroy(gameObject);
            
        }
    }
}
