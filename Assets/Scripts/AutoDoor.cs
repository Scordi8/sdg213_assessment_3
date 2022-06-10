using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    // Allows the editor to change the variables
    [SerializeField] private string tagToDetect = "Player";
    [SerializeField] private GameObject Door;
    private GameObject overlappingbody;
    [SerializeField] private Vector3 OpenPosition;
    private Vector3 ClosePosition;
    private Vector3 TargetPosition;
    [SerializeField] private float openSpeed;

    private void Start()
    {
        // Set the close position to the default door state
        ClosePosition = Door.transform.position;
        TargetPosition = ClosePosition;
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Check to see if the collider is equal to the overlapping body
        if (collider.gameObject.tag == tagToDetect)
        {
            // If the target tag enters the collider
            enabled = true;
            overlappingbody = collider.gameObject;
            TargetPosition = ClosePosition + OpenPosition;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // Check to see if the collider is equal to the overlapping body
        if (collider.gameObject == overlappingbody)
        {
            // If the target tag exits the collider
            enabled = true;
            overlappingbody = null;
            TargetPosition = ClosePosition;
        }
    }
    
    private void FixedUpdate()
    {
        // Optimise
        if (Vector3.Distance(Door.transform.position, TargetPosition) < 0.1)
        {
            enabled = false;
        }

        // Liner interpolation
        Door.transform.position = Vector3.Lerp(Door.transform.position, TargetPosition, openSpeed);
    }

}
