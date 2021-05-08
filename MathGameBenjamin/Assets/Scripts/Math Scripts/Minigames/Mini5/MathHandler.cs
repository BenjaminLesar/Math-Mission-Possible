using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MathHandler : MonoBehaviour
{
    public GameObject[] mathCanvas;
    public GameObject multCanvas;
    public GameObject addCanvas;
    public GameObject wordCanvas;
    public GameObject shapeCanvas;

    MultiplierScript multScript;
    RepAddScript addScript;
    WordScript wordScript;
    ShapeScript shapeScript;  

    public MultiplierScript MultScript { get=> multScript; set=> multScript=value; }
    public RepAddScript AddScript { get => addScript; set => addScript = value; }
    public WordScript WordScript { get => wordScript; set => wordScript = value; }
    public ShapeScript ShapeScript { get => shapeScript; set => shapeScript = value; }


    void Awake()
    {
        multScript = multCanvas.GetComponent<MultiplierScript>();
        addScript = addCanvas.GetComponent<RepAddScript>();
        wordScript = wordCanvas.GetComponent<WordScript>();
        shapeScript = shapeCanvas.GetComponent<ShapeScript>();
        RandomQuestion();
    }

    public void RandomQuestion()
    {
        DisableCanvas();
        int Idx = Random.Range(0, mathCanvas.Length);
        mathCanvas[Idx].SetActive(true);
        var mathScript = mathCanvas[Idx].GetComponent<MathScript>();

        mathScript.OpenMath();
        mathScript.Question(Idx);
        
        //return Idx;
    }

    public void DisableCanvas()
    {
        foreach (GameObject canv in mathCanvas)
        {
            canv.SetActive(false);
        }
    }

}
