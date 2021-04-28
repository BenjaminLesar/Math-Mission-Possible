using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    bool isOpen = false;
    void Awake()
    {
        instance = this;
        //GameObject canvas = GameObject.Find("ShapeCanvas");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Animator boxAnimator = GameObject.Find("ShapeAnimator").GetComponent<Animator>();
            GameObject canvas = GameObject.Find("ShapeCanvas");
            canvas.SetActive(true);
            Player.instance.FreezePlayer();
            ShapeScript geoScript = FindObjectOfType<ShapeScript>();

            // prevent MathUI open twice since player has two collider boxes
            if (!isOpen)
            {
                isOpen = true;
                geoScript.DoMath();
            }

            boxAnimator.SetTrigger("Popup");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {    
        if(other.gameObject.CompareTag("Player"))
        {
            player = GameObject.Find("Player");
            player.transform.position = Vector2.MoveTowards(endPos, endPos, moveSpeed * Time.deltaTime);
            pillar.transform.position = Vector2.MoveTowards(endPos, endPos, moveSpeed* Time.deltaTime);
        }
    }


   public void RaisePillar()
    {
        //pillarTrigger.SetActive(false);
        player.transform.position += new Vector3(0, 0.1f); //get the player out of trigger box
    }

}
