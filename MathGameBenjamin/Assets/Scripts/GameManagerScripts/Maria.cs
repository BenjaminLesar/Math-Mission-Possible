using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Maria : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartFirstLevel()
    {
        PlayerPrefs.SetString("Character", "Maria");
        PlayerPrefs.SetString("IsNotFirst", "false");
        FindObjectOfType<Player>().SetLoaded(false);
        SceneManager.LoadScene(1);
    }

}
