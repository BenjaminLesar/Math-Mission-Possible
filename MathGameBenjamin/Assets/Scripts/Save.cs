using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    public InputField textBox;

    public void SaveGame()
    {
        coinPickup[] result = FindObjectsOfType<coinPickup>();
        int x = 0;

        foreach (coinPickup c in result)
        {
            
            PlayerPrefs.SetFloat("x" + x.ToString(), c.transform.position.x);
            PlayerPrefs.SetFloat("y" + x.ToString(), c.transform.position.y);
            x++;
        }

        PlayerPrefs.SetInt("len", result.Length);

        //Debug.Log("x position " + x);
        //Debug.Log("y position " + y);
       


        PlayerPrefs.SetString("name", "Benjamin");
        PlayerPrefs.SetInt("score", FindObjectOfType<GameSession>().GetScore());
        PlayerPrefs.SetInt("lives", FindObjectOfType<GameSession>().GetLives());
        PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetFloat("health", FindObjectOfType<Player>().GetHealth());

        PlayerPrefs.Save();
        //PlayerPrefs.SetString("name", textBox.text);

    }
}

