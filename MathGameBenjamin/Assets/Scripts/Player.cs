using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public GameObject multiplierCanvas;
    public GameObject repAddCanvas;

    //These fields are serialized so that they can be changed in the Unity inspector
    [SerializeField] float runSpeed = 5f; //default run speed
    [SerializeField] float jumpSpeed = 5f; // default jump speed/height
    [SerializeField] float swimSpeed = 1.1f;
    [SerializeField] float climbSpeed = 5f; //default climb speed
    [SerializeField] float underWaterGrav = 0.06f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    

    Rigidbody2D myRigidBody; //the player character's physical frame
    Animator myAnimator; //the animation component
    CapsuleCollider2D myBodyCollider; //handles collision for the main part of the player character
    BoxCollider2D myFeet; //handles collision (and therefore jumping) for the player character's feet.
    float gravityScaleAtStart; //a variable to hold the current gravity value

    bool isAlive = true;

    bool inWater = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //setting variables equal to their actual in-game components.
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;

    }

    void Update()
    {
        //calling other methods each frame
        if (!isAlive)
        {
            return;
        }

        Run();
        Jump();
        changeAnim();
        climbLadder();
        Swim();
        Die();

    }

    // When player touches coin, coin is disabled and the coin count is increased by 1.
    void OnTriggerEnter2D(Collider2D other)
    {

        // Reference object with tag (can be changed) and disables object
        // Enables Multiplier Canvas
        // Freezes Player
        // Increases count by 1

        if (other.gameObject.CompareTag("QuestionTrigger1"))
        {
            FreezePlayer();
            repAddCanvas.SetActive(true);
        }

        if (other.gameObject.CompareTag("QuestionTrigger2"))
        {
            FreezePlayer();
            multiplierCanvas.SetActive(true);
        }

    }

    // Freezes Player at position (Is Referenced by Math Scripts)
    void FreezePlayer()
    {
        myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

    }

    // Unfreezes Player (Is Referenced by Math Scripts)
    public void UnFreezePlayer()
    {
        myRigidBody.constraints = RigidbodyConstraints2D.None;
        myRigidBody.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    private void Run()
    {
        if (inWater == false)
        {
            float left_right_movement = CrossPlatformInputManager.GetAxis("Horizontal"); //between -1 and 1, this by default gets player's "a" and "d" or left/right arrow keystrokes.
            Vector2 playerVelocity = new Vector2(left_right_movement * runSpeed, myRigidBody.velocity.y); //creates a new x vector coordinate equal to player input times the runspeed variable
            myRigidBody.velocity = playerVelocity; //sets the player velocity equal to the new vector.

            bool hSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon; //tests to see if player character x velicity is appreciably greater than zero.
            myAnimator.SetBool("Running", hSpeed); //sets the player character to running animation if bool is true. 
        }
    }

    private void climbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) //tests to see if player character's feet are NOT touching a ladder 
        {
            myAnimator.SetBool("Climbing", false); //sets climbing to false
            myRigidBody.gravityScale = gravityScaleAtStart; //sets normal gravity.
            return;
        }

        float controlFlow = CrossPlatformInputManager.GetAxis("Vertical"); //by default gets player's "w" and "s" or up/down arrow keystrokes.
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlFlow * climbSpeed); //creates a new y vector coordinate equal to player input times the climbspeed variable.
        myRigidBody.velocity = climbVelocity; //sets the player velocity equal to the new vector.
        myRigidBody.gravityScale = 0f; //removes gravity from the player while on the ladder

        bool playerHasV = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon; //tests to see if player character y velocity is appreciably greater than zero.
        myAnimator.SetBool("Climbing", playerHasV); //sets the player character to climbing animation if bool is true.
    }

    private void Swim()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Water"))) //tests to see if player character's feet are NOT touching a ladder 
        {
            inWater = false;
            myAnimator.SetBool("Running", false); //sets climbing to false
            myRigidBody.gravityScale = gravityScaleAtStart; //sets normal gravity.
            return;
        }

        inWater = true;
        myRigidBody.gravityScale = underWaterGrav;

        if (CrossPlatformInputManager.GetButtonDown("Jump")) //by default gets player's "spacebar" input.
        {
            Vector2 swimVelocity = new Vector2(0f, swimSpeed); //creates a new x vector coordinate equal to player input times the runspeed variable
            myRigidBody.velocity += swimVelocity; //sets the player velocity equal to the new vector. 
        }

        float left_right_movement = CrossPlatformInputManager.GetAxis("Horizontal"); //between -1 and 1, this by default gets player's "a" and "d" or left/right arrow keystrokes.
        Vector2 playerVelocity = new Vector2((left_right_movement * runSpeed) / 2, myRigidBody.velocity.y); //creates a new x vector coordinate equal to player input times the runspeed variable
        myRigidBody.velocity = playerVelocity; //sets the player velocity equal to the new vector.

        bool hSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon; //tests to see if player character x velicity is appreciably greater than zero.
        myAnimator.SetBool("Running", hSpeed);
    }

    private void Jump()
    {
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && CrossPlatformInputManager.GetButtonDown("Jump") && inWater == false) //by default gets player's "spacebar" input.
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed); //creates a new y vector coordinate equal to the Jumpspeed variable
            myRigidBody.velocity += jumpVelocity; //sets the player character velocity equal to the new vector.
        }

        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Trampoline")) && CrossPlatformInputManager.GetButtonDown("Jump")) //by default gets player's "spacebar" input.
        {
            Vector2 jumpVelocity = new Vector2(0f, (jumpSpeed * 1.5f)); //creates a new y vector coordinate equal to the Jumpspeed variable
            myRigidBody.velocity += jumpVelocity; //sets the player character velocity equal to the new vector.
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void changeAnim()
    {
        bool hSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon; //tests to see if player's x velocity is appreciably greater than 0.
        if (hSpeed) //if true
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f); //flips player anim direction
        }
    }

    void OnCollsionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = null;

        }
    }
}