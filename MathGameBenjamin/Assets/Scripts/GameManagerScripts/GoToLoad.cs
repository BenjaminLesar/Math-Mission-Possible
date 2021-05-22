using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GoToLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadMenu()
    {
        GameObject[] LoadMenuCanvas = GameObject.FindGameObjectsWithTag("LoadMenuCanvas");
        GameObject myCanvas = LoadMenuCanvas[0].transform.GetChild(0).gameObject;

        if (Directory.Exists(Application.persistentDataPath))
        {
            string[] m_DropOptions = Directory.GetFiles(Application.persistentDataPath);
            PlayerPrefs.SetString("name", Path.GetFileName(m_DropOptions[0]));
        }

        myCanvas.SetActive(true);
        gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        //DontDestroyOnLoad(LoadMenuCanvas);
    }
}
