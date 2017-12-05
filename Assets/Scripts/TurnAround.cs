using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TurnAround : MonoBehaviour
{
    static readonly Quaternion flipRotation =
      Quaternion.Euler(0, 180, 0);

    Rigidbody2D myBody;

    public bool isFacingLeft
    {
        get; private set;
    }

    protected void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        float xVelocity = myBody.velocity.x;

        if (Mathf.Abs(xVelocity) > 0.1)
        {
            bool isTravelingLeft = xVelocity < 0;
            if (isFacingLeft != isTravelingLeft)
            {
                isFacingLeft = isTravelingLeft;
                transform.rotation *= flipRotation;
            }
        }
    }
}