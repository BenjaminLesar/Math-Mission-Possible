using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int pointsForCoinPickup = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
        AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
