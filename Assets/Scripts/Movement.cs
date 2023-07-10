using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainPush = 1000;
    [SerializeField] float rotationPush = 100;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftPushParticles;
    [SerializeField] ParticleSystem rightPushParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPush();
        ProcessRotation();
    }

    void ProcessPush()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartPushing();
        }
        else
        {
            StopPushing();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartPushing()
    {
        // To make the force(push) applied frame rate independent, need to mult by: Time.deltaTime
        rb.AddRelativeForce(Vector3.up * mainPush * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopPushing()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationPush);
        if (!rightPushParticles.isPlaying)
        {
            rightPushParticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationPush);
        if (!leftPushParticles.isPlaying)
        {
            leftPushParticles.Play();
        }
    }

    private void StopRotating()
    {
        rightPushParticles.Stop();
        leftPushParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // to manually rotate: freeze rotation constraints of rigidbody
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation: physics system takes over
    }

}
