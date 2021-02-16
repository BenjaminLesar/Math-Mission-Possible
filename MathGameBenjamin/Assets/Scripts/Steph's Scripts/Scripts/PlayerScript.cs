using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Attached to player sprite/object
    public GameObject multiplierCanvas;
    public GameObject repAddCanvas;

    public static PlayerScript instance;

    Rigidbody2D rigidbody;

    public Text countText;

    private int count;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        count = 0;
    }

    void FixedUpdate()
    {
        SetCountText();
        
    }

    // When player touches coin, coin is disabled and the coin count is increased by 1.
    void OnTriggerEnter2D(Collider2D other)
    {
        // Change this tag to any object you would like to be collected 
        // Updates collectible count by 1 ( can change increment or remove/replace)
        if (other.gameObject.CompareTag("Coin"))
        {

            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }

        // Reference object with tag (can be changed) and disables object
        // Enables Multiplier Canvas
        // Freezes Player
        // Increases count by 1
        if(other.gameObject.CompareTag("MultiplierQuestionCoin"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            FreezePlayer();
            multiplierCanvas.SetActive(true);
            
        }

        // Similar to the above code, but references the RepAddQuestion tag and canvas
        if (other.gameObject.CompareTag("RepAddQuestionCoin"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            FreezePlayer();
            repAddCanvas.SetActive(true);

        }
    }

    //Displays coin count on canvas
    void SetCountText()
    {
        countText.text = "Coins: " + count.ToString();
    }

    // Freezes Player at position (Is Referenced by Math Scripts)
    void FreezePlayer()
    {
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

    }

    // Unfreezes Player (Is Referenced by Math Scripts)
    public void UnFreezePlayer()
    {
        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.GetComponent<Rigidbody2D>().freezeRotation = true;
    }
}
