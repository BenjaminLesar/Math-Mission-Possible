using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public static MiniGame instance;

    public GameObject pillar;
    public GameObject pillarTrigger;
    public GameObject canvas;
    public GameObject player;
    public float moveSpeed;

    public Vector2 startPos;
    public Vector2 endPos;



    //[SerializeField] Animator boxAnimator;

    void Awake()
    {

        instance = this;

    }


 


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            Player.instance.FreezePlayer();
            canvas.SetActive(true);
            //boxAnimator.SetTrigger("Popup");

           
        }


    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {
            player.transform.position = Vector2.MoveTowards(endPos, endPos, moveSpeed * Time.deltaTime);
            pillar.transform.position = Vector2.MoveTowards(endPos, endPos, moveSpeed* Time.deltaTime);
        }

        
    }

   public void RaisePillar()
    {
        pillarTrigger.SetActive(false);
    }

}
