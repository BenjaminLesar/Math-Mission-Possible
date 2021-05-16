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

    GameObject engCanvas;
    Slider myEnergyBar;
    Slider bossEnergyBar;
    static float myEnergy = 4;
    static float bossEnergy = 7;
    Trigger5 triggerScript;
    private void Awake()
    {
        engCanvas = GameObject.Find("/Trigger5/MATH UI/Energy Canvas");
        myEnergyBar = engCanvas.transform.GetChild(0).GetComponent<Slider>();
        bossEnergyBar = engCanvas.transform.GetChild(1).GetComponent<Slider>();
        engCanvas.SetActive(true);
    }


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



    void NewQuestion()
    {
        mathHandler.RandomQuestion();
    }

    IEnumerator FillBarAnimation(Slider energyBar, float myEnergy, float value)
    {
        float speed = 6;
        float currentValue = myEnergy + value;
        while (myEnergy >= currentValue)
        {
            myEnergy -= speed * Time.deltaTime;
            energyBar.value = myEnergy;
            yield return new WaitForSeconds(0.01f);
        }
    }

    override
    public void Return()
    {
        NewQuestion();
        incorrectAnswerPanel.SetActive(false);
    }

    override
    public void DisableCanvas()
    {
        GameObject trigger5 = GameObject.Find("/Trigger5");
        trigger5.SetActive(false);
    }

    override
    public void CheckAnswer()
    {    
        if(triggerScript==null) triggerScript = go.GetComponent<Trigger5>();
        if (currentShape) currentShape.sprite = shapeSprites[0];

        playerAnswer = Convert.ToInt32(input.text);

        if (playerAnswer == realAnswer)
        {
            Challenge();
        }

        else
        {

            WrongAnser();
        }
    }
    void Challenge()
    {
        triggerScript.MumKing.SetTrigger("takeDMG");

        StartCoroutine(FillBarAnimation(bossEnergyBar, bossEnergy, -1));
        bossEnergy--;
        if (bossEnergy <= 0)
        {
            correctAnswerPanel.SetActive(true);
            correctText.text = "You passed the challenge, congrats!!!";
            TextController.RunningText(correctText, 2);
        }
        else
        {
            Invoke("NewQuestion", .25f); // wait for fill Bar animation

        }
    }

    void WrongAnser()
    {
        StartCoroutine(FillBarAnimation(myEnergyBar, myEnergy, -1));
        myEnergy--;
        if (myEnergy <= 0)
        {
            incorrectAnswerPanel.SetActive(true);
            returnButton.onClick.RemoveAllListeners();
            returnButton.onClick.AddListener(Reset);

            incorrectText.text = "You failed. Let's try again";
            TextController.RunningText(incorrectText, 2);

        } 
        else
        {
            incorrectAnswerPanel.SetActive(true);
            incorrectText.text = "Incorrect! \n Your energy bar is decreased.";
        }
    }

    private void Reset()
    {
        mathHandler.ResetCurrentScene();
        myEnergy = 4;
        bossEnergy = 7;
    }
}
