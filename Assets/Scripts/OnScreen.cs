using UnityEngine.SceneManagement;
using UnityEngine;

public class OnScreen : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Debug.Log(" ");
        Health.health = Health.numOfHearts + 1; // Avoid Results Canvas in Main Menu
        SceneManager.LoadScene(0);
    }
}
