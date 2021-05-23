using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MathParent:MonoBehaviour
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
    public InputField input;

    public GameObject correctAnswerPanel;
    public GameObject incorrectAnswerPanel;
    public Animator boxAnimator;

    protected GameObject myCanvas;
    protected int realAnswer;
    protected int playerAnswer;

    protected GameObject go;
    string mathQuestion;
  

    // will be overridden in child class
    public virtual int Question(Text questionText) { return 0; }
    public virtual int Question(Text questionText, Image currentShape) { return 0; }
    private void Awake()
    {

    }
    public void OnStart()
    {
        myCanvas = this.gameObject;
        returnButton.onClick.AddListener(Return);
        continueButton.onClick.AddListener(Continue);
    }

    private void Start()
    {
        OnStart();
        mathQuestion = PlayerPrefs.GetString("mathQuestion");
        go = GameObject.Find(mathQuestion);
    }

    void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
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
                //Return();
                returnButton.onClick.Invoke();
            else if (input.text != "")
                CheckAnswer();
        }
    }

    public virtual void DoMath()
    {
        mathQuestion = PlayerPrefs.GetString("mathQuestion");
        go = GameObject.Find(mathQuestion);
        correctAnswerPanel.SetActive(false);
        incorrectAnswerPanel.SetActive(false);
        Return();
    }

    public void Continue()
    {
        boxAnimator.SetTrigger("Close");
        if (go && go.tag == "Treasure")
        {
            // 1. dupliacate the Treasure
            // 2. remove its script
            // 3. change sprite to opened chest
            GameObject openChest = Instantiate(go) as GameObject;
            Destroy(openChest.GetComponent<TriggerScript>());
            openChest.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("AnimImages/TreasureChest/LargerSize/Open");
            FindObjectOfType<GameSession>().AddToScore(20);
            
        }
        Destroy(go);
        input.text = null;
        Time.timeScale = 1;
        Player.instance.UnFreezePlayer();
        Invoke("DisableCanvas", 0.25f); // wait .25s for box animator finishes closing
    }

    public virtual void DisableCanvas()
    {
        myCanvas.SetActive(false);
    }

    public virtual void Return()
    {
        realAnswer = Question(questionText);
        incorrectAnswerPanel.SetActive(false);
    }

    public virtual void CheckAnswer()
    {
        // Takes the players answer and converts it to an int
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
