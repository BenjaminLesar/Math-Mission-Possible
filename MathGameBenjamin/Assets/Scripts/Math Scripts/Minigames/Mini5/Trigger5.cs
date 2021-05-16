using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger5 : MonoBehaviour
{
    [SerializeField] Animator mumKing;
    MathHandler mathHandler;
    public Animator MumKing { get=> mumKing; }
    void Awake()
    {
        PlayerPrefs.SetString("mathQuestion", this.name);

        mathHandler = transform.GetChild(0).gameObject.GetComponent<MathHandler>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mumKing.SetTrigger("showup");
            Invoke("StartMath", .7f);

            Player.instance.FreezePlayer();
        }
    }

    void StartMath()
    {
        mathHandler.RandomQuestion();

    }
}
