using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip obstacleCrash;
    [SerializeField] AudioClip reachFinishPad;

    [SerializeField] ParticleSystem obstacleCrashParticles;
    [SerializeField] ParticleSystem reachFinishPadParticles;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) // load next level immediately
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)) // player collision ON/OFF
        {
            collisionDisabled = !collisionDisabled; // toggle(ON/OFF) collision
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("Go");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop(); // stop all sounds
        audioSource.PlayOneShot(obstacleCrash);
        obstacleCrashParticles.Play();
        GetComponent<Movement>().enabled = false; // unable player movement
        Invoke("ReloadLevel", levelLoadDelay); //reload level after X seconds delay
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // reload current level
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
        }

        SceneManager.LoadScene(nextSceneIndex); // reload next level
    }
}
