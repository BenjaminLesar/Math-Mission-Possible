﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class MultiplierScript : MathParent
{
    // Displays variables gathered within the question text
    // Multiplies variables and assigns this number as the correct answer
    override
    public int Question(Text questionText)
    {
        int f = Random.Range(1, 11);
        int s = Random.Range(0, 10);
        questionText.text = "" + f + " X " + s + " = ";
        return  f * s;
    }
}
