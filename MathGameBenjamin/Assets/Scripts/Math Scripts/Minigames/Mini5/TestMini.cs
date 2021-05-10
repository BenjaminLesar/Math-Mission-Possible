using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMini : MathParent
{
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    override
    public int Question(Text questionText)
    {
        int f = Random.Range(1, 11);
        int s = Random.Range(0, 10);
        questionText.text = "" + f + " X " + s + " = ";
        return f * s;
    }

}
