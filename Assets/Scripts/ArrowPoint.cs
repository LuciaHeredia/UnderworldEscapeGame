using UnityEngine;

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
        audioSource = GetComponent<AudioSource>();
        landingPadStartPosition = landingPad.transform.position;
        spacecraftStartPosition = landingPadStartPosition + new Vector3(0, 1.5f, 0);
        spacecraftStartRotation = Quaternion.identity;

        if (IsPointPassed())
        {
            gameObject.SetActive(false);
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
            player = GameObject.FindWithTag("Player");

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
                player.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                Invoke("ArrowSequence", 1f); //disable arrow after X seconds delay
            }
        }
    }

    void ArrowSequence()
    {
        gameObject.SetActive(false);
        player.GetComponent<Rigidbody>().isKinematic = false;
    }

}
