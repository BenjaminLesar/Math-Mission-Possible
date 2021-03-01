using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class ShapeScript : MonoBehaviour
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
    public GameObject shapeCanvas;


    private int[] number = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

    public GameObject[] shapes;

    public InputField input;

    private int n;

    private GameObject selectedShape;
    private GameObject go;

    private int realAnswer;
    private int playerAnswer;



    void Start()
    {

        go = GameObject.FindWithTag("QuestionTrigger2");
        n = number[Random.Range(0, number.Length)];
        selectedShape = shapes[Random.Range(0, shapes.Length)];
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
        if (selectedShape == shapes[0])
        {
            selectedShape.SetActive(true);
            questionText.text = "" + n + "in.";
            realAnswer = 4 * n;
        }

        else if (selectedShape == shapes[1])
        {
            selectedShape.SetActive(true);
            questionText.text = "" + n + "in.";
            realAnswer = 6 * n;
        }

        else if (selectedShape == shapes[1])
        {
            selectedShape.SetActive(true);
            questionText.text = "" + n + "in.";
            realAnswer = 8 * n;
        }




    }



    void Return()
    {
        n = number[Random.Range(0, number.Length)];
        selectedShape = shapes[Random.Range(0, shapes.Length)];
        Question();
        incorrectAnswerPanel.SetActive(false);
    }


    void Continue()
    {
        go.SetActive(false);
        shapeCanvas.SetActive(false);
        Player.instance.UnFreezePlayer();
    }


    void CheckAnswer()
    {

        selectedShape.SetActive(false);

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
