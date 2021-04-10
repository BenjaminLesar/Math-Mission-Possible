using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Save : MonoBehaviour
{
    public InputField textBox;
    public Text saveGameMessage;
    [SerializeField] float MessageDelay = 0.3f;

    public void SaveGame()
    {
        GameObject[] SaveMenuCanvas = GameObject.FindGameObjectsWithTag("SaveMenuCanvas");
        GameObject myCanvas = SaveMenuCanvas[0].transform.GetChild(0).gameObject;

        //GameObject myText = myCanvas.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1);
        //InputField myText2 = (InputField)myText;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + textBox.text + ".dat");
        SaveObject mySave = new SaveObject();
        mySave.score = FindObjectOfType<GameSession>().GetScore();
        mySave.lives = FindObjectOfType<GameSession>().GetLives();
        mySave.level = SceneManager.GetActiveScene().buildIndex;
         
        coinPickup[] result = FindObjectsOfType<coinPickup>();

        foreach (coinPickup c in result)
        {
            mySave.xcoord.Add(c.transform.position.x);
            mySave.ycoord.Add(c.transform.position.y);
        }

        TriggerScript[] result2 = FindObjectsOfType<TriggerScript>();

        foreach (TriggerScript t in result2)
        {
            mySave.mathXCoord.Add(t.transform.position.x);
            mySave.mathYCoord.Add(t.transform.position.y);
        }

        mySave.health = FindObjectOfType<Player>().GetHealth();

        bf.Serialize(file, mySave);
        file.Close();

        StartCoroutine(DisplayMessage());

        IEnumerator DisplayMessage()
        {    
            saveGameMessage.transform.parent.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(MessageDelay);
            myCanvas.SetActive(false);
            saveGameMessage.transform.parent.gameObject.SetActive(false);
        }
    }
}

