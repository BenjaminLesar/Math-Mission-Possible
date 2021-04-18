using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class WordScript : MonoBehaviour
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
    public GameObject wordCanvas;
    private GameObject go;

    private String[] question = { "firstQuestion", "secondQuestion", "thirdQuestion", "fourthQuestion", "fifthQuestion" };
    public String[] numberText;
    private int[] numberOne = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
    private int[] numberTwo = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

    public InputField input;
    [SerializeField] Animator boxAnimator;

    private bool isText;
    private String q;
    private String nT1;
    private String nT2;

    private int n1;
    private int n2;


    private int realAnswer;
    private int playerAnswer;

    string mathQuestion;

    public void DoMath()
    {
        mathQuestion = PlayerPrefs.GetString("mathQuestion");
        go = GameObject.Find(mathQuestion);
        isText = true;
        n1 = numberOne[Random.Range(0, numberOne.Length)];
        n2 = numberTwo[Random.Range(0, numberTwo.Length)];

        //Change [Random.Range(0, question.Length)] to 0-4 for testing specific questions
        //Eg. [Random.Range(4,4)], only question 5 will show
        q = question[Random.Range(2, 2)];
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

    //Used for last question, However when isText = true realAnswer continuosly changes
    //Used Debug.Log(realAnswer) within return method 
    //Used to change the numbers to text within the question eg. 5 = five
    void NumberToText()
    {
        if (isText)
        {
            if (n1 == numberOne[0])
            {
                nT1 = numberText[0];

            }

            else if (n1 == numberOne[1])
            {
                nT1 = numberText[1];
            }

            else if (n1 == numberOne[2])
            {
                nT1 = numberText[2];
            }

            else if (n1 == numberOne[3])
            {
                nT1 = numberText[3];
            }

            else if (n1 == numberOne[4])
            {
                nT1 = numberText[4];
            }

            else if (n1 == numberOne[5])
            {
                nT1 = numberText[5];
            }

            else if (n1 == numberOne[6])
            {
                nT1 = numberText[6];
            }

            else if (n1 == numberOne[7])
            {
                nT1 = numberText[7];
            }

            else if (n1 == numberOne[8])
            {
                nT1 = numberText[8];
            }

            else if (n1 == numberOne[9])
            {
                nT1 = numberText[9];
            }

            else if (n1 == numberOne[10])
            {
                nT1 = numberText[10];
            }

            else if (n1 == numberOne[11])
            {
                nT1 = numberText[11];
            }

            if (n2 == numberTwo[0])
            {
                nT2 = numberText[0];
            }

            else if (n2 == numberTwo[1])
            {
                nT2 = numberText[1];
            }

            else if (n2 == numberTwo[2])
            {
                nT2 = numberText[2];
            }

            else if (n2 == numberTwo[3])
            {
                nT2 = numberText[3];
            }

            else if (n2 == numberTwo[4])
            {
                nT2 = numberText[4];
            }

            else if (n2 == numberTwo[5])
            {
                nT2 = numberText[5];
            }

            else if (n2 == numberTwo[6])
            {
                nT2 = numberText[6];
            }

            else if (n2 == numberTwo[7])
            {
                nT2 = numberText[7];
            }

            else if (n2 == numberTwo[8])
            {
                nT2 = numberText[8];
            }

            else if (n2 == numberTwo[9])
            {
                nT2 = numberText[9];
            }

            else if (n2 == numberTwo[10])
            {
                nT2 = numberText[10];
            }

            else if (n2 == numberTwo[11])
            {
                nT2 = numberText[11];
            }
        }

        else
        {
            nT1 = Convert.ToString(n1);
            nT2 = Convert.ToString(n2);
        }
    }

    void Question()
    {
        if (q == question[0])
        {
            //isText should be true
            isText = false;
            NumberToText();
            questionText.text = "There are " + nT1 + " groups of flamingos." + "\n" +
                "If there are " + nT2 + " flamingos in each group, " + "\n" +
                "what is the total amount of flamingos?";
            realAnswer = n1 * n2;
        }

        else if (q == question[1])
        {
            isText = false;
            NumberToText();
            questionText.text = "There are " + nT1 + " bowls of oranges, " + "\n" +
                "and each bowl has " + nT2 + " oranges in it." + "\n" +
                "How many oranges are there in all?";
            realAnswer = n1 * n2;
        }

        if (q == question[2])
        {
            //isText should be true
            isText = false;
            NumberToText();
            questionText.text = "An octopus has " + nT1 + " legs." + "\n" +
                "How many legs are there in all, " + "\n" +
                "for " + nT2 + " octopi?";
            realAnswer = n1 * n2;
        }

        if (q == question[3])
        {
            //isText should be true
            isText = false;
            NumberToText();
            questionText.text = "David has " + nT1 + " pairs of socks." + "\n" +
                "How many socks does he have altogether?";
            realAnswer = n1 * 2;
        }

        else if (q == question[4])
        {
            isText = false;
            NumberToText();
            questionText.text = "Coach Tracy bought " + nT1 + " pizzas " + "\n" +
                "to celebrate the tennis team's win. " + "\n" +
                "If each pizza has " + nT2 + " slices, " + "\n" +
                "how many total slices do the players have?";
            realAnswer = n1 * n2;
        }


    }



    void Return()
    {
        n1 = numberOne[Random.Range(0, numberOne.Length)];
        n2 = numberTwo[Random.Range(0, numberTwo.Length)];
        //Change [Random.Range(0, question.Length)] to 0-4 for testing specific questions
        //Eg. [Random.Range(4,4)], only question 5 will show
        q = question[Random.Range(2, 2)];

        Debug.Log(realAnswer);
        Question();
        incorrectAnswerPanel.SetActive(false);
    }


    void Continue()
    {
        go.SetActive(false);
        wordCanvas.SetActive(false);
        Destroy(go);
        input.text = null;
        Time.timeScale = 1;
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

