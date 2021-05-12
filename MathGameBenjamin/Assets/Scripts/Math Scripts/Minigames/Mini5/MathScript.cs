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
