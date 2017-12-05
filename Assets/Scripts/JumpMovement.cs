using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(FloorDetector))]
public class JumpMovement : MonoBehaviour
{
    [HideInInspector]
    public bool jumpRequested;

    [SerializeField]
    AudioClip jumpSound;

    [SerializeField]
    float jumpSpeed = 7f;

    Rigidbody2D myBody;

    AudioSource audioSource;

    FloorDetector floorDetector;

    protected void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        floorDetector = GetComponent<FloorDetector>();
        audioSource = GetComponent<AudioSource>();
    }

    protected void FixedUpdate()
    {
        if (jumpRequested && floorDetector.isTouchingFloor)
        {
            myBody.AddForce(
                new Vector2(0, jumpSpeed),
                ForceMode2D.Impulse);

            audioSource.PlayOneShot(jumpSound);
        }

        jumpRequested = false;
    }
}