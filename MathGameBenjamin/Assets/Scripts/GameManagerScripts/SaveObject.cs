using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveObject
{
    public int score;
    public int lives;
    public int level;
    public List<float> xcoord = new List<float>();
    public List<float> ycoord = new List<float>();
    public List<float> mathXCoord = new List<float>();
    public List<float> mathYCoord = new List<float>();
    public float health;
}
