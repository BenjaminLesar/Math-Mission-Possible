using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    public void LoadLevel()
    {
        FindObjectOfType<Player>().SetLoaded(true);
        SceneManager.LoadScene(PlayerPrefs.GetInt("level"));

        
    }
}
