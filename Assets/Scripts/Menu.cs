using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{

    [SerializeField] Canvas MainMenu;
    [SerializeField] Canvas Results;
    [SerializeField] TMP_Text resultText;

    string winText = "YOU ESCAPED, CONGRATS.";
    string loseText = "YOU DIED, GOTTA FOCUS NEXT TIME.";

    void Start()
    {
        if (Health.health == 0)
        { // lost
            MainMenu.gameObject.SetActive(false);
            Results.gameObject.SetActive(true);
            resultText.text = loseText;
        }
        else if (Health.health < Health.numOfHearts)
        { // win
            MainMenu.gameObject.SetActive(false);
            Results.gameObject.SetActive(true);
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
