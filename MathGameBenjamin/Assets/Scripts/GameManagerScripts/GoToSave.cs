using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSave : MonoBehaviour
{
    // Start is called before the first frame update
    public void SaveMenu()
    {
        GameObject[] SaveMenuCanvas = GameObject.FindGameObjectsWithTag("SaveMenuCanvas");
        GameObject myCanvas = SaveMenuCanvas[0].transform.GetChild(0).gameObject;

        myCanvas.SetActive(true);
        gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
