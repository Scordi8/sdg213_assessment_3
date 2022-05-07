using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        if (UseConstantMovement)
        {
            Vector3 dir = direction;
            if (UseOrientation) {dir = Quaternion.Euler(-transform.localRotation.eulerAngles) * dir;}
            Move(dir * MovementSpeed);
        }
        if (UseConstantRotation)
        {
            Rotate(RotationAxis, RotationSpeed);
        }
    }

    public void Move(Vector3 velocity)
    {
        body.AddForce(velocity);
    }
    public void Rotate(Vector3 axis, float angle)
    {
        body.AddRelativeTorque(axis * angle);
    }
}
