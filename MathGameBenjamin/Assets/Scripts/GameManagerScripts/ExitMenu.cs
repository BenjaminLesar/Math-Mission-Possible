using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Exit()
    {
        //PlayerPrefs.SetString("IsNotFirst", "false");
        GameObject[] PauseMenuCanvas = GameObject.FindGameObjectsWithTag("PauseMenuCanvas");
        GameObject myCanvas = PauseMenuCanvas[0].transform.GetChild(0).gameObject;
        myCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
