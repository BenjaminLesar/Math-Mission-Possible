using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class TriggerScriptLevel_004 : MonoBehaviour
{
    public GameObject[] triggerObject;
    public static TriggerScriptLevel_004 instance;
    public GameObject panel;

    [SerializeField] Animator boxAnimator;
    Minigame4Script multScript;

    void Awake()
    {

        instance = this;
        multScript = FindObjectOfType<Minigame4Script>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            multScript.DoMath();
            Player.instance.FreezePlayer();
            panel.SetActive(true);
            boxAnimator.SetTrigger("Popup");
        }
        Destroy(gameObject);
    }
}
