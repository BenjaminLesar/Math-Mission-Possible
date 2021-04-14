using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Minigame4Script : MonoBehaviour
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

    public InputField input;

    private int realAnswer;
    private int playerAnswer;
    Slider energyBar;
    float energyValue = 0;

    string mathQuestion;
    TextController txtController;

    Text challengeText;
    public GameObject energyPannel;
    void Start()
    {
        txtController = FindObjectOfType<TextController>();
        challengeText = GameObject.Find("ChallengeText").GetComponent<Text>();

    }

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
        energyPannel.SetActive(true);
        energyBar = GameObject.Find("EnergyBar").GetComponent<Slider>();
        txtController.RunAnimationText(challengeText, 2);

    }

    // Waits for user interaction with each button
    void Update()
    {
        checkAnswer.onClick.RemoveAllListeners();
        // Check answer by enter key
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp("enter"))
        {
            CheckAnswer();
        }
        checkAnswer.onClick.AddListener(CheckAnswer);

        returnButton.onClick.RemoveAllListeners();
        returnButton.onClick.AddListener(Return);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(Continue);
    }

    // Displays variables gathered within the question text
    // Multiplies variables and assigns this number as the correct answer
    void Question()
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

    // Correct answer
    void Continue()
    {
        Destroy(go);
        input.text = null;
        multiplierCanvas.SetActive(false);
        Time.timeScale = 1;
        Player.instance.UnFreezePlayer();
    }

    void Challenge()
    {
        StartCoroutine(FillBarAnimation(1));
        if (energyValue > 3)
        {
            correctAnswerPanel.SetActive(true);
            correctText.text = "You passed the chanllenge, congrats!!!";
            txtController.RunAnimationText(correctText, 2);
        }
        Question();
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
            Challenge();
        }

        // If the player's answer does not match the correct answer
        // Incorrect panel is enabled
        // Incorrect text is set (Can be altered)
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
