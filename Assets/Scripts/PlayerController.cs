using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player atributtes
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float move;
    [SerializeField] private float speed = 5;
    [SerializeField] private bool isOnGround = true;
    [SerializeField] private bool jumpLand = false;
    [SerializeField] private float attackCooldown = 0.5f;
    private Rigidbody playerRb;

    //Bound to movement
    [SerializeField] private float moveBound = 12f;

    //Player animator
    private Animator playerAnim;

    //Game Objects
    private GameManager gameManager;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject playerAttack;

    //Audio
    [SerializeField] private AudioSource attackAudioSource;
    [SerializeField] private AudioSource runAudioSource;
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private AudioSource jumpAudioSource;
    [SerializeField] private AudioClip[] attackAudioClip;
    [SerializeField] private AudioClip runningAudioClip;
    [SerializeField] private AudioClip[] jumpStartAudioClip;
    [SerializeField] private AudioClip[] jumpEndAudioClip;
    [SerializeField] private AudioClip[] deathAudioClip;
    

    //public ParticleSystem explosionParticle;
    //public ParticleSystem dirtParticle;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //Gets the Rigidbody
        playerAnim = GetComponent<Animator>(); //Gets the animator
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); //Gets the Game Manager script
    }


    void Update()
    {
        if (gameManager.isGameActive) //If the game is active
        {
            Movement();
            
            attackCooldown -= Time.deltaTime; //Reduces the cd timer

            Attack();

            if (!isOnGround) //If he's in the air
            {
                playerAnim.SetBool("Run", false); //Stop the running animation
                jumpLand = false; //Prepares the landing sound

                if (runAudioSource.isPlaying == true) //Checks if the run sound is on
                {
                    runAudioSource.Stop(); //Stop the run sound
                }

                if (playerRb.linearVelocity.y < 0) //Checks if he's falling
                {
                    playerAnim.SetBool("Fall", true); //Set the fall animation
                }
            }
            else //If he's on the ground
            {
                playerAnim.SetBool("Run", true); //Set the run animation

                if (jumpLand == false) //If he ins't in the air
                {
                    int rand = Random.Range(0, jumpEndAudioClip.Length); //Get a random number
                    jumpAudioSource.PlayOneShot(jumpEndAudioClip[rand], 1.0f); //Plays the landing sound once
                    jumpLand = true; //Sets the bool
                }

                if (runAudioSource.isPlaying == false) //If the running sound isn't on
                {
                    runAudioSource.Play(); //Plays it
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision) //On detection of other object with collision
    {
        if (collision.gameObject.CompareTag("Ground")) //If is the ground
        {
            isOnGround = true;
            //dirtParticle.Play();
        }

        else if (collision.gameObject.CompareTag("Enemy")) //If the player collides with an enemy
        {
            int rand = Random.Range(0, deathAudioClip.Length); //Set a random audio to play
            deathAudioSource.clip = deathAudioClip[rand]; //Get a random number
            deathAudioSource.Play(); //Play the death audio
            gameManager.GameOver(); //Set the game over class
            runAudioSource.Stop(); //Stop the running audio
            playerAnim.SetBool("Hit", true); //Hit animation
            playerAnim.SetBool("Death", true); //Death animation
            //dirtParticle.Stop();
            //explosionParticle.Play();
        }
    }


    void Movement() //General movement of the player
    {
        move = Input.GetAxis("Horizontal"); //Get the buttons to move

        if (transform.position.x < -moveBound) //Block the player from going out of the screen
        {
            transform.position = new Vector3(-moveBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > moveBound)
        {
            transform.position = new Vector3(moveBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x >= -moveBound && transform.position.x <= moveBound) //Controls the movement
        {
            transform.Translate(Vector3.right * move * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) //Controls the jump
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //Adds the force to the rigid body
            isOnGround = false; //Isn't on ground anymore
            playerAnim.SetTrigger("Jump"); //Jump animation
            int rand = Random.Range(0, jumpStartAudioClip.Length); //Get a random number
            jumpAudioSource.PlayOneShot(jumpStartAudioClip[rand], 1.0f); //Play the jump audio once
            //dirtParticle.Stop();
        }
    }


    void Attack() //General attackof the player
    {
        if (Input.GetMouseButtonDown(0) && attackCooldown <= 0) //If the player left click the mouse
        {
            if (isOnGround)
            {
                playerAnim.SetTrigger("Attack");
            }
            else
            {
                playerAnim.SetTrigger("Jump_Attack");
            }

            int rand = Random.Range(0, attackAudioClip.Length);
            attackAudioSource.PlayOneShot(attackAudioClip[rand], 1f);
            attackCooldown = 0.5f;
        }

        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack2 Hero") || playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack2 Hero 0"))
        {
            playerAttack.gameObject.SetActive(true);
        }
        else
        {
            playerAttack.gameObject.SetActive(false);
        }
    }
}
