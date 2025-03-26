using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy Atribbutes
    [SerializeField] private float speed = 5; //Speed of the enemy
    [SerializeField] private float leftBound = -15; //Bound where he will despawn
    [SerializeField] private int pointValue; //His value of points
    private bool pointCounted; //Bool to count the points once
    public bool hit = false; //Death bool
    private BoxCollider enemyCollider;
    [SerializeField] private bool isRunning = false; //Bool to running sound and animation

    //Animator
    private Animator enemyAnim;

    //Game Objects
    private GameManager gameManager;

    //Audio
    [SerializeField] private AudioSource enemyDeathAudioSource;
    [SerializeField] private AudioSource enemyRunAudioSource;
    [SerializeField] private AudioClip[] deathAudioClip;
    [SerializeField] private AudioClip runAudioClip;


    void Start()
    {
        enemyAnim = GetComponent<Animator>(); //Gets the animator
        enemyCollider = GetComponent<BoxCollider>(); //Gets the collider
        enemyRunAudioSource.clip = runAudioClip; //Sets the run audio clip to the audio source

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); //Gets the Game Manager script
    }

    void Update()
    {
        if (gameManager.isGameActive) //If the game is active
        {
            Velocity(speed); //Sets the movement of the enemy

            if (isRunning == false)
            {
                enemyRunAudioSource.Play(); //Play the run sound
                isRunning = true; //Changes the bool to occur one time only
            }

            if (hit == true) //If the enemy dies
            {
                enemyAnim.SetBool("Hit", true); //Plays the hit animation
                enemyAnim.SetBool("Death", true); //Plays the death animation
                enemyCollider.enabled = false; //If the enemy dies, deacitvate it's collider
                speed = 5; //Set the speed to the same of the background


                if (pointCounted == false) //Give the points to the player
                {
                    gameManager.UpdateScore(pointValue * gameManager.difMult); //Give the score
                    enemyRunAudioSource.Stop(); //Stop the running audio
                    int rand = Random.Range(0, deathAudioClip.Length); //Set a random audio to play
                    enemyDeathAudioSource.PlayOneShot(deathAudioClip[rand], 1f); //Play the death sound once
                    pointCounted = true; //Sets the bool
                }
            }

            if (transform.position.x < leftBound) //If he hits the boundarie
            {
                Destroy(gameObject); //Destroy it
            }
        }
        else
        {
            if (enemyRunAudioSource.isPlaying == true) //When the game ends and the running sound stills on
            {
                enemyRunAudioSource.Stop(); //Stops the audio
            }
        }
    }

    void Velocity(float speed) //Set the movement
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed * gameManager.difMult);
    }


}
