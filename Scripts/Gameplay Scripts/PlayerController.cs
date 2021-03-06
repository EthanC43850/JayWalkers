using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Outside Scripts")]
    public PowerUpMeter powerupMeterScript;
    private MainCamera mainCameraScript;
    private PlatformSpawner platformSpawnerScript;
    [HideInInspector]public GameManager gameManagerScript;
    public Player playerScript;
    public AuthManager authManagerScript;

    [Header("Player Controls:")]
    public float speed;
    public float startSpeed;
    public float speedBeforePowerup;
    [SerializeField] LayerMask groundMask;
    public float jumpForce;
    public float longJumpForce;
    private float jumpTimeCounter = 0;
    private float jumpTime = 0.75f;
    public float gravityModifier;
    public bool isOnGround;
    private bool isJumping;
    private bool isCrouching;
    public Transform[] lanes;
    public int laneNumber = 1;
    public float changeLaneSpeed;

    /* Attempted to make a Rb controller that can smoothly step over bumps
    [Header("Player Step Climb:")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f; //The higher the step smooth is, the higher the character will jump up. The lower the step smooth, the smoother the transition*/

    [Header("Player Endurance:")]
    public float energyEnduranceTime;
    public float energyEnduranceDuration;
    public float energyRefillAmount = 7;
    public GameObject sodaEndurancePowerupMeterUI;

    [Header("Balloon Powerup:")]
    public bool balloonPowerup;
    public float balloonPowerUpTime;
    public float balloonPowerupDuration;
    public GameObject balloonPowerupMeterUI;
    public ParticleSystem balloonDisappearFx;
    public GameObject balloonEasterEggCollider;

    [Header("Tank Powerup:")]
    public bool tankPowerup;
    public float tankPowerUpTime;
    public float tankPowerupDuration;
    public GameObject miniTank;
    public GameObject tankPowerUpMeterUI;
    public Quaternion normalRotation = Quaternion.Euler(0f, -90, 0f);

    [Header("Plane Powerup:")]
    public bool planePowerup;
    public float planeSmoothTime;
    public float planePowerupTime;
    public float planePowerupTimeLeft;
    public GameObject plane;
    public GameObject planePowerupMeterUI;
    private Quaternion planeTiltUp = Quaternion.Euler(-29f, -0.25f, 0f);
    private Quaternion planeFlat = Quaternion.Euler(0f, -0.25f, 0f);
    [SerializeField] LayerMask planeLayerMask;                              

 


    [Header("Particles:")]
    public ParticleSystem yellowFireWork;
    public ParticleSystem greenFireWork;
    public ParticleSystem powerupSmoke;
    public ParticleSystem deathSmoke;
    public ParticleSystem runParticles;
    public GameObject playerBalloons;



    public GameObject playerStartPos;

    [Header("Progression")]
    public int soundTrack1_Progress;
    public int soundTrack2_Progress;
    public int soundTrack3_Progress;


    [Header("Player Build")]
    public Animator playerAnim;
    public Rigidbody playerRb;
    public BoxCollider playerCollider;
    public GameObject playerMesh;

    [Header("Sound FX:")]
    public AudioSource audioSource;
    public AudioClip laneSwitchSound;
    public AudioClip jumpSound;
    public AudioClip slideSound;
    public AudioClip deathSound;
    public AudioClip planeSound;
    public AudioClip coinSFX;
    //public AudioClip coinSound;
    //public AudioClip Soda;

    [Header("Hot patch")]
    public GameObject[] UIElementsToDisable;


    void Start()
    {

        speed = startSpeed;               //Initialize player speed to the slowest speed         
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();         
        playerCollider = GetComponent<BoxCollider>();
        mainCameraScript = GameObject.Find("CM Running Vcam").GetComponent<MainCamera>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        platformSpawnerScript = GameObject.Find("Platform Spawner").GetComponent<PlatformSpawner>();


        //transform.position = new Vector3(transform.position.x, transform.position.y, lanes[1].transform.position.z); //Places player in lane one
    }


    private void FixedUpdate()
    {

        
    }


    void Update()
    {
        //Running-Player Movement
        if (gameManagerScript.gameOver != true && gameManagerScript.mainMenu != true && gameManagerScript.insideTown != true)
        {
            playerAnim.SetBool("Death_b", false);
            playerAnim.SetBool("Running_b", true);
            transform.position += transform.forward * speed * Time.deltaTime;
            CheckForJump();
            SwitchLanes();
            BalloonPowerup();
            TankPowerup();
            PlanePowerup();
            PlayerEnergyMeter();
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, lanes[laneNumber].transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, changeLaneSpeed);         //smooth transition from original position to target lane
        }


        //Jump when player presses space bar, long jump if player holds space bar
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isOnGround && gameManagerScript.gameOver == false && gameManagerScript.insideTown != true)
        {
            audioSource.PlayOneShot(jumpSound, 1.0f);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //playerAnim.SetBool("Crouch_b", false);
            isCrouching = false;
            isJumping = true;
            jumpTimeCounter = jumpTime;


        }


        if (Input.GetKey(KeyCode.Space) && balloonPowerup)
        {
            if (jumpTimeCounter > 0 && isJumping == true)
            {
                playerRb.AddForce(Vector3.up * longJumpForce, ForceMode.Impulse);

                playerAnim.SetBool("Jump_b", true);

                jumpTimeCounter -= Time.deltaTime;

            }
            else
            {
                isJumping = false;

            }
        }



        /*if (Input.GetKeyDown(KeyCode.R))
        {
            gameManagerScript.RestartGame();
            

        }*/
    }


    /*void FixedUpdate()  //When dealing with RB's FixedUpdate should be used (Disagree)
    {
        //stepClimb();
    }*/


    /*void stepClimb()
    {
        RaycastHit hitLower;
        Debug.DrawRay(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), Color.red, 0.1f);
        Debug.DrawRay(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), Color.red, 0.2f);
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;

            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                Debug.Log("Moving up " + playerRb.position);
                //playerRb.MovePosition(playerRb.position + new Vector3(0f, stepSmooth, 0f));
                //playerRb.transform.position += new Vector3(0f, stepSmooth, 0f);
                //playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
        {

            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
            {
                playerRb.transform.position += new Vector3(0f, stepSmooth * Time.deltaTime, 0f);
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
        {

            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
            {
                playerRb.transform.position += new Vector3(0f, stepSmooth * Time.deltaTime, 0f);
            }
        }
    }*/

    private void PlayerEnergyMeter()
    {
        if (energyEnduranceDuration > 0 && gameManagerScript.gameOver == false && !planePowerup && !tankPowerup) //The meter for the player endurance duration decreases
        {
            energyEnduranceDuration -= Time.deltaTime;
            powerupMeterScript.UpdateEnergyMeter(energyEnduranceDuration); //The amount of time left for the baloon power up is displayed in the UI meter
        }
    }

    //Powerup Functions
    private void BalloonPowerup()
    {
        if (balloonPowerup && balloonPowerupDuration > 0) //The meter for the balloon powerup duration decreases
        {
            balloonPowerupMeterUI.SetActive(true);
            playerBalloons.SetActive(true);
            balloonPowerupDuration -= Time.deltaTime;
            powerupMeterScript.UpdateBalloonPowerMeter(balloonPowerupDuration); //The amount of time left for the baloon power up is displayed in the UI meter
        }
    }
    private void PlanePowerup()
    {

        // Manual Powerup test
         /*if (Input.GetKeyDown(KeyCode.O))
         {
             planePowerup = true;
            speedBeforePowerup = speed;

            speed = 120;
             audioSource.clip = planeSound;
             audioSource.loop = true;
             audioSource.Play();
             plane.SetActive(true);               
             playerMesh.SetActive(false);       
             playerRb.useGravity = false;
             powerupMeterScript.SetPlanePowerMeterMax(planePowerupTime);
             planePowerupTimeLeft = planePowerupTime;
         }*/

        if (planePowerup && planePowerupTimeLeft > 0)
        {
            //playerCollider.size = new Vector3(13, 3, 6.0f);
            playerRb.velocity = Vector3.zero;       
            planePowerupMeterUI.SetActive(true);
            planePowerupTimeLeft -= Time.deltaTime;
            powerupMeterScript.UpdatePlanePowerMeter(planePowerupTimeLeft); //The amount of time left for the plane power up is displayed in the UI meter
        }

        RaycastHit hit;
        if (planePowerup)
        {
            //Keep in mind, when player jumps on an elevated platform and activates a plane, the jump makes the plane soar much higher
            //because of velocity, which is why I set velocity to zero before activating powerup
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, Mathf.Infinity, planeLayerMask))
            {
                float distanceToGround = hit.distance;
                Debug.DrawRay(transform.position + Vector3.up, Vector3.down * distanceToGround, Color.yellow);

                if (distanceToGround <= 11.0f)  //Plane rotates and rises up when ground gets closer
                {
                    transform.rotation = Quaternion.Euler(-20f, -90f, 0f);

                }
                else if (distanceToGround > 16)
                {

                    transform.rotation = Quaternion.Euler(0f, -90f, 0f);

                }
            }
        }
    }

    private void TankPowerup() 
    {
        
        // Manual Powerup test
        /*if (Input.GetKeyDown(KeyCode.T))
        {
            powerupSmoke.Play();
            tankPowerup = true;
            playerMesh.SetActive(false);
            playerRb.constraints = RigidbodyConstraints.None;  //Unfreeze constraint
            playerRb.constraints = RigidbodyConstraints.FreezeRotationY;
            playerRb.constraints = RigidbodyConstraints.FreezeRotationZ;

            miniTank.SetActive(true);
            powerupMeterScript.SetTankMeterMax(tankPowerUpTime);
            tankPowerupDuration = tankPowerUpTime;
        }*/

        if (tankPowerup && tankPowerupDuration > 0) //The meter for the tank powerup duration decreases
        {
            miniTank.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, lanes[laneNumber].transform.position.z), changeLaneSpeed);
            //playerCollider.size = new Vector3(2, 3, 6.8f);
            tankPowerUpMeterUI.SetActive(true);
            tankPowerupDuration -= Time.deltaTime;
            powerupMeterScript.UpdateTankPowerMeter(tankPowerupDuration); //The amount of time left for the baloon power up is displayed in the UI coin meter
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !tankPowerup && !planePowerup && gameManagerScript.gameOver == false)
        {
            GameOver();
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), playerCollider);
            if (collision.gameObject.layer == 10) //check to see if obstacle is a vehicle
            {
                Debug.Log("Stopped vehicle wheels and speed");
                collision.gameObject.GetComponentInParent<ObstacleMovement>().speed = 0;    //Stop the vehicle that the player has hit
                collision.gameObject.GetComponentInParent<ObstacleMovement>().wheelSpinSpeed = 0;  //Stop vehicle wheels from spinning
            }

        }
        else if (collision.gameObject.CompareTag("Obstacle") && tankPowerup == true)
        {
            Destroy(collision.gameObject);

        }
    }





    private void SwitchLanes()
    {
        if (gameManagerScript.gameOver != true && mainCameraScript.inverseControls != true && gameManagerScript.insideTown != true)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if(planePowerup != true)
                {
                    audioSource.PlayOneShot(laneSwitchSound, 0.7f);
                }
                laneNumber--;
                laneNumber = Mathf.Clamp(laneNumber, 0, 3); //If lane number is less than 0, return the minimum value 0. If the lane number becomes greater than 3, return the maximum value 3
                //Debug.Log(laneNumber);

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (planePowerup != true)
                {
                    audioSource.PlayOneShot(laneSwitchSound, 1.0f);
                }
                laneNumber++;
                laneNumber = Mathf.Clamp(laneNumber, 0, 3);

            }
        }
        else if (gameManagerScript.gameOver != true && mainCameraScript.inverseControls == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                laneNumber++;
                laneNumber = Mathf.Clamp(laneNumber, 0, 3); //If lane number is less than 0, return the minimum value 0. If the lane number becomes greater than 3, return the maximum value 3

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                laneNumber--;
                laneNumber = Mathf.Clamp(laneNumber, 0, 3);

            }
        }

    }

    private void CheckForJump()
    {
        //Check to see if ray intersects with ground
        
        isOnGround = Physics.Raycast(transform.position + Vector3.up, Vector3.down * 1.5f, 2f, groundMask);
        Debug.DrawRay(transform.position + Vector3.up, Vector3.down * 1.5f, Color.green);

        if (!isOnGround && !isCrouching)
        {
            playerAnim.SetBool("Jump_b", true);
        }
        if (isOnGround == true)
        {
            playerAnim.SetBool("Jump_b", false);
        }


        #region Now placed jump in fixed update
        /*//Jump when player presses space bar, long jump if player holds space bar
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isOnGround && gameManagerScript.gameOver == false && gameManagerScript.insideTown != true)
        {
            audioSource.PlayOneShot(jumpSound, 1.0f);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //playerAnim.SetBool("Crouch_b", false);
            isCrouching = false;
            isJumping = true;
            jumpTimeCounter = jumpTime;


        }


        if (Input.GetKey(KeyCode.Space) && balloonPowerup)
        {
            if (jumpTimeCounter > 0 && isJumping == true)
            {
                playerRb.AddForce(Vector3.up * longJumpForce, ForceMode.Impulse);

                playerAnim.SetBool("Jump_b", true);

                jumpTimeCounter -= Time.deltaTime;

            }
            else
            {
                isJumping = false;

            }
        }*/

        #endregion

        //Player Slide
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && gameManagerScript.gameOver == false && gameManagerScript.insideTown != true) //prevents player from accidently falling back down to ground
        {
            //Deactivate plane powerup
            if (planePowerupTimeLeft > 0)
            {
                planePowerupTimeLeft = 0.1f;
            }

            StartCoroutine("Slide"); //If player presses down arrow, then the player should slide
        }

    }

    IEnumerator Slide()
    {
        isCrouching = true;
        playerCollider.size = new Vector3(2, 1.5f, 2);
        playerCollider.center = new Vector3(0, 0.7f, playerCollider.center.z);

        playerAnim.SetBool("Slide_b", true); //drop down animation begin
        playerAnim.SetBool("Jump_b", false);
        playerRb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
        audioSource.PlayOneShot(slideSound, 1.0f);


        yield return new WaitForSeconds(1.0f);    //length of slide
        playerAnim.SetBool("Slide_b", false); 
        isCrouching = false;
        playerCollider.size = new Vector3(2, 3, 2);
        playerCollider.center = new Vector3(playerCollider.center.x, 1.5f, playerCollider.center.z);
        

    }

    public void GameOver()
    {
        gameManagerScript.gameOver = true;
        //powerupMeterScript.UpdateBalloonPowerMeter(0);
        deathSmoke.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound, 1.0f);
        speed = 0;
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);

        if(gameManagerScript.score > playerScript.highScore)
        {
            playerScript.highScore = gameManagerScript.score;
            authManagerScript.SaveHighScoreData(gameManagerScript.score);
            // Activate high score timeline
            gameManagerScript.achievedHighScore = true;
        }


        gameManagerScript.titleScreenScript.shopUIScript.AddProgressToSoundTrack(gameManagerScript.score);


    }





    private void OnTriggerEnter(Collider other)
    {
        //Activates the next platform
        if (other.gameObject.CompareTag("Platform"))
        {
            platformSpawnerScript.SpawnPlatform();  //causes the next platform to spawn
            Destroy(other.transform.parent.gameObject, 1.0f);  // Call a function that destroys platform and objects
            
        }


        #region Test Trigger Collision
        // This code is if I want the player to be able to hit the side of a car and still live ORRR Keep track of the previous lane number by making a previous lane variable and simply return to previous lane
        /*if (other.gameObject.tag == "VehicleRightSide")
        {
            laneNumber++;
            deathSmoke.Play();
        }
        else if (other.gameObject.tag == "VehicleLeftSide")
        {
            laneNumber--;
            deathSmoke.Play();
        }*/
        #endregion


        if (other.gameObject.tag == "Obstacle" && tankPowerup == false && planePowerup == false)
        {
            GameOver();
            Physics.IgnoreCollision(other.gameObject.GetComponentInParent<Collider>(), playerCollider); //Makes sure that vehicle does not go off track after death

            //other.gameObject.GetComponentInParent<ObstacleMovement>().speed = 0;    //Stop the vehicle that the player has hit
            //other.gameObject.GetComponentInParent<ObstacleMovement>().wheelSpinSpeed = 0;  //Stop vehicle wheels from spinning

            //Stop vehicle wheels from spinning

        }
        else if (other.gameObject.tag == "Obstacle" && tankPowerup == true)
        {
            Destroy(other.transform.parent.gameObject);


        }


        if (other.gameObject.CompareTag("Power Drink"))
        {
            greenFireWork.Play();
            Destroy(other.gameObject);
            gameManagerScript.UpdateDrinkCount(1);
            playerScript.currentTotalSodaCount++;
            energyEnduranceDuration += energyRefillAmount + (playerScript.enduranceUpgradeLevel * 1.5f);
            energyEnduranceDuration = Mathf.Clamp(energyEnduranceDuration, 0, energyEnduranceTime + (playerScript.enduranceUpgradeLevel * 2.5f)); // Max soda time left is dependent on the players endurance level


        }
        if (other.gameObject.CompareTag("Money"))
        {
            yellowFireWork.Play();
            audioSource.PlayOneShot(coinSFX, 0.5f);

            Destroy(other.gameObject);
            gameManagerScript.UpdateCashCount(1);
            playerScript.currentTotalCoinCount++;
            playerScript.networth++;

        }

        if (other.gameObject.CompareTag("BalloonPowerup"))
        {
            balloonPowerup = true;
            powerupMeterScript.SetBalloonMeterMax(balloonPowerUpTime + (playerScript.balloonUpgradeLevel * 1.5f));
            balloonPowerupDuration = balloonPowerUpTime + (playerScript.balloonUpgradeLevel * 1.5f);
            yellowFireWork.Play();
            balloonEasterEggCollider.SetActive(false);
            Destroy(other.gameObject);


        }

        if (other.gameObject.CompareTag("TankPowerup"))
        {
            powerupSmoke.Play();
            tankPowerup = true;
            playerMesh.SetActive(false);
            /*playerRb.constraints = RigidbodyConstraints.None;  //Unfreeze constraint
            playerRb.constraints = RigidbodyConstraints.FreezeRotationY;
            playerRb.constraints = RigidbodyConstraints.FreezeRotationZ;*/

            miniTank.SetActive(true);
            powerupMeterScript.SetTankMeterMax(tankPowerUpTime + (playerScript.tankUpgradeLevel * 1.0f));
            tankPowerupDuration = tankPowerUpTime + (playerScript.tankUpgradeLevel * 1.0f);
            Destroy(other.gameObject);


        }

        if (other.gameObject.CompareTag("PlanePowerup"))
        {
            planePowerup = true;

            speedBeforePowerup = speed;
            speed = 120;
            audioSource.clip = planeSound;
            audioSource.loop = true;
            audioSource.Play();
            plane.SetActive(true);
            playerMesh.SetActive(false);
            playerRb.useGravity = false;
            powerupMeterScript.SetPlanePowerMeterMax(planePowerupTime + (playerScript.planeUpgradeLevel * 0.5f));
            planePowerupTimeLeft = planePowerupTime + (playerScript.planeUpgradeLevel * 0.5f);


        }








    }


    public void EnableRunningState()
    {

        

        playerRb.isKinematic = false;
        speed = startSpeed;
        StartCoroutine(IncreaseSpeedOvertime());
        powerupMeterScript.SetEnergyMeterMax(energyEnduranceTime + (playerScript.enduranceUpgradeLevel * 2.5f)); // Max run time is 30 seconds 
        energyEnduranceDuration = energyEnduranceTime + (playerScript.enduranceUpgradeLevel * 2.5f);
        playerRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        gameObject.GetComponent<CharacterController>().enabled = !enabled;
        gameObject.GetComponent<ThirdPersonMovement>().enabled = !enabled;
    }

    IEnumerator IncreaseSpeedOvertime()
    {
        while(gameManagerScript.gameOver != true)
        {

            yield return new WaitForSeconds(3.0f);
            if(planePowerup == false)
            {
                speed += 1;
                speed = Mathf.Clamp(speed, 20, 75); // slowest speed is 20, fastest is 55
                foreach (GameObject characterSelectUI in UIElementsToDisable)
                {
                    characterSelectUI.SetActive(false);
                }
            }
        }

    }


}
