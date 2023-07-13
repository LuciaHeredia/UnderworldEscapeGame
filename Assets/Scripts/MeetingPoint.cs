using UnityEngine;

public class MeetingPoint : MonoBehaviour
{
    [SerializeField] GameObject obj1;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            switch (obj1.tag)
            {
                case "Lvl3Gate1Up":
                    // dissapear arrow
                    moveGateObstacle(obj1, obj1.transform.position, new Vector3(0, 8, 0), 3f);
                    break;
                case "Lvl3Gate2Down":
                    // dissapear arrow
                    moveGateObstacle(obj1, obj1.transform.position, new Vector3(0, -5, 0), 4f);
                    break;
                default:
                    break;
            }
        }
    }

    /** 
    * gate - gate reference
    * startPosition
    * movementVector
    * period - obstacle speed
    **/
    void moveGateObstacle(GameObject gate, Vector3 startPosition, Vector3 movementVector, float period)
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        float movementFactor = (rawSinWave + 1f) / 2f; // from: -1 to 1 --> to: 0 to 2

        Vector3 offset = movementVector * movementFactor;
        gate.transform.position = startPosition + offset;
    }


}
