using UnityEngine;

public class ArrowPoint : MonoBehaviour
{
    [SerializeField] GameObject landingPad;

    AudioSource audioSource;
    Vector3 startPosition; // object start position
    GameObject player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPosition = landingPad.transform.position;
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
                player.transform.position = startPosition + new Vector3(0, 1.5f, 0);
                player.transform.rotation = Quaternion.identity;
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
