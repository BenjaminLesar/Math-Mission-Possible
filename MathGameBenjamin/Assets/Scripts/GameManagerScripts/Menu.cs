using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void CharChoice()
    {
        PlayerPrefs.SetString("Character", "Maria");
        GameObject myParent = gameObject.transform.parent.gameObject;
        myParent.transform.GetChild(4).gameObject.SetActive(true);
        myParent.transform.GetChild(5).gameObject.SetActive(true);
        myParent.transform.GetChild(6).gameObject.SetActive(true);
        myParent.transform.GetChild(7).gameObject.SetActive(true);
        myParent.transform.GetChild(8).gameObject.SetActive(true);

        myParent.transform.GetChild(3).gameObject.SetActive(false);
        myParent.transform.GetChild(2).gameObject.SetActive(false);
        myParent.transform.GetChild(1).gameObject.SetActive(false);
        myParent.transform.GetChild(0).gameObject.SetActive(false);
    }
}
