using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPause : MonoBehaviour
{
    // Start is called before the first frame update
    public void Back()
    {
        GameObject ReturnCanvas = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        GameObject PauseMenuCanvas = ReturnCanvas.transform.parent.gameObject.transform.GetChild(0).gameObject;
        PauseMenuCanvas.SetActive(true);
        ReturnCanvas.SetActive(false);
    }
}
