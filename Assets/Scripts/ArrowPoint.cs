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
            audioSource.Play();
            DisablePlayerSequence();
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
            gameObject.GetComponent<Collider>().enabled = false; // let player body fall through the arrow

            if (player.GetComponent<Movement>().enabled == false) // player not moving -> crashed
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.Play();

                // save new position of Player 
                LastMeetingPoint.locationChanged = true;
                LastMeetingPoint.pointsList.Add(spacecraftStartPosition);
                player.transform.position = spacecraftStartPosition;
                player.transform.rotation = spacecraftStartRotation;

                DisablePlayerSequence();
            }
        }
    }

    void DisablePlayerSequence()
    {
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Movement>().enabled = false; // unable player movement
        Invoke("EnablePlayerDisableArrowSequence", 0.5f); // do 'FUNCTION' after X seconds delay
    }

    void EnablePlayerDisableArrowSequence()
    {
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Movement>().enabled = true; // enable player movement
        gameObject.SetActive(false); // disable arrow
    }

}
