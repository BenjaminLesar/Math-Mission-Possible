using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnYesOrNo : MonoBehaviour
{
    // Start is called before the first frame update
    public void AskReturn()
    {
        GameObject PauseCanvasParent = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        GameObject PauseMenuCanvas = PauseCanvasParent.transform.GetChild(0).gameObject;
        GameObject ReturnCanvas = PauseCanvasParent.transform.GetChild(1).gameObject;
        ReturnCanvas.SetActive(true);
        PauseMenuCanvas.SetActive(false);
    }
}
