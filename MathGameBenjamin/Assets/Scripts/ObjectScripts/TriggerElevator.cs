using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerElevator : MonoBehaviour
{
    public Elevator elevator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        elevator.ActivateElevator();
        Destroy(gameObject);
    }
}
