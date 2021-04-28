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
    [SerializeField] Animator boxAnimator;

    private int f;
    private int s;

    private int realAnswer;
    private int playerAnswer;


    string mathQuestion;
    // Calls random int from array and assigns to f & s
    // Panels for correct/incorrect panels disabled
    // Question method called
    public void DoMath()
    {
        mathQuestion = PlayerPrefs.GetString("mathQuestion");
        go = GameObject.Find(mathQuestion);
        correctAnswerPanel.SetActive(false);
        incorrectAnswerPanel.SetActive(false);
        Question();
    }

    private void Start()
    {
        returnButton.onClick.AddListener(Return);
        continueButton.onClick.AddListener(Continue);
    }
    // Waits for user interaction with each button
    void Update()
    {
        if (input.IsActive())
            input.ActivateInputField();
        if (input.text != "") // prevent empty input from player, which cause invalid input error
        {
            checkAnswer.onClick.RemoveAllListeners();
            checkAnswer.onClick.AddListener(CheckAnswer);
        }

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp("enter"))
        {
            if (correctAnswerPanel.activeInHierarchy)
            {
                Continue();
            }
            else if (incorrectAnswerPanel.activeInHierarchy)
                Return();
            else if (input.text != "")
                CheckAnswer();
        }
    }

    // Displays variables gathered within the question text
    // Multiplies variables and assigns this number as the correct answer
    void Question()
    {
        f = firstNumber[Random.Range(0, firstNumber.Length)];
        s = secondNumber[Random.Range(0, secondNumber.Length)];
        questionText.text = "" + f + " X " + s + " = ";
        realAnswer = f * s;
    }


    // When return button is pressed, brings player back to question screen
    // New variables are assigned and Question function is called
    // Incorrect panel is disabled
    void Return()
    {
        //Debug.Log("return");
        Question();
        incorrectAnswerPanel.SetActive(false);
    }

    // When continue button is pressed on correct panel, main panel is deactivated
    // Calls Player script
    void Continue()
    {
        Destroy(go);
        input.text = null;
        multiplierCanvas.SetActive(false);
        Time.timeScale = 1;
        Player.instance.UnFreezePlayer();
    }

    
    // Checks whether player answer matches the correct answer
    // Need to implement method so player cannot input letters
    void CheckAnswer()
    {
        //Debug.Log("checkanswer");
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
