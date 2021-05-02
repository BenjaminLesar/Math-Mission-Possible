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

    public Sprite[] shapeSprites;
    public Image currentShape;
    public InputField input;

    [SerializeField] Animator boxAnimator;

    private int n;
    private int[] number = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    private int realAnswer;
    private int playerAnswer;
    string mathQuestion;

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

    void Question()
    {
        n = number[Random.Range(0, number.Length)];
        int shapeIdx = Random.Range(1, shapeSprites.Length);
        currentShape.sprite = shapeSprites[shapeIdx];

        if (shapeIdx == 1)
        {
            questionText.text = "" + n + "in.";
            realAnswer = 4 * n;
        }

        else if (shapeIdx == 2)
        {
            questionText.text = "" + n + "in.";
            realAnswer = 5 * n;
        }

        else if (shapeIdx == 3)
        {
            questionText.text = "" + n + "in.";
            realAnswer = 6 * n;
        }

        else if (shapeIdx == 4)
        {
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
        boxAnimator.SetTrigger("Close");
        Destroy(go);
        input.text = null;
        Time.timeScale = 1;
        Player.instance.UnFreezePlayer();
        Invoke("DisableCanvas", 0.25f); // wait .25s for box animator finishes closing
    }

    void DisableCanvas()
    {
        Minigame2.instance.RaisePillar();
        correctAnswerPanel.SetActive(false);
        
    }


    void CheckAnswer()
    {
        // disable image
        currentShape.sprite = shapeSprites[0];

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
