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

    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (UseConstantMovement) // Only apply Constant Movement if it's enabled 
        {
            // dir need to be copied here because we don't want to change the direction variable
            Vector3 dir = direction;
            if (UseOrientation) // If using orientation, rotate the force direction to match the orientation of the object
                { dir = Quaternion.Euler(-transform.localRotation.eulerAngles + OrientationOffset) * dir;} 
            Move(dir * MovementSpeed);
        }
        if (UseConstantRotation) // Only apply Constant Rotation if it's enabled 
        {
            Rotate(RotationAxis, RotationSpeed);
        }
    }

    // Public movment and rotation functions. these are the only things that should be called on EngineBase.
    // They are currently defined as functions rather then expression bodies, as the method is expected to change
    public void Move(Vector3 velocity)
    {
        body.AddForce(velocity);
    }
    public void Rotate(Vector3 axis, float angle)
    {
        body.AddRelativeTorque(axis * angle);
    }
}
