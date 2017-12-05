using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterAnimatorController : MonoBehaviour
{
    [SerializeField]
    Animator CharacterAnimator;
    [SerializeField]
    Animator GunAnimator;
    Rigidbody2D myBody;
    FloorDetector floorDetector;
    GunController gunController;

    protected void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        floorDetector = GetComponentInChildren<FloorDetector>();
        gunController = GetComponentInChildren<GunController>();
    }

    protected void LateUpdate()
    {
        CharacterAnimator.SetFloat("Speed", myBody.velocity.magnitude);
        CharacterAnimator.SetBool("isTouchingFloor", floorDetector.isTouchingFloor);
        GunAnimator.SetBool("isTriggered", gunController.IsTriggered);
    }
}