using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    // Start is called before the first frame update
    public void PauseGame()
    {
        GameObject[] PauseMenuCanvas = GameObject.FindGameObjectsWithTag("PauseMenuCanvas");
        GameObject myCanvas = PauseMenuCanvas[0].transform.GetChild(0).gameObject;
        myCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}