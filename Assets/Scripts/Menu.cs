using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Load scene
    public void Play()
    {
        Debug.Log(" ");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quit game
    public void Quit()
    {
        //Application.Quit();
        Debug.Log("Quit");
    }

}
