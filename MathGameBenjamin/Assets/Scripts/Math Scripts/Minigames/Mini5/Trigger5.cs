using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger5 : MonoBehaviour
{
    GameObject myCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        myCanvas = GameObject.Find("/Math UI");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetString("mathQuestion", this.name);
            Time.timeScale = 0;
            Player.instance.FreezePlayer();
        }
    }
}
