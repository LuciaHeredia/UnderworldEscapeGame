using UnityEngine;

/*
* Script can be attached to any object,
* and it alteres another object by Tag.
*
* Example of object altered:
*    GATE: 
*       - open gate
*       - play sound opening gate
*/
public class MeetingPoint : MonoBehaviour
{
    [SerializeField] GameObject obj1;
    [SerializeField] AudioClip obj1Sound;
    [SerializeField] AudioClip meetingPointSound;

    AudioSource audioSource;

    /* Object(Gate) movement */
    float period; // object speed
    Vector3 startPosition; // object start position
    Vector3 movementVector; // object movement 
    float movementFactor;
    bool openGate = false; // object = gate  

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPosition = obj1.transform.position;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player")) // player reached meeting point
        {
            audioSource.PlayOneShot(meetingPointSound);

            switch (obj1.tag) // object tag
            {
                case "Lvl3Gate1Open":
                    // dissapear arrow
                    audioSource.PlayOneShot(obj1Sound);
                    movementVector = new Vector3(0, 6, 0);
                    period = 2f;
                    openGate = true;
                    gameObject.GetComponent<Collider>().enabled = false;
                    break;
                case "Lvl3Gate2Open":
                    // dissapear arrow
                    audioSource.PlayOneShot(obj1Sound);
                    movementVector = new Vector3(8, 0, 0);
                    period = 2f;
                    openGate = true;
                    gameObject.GetComponent<Collider>().enabled = false;
                    break;
                default:
                    break;
            }
        }
    }

    void Update()
    {
        if (openGate)
        {
            moveGateObstacle();
        }
    }

    void moveGateObstacle()
    {
        switch (obj1.tag) // object tag
        {
            case "Lvl3Gate1Open":
                if (obj1.transform.position.y < (startPosition + movementVector).y)
                {
                    obj1.transform.position += Vector3.up * Time.deltaTime * period;
                }
                else
                {
                    openGate = false;
                }
                break;
            case "Lvl3Gate2Open":
                if (obj1.transform.position.x < (startPosition + movementVector).x)
                {
                    obj1.transform.position += Vector3.right * Time.deltaTime * period;
                }
                else
                {
                    openGate = false;
                }
                break;
            default:
                break;
        }
    }

}
