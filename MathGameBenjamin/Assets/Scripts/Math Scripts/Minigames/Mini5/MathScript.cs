using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathScript : MonoBehaviour
{
    public Text questionText;
    public Text correctText;
    public Text incorrectText;


    public Button checkAnswer;
    public Button returnButton;
    public Button continueButton;

    public GameObject correctAnswerPanel;
    public GameObject incorrectAnswerPanel;

    public InputField input;

    public Image currentShape;
    public Sprite[] shapeSprites;

    public Animator boxAnimator; 
    public MathHandler mathHandler;

    private int realAnswer = 0;
    private int playerAnswer;
    MultiplierScript multScript;
    RepAddScript addScript;
    WordScript wordScript;
    ShapeScript shapeScript;

    void Start()
    {
        returnButton.onClick.AddListener(Return);
        continueButton.onClick.AddListener(Continue);
        //ShapeScript test = mathHandler.shapeCanvas.GetComponent<ShapeScript>();
        //test = mathHandler.ShapeScript;
        //realAnswer = test.Question(questionText, currentShape);
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

    public void Question(int Idx)
    {
        //int Idx = mathHandler.RandomQuestion();
        input.text = null;
        switch (Idx)
        {
            case 0:
                multScript = mathHandler.MultScript;
                realAnswer = multScript.Question(questionText);
                break;
            case 1:
                addScript = mathHandler.AddScript;
                realAnswer = addScript.Question(questionText);
                break;
            case 2:
                wordScript = mathHandler.WordScript;
                realAnswer = wordScript.Question(questionText);
                break;
            case 3:
                shapeScript = mathHandler.ShapeScript;
                realAnswer = shapeScript.Question(questionText, currentShape);
                break;

        }    
    }

    public void OpenMath()
    {
        boxAnimator.SetTrigger("Popup");
    }

    void Challenge()
    {
        mathHandler.RandomQuestion();
    }

    void Return()
    {
        mathHandler.RandomQuestion();
        incorrectAnswerPanel.SetActive(false);
    }

    void Continue()
    {
        input.text = null;
        Destroy(gameObject.transform.parent.gameObject);
        Time.timeScale = 1;
        Player.instance.UnFreezePlayer();
    }

    void CheckAnswer()
    {
       
        if(currentShape) currentShape.sprite = shapeSprites[0];

        playerAnswer = Convert.ToInt32(input.text);

        if (playerAnswer == realAnswer)
        {
            Challenge();
        }

        else
        {
            incorrectAnswerPanel.SetActive(true);
            incorrectText.text = "Incorrect! Please try again";
        }
    }
}
