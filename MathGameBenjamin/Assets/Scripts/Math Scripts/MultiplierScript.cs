using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class MultiplierScript : MonoBehaviour
{
    // Will need to create UI in project
    // Attach script to created UI (canvas, panels, text, etc.)
    // Script is called within PlayerScript (reference PlayerScript for more details)
    public Text questionText;
    public Text correctText;
    public Text incorrectText;
    

    public Button checkAnswer;
    public Button returnButton;
    public Button continueButton;

    public GameObject correctAnswerPanel;
    public GameObject incorrectAnswerPanel;
    public GameObject multiplierCanvas;

    private GameObject go;

    private int[] firstNumber = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    private int[] secondNumber = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public InputField input;

    private int f;
    private int s;

    private int realAnswer;
    private int playerAnswer;

   
    // Calls random int from array and assigns to f & s
    // Panels for correct/incorrect panels disabled
    // Question method called
    void Start()
    {
        go = GameObject.FindWithTag("QuestionTrigger2");
        f = firstNumber[Random.Range(0, firstNumber.Length)];
        s = secondNumber[Random.Range(0, secondNumber.Length)];
        correctAnswerPanel.SetActive(false);
        incorrectAnswerPanel.SetActive(false);
        Question();
    }

    // Waits for user interaction with each button
    void Update()
    {
        // Check answer by enter key
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKey("enter"))
        {
            CheckAnswer();
        }
        checkAnswer.onClick.AddListener(CheckAnswer);
        returnButton.onClick.AddListener(Return);
        continueButton.onClick.AddListener(Continue);
    }

    // Displays variables gathered within the question text
    // Multiplies variables and assigns this number as the correct answer
    void Question()
    {

        questionText.text = "" + f + " X " + s + " = ";
        realAnswer = f * s;


    }


    // When return button is pressed, brings player back to question screen
    // New variables are assigned and Question function is called
    // Incorrect panel is disabled
    void Return()
    {
        f = firstNumber[Random.Range(0, firstNumber.Length)];
        s = secondNumber[Random.Range(0, secondNumber.Length)];
        Question();
        incorrectAnswerPanel.SetActive(false);
    }

    // When continue button is pressed on correct panel, main panel is deactivated
    // Calls Player script
    void Continue()
    {
        go.SetActive(false);
        multiplierCanvas.SetActive(false);
        Player.instance.UnFreezePlayer();
    }

    
    // Checks whether player answer matches the correct answer
    // Need to implement method so player cannot input letters
    void CheckAnswer()
    {
        // Takes the players answer and converts it to an int
        playerAnswer = Convert.ToInt32(input.text);

        // If the player's answer and the correct answer match
        // Correct panel is enabled
        // Correct text is set (Can be altered)
        if (playerAnswer == realAnswer)
        {
            
            correctAnswerPanel.SetActive(true);
            correctText.text = "CORRECT! GREAT JOB!";
            
        }

        // If the player's answer does not match the correct answer
        // Incorrect panel is enabled
        // Incorrect text is set (Can be altered)
        else
        {
            incorrectAnswerPanel.SetActive(true);
            incorrectText.text = "Incorrect! Please try again";

        }


    }


}
