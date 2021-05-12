using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_001EndScene : MonoBehaviour
{
    
    public GameObject player;
    private ParticleSystem laser;
    public ParticleSystem teleporter;
    public GameObject finalPanel;
    public Animator faderScreen;

    IEnumerator timer()
    {
        Player.instance.FreezePlayer();
        laser = gameObject.GetComponent<ParticleSystem>();
        laser.Play();
        faderScreen.SetBool("fade", true);
        yield return new WaitForSeconds(4);
        finalPanel.SetActive(true);
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        StartCoroutine(timer());
    }
}
