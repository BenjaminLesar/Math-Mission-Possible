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
    public Animator playerAnim;

    IEnumerator timer()
    {
        Player.instance.FreezePlayer();
        yield return new WaitForSeconds(1);
        playerAnim.SetBool("EndingScene", true);
        yield return new WaitForSeconds(1);
        playerAnim.SetBool("EndingSceneIdle", true);
        yield return new WaitForSeconds(1);
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
