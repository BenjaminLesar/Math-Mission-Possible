using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class ShapeScript : MathParent
{

    public Sprite[] shapeSprites;
    public Image currentShape;

    public Minigame2 myPillar { get; set; }
    override
    public int Question(Text questionText, Image currentShape)
    {
        int n = Random.Range(1, 11);
        int shapeIdx = Random.Range(1, shapeSprites.Length);  //ignore the first one because it is null
        currentShape.sprite = shapeSprites[shapeIdx];

        if (shapeIdx == 1)
        {
            questionText.text = "" + n + "in.";
            realAnswer = 4 * n;
        }

        else if (shapeIdx == 2)
        {
            questionText.text = "" + n + "in.";
            realAnswer = 5 * n;
        }

        else if (shapeIdx == 3)
        {
            questionText.text = "" + n + "in.";
            realAnswer = 6 * n;
        }

        else if (shapeIdx == 4)
        {
            questionText.text = "" + n + "in.";
            realAnswer = 8 * n;
        }
        return realAnswer;

    }
    override
    public  void Return()
    {
        //Debug.Log("return");
        realAnswer = Question(questionText, currentShape);
        incorrectAnswerPanel.SetActive(false);
    }

    override
    public void DisableCanvas()
    {
        //Minigame2.disableTrigger();// RaisePillar();
        myPillar.RaisePillar();
        correctAnswerPanel.SetActive(false);
        gameObject.SetActive(false);
        
    }

    override
    public void CheckAnswer()
    {
        // disable image
        currentShape.sprite = shapeSprites[0];

        playerAnswer = Convert.ToInt32(input.text);

        if (playerAnswer == realAnswer)
        {
            correctAnswerPanel.SetActive(true);
            correctText.text = "CORRECT! GREAT JOB!";

        }

        else
        {
            incorrectAnswerPanel.SetActive(true);
            incorrectText.text = "Incorrect! Please try again";
        }
        input.text = "";
    }

}
