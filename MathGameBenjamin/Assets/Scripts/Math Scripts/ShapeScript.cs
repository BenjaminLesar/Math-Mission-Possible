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

    private GameObject go;

    public GameObject[] shapes;
    public InputField input;

    [SerializeField] Animator boxAnimator;

    private int n;
    private int[] number = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    private GameObject selectedShape;

    private int realAnswer;
    private int playerAnswer;
    string mathQuestion;

    public void DoMath()
    {
        mathQuestion = PlayerPrefs.GetString("mathQuestion");
        go = GameObject.Find(mathQuestion);
        correctAnswerPanel.SetActive(false);
        incorrectAnswerPanel.SetActive(false);
        n = number[Random.Range(0, number.Length)];
        selectedShape = shapes[Random.Range(0, shapes.Length)];
        Question();
    }

    void Update()
    {
        checkAnswer.onClick.RemoveAllListeners();
        checkAnswer.onClick.AddListener(CheckAnswer);

        returnButton.onClick.RemoveAllListeners();
        returnButton.onClick.AddListener(Return);

        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(Continue);

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKey("enter"))
        {
            CheckAnswer();
        }
    }

    void Question()
    {
        n = number[Random.Range(0, number.Length)];
        selectedShape = shapes[Random.Range(0, shapes.Length)];

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
           // questionText.transform.position = new Vector2(-206f, 19f);
            realAnswer = 5 * n;
        }

        else if (selectedShape == shapes[2])
        {
            selectedShape.SetActive(true);
            questionText.text = "" + n + "in.";
            realAnswer = 6 * n;
        }

        else if (selectedShape == shapes[3])
        {
            selectedShape.SetActive(true);
            questionText.text = "" + n + "in.";
            realAnswer = 8 * n;
        }


    }



    void Return()
    {
        Question();
        incorrectAnswerPanel.SetActive(false);
    }


    void Continue()
    {
        //MiniGame.instance.RaisePillar();
        //shapeCanvas.SetActive(false);
        //correctAnswerPanel.SetActive(false);
        //Player.instance.UnFreezePlayer();
        //Question();

        boxAnimator.SetTrigger("Close");
        Invoke("DisableCanvas", 0.25f);
    }

    void DisableCanvas()
    {
        MiniGame.instance.RaisePillar();
        shapeCanvas.SetActive(false);
        correctAnswerPanel.SetActive(false);
        Destroy(go);
        input.text = null;
        Time.timeScale = 1;
        Player.instance.UnFreezePlayer();
        Question();
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

        input.text = "";
    }

}
