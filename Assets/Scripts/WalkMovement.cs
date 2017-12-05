using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkMovement : MonoBehaviour
{
    public float desiredWalkDirection;

    [SerializeField]
    float walkSpeed = 100;

    Rigidbody2D myBody;

    protected void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        float desiredXVelocity
          = desiredWalkDirection
            * walkSpeed
            * Time.fixedDeltaTime;

        myBody.velocity = new Vector2(
          desiredXVelocity,
          myBody.velocity.y);
    }
}