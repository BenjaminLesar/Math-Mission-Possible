using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame2 : MonoBehaviour
{
    public static Minigame2 instance;

    public GameObject pillar;
    public GameObject pillarTrigger;
    public GameObject canvas;
    public GameObject player;
    public float moveSpeed;

    public Vector2 startPos;
    public Vector2 endPos;

    GameObject canvasParent;
    Animator boxAnimator;
    void Awake()
    {
        instance = this;
        //GameObject canvas = GameObject.Find("ShapeCanvas");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!canvasParent)
            {
                canvasParent = GameObject.Find("ShapeCanvasParent");
                canvas = canvasParent.transform.GetChild(0).gameObject;
                boxAnimator = canvas.GetComponentInChildren<Animator>();
            }

            canvas.SetActive(true);
            Player.instance.FreezePlayer();
            ShapeScript geoScript = FindObjectOfType<ShapeScript>();
            geoScript.DoMath();

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
