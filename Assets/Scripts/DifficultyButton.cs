using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private MusicManager musicManager;
    private Button button;
    public int difficulty;


    void Start()
    {
        button = GetComponent<Button>(); //Get the button component
        button.onClick.AddListener(SetDifficulty); //Set the button listener
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); //Finds the Game Manager script
        musicManager = GameObject.Find("Music Manager").GetComponent<MusicManager>(); //Finds the Music Manager script
    }

    void SetDifficulty()
    {
        gameManager.StartGame(difficulty); //Calls the StartGame function on Game Manager
        musicManager.StartGameMusic(difficulty); //Calls the StartGameMusic function on Music Manager
    }
}
