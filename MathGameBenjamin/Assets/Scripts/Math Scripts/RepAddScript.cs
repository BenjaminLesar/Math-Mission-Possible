using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
public class RepAddScript : MathParent
{
    public static RepAddScript instance;
  
    void Awake()
    {
        instance = this;
    }

    override
    public int Question(Text questionText)
    {
        int n = Random.Range(1, 11);
        int nRange = Random.Range(3, 7); // 3-6
        String tempText = "";


        for (int i = 0; i < nRange - 1; i++)
        {
            tempText += n + " + ";
        }
        tempText += n + " = ";

        questionText.text = tempText;
        return  n * nRange;
    }
}
