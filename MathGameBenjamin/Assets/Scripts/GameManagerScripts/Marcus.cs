using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Marcus : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartFirstLevel()
    {
        PlayerPrefs.SetString("Character", "Marcus");
        PlayerPrefs.SetString("IsNotFirst", "false");
        FindObjectOfType<Player>().SetLoaded(false);
        SceneManager.LoadScene(1);
    }

}
