using UnityEngine.SceneManagement;
using UnityEngine;

public class OnScreen : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Debug.Log(" ");

        // Avoid Results Canvas in Main Menu
        Health.health = Health.numOfHearts + 1;

        // init meeting points
        LastMeetingPoint.locationChanged = false;
        LastMeetingPoint.pointsList.Clear();

        SceneManager.LoadScene(0);
    }
}
