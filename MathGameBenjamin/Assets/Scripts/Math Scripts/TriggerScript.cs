using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class TriggerScript : MonoBehaviour
{

    public GameObject[] triggerObject;
    public static TriggerScript instance;
    public GameObject canvas;

    [SerializeField] Animator boxAnimator;



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
            boxAnimator.SetTrigger("Popup");
        }
    }

    public void DestroyTriggerPoint()
    {
        Destroy(gameObject);
    }
}
