using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveLight : MonoBehaviour
{
	// Allow this object to be edited in editor
	[SerializeField] private GameObject Light;
	[SerializeField] private Color NotCloseColour = Color.red;
	[SerializeField] private Color CloseColour = Color.green;
	
	// Allow the object to look for different tags if specified
	[SerializeField] private string tagToDetect = "Player";
	
    void Start()
    {
		// Set the lights start colour to red
		GetComponentInChildren<Light>().color = (NotCloseColour);
    }
	
	private void OnTriggerEnter(Collider collider)
    {
		// Check to see if the collider is colliding with a tagToDetect
        if (collider.gameObject.tag == tagToDetect)
        {
			GetComponentInChildren<Light>().color = (CloseColour);
		}
	}
	
	private void OnTriggerExit(Collider collider)
    {
        // Check to see if the collider is colliding with a tagToDetect
        if (collider.gameObject.tag == tagToDetect)
        {
			GetComponentInChildren<Light>().color = (NotCloseColour);
		}
    }

}