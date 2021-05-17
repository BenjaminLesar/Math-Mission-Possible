using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame2 : MonoBehaviour
{
    //public static Minigame2 instance;

    public GameObject pillar;
    public GameObject canvas;
    public GameObject player;

    public float moveSpeed =5;

    Vector2 startPos;
    Vector2 endPos;

    GameObject canvasParent;
    Animator boxAnimator;
    BoxCollider2D myBox;
    ShapeScript geoScript;
    float heightToRaise = 3.5f;
    ResetMiniGame gameReset;
    Coroutine raisePillar;

    void Awake()
    {
        myBox = GetComponent<BoxCollider2D>();
        startPos = pillar.transform.position;
        endPos = startPos + new Vector2(0, heightToRaise);

        if (!canvasParent)
        {
            canvasParent = GameObject.Find("ShapeCanvasParent");

        }
        gameReset = FindObjectOfType<ResetMiniGame>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas = canvasParent.transform.GetChild(0).gameObject;
            canvas.SetActive(true);

            boxAnimator = canvas.GetComponentInChildren<Animator>();
            geoScript = FindObjectOfType<ShapeScript>();

            Player.instance.FreezePlayer();
            geoScript.DoMath();
            boxAnimator.SetTrigger("Popup");
            geoScript.myPillar = this; // mark this so the according pillar can raise
            gameReset.myPillar = this; 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {    
        if(other.gameObject.CompareTag("Player"))
        {
            player = GameObject.Find("Player");
            raisePillar = StartCoroutine( MoveAnimation());
        }
    }

   public void RaisePillar()  //=> exit trigger
    {
        myBox.enabled = false;
    }


    public void StopRaisePillar()
    {
        StopCoroutine(raisePillar);
    }



    IEnumerator MoveAnimation()
    {
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime/moveSpeed;
            pillar.transform.position = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
    }

}
