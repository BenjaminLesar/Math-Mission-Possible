using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartFirstLevel()
    {
        PlayerPrefs.SetString("IsNotFirst", "false");
        FindObjectOfType<Player>().SetLoaded(false);
        SceneManager.LoadScene(1);
    }
}
