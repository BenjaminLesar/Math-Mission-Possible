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
    public List<float> SavePointX = new List<float>();
    public List<float> SavePointY = new List<float>();
    public float playerXCoord;
    public float playerYCoord;
    public float health;
}
