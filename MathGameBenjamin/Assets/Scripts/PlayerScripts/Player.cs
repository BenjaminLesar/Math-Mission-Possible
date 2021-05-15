using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
   

    //These fields are serialized so that they can be changed in the Unity inspector
    [SerializeField] float runSpeed = 5.2f; //default run speed
    [SerializeField] float jumpSpeed = 14.4f; // default jump speed/height
    [SerializeField] float swimSpeed = 1.2f;
    [SerializeField] float climbSpeed = 5f; //default climb speed
    [SerializeField] float underWaterGrav = 0.06f; //pretty sure this no longer does anything (sinkSpeed handles this). Variable is still used but I think has no appreciable effect.
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] float sinkSpeed = -3f;
    [SerializeField] float deathFall = -40f;
    private static bool Loadcheck;
    public float health = 3;
    public string LoadName;
    

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float x)
    {
        health = x;
    }

    public bool GetLoaded()
    {
        return Loadcheck;
    }

    public void SetLoaded(bool x)
    {
        Loadcheck = x;
    }

    

    Rigidbody2D myRigidBody; //the player character's physical frame
    Animator myAnimator; //the animation component
    CapsuleCollider2D myBodyCollider; //handles collision for the main part of the player character
    BoxCollider2D myFeet; //handles collision (and therefore jumping) for the player character's feet.
    float gravityScaleAtStart; //a variable to hold the current gravity value
    Slider healthBar;

    bool isAlive = true;
    bool inWater = false;
    bool isTakingDM = false; // stop taking damage when the value is true for 1.5s.

    // Flashing effect
    Material matWhite;
    Material matDefault;
    SpriteRenderer sr;

    Pause myPause;
    public GameObject SaveMenuCanvas;
    public GameObject coinPrefab;
    public GameObject mathPrefab;
    public GameObject checkPointPrefab;

    public Sprite Marcus;
    public bool shouldLock = false;
    void Awake()
    {  
        instance = this;
    }

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        //Debug.Log(health);

        //setting variables equal to their actual in-game components.
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();

        
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        string myCheck;

        myCheck = PlayerPrefs.GetString("IsNotFirst");

        if (PlayerPrefs.GetString("Character") == "Marcus")
        {
            sr.sprite = Marcus;
            myAnimator.runtimeAnimatorController = Instantiate(Resources.Load("BBoy") as RuntimeAnimatorController);
            myAnimator.runtimeAnimatorController.name = "BBoy";
        }

        if (myCheck.Length != 0)
        {
            if (myCheck.Equals("true"))
            {
                this.transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
            }
        }
        

        if (GetLoaded())
        {
            LoadName = PlayerPrefs.GetString("name");

            if (File.Exists(Application.persistentDataPath + "/" + LoadName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + LoadName, FileMode.Open);
                SaveObject mySave = (SaveObject)bf.Deserialize(file);
                file.Close();

                SetHealth(mySave.health);
                healthBar.value = mySave.health;

                FindObjectOfType<GameSession>().SetLives(mySave.lives);
                FindObjectOfType<GameSession>().SetScore(mySave.score);
                FindObjectOfType<GameSession>().LoadScore();

                coinPickup[] result = FindObjectsOfType<coinPickup>();

                foreach (coinPickup c in result)
                {
                    Destroy(c.gameObject);
                }

 
                for (int i = 0; i < mySave.xcoord.Count; i++)
                {
                    var newObj = Instantiate(coinPrefab, new Vector2(mySave.xcoord[i], mySave.ycoord[i]), Quaternion.identity);
                    newObj.transform.parent = GameObject.Find("Pickups").transform;
                    newObj.name = "Coin" + i.ToString();
                }

                CheckPointSave[] SPResult = FindObjectsOfType<CheckPointSave>();

                foreach (CheckPointSave cp in SPResult)
                {
                    Destroy(cp.gameObject);
                }

                for (int i = 0; i < mySave.SavePointX.Count; i++)
                {
                    var newObj = Instantiate(checkPointPrefab, new Vector2(mySave.SavePointX[i], mySave.SavePointY[i]), Quaternion.identity);
                    newObj.transform.parent = GameObject.Find("ScenePersist").transform;
                    newObj.name = "CheckPoint" + i.ToString();
                }

                TriggerScript[] result2 = FindObjectsOfType<TriggerScript>();

                foreach (TriggerScript t in result2)
                {
                    Destroy(t.gameObject);
                }

                for (int i = 0; i < mySave.mathXCoord.Count; i++)
                {
                    var newObj = Instantiate(mathPrefab, new Vector2(mySave.mathXCoord[i], mySave.mathYCoord[i]), Quaternion.identity);
                    newObj.transform.parent = GameObject.Find("Questions").transform;
                    newObj.name = "Question" + i.ToString();
                }

                if (mySave.playerXCoord != 0)
                {
                    this.transform.position = new Vector2(mySave.playerXCoord, mySave.playerYCoord);
                }


                if(mySave.character == "BBoy")
                {
                    myAnimator.runtimeAnimatorController = Instantiate(Resources.Load("BBoy") as RuntimeAnimatorController);
                    PlayerPrefs.SetString("Character", "Marcus");
                    myAnimator.runtimeAnimatorController.name = "BBoy";
                }
                else
                {
                    myAnimator.runtimeAnimatorController = Instantiate(Resources.Load("HGirl") as RuntimeAnimatorController);
                    PlayerPrefs.SetString("Character", "Maria");
                    myAnimator.runtimeAnimatorController.name = "HGirl";
                }


            }
        }
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

        
        if (other.CompareTag("Enemy") || myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            if (isTakingDM == false)
            {
                StartCoroutine(TakeDamage());
            }

        }

    }

    // Freezes Player at position (Is Referenced by Math Scripts)
    public void FreezePlayer()
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
        if (inWater == false && !myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            CanRun();
        }
    }

    void CanRun()
    {
        shouldLock = false;

        if (SceneManager.GetActiveScene().name == "World_001Ending")
        {
            shouldLock = true;
        }

        Vector2 playerVelocity;
        bool hSpeed;

        myAnimator.SetBool("Swimming", false);
        float left_right_movement = CrossPlatformInputManager.GetAxis("Horizontal"); //between -1 and 1, this by default gets player's "a" and "d" or left/right arrow keystrokes.

        if(shouldLock == false)
        {
            playerVelocity = new Vector2(left_right_movement * runSpeed, myRigidBody.velocity.y); //creates a new x vector coordinate equal to player input times the runspeed variable
            myRigidBody.velocity = playerVelocity; //sets the player velocity equal to the new vector.

            hSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon; //tests to see if player character x velicity is appreciably greater than zero.
            myAnimator.SetBool("Running", hSpeed); //sets the player character to running animation if bool is true. 
        }

        if (shouldLock == true && left_right_movement > 0)
        {
            playerVelocity = new Vector2(left_right_movement * runSpeed, myRigidBody.velocity.y); //creates a new x vector coordinate equal to player input times the runspeed variable
            myRigidBody.velocity = playerVelocity; //sets the player velocity equal to the new vector.

            hSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon; //tests to see if player character x velicity is appreciably greater than zero.
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

        myAnimator.SetBool("Swimming", false);
        float controlFlow = CrossPlatformInputManager.GetAxis("Vertical"); //by default gets player's "w" and "s" or up/down arrow keystrokes.
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlFlow * climbSpeed); //creates a new y vector coordinate equal to player input times the climbspeed variable.
        myRigidBody.velocity = climbVelocity; //sets the player velocity equal to the new vector.
        myRigidBody.gravityScale = 0f; //removes gravity from the player while on the ladder

        bool playerHasV = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon; //tests to see if player character y velocity is appreciably greater than zero.
        myAnimator.SetBool("Climbing", playerHasV); //sets the player character to climbing animation if bool is true.

        // when Player is touching the Ground and inside the Ladder Colider Box
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
             CanRun();
        }
    }

    private void Swim()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Water"))) //tests to see if player character's feet are NOT touching a ladder 
        {
            myAnimator.SetBool("Swimming", false);
            myAnimator.SetBool("IdleSwim", false);
            inWater = false;
            //myAnimator.SetBool("Running", false); //sets climbing to false
            myRigidBody.gravityScale = gravityScaleAtStart; //sets normal gravity.
            return;
        }

        myAnimator.SetBool("Running", false);
        bool hSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        bool noSpeed = Mathf.Abs(myRigidBody.velocity.x) <= Mathf.Epsilon;
        inWater = true;
        myAnimator.SetBool("Swimming", hSpeed);
        myAnimator.SetBool("IdleSwim", noSpeed);
        myRigidBody.gravityScale = underWaterGrav;

        if (myRigidBody.velocity.y < sinkSpeed)
        {
            
            Vector2 waterVelocity = new Vector2(myRigidBody.velocity.x, sinkSpeed);
            myRigidBody.velocity = waterVelocity;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump")) //by default gets player's "spacebar" input.
        {
            Vector2 swimVelocity = new Vector2(0f, swimSpeed); //creates a new x vector coordinate equal to player input times the runspeed variable
            myRigidBody.velocity += swimVelocity; //sets the player velocity equal to the new vector. 
        }

        float left_right_movement = CrossPlatformInputManager.GetAxis("Horizontal"); //between -1 and 1, this by default gets player's "a" and "d" or left/right arrow keystrokes.
        Vector2 playerVelocity = new Vector2((left_right_movement * runSpeed) / 2, myRigidBody.velocity.y); //creates a new x vector coordinate equal to player input times the runspeed variable
        myRigidBody.velocity = playerVelocity; //sets the player velocity equal to the new vector.

        //bool hSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon; //tests to see if player character x velicity is appreciably greater than zero.
        //myAnimator.SetBool("Running", hSpeed);
    }

    private void Jump()
    {
       
        
        
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && CrossPlatformInputManager.GetButtonDown("Jump") && inWater == false && shouldLock == false) //by default gets player's "spacebar" input.
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed); //creates a new y vector coordinate equal to the Jumpspeed variable
            myRigidBody.velocity += jumpVelocity; //sets the player character velocity equal to the new vector.
            myAnimator.SetTrigger("isJumping");
            FindObjectOfType<AudioController>().Play("Jump");
           
        }

        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Enemy")) && CrossPlatformInputManager.GetButtonDown("Jump") && inWater == false) //by default gets player's "spacebar" input.
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed); //creates a new y vector coordinate equal to the Jumpspeed variable
            myRigidBody.velocity += jumpVelocity; //sets the player character velocity equal to the new vector.
            myAnimator.SetTrigger("isJumping");
        }

        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Trampoline")) && !myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && CrossPlatformInputManager.GetButtonDown("Jump")) //by default gets player's "spacebar" input.
        {
            Vector2 jumpVelocity = new Vector2(0f, (jumpSpeed * 1.5f)); //creates a new y vector coordinate equal to the Jumpspeed variable
            myRigidBody.velocity += jumpVelocity; //sets the player character velocity equal to the new vector.
            myAnimator.SetTrigger("isJumping");
            FindObjectOfType<AudioController>().Play("Boing");
        }
    }

    private void Die()
    {
        //myPause = FindObjectOfType<Pause>();
        
        if (myRigidBody.transform.position.y < deathFall || health <= 0)
        {
            //DontDestroyOnLoad(myPause.PauseMenuCanvas);
            //DontDestroyOnLoad(SaveMenuCanvas);
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            FindObjectOfType<Player>().SetLoaded(false);
            ResetHealth();
        }
    }

    private void changeAnim()
    {
        bool hSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon; //tests to see if player's x velocity is appreciably greater than 0.
        if (hSpeed && shouldLock == false) //if true
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f); //flips player anim direction
        }
    }

    IEnumerator TakeDamage()
    {
        isTakingDM = true;

        if (isTakingDM == true){

            FindObjectOfType<AudioController>().Play("Hit");
            
        }
        health -= 1;
        healthBar.value = health;      // update health bar UI
        // Flashing 5 times
        for (int i = 0; i < 5; i++)
        {
            sr.material = matWhite;
            yield return new WaitForSeconds(.1f);
            
            
            ResetMaterial();
            yield return new WaitForSeconds(.1f);
        }
        
        isTakingDM = false;
        yield return new WaitForSeconds(0.5f);
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void ResetHealth()
    {
        health = 6;
        healthBar.value = health;
    }

    void OnCollsionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = null;

        }
    }
}