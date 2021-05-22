using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    // Start is called before the first frame update
    public void SeeControls()
    {
        GameObject PauseCanvasParent = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        GameObject PauseMenuCanvas = PauseCanvasParent.transform.GetChild(0).gameObject;
        GameObject ReturnCanvas = PauseCanvasParent.transform.GetChild(2).gameObject;
        ReturnCanvas.SetActive(true);
        PauseMenuCanvas.SetActive(false);
    }
}
