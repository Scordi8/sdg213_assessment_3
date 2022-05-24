using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour, IDetectTag
{
    // Allows the editor to change the detected tag
    [SerializeField] private string tagToDetect = "Player";

    // Run the detect tag code when colliding with an object.tag
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToDetect)
        {
            DetectTag(tagToDetect);
        }
    }

    // When called, this will hide the door which will allow the player to walk thru
    public void DetectTag(string tag)
    {
        // Debug
        Debug.Log("poggers");
    }
}
