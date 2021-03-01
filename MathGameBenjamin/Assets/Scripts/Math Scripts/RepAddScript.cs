using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
public class RepAddScript : MonoBehaviour
{
    // Will need to create UI in project
    // Attach script to created UI (canvas, panels, text, etc.)
    // Script is called within PlayerScript (reference PlayerScript for more details)
    // View MultiplierScript for a more detailed explanation on methods
    public Text questionText;
    public Text correctText;
    public Text incorrectText;


    public Button checkAnswer;
    public Button returnButton;
    public Button continueButton;

    public GameObject correctAnswerPanel;
    public GameObject incorrectAnswerPanel;
    public GameObject repAddCanvas;

    private GameObject go;

    private int[] number = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
    private int[] numberRange = { 3, 4, 5, 6 };
    
    public InputField input;

    private int n;
    private int nRange;
    

    private int realAnswer;
    private int playerAnswer;



    void Start()
    {

        go = GameObject.FindWithTag("QuestionTrigger1");
        n = number[Random.Range(0, number.Length)];
        nRange = numberRange[Random.Range(0, numberRange.Length)];
        correctAnswerPanel.SetActive(false);
        incorrectAnswerPanel.SetActive(false);
        Question();
    }

    void Update()
    {
        checkAnswer.onClick.AddListener(CheckAnswer);
        returnButton.onClick.AddListener(Return);
        continueButton.onClick.AddListener(Continue);
    }

    void Question()
    {
        if(nRange == numberRange[0])
        {
            questionText.text = "" + n + " + " + n + " + " + n + " = ";
            realAnswer = n + n + n;
        }

        else if (nRange == numberRange[1])
        {
            questionText.text = "" + n + " + " + n + " + " + n + " + " + n + " = ";
            realAnswer = n + n + n + n;
        }

        else if (nRange == numberRange[2])
        {
            questionText.text = "" + n + " + " + n + " + " + n + " + " + n + " + " + n + " = ";
            realAnswer = n + n + n + n + n;
        }

        else if (nRange == numberRange[3])
        {
            questionText.text = "" + n + " + " + n + " + " + n + " + " + n + " + " + n + " + " + n + " = ";
            realAnswer = n + n + n + n + n;
        }
        

    }



    void Return()
    {
        n = number[Random.Range(0, number.Length)];
        nRange = numberRange[Random.Range(0, numberRange.Length)];
        Question();
        incorrectAnswerPanel.SetActive(false);
    }


    void Continue()
    {
        go.SetActive(false);
        repAddCanvas.SetActive(false);
        Player.instance.UnFreezePlayer();
    }


    void CheckAnswer()
    {


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


    }
}
