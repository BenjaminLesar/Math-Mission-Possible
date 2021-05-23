using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Add_MultiChoice : MonoBehaviour
{
    [SerializeField] Text questionText;
    [SerializeField] Text correctText;
    [SerializeField] Text incorrectText;

    [SerializeField] Button answer1;
    [SerializeField] Button answer2;
    [SerializeField] Button answer3;
    [SerializeField] Button answer4;
    Text answer1Text;
    Text answer2Text;
    Text answer3Text;
    Text answer4Text;

    [SerializeField] Button returnButton;
    [SerializeField] Button continueButton;

    [SerializeField] GameObject correctAnswerPanel;
    [SerializeField] GameObject incorrectAnswerPanel;
    [SerializeField] GameObject AddPannel;
    [SerializeField] Animator boxAnimator;

    GameObject go;
    int[] numberRange = { 3, 4, 5, 6 };

    int randomOffset;
    int realAnswer;
    int playerAnswer;

    void Start()
    {
        answer1Text = GameObject.Find("Answer1").GetComponentInChildren<Text>();
        answer2Text = GameObject.Find("Answer2").GetComponentInChildren<Text>();
        answer3Text = GameObject.Find("Answer3").GetComponentInChildren<Text>();
        answer4Text = GameObject.Find("Answer4").GetComponentInChildren<Text>();


        go = GameObject.FindWithTag("QuestionTrigger1");
        correctAnswerPanel.SetActive(false);
        incorrectAnswerPanel.SetActive(false);
        Question();

        answer1.onClick.AddListener(() => CheckAnswer(answer1Text.text));
        answer2.onClick.AddListener(() => CheckAnswer(answer2Text.text));
        answer3.onClick.AddListener(() => CheckAnswer(answer3Text.text));
        answer4.onClick.AddListener(() => CheckAnswer(answer4Text.text));


        returnButton.onClick.AddListener(Return);
        continueButton.onClick.AddListener(Continue);
    }

    void Update()
    {

    }

    void Question()
    {
        List<int> offsets = new List<int>() { -5, -4, -3, -2, -1, 1, 2, 3, 4, 5};
        List<int> answerList = new List<int>();

        int n = Random.Range(1, 10);
        int nRange = numberRange[Random.Range(0, numberRange.Length)];
        String tempText = "";

        for (int i = 0; i < nRange - 1; i++)
        {
            tempText += n + " + ";
        }
        tempText += n + " = ";

        questionText.text = tempText;
        realAnswer = n * nRange;
        //realAnswer = 4;
        //List<int> answerList = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, offsets.Count);
            answerList.Add(realAnswer + offsets[randomIndex]);    // Add random answer to the list
            offsets.RemoveAt(randomIndex);                        // Remove the added answer to avoid duplication              
        }
        answerList.Add(realAnswer);
        shuffle(answerList);
        answer1Text.text = answerList[0].ToString();
        answer2Text.text = answerList[1].ToString();
        answer3Text.text = answerList[2].ToString();
        answer4Text.text = answerList[3].ToString();
        answerList.Clear();
    }



    void Return()
    {
        //n = number[Random.Range(0, number.Length)];
        //nRange = numberRange[Random.Range(0, numberRange.Length)];
        Question();
        incorrectAnswerPanel.SetActive(false);
    }


    void Continue()
    {
        print("continue");
        boxAnimator.SetTrigger("Close");
        Invoke("DisableCanvas", 0.25f);
        Question();
    }

    void DisableCanvas()
    {
        correctAnswerPanel.SetActive(false);
        // TriggerScript.instance.triggerObject.SetActive(false);
        //TriggerScript.instance.canvas.SetActive(false);
        Player.instance.UnFreezePlayer();
    }


    void CheckAnswer(string textAnswer)
    {
        playerAnswer = Convert.ToInt32(textAnswer);

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
    }

    void shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
