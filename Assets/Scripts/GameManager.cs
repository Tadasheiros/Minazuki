using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Managers
    private MusicManager musicManager;

    //Game Parameters
    public bool isGameActive = false; //Used globally
    public bool gameEnded = false; //Used globally
    public int difMult; //Used globally
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private int gameStartTime;
    [SerializeField] private int timeCounter;

    //Enemy spawn
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private Vector3 spawnPos = new Vector3(17, 5.75f, -2.5f);

    //Score
    [SerializeField] private int score;

    //GUI Texts
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject soundMenu;

    void Start()
    {
        musicManager = GameObject.Find("Music Manager").GetComponent<MusicManager>(); //Gets music manager script
    }

    void Update()
    {
        if (isGameActive == true) //If the is active
        {
            timeCounter = (int)Time.time - gameStartTime; //Using this to reset the count
            timerText.text = "Time: " + timeCounter.ToString("#00"); //Puts the text on the GUI
        }
        
    }

    
    IEnumerator SpawnTarget() //Spawn the enemies
    {
        while (isGameActive) //While the game is active
        {
            yield return new WaitForSeconds(spawnRate); //Whait to spawn
            int index = Random.Range(0, targets.Count); //Generates a random int that will choose the monster to spawn
            Instantiate(targets[index], spawnPos, targets[index].transform.rotation); //Spawns the monster
        }
    }

    
    public void UpdateScore(int scoreToAdd) //Updates the score
    {
        score += scoreToAdd; //Defines the score
        scoreText.text = "Points: " + score; //Show the score on the screen
    }

    
    public void GameOver() //Is called by PlayerController to end the game
    {
        isGameActive = false; //Sets the game off
        gameEnded = true; //A bool to the music manager
        restartButton.gameObject.SetActive(true); //Show the restart buttom
        gameOverText.gameObject.SetActive(true); //Show the game over text
        soundMenu.gameObject.SetActive(true); //Set the audio menu on
        musicManager.EndMusic(); //Call the class on the music manager
    }

    
    public void RestartGame() //When the game ends, the restart buttom calls this function
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reloads the scene
    }

    
    public void StartGame(int difficulty) //The DifficultyButton will call this function and atribute a int to it
    {
        titleScreen.gameObject.SetActive(false); //Set the title screen off
        soundMenu.gameObject.SetActive(false); //Set the audio menu off
        isGameActive = true; //Activate the game
        difMult = difficulty; //Sets the speed based on the difficulty chosen
        StartCoroutine(SpawnTarget()); //Starts the mob spawn
        gameStartTime = (int) Time.time; //Sets the time of start
        timeCounter = 0; //Reset the counter
        score = 0; //Reset the score
        UpdateScore(0); //Also reset the score
    }

}
