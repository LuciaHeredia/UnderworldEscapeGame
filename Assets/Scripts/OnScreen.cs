using UnityEngine.SceneManagement;
using UnityEngine;

public class OnScreen : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Debug.Log(" ");
        Health.health = Health.numOfHearts; // full health
        SceneManager.LoadScene(0);
    }
}
