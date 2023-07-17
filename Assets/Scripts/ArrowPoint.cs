using UnityEngine;

/*
* If Player enters Arrow's collider:
* - play sound of arrow
* - respawn Player above landingPad position(new starting position for Player)
* - save position for later
* - Arrow disable
*/
public class ArrowPoint : MonoBehaviour
{
    [SerializeField] GameObject landingPad;

    AudioSource audioSource;
    Vector3 landingPadStartPosition;
    Vector3 spacecraftStartPosition;
    Quaternion spacecraftStartRotation;
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        landingPadStartPosition = landingPad.transform.position;
        spacecraftStartPosition = landingPadStartPosition + new Vector3(0, 1.5f, 0);
        spacecraftStartRotation = Quaternion.identity;

        if (IsPointPassed())
        {
            DisablePlayerAndArrowSequence();
        }
    }

    bool IsPointPassed()
    {
        if (LastMeetingPoint.locationChanged &&
            LastMeetingPoint.pointsList.Contains(spacecraftStartPosition))
        {
            return true;
        }
        return false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player")) // player reached arrow
        {
            if (player.GetComponent<Movement>().enabled == false) // player not moving -> crashed
            {
                audioSource.Stop();
                gameObject.GetComponent<Collider>().enabled = false; // let player body fall to ground
            }
            else
            {
                audioSource.Play();
                gameObject.GetComponent<Collider>().enabled = false;
                LastMeetingPoint.locationChanged = true;
                LastMeetingPoint.pointsList.Add(spacecraftStartPosition);
                player.transform.position = spacecraftStartPosition;
                player.transform.rotation = spacecraftStartRotation;
                DisablePlayerAndArrowSequence();
            }
        }
    }

    void DisablePlayerAndArrowSequence()
    {
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Movement>().enabled = false; // unable player movement
        gameObject.SetActive(false); // disable arrow
        Invoke("EnablePlayerSequence", 0.5f); //disable arrow after X seconds delay
    }

    void EnablePlayerSequence()
    {
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Movement>().enabled = true; // enable player movement
    }

}
