using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //Game objects
    private GameManager gameManager;

    //Music bools
    private bool intro2Playing = false;

    //Audio Source
    public AudioSource introSource;
    public AudioSource introGameSource;
    public AudioSource gameSource;

    //Musics
    [SerializeField] private AudioClip introAudioClip1;
    [SerializeField] private AudioClip introAudioClip2;
    [SerializeField] private AudioClip gameSeparatorAudioClip;
    [SerializeField] private AudioClip[] gameAudioClip;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        introGameSource.clip = gameSeparatorAudioClip;
        IntroMusic(false);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //When the intro 1 ends, intro 2 enters
        if (introSource.isPlaying == false && intro2Playing == false && gameManager.isGameActive == false)
        {
            introSource.clip = introAudioClip2; //Intro 2 is put to play
            introSource.loop = true; //Loops the music
            introSource.Play();
            intro2Playing = true;
        }
    }

    public void EndMusic()
    {
        gameSource.Stop();
        introGameSource.Play();
        IntroMusic(true);
    }

    private void IntroMusic(bool loop)
    {
        introSource.loop = loop;
        introSource.clip = introAudioClip1; //Intro 1 is put to play
        introSource.Play();
    }

    public void StartGameMusic(int difficulty)
    {
        introSource.Stop();
        introGameSource.Play();
        gameSource.clip = gameAudioClip[difficulty - 1];
        gameSource.Play();
    }
}
