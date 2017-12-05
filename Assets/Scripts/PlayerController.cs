using UnityEngine;

[RequireComponent(typeof(WalkMovement))]
[RequireComponent(typeof(JumpMovement))]
public class PlayerController : MonoBehaviour
{
    WalkMovement walkMovement;
    JumpMovement jumpMovement;

    protected void Awake()
    {
        walkMovement = GetComponent<WalkMovement>();
        jumpMovement = GetComponent<JumpMovement>();
    }

    protected void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpMovement.jumpRequested = true;
        }
    }

    protected void FixedUpdate()
    {
        walkMovement.desiredWalkDirection
          = Input.GetAxis("Horizontal");
    }
}