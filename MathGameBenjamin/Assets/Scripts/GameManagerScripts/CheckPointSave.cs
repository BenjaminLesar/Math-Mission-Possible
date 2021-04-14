using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class CheckPointSave : MonoBehaviour
{
    public static CheckPointSave instance;
    public Text checkPointMessage;
    [SerializeField] float MessageDelay = 0.3f;

    void Awake()
    {
        instance = this;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Touched checkpoint");
            PlayerPrefs.SetFloat("PlayerX", this.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", this.transform.position.y);
            PlayerPrefs.SetString("IsNotFirst", "true");

            AutoSave();

            void AutoSave()
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/" + "Autosave.dat");
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

                CheckPointSave[] SPResult = FindObjectsOfType<CheckPointSave>();

                foreach (CheckPointSave cp in SPResult)
                {
                    mySave.SavePointX.Add(cp.transform.position.x);
                    mySave.SavePointY.Add(cp.transform.position.y);
                }

                TriggerScript[] result2 = FindObjectsOfType<TriggerScript>();

                foreach (TriggerScript t in result2)
                {
                    mySave.mathXCoord.Add(t.transform.position.x);
                    mySave.mathYCoord.Add(t.transform.position.y);
                }

                mySave.playerXCoord = PlayerPrefs.GetFloat("PlayerX");
                mySave.playerYCoord = PlayerPrefs.GetFloat("PlayerY");

                mySave.health = FindObjectOfType<Player>().GetHealth();

                bf.Serialize(file, mySave);
                file.Close();
            }


            StartCoroutine(DisplayMessage());

            IEnumerator DisplayMessage()
            {
                checkPointMessage.transform.parent.gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(MessageDelay);
                checkPointMessage.transform.parent.gameObject.SetActive(false);
                Destroy(gameObject);
            }
            
        }
    }
}