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
        int speed = 1;
        float lerpTime = 10000f;
        lerpTime = lerpTime / speed; //if speed = 1, it takes 5s to finsih the animation
        float currentTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentTime += Time.deltaTime;
            t = currentTime / lerpTime;
            this.transform.position = Vector3.Lerp(this.transform.position, endPoint.position, t);
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
