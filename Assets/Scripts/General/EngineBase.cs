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
    [SerializeField]
    [Tooltip("Enables Constant movment on the object")]
#endif
    public bool UseConstantMovement = false;
#if UNITY_EDITOR
    [ConditionalHide("UseConstantMovement", true)]
    [Tooltip("Direction of the constant Movement")]
#endif
    private Vector3 direction = new Vector3(0, 0, 0);
#if UNITY_EDITOR
    [ConditionalHide("UseConstantMovement", true)]
    [Tooltip("Speed of the constant Movement")]
#endif
    public float MovementSpeed = 100f;
#if UNITY_EDITOR
    [ConditionalHide("UseConstantMovement", true)]
    [Tooltip("Whether the velocity is applied relative to object orientation")]
#endif
    private bool UseOrientation = true;
#if UNITY_EDITOR
    [ConditionalHide("UseConstantMovement", "UseOrientation", true)]
    [Tooltip("Offset to the orientation of the object to apply the velocity to")]
#endif
    private Vector3 OrientationOffset = new Vector3(0, 0, 0); // Offset for manual adjustment if the orientation of the object doesn't match directions


    // Editor exposed fields for controlling constant rotation
#if UNITY_EDITOR
    [Header("Constant Rotation")]
    [Tooltip("Enables Constant rotation on the object")]
#endif
    public bool UseConstantRotation = false;
#if UNITY_EDITOR
    [ConditionalHide("UseConstantRotation", true)]
    [Tooltip("Axis of the constant rotation")]
#endif
    private Vector3 RotationAxis = new Vector3(0, 0, 0);
#if UNITY_EDITOR
    [ConditionalHide("UseConstantRotation", true)]
    [Tooltip("Speed of the constant rotation")]
#endif
    public float RotationSpeed = 100f;

#if UNITY_EDITOR
    [Header("Limits")]
    [SerializeField]
    [Tooltip("Enable Limiting")]
#endif
#pragma warning disable CS0414 // Suppress unused field warning - It's used by ConditionalHide
    private bool UseLimits = true;
#pragma warning restore CS0414 // Restore warning
#if UNITY_EDITOR
    [ConditionalHide("UseLimits", true)]
    [Tooltip("Whether to limit the linear velocity")]
#endif
    private float PositionalVelocityLimit = 20f;
#if UNITY_EDITOR
    [ConditionalHide("UseLimits", true)]
    [Tooltip("Whether to limit the angular velocity")]
#endif
    private float AngularVelocityLimit = 20f;

#if UNITY_EDITOR
    [Header("Misc")]
    [SerializeField]
    [Tooltip("Orient the body to Velocity")]
#endif
    private bool RotateToVelocity = false;
#if UNITY_EDITOR
    [ConditionalHide("RotateToVelocity")]
    [Tooltip("Speed to orient at")]
#endif
    private float TurningSpeed = 5f;
#if UNITY_EDITOR
    [ConditionalHide("RotateToVelocity")]
    [Tooltip("Whether to consider the X velocity ")]
#endif
    private bool _X = true;
#if UNITY_EDITOR
    [ConditionalHide("RotateToVelocity")]
    [Tooltip("Whether to consider the Y velocity ")]
#endif
    private bool _Y = false;
#if UNITY_EDITOR
    [ConditionalHide("RotateToVelocity")]
    [Tooltip("Whether to consider the Z velocity ")]
#endif
    private bool _Z = true;
    private Vector3 allowedAxis;

    private Rigidbody body;
    private GameObject trackGO;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        if (RotateToVelocity) { trackGO = new GameObject(); }
        allowedAxis = new Vector3(BoolToInt(_X), BoolToInt(_Y), BoolToInt(_Z));
    }

    private int BoolToInt(bool opt) { if (opt) { return 1; } return 0; }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 v3mul(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
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
            Quaternion dirQ = Quaternion.LookRotation(v3mul(body.velocity, allowedAxis));
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
