using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] Canvas MainMenu;
    [SerializeField] Canvas Results;
    [SerializeField] TMP_Text resultText;
    [SerializeField] AudioClip winAudio;
    [SerializeField] AudioClip loseAudio;
    [SerializeField] AudioClip winSpeechAudio;
    [SerializeField] AudioClip loseSpeechAudio;

    AudioSource audioSource;

    string winText = "YOU SUCCEEDED! Very few survived after such a mission. You got what it takes.";
    string loseText = "YOU CRASHED! Focus is everything.";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameResults();
    }

    void GameResults()
    {
        if (Health.health == 0)
        { // lost
            MainMenu.gameObject.SetActive(false);
            Results.gameObject.SetActive(true);
            audioSource.PlayOneShot(loseAudio);
            audioSource.PlayOneShot(loseSpeechAudio);
            resultText.text = loseText;
        }
        else if (Health.health <= Health.numOfHearts)
        { // win
            MainMenu.gameObject.SetActive(false);
            Results.gameObject.SetActive(true);
            audioSource.PlayOneShot(winAudio);
            audioSource.PlayOneShot(winSpeechAudio);
            resultText.text = winText;
        }
        else
        { // main menu
            MainMenu.gameObject.SetActive(true);
            Results.gameObject.SetActive(false);
        }
    }

    // Load scene
    public void Play()
    {
        Debug.Log(" ");
        Health.health = Health.numOfHearts; // full health
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quit game
    public void Quit()
    {
        //Application.Quit();
        Debug.Log("Quit");
    }

}
