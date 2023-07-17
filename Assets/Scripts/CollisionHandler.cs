using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    /**
    * Green Hearts Icons by rukanicon - 
    * https://www.freepik.com/icon/love_6052036
    * https://www.freepik.com/icon/love_6052056
    **/

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip obstacleCrash;
    [SerializeField] AudioClip reachFinishPad;

    [SerializeField] ParticleSystem obstacleCrashParticles;
    [SerializeField] ParticleSystem reachFinishPadParticles;

    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    int health = Health.health;
    int numOfHearts = Health.numOfHearts;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        HealthCheck();
        LastMeetingLocationCheck();
    }

    void HealthCheck()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void LastMeetingLocationCheck()
    {
        if (LastMeetingPoint.locationChanged)
        {
            gameObject.transform.position = LastMeetingPoint.pointsList[LastMeetingPoint.pointsList.Count - 1];
        }
    }

    void Update()
    {
        //RespondToDebugKeys();
    }

    /* Developer Tools*/
    /*void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) // load next level immediately
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)) // player collision ON/OFF
        {
            collisionDisabled = !collisionDisabled; // toggle(ON/OFF) collision
        }
    }*/

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("Play: Level " + SceneManager.GetActiveScene().buildIndex);
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Arrow":
                break;
            case "MeetingPoint":
                Debug.Log("Landed in Meeting Point");
                break;
            default:
                Debug.Log("Crash");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        Health.health = health - 1;
        isTransitioning = true;
        audioSource.Stop(); // stop all sounds
        audioSource.PlayOneShot(obstacleCrash);
        obstacleCrashParticles.Play();
        GetComponent<Movement>().enabled = false; // unable player movement

        Invoke("ReloadLevel", levelLoadDelay); //reload level after X seconds delay
    }

    void ReloadLevel()
    {
        if (Health.health == 0) // no more lives
        {
            // init meeting points
            LastMeetingPoint.locationChanged = false;
            LastMeetingPoint.pointsList.Clear();

            int currentSceneIndex = 0;
            SceneManager.LoadScene(currentSceneIndex); // load main menu
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex); // reload current level
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop(); // stop all sounds
        audioSource.PlayOneShot(reachFinishPad);
        reachFinishPadParticles.Play();
        GetComponent<Movement>().enabled = false; // unable player movement

        Invoke("NextLevel", levelLoadDelay); //go to start level after X seconds delay
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
            Health.didPlayerWin = true;
        }

        // init meeting points
        LastMeetingPoint.locationChanged = false;
        LastMeetingPoint.pointsList.Clear();

        SceneManager.LoadScene(nextSceneIndex); // reload next level
    }

}
