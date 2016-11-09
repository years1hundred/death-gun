using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private PlayerCharacterStat sprintBar;

    [SerializeField]
    private PlayerCharacterStat healthBar;

    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    public float moveSpeed;
    public float crouchSpeed;
    public float waterSpeed;
    public float groundCheckRadius;
    public float tunnelCheckRadius;
    public float solidObjectCheckRadius;
    public float ladderClimbSpeed;
    public float sprintSpeed;
    public float sprintDeduction;
    public float sprintRegen;
    public float sprintExhaustedRegen;
    public float fireVulnerability;
    private float ladderClimbVelocity;
    private float gravityStore;

    private int groundedCounter = 0;

    public bool sprintExhausted;
    public bool healthRegen;
    public bool resetInteractionText;
    public bool puttingOutFire;
    public bool touchingFire;
    private bool fire;
    private bool isGrounded;
    private bool insideTunnel;
    private bool insideSolidObject;
    private bool onLadder;
    private bool sprint;
    private bool isCrouched;
    private bool holdingDownCrouch;
    private bool insideWater;
    private bool wasGrounded;
    private bool groundedExit;
    private bool ladderZone;
    private bool interactedWithLadder;
    private bool usingDoor = false;
	private bool usingRamp = false;
    private bool disableMovement;

    public Transform groundCheck;
    public Transform tunnelCheck;
    public Transform solidObjectCheck;

    public LayerMask whatIsGround;
    public LayerMask whatIsTunnel;
    public LayerMask whatIsSolidObject;

    private Rigidbody2D myRigidbody;

    private Animator myAnimator;



    private void Awake()
    {
        sprintBar.Initialize();
        healthBar.Initialize();
    }



    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        myAnimator = GetComponent<Animator>();

        gravityStore = myRigidbody.gravityScale;
    }



    void Update()
    {
        if (ladderZone)
        {
            LadderInteractions();
        }


        //Enables the sprint function to work only while the left shift key is held down, but only if the sprint meter isn't exhausted
        if (!sprintExhausted && !insideTunnel)
        {
            if (Input.GetButtonDown("Sprint"))
            {
                sprint = true;
            }
            else if (Input.GetButtonUp("Sprint"))
            {
                sprint = false;
            }
        }


        if (healthRegen)
        {
            healthBar.CurrentVal += .1f;
        }


        //Checks to see if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


        if (isGrounded)
        {
            wasGrounded = true;
            //Debug.Log ("PlayerController    " + isGrounded);
        }


        //this if startement only activates if you go from being grounded to not being grounded between 2 frames- a sort of onColliderExit but for the grounded check
        if (!isGrounded && wasGrounded)
        {
            groundedExit = true;
        }
        else
        {
            groundedExit = false;
        }
        if (groundedExit)
        {
            //Debug.Log ("goundedexit" + groundedExit);
        }
        if (!isGrounded)
        {
            wasGrounded = false;
        }


        if (!onLadder)
        {
            //Checks to see if the player is inside a tunnel
            insideTunnel = Physics2D.OverlapCircle(tunnelCheck.position, tunnelCheckRadius, whatIsTunnel);
        }


        //Checks to see if the player is inside a solid object
        insideSolidObject = Physics2D.OverlapCircle(solidObjectCheck.position, solidObjectCheckRadius, whatIsSolidObject);


        if (!onLadder)
        {
			
            if (!usingDoor)
            {
                //handles basic horizontal movement for the player on normal ground
                BasicUpdateMovement();
            }
        }


        //Controls the basic animations
        myAnimator.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnimator.SetBool("Grounded", isGrounded);


        if (usingDoor)
        {
            //sets upward motion
            //can be changed later to allow for up or down
            myRigidbody.velocity = new Vector3(0f, moveSpeed, 0f);
            myRigidbody.gravityScale = 0;
            this.GetComponent<PolygonCollider2D>().enabled = false;

            if (groundedExit && groundedCounter == 0)
            {
                groundedCounter += 1;
                //Debug.Log (groundedCounter);
            }
            else if (groundedExit && groundedCounter != 0)
            {
                groundedCounter = 0;
                myRigidbody.velocity = new Vector3(0f, 0f, 0f);
                myRigidbody.gravityScale = 1;
                this.GetComponent<PolygonCollider2D>().enabled = true;
                usingDoor = false;
            }
        }


        disableMovement = insideSolidObject && ladderZone;


        //Causes the sprint meter to deplete and regenerate appropriately
        if (!sprintExhausted)
        {
            if (sprint && !onLadder && !insideSolidObject && !insideWater)
            {
                sprintBar.CurrentVal -= sprintDeduction;
                myAnimator.SetBool("Sprinting", true);
            }
            else if (sprint && onLadder)
            {
                sprintBar.CurrentVal += sprintRegen;
                myAnimator.SetBool("Sprinting", false);
            }
            else if (sprint && insideWater)
            {
                sprintBar.CurrentVal += sprintRegen;
                myAnimator.SetBool("Sprinting", false);
            }
            else if (!sprint)
            {
                sprintBar.CurrentVal += sprintRegen;
                myAnimator.SetBool("Sprinting", false);
            }
        }

        if (sprintExhausted)
        {
            if (!sprint)
            {
                sprintBar.CurrentVal += sprintExhaustedRegen;
            }
            else if (sprint && onLadder)
            {
                sprintBar.CurrentVal += sprintExhaustedRegen;
            }
        }

        //Makes it so that if the sprint meter regenerates fully, it unheats and can be used again
        if (sprintBar.CurrentVal == sprintBar.MaxVal * .3)
        {
            sprintExhausted = false;
        }

        //Makes it so that if the sprint meter hits zero, then it overheats and must fully recharge before being used again
        if (sprintBar.CurrentVal == 0)
        {
            sprint = false;
            sprintExhausted = true;
            myAnimator.SetBool("Sprinting", false);
        }


        //Allows the player to start eliminating the health bar of the emergency fire that is being interacted with
        if (fire)
        {
            if (Input.GetButtonDown("Interact"))
            {
                puttingOutFire = true;
            }
            else if (Input.GetButtonUp("Interact"))
            {
                puttingOutFire = false;
            }
        }

        //This damages the player character if he's standing too close to the fire
        if (touchingFire)
        {
            healthBar.CurrentVal -= fireVulnerability;
        }
    }



    void FixedUpdate()
    {
        if (!onLadder)
        {
            if (!usingDoor)
            {
				if (!usingRamp) {
					//handles basic horizontal movement for the player on normal ground
					BasicFixedUpdateMovement ();
				}
            
			}
        }
		if (usingRamp) {
			if (Input.GetAxisRaw("Horizontal") > 0f)
			{
				myRigidbody.velocity = new Vector3(moveSpeed, moveSpeed, 0f);
				transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (Input.GetAxisRaw("Horizontal") < 0f)
			{
				myRigidbody.velocity = new Vector3(-moveSpeed, -moveSpeed, 0f);
				transform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else
			{
				myRigidbody.velocity = new Vector3(0f, 0f, 0f);
			}


			//Handles sprint movement
			if (Input.GetAxisRaw("Horizontal") > 0f && sprint)
			{
				myRigidbody.velocity = new Vector3(sprintSpeed, sprintSpeed, 0f);
				transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (Input.GetAxisRaw("Horizontal") < 0f && sprint)
			{
				myRigidbody.velocity = new Vector3(-sprintSpeed, -sprintSpeed, 0f);
				transform.localScale = new Vector3(-1f, 1f, 1f);
			}


			//Handles normal crouching movement
			if (Input.GetAxisRaw("Horizontal") > 0f && isCrouched)
			{
				myRigidbody.velocity = new Vector3(crouchSpeed, crouchSpeed, 0f);
				transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (Input.GetAxisRaw("Horizontal") < 0f && isCrouched)
			{
				myRigidbody.velocity = new Vector3(-crouchSpeed, -crouchSpeed, 0f);
				transform.localScale = new Vector3(-1f, 1f, 1f);
			}


		}
    }



    void BasicUpdateMovement()
    {
        if (!disableMovement)
        {
            //Handles crouching
            if (Input.GetButtonDown("Crouch"))
            {
                myAnimator.SetBool("Crouched", true);
                isCrouched = true;
                holdingDownCrouch = true;
            }

            if (Input.GetButtonUp("Crouch"))
            {
                //"holdingDownCrouch" ensures that if the player is inside a tunnel and the release crouch, the player character doesn't stand in the middle of the tunnel
                holdingDownCrouch = false;
                if (insideTunnel == false)
                {
                    myAnimator.SetBool("Crouched", false);
                    isCrouched = false;
                }
            }

            if (holdingDownCrouch == false && insideTunnel == false)
            {
                myAnimator.SetBool("Crouched", false);
                isCrouched = false;
            }


            if (Input.GetButtonDown("Submit"))
            {
                usingDoor = true;
            }


            if (sprint)
            {
                myAnimator.SetBool("Sprinting", true);
            }

            if (!sprint)
            {
                myAnimator.SetBool("Sprinting", false);
            }
        }
    }



    void BasicFixedUpdateMovement()
    {
        if (!disableMovement)
        {
            //Handles normal walking movement
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            }


            //Handles sprint movement
            if (Input.GetAxisRaw("Horizontal") > 0f && sprint)
            {
                myRigidbody.velocity = new Vector3(sprintSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f && sprint)
            {
                myRigidbody.velocity = new Vector3(-sprintSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }


            //Handles normal crouching movement
            if (Input.GetAxisRaw("Horizontal") > 0f && isCrouched)
            {
                myRigidbody.velocity = new Vector3(crouchSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f && isCrouched)
            {
                myRigidbody.velocity = new Vector3(-crouchSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }


            //Handles movement while in water
            if (Input.GetAxisRaw("Horizontal") > 0f && insideWater)
            {
                myRigidbody.velocity = new Vector3(moveSpeed * waterSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f && insideWater)
            {
                myRigidbody.velocity = new Vector3(-moveSpeed * waterSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            if (Input.GetAxisRaw("Horizontal") > 0f && isCrouched && insideWater)
            {
                myRigidbody.velocity = new Vector3(crouchSpeed * waterSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f && isCrouched && insideWater)
            {
                myRigidbody.velocity = new Vector3(-crouchSpeed * waterSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }



    void LadderInteractions()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (interactedWithLadder == false)
            {
                interactedWithLadder = true;

                onLadder = true;

                isCrouched = false;

                myRigidbody.velocity = new Vector3(0f, 0f, 0f);

                myAnimator.SetBool("ClimbingLadder", true);

                myAnimator.SetBool("Crouched", false);

                myAnimator.SetBool("Sprinting", false);
            }
            else if (interactedWithLadder == true)
            {
                interactedWithLadder = false;

                onLadder = false;

                myAnimator.SetBool("ClimbingLadder", false);
            }
        }


        if (onLadder)
        {
            myRigidbody.gravityScale = 0f;

            ladderClimbVelocity = ladderClimbSpeed * Input.GetAxisRaw("Vertical");

            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, ladderClimbVelocity);

            Physics2D.IgnoreLayerCollision(9, 8, true);
        }

        if (!onLadder)
        {
            myRigidbody.gravityScale = gravityStore;

            if (!insideSolidObject)
            {
                Physics2D.IgnoreLayerCollision(9, 8, false);
            }
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        //Enables water movement and water animations
        if (other.gameObject.CompareTag("Water"))
        {
            insideWater = true;
            myAnimator.SetBool("InsideWater", true);
            myAnimator.SetBool("Sprinting", false);
        }


        if (other.gameObject.CompareTag("Ladder"))
        {
            ladderZone = true;
        }


        if (other.gameObject.CompareTag("Fire"))
        {
            fire = true;
        }

        if (other.gameObject.CompareTag("TouchingFire"))
        {
            touchingFire = true;
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        //Enables water movement and water animations
        if (other.gameObject.CompareTag("Water"))
        {
            insideWater = false;
            myAnimator.SetBool("InsideWater", false);
        }


        if (other.gameObject.CompareTag("Ladder"))
        {
            ladderZone = false;

            interactedWithLadder = false;

            onLadder = false;

            myAnimator.SetBool("ClimbingLadder", false);

            myRigidbody.gravityScale = gravityStore;

            Physics2D.IgnoreLayerCollision(9, 8, false);
        }


        if (other.gameObject.CompareTag("Fire"))
        {
            fire = false;
        }

        if (other.gameObject.CompareTag("TouchingFire"))
        {
            touchingFire = false;
        }
    }



    public void SetCollidersForSprite(int spriteNum)
    {
        if (!usingDoor)
        {
            //Handles the different colliders that the player character switches between depending on the sprite and animations
            colliders[currentColliderIndex].enabled = false;
            currentColliderIndex = spriteNum;
            colliders[currentColliderIndex].enabled = true;
        }
    }


	//ramp managers
	public void enterRamp(){
	myRigidbody.velocity = new Vector3 (0f, 0f, 0f);
		usingRamp = true;
		myRigidbody.gravityScale = 0;
	}

	public void exitRamp(){
		//myRigidbody.velocity = new Vector3 (0f, 0f, 0f);
		usingRamp = false;
		myRigidbody.gravityScale = 1;
	}
	//for when you reach the end of a ramp
	public void endRamp(){
		myRigidbody.velocity = new Vector3 (0f, -moveSpeed, 0f);
		usingRamp = false;
		myRigidbody.gravityScale = 1;
	}
	public bool getUsingRamp(){
		return usingRamp;
	}
}
