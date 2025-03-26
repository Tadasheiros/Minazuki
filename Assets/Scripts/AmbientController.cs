using UnityEngine;

public class AmbientController : MonoBehaviour
{
    //Game Objects
    private GameManager gameManager;

    //Atribbutes
    public float speed = 5;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float repeatWidth;


    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); //Get the Game Manager script

        //Background related
        startPos = transform.position; //Get the position of this object
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; //Gets half the x size
    }


    void Update()
    {
        if (gameManager.isGameActive)
        {
            SceneVelocity(speed);
        }

        
        if (transform.position.x < startPos.x - repeatWidth) //Repeat the background infinitely
        {
            transform.position = startPos;
        }
    }

    void SceneVelocity(float speed) //Set the velocity of the scenary
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed * gameManager.difMult);
    }
}
