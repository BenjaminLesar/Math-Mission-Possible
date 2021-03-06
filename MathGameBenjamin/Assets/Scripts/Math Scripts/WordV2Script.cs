using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class WordV2Script : MonoBehaviour
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
    public GameObject wordV2Canvas;

    private String[] question = { "firstQuestion", "secondQuestion", "thirdQuestion", "fourthQuestion", "fifthQuestion" };
    public String[] numberText;
    private int[] numberOne = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
    private int[] numberTwo = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

    public InputField input;

    private bool isText;
    private String q;
    private String nT1;
    private String nT2;

    private int n1;
    private int n2;


    private int realAnswer;
    private int playerAnswer;

    private GameObject go;

    void Start()
    {
        go = GameObject.FindWithTag("QuestionTrigger5");
        isText = true;
        n1 = numberOne[Random.Range(0, numberOne.Length)];
        n2 = numberTwo[Random.Range(0, numberTwo.Length)];
        //Change [Random.Range(0, question.Length)] to 0-4 for testing specific questions
        //Eg. [Random.Range(4,4)], only question 5 will show
        q = question[Random.Range(0, question.Length)];
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
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[1])
            {
                nT1 = numberText[1];
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[2])
            {
                nT1 = numberText[2];
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[3])
            {
                nT1 = numberText[3];
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[4])
            {
                nT1 = numberText[4];
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[5])
            {
                nT1 = numberText[5];
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[6])
            {
                nT1 = numberText[6];
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[7])
            {
                nT1 = numberText[7];
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[8])
            {
                nT1 = numberText[8];
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[9])
            {
                nT1 = numberText[9];
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[10])
            {
                nT1 = numberText[10];
                nT2 = Convert.ToString(n2);

            }

            else if (n1 == numberOne[11])
            {
                nT1 = numberText[11];
                nT2 = Convert.ToString(n2);

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
            isText = false;
            NumberToText();
            questionText.text = "Tony, Jazz, Richard, and Sue each have " + nT1 + "\n" +
                " pairs of sunglasses, how many sunglasses do" + "\n" +
                "they have altogether?";

            realAnswer = n1 * 4;
        }

        else if (q == question[1])
        {
            isText = false;
            NumberToText();
            questionText.text = "At an ice cream shop, " + nT1 + " students ordered " + nT2 + "\n" +
                "scoop cones. How many scoops of ice cream did" + "\n" +
                "the " + nT1 + " students have in all?";
            realAnswer = n1 * n2;
        }

        if (q == question[2])
        {
            isText = false;
            NumberToText();
            questionText.text = "For a field trip, Ms. Stevens bought " + nT1 + "\n" +
                "packs of cookies that cost $" + nT2 + " each. What was" + "\n" +
                "the total amount for the cookies?";
            realAnswer = n1 * n2;
        }

        if (q == question[3])
        {

            isText = false;
            NumberToText();
            questionText.text = "There were " + nT1 + " dogs at " + "\n" +
                "Sunny Dayz Dog Park. If each dog has " + nT2 + " legs, " + "\n" +
                "how many dog legs were there in all?";
            realAnswer = n1 * n2;
        }

        else if (q == question[4])
        {
            //isText should be true
            isText = false;
            NumberToText();
            questionText.text = "Sonya received " + nT1 + " gift cards worth " + "\n" +
                "$" + nT2 + " for her birthday. How much money " + "\n" +
                "did she have in gift cards?";
            realAnswer = n1 * n2;
        }


    }



    void Return()
    {
        n1 = numberOne[Random.Range(0, numberOne.Length)];
        n2 = numberTwo[Random.Range(0, numberTwo.Length)];

        //Change [Random.Range(0, question.Length)] to 0-4 for testing specific questions
        //Eg. [Random.Range(4,4)], only question 5 will show
        q = question[Random.Range(4, 4)];

        //Used to debug realAnswer in Console
        Debug.Log(realAnswer);
        Question();
        incorrectAnswerPanel.SetActive(false);
    }


    void Continue()
    {
        go.SetActive(false);
        wordV2Canvas.SetActive(false);
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