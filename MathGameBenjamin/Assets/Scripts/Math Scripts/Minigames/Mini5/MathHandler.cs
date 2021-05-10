using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MathHandler : MonoBehaviour
{
    public GameObject[] currentCanvas;
    public GameObject[] mathCanvas;

    MathParent[] mathScript;

    public MathParent[] MathScript { get => mathScript; set => mathScript = value; }

    void Awake()
    {
        int mathSize = mathCanvas.Length;
        mathScript = new MathParent[mathSize];

        for (int i = 0; i < mathSize; i++)
        {
            mathScript[i] = mathCanvas[i].GetComponent<MathParent>();
        }

        RandomQuestion();
    }

    /// <summary>
    /// 1. Disable all math canvas
    /// 2. activate a random math canvas
    /// 3. call the relative question
    /// </summary>
    public void RandomQuestion()
    {
        DisableCanvas();
        int Idx = Random.Range(0, currentCanvas.Length);
        currentCanvas[Idx].SetActive(true);
        var mathScript = currentCanvas[Idx].GetComponent<MathScript>();

        mathScript.OpenMath();
        mathScript.Question(Idx);
    }

    public void DisableCanvas()
    {
        foreach (GameObject canv in currentCanvas)
        {
            canv.SetActive(false);
        }
    }

}
