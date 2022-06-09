using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveLight : MonoBehaviour
{
	// Allow this object to be edited in editor
	[SerializeField] private GameObject Light;
	
	// Allow the object to look for different tags if specified
	[SerializeField] private string tagToDetect = "Player";
	
    void Start()
    {
		// Set the lights start colour to red
		GetComponentInChildren<Light>().color = (Color.red);
    }
	
	private void OnTriggerEnter(Collider collider)
    {
		// Check to see if the collider is colliding with a tagToDetect
        if (collider.gameObject.tag == tagToDetect)
        {
			GetComponentInChildren<Light>().color = (Color.green);
		}
	}
	
	private void OnTriggerExit(Collider collider)
    {
        // Check to see if the collider is colliding with a tagToDetect
        if (collider.gameObject.tag == tagToDetect)
        {
			GetComponentInChildren<Light>().color = (Color.red);
		}
    }

}