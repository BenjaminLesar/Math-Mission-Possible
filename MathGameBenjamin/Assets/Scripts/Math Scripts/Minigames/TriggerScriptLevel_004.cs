using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class TriggerScriptLevel_004 : MonoBehaviour
{
    public static TriggerScriptLevel_004 instance;
    public GameObject panel;

    [SerializeField] Animator boxAnimator;
    Minigame4 multScript;

    void Awake()
    {
        instance = this;
        multScript = transform.GetChild(0).gameObject.GetComponent<Minigame4>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetString("mathQuestion", this.name);
            multScript.DoMath();
            Player.instance.FreezePlayer();
            panel.SetActive(true);
            boxAnimator.SetTrigger("Popup");
        }
    }
}
