using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathScript : MathParent
{
    public Image currentShape;
    public Sprite[] shapeSprites;

    public MathHandler mathHandler;

    public void Question(int Idx)
    {
        input.text = null;
        if (Idx == 3)
            realAnswer = mathHandler.MathScript[Idx].Question(questionText, currentShape);
        else
            realAnswer = mathHandler.MathScript[Idx].Question(questionText);

        //switch (Idx)
        //{
        //    case 0:
        //        multScript = mathHandler.MultScript;
        //        realAnswer = multScript.Question(questionText);
        //        break;
        //    case 1:
        //        addScript = mathHandler.AddScript;
        //        realAnswer = addScript.Question(questionText);
        //        break;
        //    case 2:
        //        wordScript = mathHandler.WordScript;
        //        realAnswer = wordScript.Question(questionText);
        //        break;
        //    case 3:
        //        shapeScript = mathHandler.ShapeScript;
        //        realAnswer = shapeScript.Question(questionText, currentShape);
        //        break;

        //}    
    }

    public void OpenMath()
    {
        boxAnimator.SetTrigger("Popup");
    }

    void Challenge()
    {
        mathHandler.RandomQuestion();
    }

    override
    public void Return()
    {
        mathHandler.RandomQuestion();
        incorrectAnswerPanel.SetActive(false);
    }

    void Continue2()
    {
        input.text = null;
        Destroy(gameObject.transform.parent.gameObject);
        Time.timeScale = 1;
        Player.instance.UnFreezePlayer();
    }

    override
    public void CheckAnswer()
    {
       
        if(currentShape) currentShape.sprite = shapeSprites[0];

        playerAnswer = Convert.ToInt32(input.text);

        if (playerAnswer == realAnswer)
        {
            Challenge();
        }

        else
        {
            incorrectAnswerPanel.SetActive(true);
            incorrectText.text = "Incorrect! Please try again";
        }
    }
}
