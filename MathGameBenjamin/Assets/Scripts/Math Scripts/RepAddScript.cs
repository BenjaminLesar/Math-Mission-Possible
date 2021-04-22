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

    public static RepAddScript instance;
    private GameObject go;

    private int[] number = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    private int[] numberRange = { 3, 4, 5, 6 };
    
    public InputField input;

    [SerializeField] Animator boxAnimator;

    private int n;
    private int nRange;
    

    private int realAnswer;
    private int playerAnswer;
    string mathQuestion;
    void Awake()
    {
        instance = this;
    }

    public void DoMath()
    {

        mathQuestion = PlayerPrefs.GetString("mathQuestion");
        go = GameObject.Find(mathQuestion);
        n = number[Random.Range(0, number.Length)];
        nRange = numberRange[Random.Range(0, numberRange.Length)];
        correctAnswerPanel.SetActive(false);
        incorrectAnswerPanel.SetActive(false);
        Question();
    }

    void Update()
    {
        // Check answer by enter key

        // Do this for return and continue as well
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKey("enter"))
        {
            CheckAnswer();
        }
        checkAnswer.onClick.AddListener(CheckAnswer);
        returnButton.onClick.AddListener(Return);
        continueButton.onClick.AddListener(Continue);
    }

    void Question1()
    {
        if(nRange == numberRange[0])
        {
            questionText.text = "" + n + " + " + n + " + " + n + " = ";
            realAnswer = n*3;
        }

        else if (nRange == numberRange[1])
        {
            questionText.text = "" + n + " + " + n + " + " + n + " + " + n + " = ";
            realAnswer = n*4;
        }

        else if (nRange == numberRange[2])
        {
            questionText.text = "" + n + " + " + n + " + " + n + " + " + n + " + " + n + " = ";
            realAnswer = n*5;
        }

        else if (nRange == numberRange[3])
        {
            questionText.text = "" + n + " + " + n + " + " + n + " + " + n + " + " + n + " + " + n + " = ";
            realAnswer = n*6;
        }
    }

    void Question()
    {
        n = number[Random.Range(0, number.Length)];
        nRange = numberRange[Random.Range(0, numberRange.Length)];
        String tempText = "";


        for (int i = 0; i < nRange - 1; i++)
        {
            tempText += n + " + ";
        }
        tempText += n + " = ";

        questionText.text = tempText;
        realAnswer = n * nRange;
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
        boxAnimator.SetTrigger("Close");
        Destroy(go);
        input.text = null;
        correctAnswerPanel.SetActive(false);
        Time.timeScale = 1;
        Invoke("DisableCanvas", 0.25f);

    }

    void DisableCanvas()
    {       
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
