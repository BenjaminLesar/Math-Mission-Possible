using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMiniGame : MonoBehaviour
{

    public GameObject[] pillars;
    public GameObject[] pillarTriggers;
    public GameObject player;

    public Vector2[] startPos;
    public Vector2[] endPos;

    public float moveSpeed;

    public Vector2 teleportPos;

    void OnStart()
    {
        pillars[0].transform.position = startPos[0];
        pillars[1].transform.position = startPos[1];
        pillars[2].transform.position = startPos[2];
        pillars[3].transform.position = startPos[3];
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            pillars[0].transform.position = Vector2.MoveTowards(endPos[0], endPos[0], moveSpeed * Time.deltaTime);
            pillars[1].transform.position = Vector2.MoveTowards(endPos[1], endPos[1], moveSpeed * Time.deltaTime);
            pillars[2].transform.position = Vector2.MoveTowards(endPos[2], endPos[2], moveSpeed * Time.deltaTime);
            pillars[3].transform.position = Vector2.MoveTowards(endPos[3], endPos[3], moveSpeed * Time.deltaTime);
            pillarTriggers[0].SetActive(true);
            pillarTriggers[1].SetActive(true);
            pillarTriggers[2].SetActive(true);
            pillarTriggers[3].SetActive(true);
            ResetPlayer();

        }

    }

    void ResetPlayer()
    {
        ShapeScript geoScript = FindObjectOfType<ShapeScript>();
        geoScript.DoMath();
        player.transform.position = teleportPos;

    }
}
