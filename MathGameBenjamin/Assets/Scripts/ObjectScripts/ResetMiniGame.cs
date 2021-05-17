using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMiniGame : MonoBehaviour
{
    public GameObject[] pillars;
    public GameObject[] pillarTriggers;
    public GameObject player;

    public Vector2 teleportPos;
    public Minigame2 myPillar { get; set; }

    Vector3[] start = new Vector3[4];
    private void Awake()
    {
        for (int i=0; i<4; i++)
        {
            start[i] = pillars[i].transform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        myPillar.StopRaisePillar(); // need to stop animation first before set position
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i=0; i < pillars.Length; i++)
            {
                pillars[i].transform.position = start[i];
                pillarTriggers[i].GetComponent<BoxCollider2D>().enabled = true;
            }
            ResetPlayer();
            
        }
    }

    void ResetPlayer()
    {
        player.transform.position = teleportPos;
    }
}
