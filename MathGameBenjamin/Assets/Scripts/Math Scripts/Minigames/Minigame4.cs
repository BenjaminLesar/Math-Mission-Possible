using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Minigame4 : MathParent
{

    Slider energyBar;
    float energyValue = 0;

    string mathQuestion;
    TextController txtController;

    Text challengeText;
    public GameObject energyPannel;

    

    override
    public void DoMath()
    {
        txtController = gameObject.transform.GetChild(1).gameObject.GetComponent<TextController>();
        challengeText = gameObject.transform.GetChild(0).transform.GetChild(6).transform.GetChild(0).GetComponent<Text>();
        gameObject.SetActive(true);
        mathQuestion = PlayerPrefs.GetString("mathQuestion");
        go = GameObject.Find(mathQuestion);
        correctAnswerPanel.SetActive(false);
        incorrectAnswerPanel.SetActive(false);
        realAnswer = Question(questionText);
        energyPannel.SetActive(true);
        energyBar = GameObject.Find("EnergyBar").GetComponent<Slider>();
        txtController.RunAnimationText(challengeText, 2);

    }

    // Displays variables gathered within the question text
    // Multiplies variables and assigns this number as the correct answer
    override
    public int Question(Text questionText)
    {
        input.text = "";
        int operand1 = Random.Range(2, 13);
        int operand2 = Random.Range(2, 13);
        int questionType = Random.Range(1, 4);

        switch (questionType)
        {
            case 1:
                questionText.text = operand1 + " x " + operand2 + " = ___";
                realAnswer = operand1 * operand2;
                break;
            case 2:
                questionText.text = "___" + " x " + operand1 + " = " + operand1 * operand2;
                realAnswer = operand2;
                break;
            case 3:
                questionText.text = operand1 + " x " + "___" + " = " + operand1 * operand2;
                realAnswer = operand2;
                break;
            default:
                print("invalid question type!");
                break;
        }
        return realAnswer;
    }

    void Challenge()
    {
        int questionNumber = 4;
        StartCoroutine(FillBarAnimation(1));
        if (energyValue > questionNumber-1)
        {
            correctAnswerPanel.SetActive(true);
            correctText.text = "You passed the challenge, congrats!!!";
            txtController.RunAnimationText(correctText, 2);
        }
        Question(questionText);
    }

    override
    public void CheckAnswer()
    {
        playerAnswer = Convert.ToInt32(input.text);

        if (playerAnswer == realAnswer)
        {
            Challenge();
        }

        else
        {
            incorrectAnswerPanel.SetActive(true);
            incorrectText.text = "Incorrect! Please try again";
            StartCoroutine(DeleteBarAnimation());
        }
    }

    IEnumerator FillBarAnimation(float value)
    {
        float speed = 6;
        float currentValue = energyValue + value;
        while (energyValue <= currentValue)
        {
            energyValue += speed*Time.deltaTime;
            energyBar.value = energyValue;
            yield return new WaitForSeconds(0.01f); 
        }
    }

    IEnumerator DeleteBarAnimation()
    {
        float speed = 5;
        while (energyValue > 0)
        {
            energyValue -= speed * Time.deltaTime;
            energyBar.value = energyValue;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void AcceptChanllenge()
    {
        GameObject clPannel = GameObject.Find("ChallengePannel");
        clPannel.SetActive(false);
    }
}
