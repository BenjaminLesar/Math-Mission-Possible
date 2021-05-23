using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TeleportTo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Player.instance.FreezePlayer();
        SceneManager.LoadScene("World_001Ending");
    }
}
