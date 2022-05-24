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
#if UNITY_EDITOR 
    [Header("Constant Movement")]
#endif
    public bool UseConstantMovement = false;
#if UNITY_EDITOR
    [ConditionalHide("UseConstantMovement", true)]
#endif
    public Vector3 direction = new Vector3(0, 0, 0);
#if UNITY_EDITOR
    [ConditionalHide("UseConstantMovement", true)]
#endif
    public float MovementSpeed = 100f;
#if UNITY_EDITOR
    [ConditionalHide("UseConstantMovement", true)]
#endif
    public bool UseOrientation = true;
#if UNITY_EDITOR
    [ConditionalHide("UseConstantMovement", "UseOrientation", true)]
#endif
    public Vector3 OrientationOffset = new Vector3(0, 0, 0); // Offset for manual adjustment if the orientation of the object doesn't match directions


    // Editor exposed fields for controlling constant rotation
#if UNITY_EDITOR
    [Header("Constant Rotation")]
#endif
    public bool UseConstantRotation = false;
#if UNITY_EDITOR
    [ConditionalHide("UseConstantRotation", true)]
#endif
    public Vector3 RotationAxis = new Vector3(0, 0, 0);
#if UNITY_EDITOR
    [ConditionalHide("UseConstantRotation", true)]
#endif
    public float RotationSpeed = 100f;

#if UNITY_EDITOR
    [Header("Limits")]
#endif
    public bool UseLimits = true;
#if UNITY_EDITOR
    [ConditionalHide("UseLimits", true)]
#endif
    public float PositionalVelocityLimit = 20f;
#if UNITY_EDITOR
    [ConditionalHide("UseLimits", true)]
#endif
    public float AngularVelocityLimit = 20f;

#if UNITY_EDITOR
    [Header("Misc")]
#endif
    public bool RotateToVelocity = false;
#if UNITY_EDITOR
    [ConditionalHide("RotateToVelocity")]
#endif
    public float TurningSpeed = 5f;
#if UNITY_EDITOR
    [ConditionalHide("RotateToVelocity")]
#endif
    public bool _X = true;
#if UNITY_EDITOR
    [ConditionalHide("RotateToVelocity")]
#endif
    public bool _Y = false;
#if UNITY_EDITOR
    [ConditionalHide("RotateToVelocity")]
#endif
    public bool _Z = true;
    private Vector3 allowedAxis;

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
            Quaternion dirQ = Quaternion.LookRotation(body.velocity * );
            Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, body.velocity.magnitude * TurningSpeed * Time.deltaTime);
            body.MoveRotation(slerp);   
        }
    }

    

    // Public movment and rotation functions. these are the only things that should be called on EngineBase.
    // They are currently defined as functions rather then expression bodies, as the method is expected to change
    public void Move(Vector3 velocity)
    {
        bool moving;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        if (body.velocity.magnitude > PositionalVelocityLimit)
        {

        }
        else
        {
            body.AddForce(velocity);
        }
        if (moving == false)
        {
            //body.velocity = body.velocity / 2;
            //body.velocity = Vector3.zero;
        }
    }
    // Vector2 Overload
    public void Move(Vector2 velocity){Move(new Vector3(velocity.x, 0f, velocity.y));}

    public void Rotate(Vector3 axis, float angle)
    {
        body.AddRelativeTorque(axis * angle);
    }
}
