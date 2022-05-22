using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* EngineBase is the component resposible for all direct movement.
 * If *anything* moves independantly, it should be done via EngineBase
 */

[RequireComponent(typeof(Rigidbody))]

public class EngineBase : MonoBehaviour
{
    // Editor exposed fields for controlling constant movement
    [Header("Constant Movement")]
    public bool UseConstantMovement = false;
    [ConditionalHide("UseConstantMovement", true)]
    public Vector3 direction = new Vector3(0, 0, 0);
    [ConditionalHide("UseConstantMovement", true)]
    public float MovementSpeed = 100f;
    [ConditionalHide("UseConstantMovement", true)]
    public bool UseOrientation = true;
    [ConditionalHide("UseConstantMovement", "UseOrientation", true)]
    public Vector3 OrientationOffset = new Vector3(0, 0, 0); // Offset for manual adjustment if the orientation of the object doesn't match directions


    // Editor exposed fields for controlling constant rotation
    [Header("Constant Rotation")]
    public bool UseConstantRotation = false;
    [ConditionalHide("UseConstantRotation", true)]
    public Vector3 RotationAxis = new Vector3(0, 0, 0);
    [ConditionalHide("UseConstantRotation", true)]
    public float RotationSpeed = 100f;

    [Header("Limits")]
    public bool UseLimits = true;
    [ConditionalHide("UseLimits", true)]
    public float PositionalVelocityLimit = 20f;
    [ConditionalHide("UseLimits", true)]
    public float AngularVelocityLimit = 20f;

    [Header("Misc")]
    public bool RotateToVelocity = false;
    [ConditionalHide("RotateToVelocity")]
    public float TurningSpeed = 5f;


    private Rigidbody body;
    private GameObject trackGO;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        if (RotateToVelocity) { trackGO = new GameObject(); }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        bool linear_exceeding = body.velocity.magnitude > PositionalVelocityLimit;
        bool angular_exceeding = body.angularVelocity.magnitude > AngularVelocityLimit;


        if (UseConstantMovement && !linear_exceeding) // Only apply Constant Movement if it's enabled and isn't going too fast
        {
            // dir need to be copied here because we don't want to change the direction variable
            Vector3 dir = direction;
            if (UseOrientation) // If using orientation, rotate the force direction to match the orientation of the object
            { dir = Quaternion.Euler(-transform.localRotation.eulerAngles + OrientationOffset) * dir; }
            Move(dir * MovementSpeed);
        }
        if (UseConstantRotation && !angular_exceeding) // Only apply Constant Rotation if it's enabled and isn't spinning too fast
        {
            Rotate(RotationAxis, RotationSpeed);
        }
        // Supposed to limit the velocity. Is currently untested
        if (linear_exceeding)
        {
            Move((body.angularVelocity.magnitude - PositionalVelocityLimit) * -body.angularVelocity.normalized);
        }

        if (RotateToVelocity && body.velocity.magnitude > 0.1)
        {
            Quaternion dirQ = Quaternion.LookRotation(body.velocity);
            Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, body.velocity.magnitude * TurningSpeed * Time.deltaTime);
            body.MoveRotation(slerp);   
        }
    }

    

    // Public movment and rotation functions. these are the only things that should be called on EngineBase.
    // They are currently defined as functions rather then expression bodies, as the method is expected to change
    public void Move(Vector3 velocity)
    {
        body.AddForce(velocity);
    }
    // Vector2 Overload
    public void Move(Vector2 velocity){Move(new Vector3(velocity.x, 0f, velocity.y));}

    public void Rotate(Vector3 axis, float angle)
    {
        body.AddRelativeTorque(axis * angle);
    }
}
