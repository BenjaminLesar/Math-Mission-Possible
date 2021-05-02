using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject dialogueBox;

    GameObject target = null;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
    }


    public void ActivateElevator()
    {
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        int timeToReachTarget = 7;
        float t = 0;
        var currentPos = transform.position;
        while (t < 1)
        {
           
            t += Time.deltaTime / timeToReachTarget;
            this.transform.position = Vector3.Lerp(currentPos, endPoint.position, t);
            yield return null;
        }
    }

    // adhere the player the to elevator
    void OnTriggerStay2D(Collider2D col)
    {
        target = col.gameObject;
        offset = target.transform.position - transform.position;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        target = null;
        dialogueBox.SetActive(false);
    }
    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }
}
