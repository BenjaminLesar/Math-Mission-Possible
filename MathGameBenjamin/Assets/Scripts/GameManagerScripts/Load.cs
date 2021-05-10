using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public class Load : MonoBehaviour
{
    public void LoadGame()
    {
        GameObject[] LoadMenuCanvas = GameObject.FindGameObjectsWithTag("LoadMenuCanvas");
        GameObject myCanvas = LoadMenuCanvas[0].transform.GetChild(0).gameObject;

        string LoadName = PlayerPrefs.GetString("name");
        if (File.Exists(Application.persistentDataPath + "/" + LoadName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + LoadName, FileMode.Open);
            SaveObject mySave = (SaveObject)bf.Deserialize(file);
            file.Close();

            FindObjectOfType<Player>().SetLoaded(true);
            myCanvas.SetActive(false);
            SceneManager.LoadScene(mySave.level);
            Time.timeScale = 1;
        }
    }
}
