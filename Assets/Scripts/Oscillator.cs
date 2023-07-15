using UnityEngine;

public class Oscillator : MonoBehaviour
{
    float period; // obstacle speed
    Vector3 startPosition;
    Vector3 movementVector;
    float movementFactor;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {

        switch (gameObject.tag)
        {
            case "Lvl2Obst1Up":
                movementVector = new Vector3(0, 8, 0);
                period = 3f;
                break;
            case "Lvl2Obst1Down":
                movementVector = new Vector3(0, -5, 0);
                period = 4f;
                break;
            case "Lvl2Obst2Up":
                movementVector = new Vector3(0, 5, 0);
                period = 4f;
                break;
            case "Lvl2Obst2Down":
                movementVector = new Vector3(0, -8, 0);
                period = 2f;
                break;
            case "Lvl3Obst1Down":
                movementVector = new Vector3(0, -5, 0);
                period = 5f;
                break;
            case "Lvl3Obst1Up":
                movementVector = new Vector3(0, 6, 0);
                period = 4.5f;
                break;
            case "Lvl3Obst2Up":
                movementVector = new Vector3(0, 7, 0);
                period = 2.5f;
                break;
            case "Arrow":
                movementVector = new Vector3(0, 0.5f, 0);
                period = 3f;
                break;
            default: // no tag obstacle(horizontal)
                movementVector = new Vector3(8, 0, 0);
                period = 3f;
                break;
        }
        movingObstacle();
    }

    void movingObstacle()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f; // from: -1 to 1 --> to: 0 to 2

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }

}
