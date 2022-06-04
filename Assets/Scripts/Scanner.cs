using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
#if UNITY_EDITOR 
    [Header("Scanner Settings")]
#endif
    [SerializeField]
    public int requiredHits = 100;
    [SerializeField]
    public float relaxspeed = 0.25f;
    [SerializeField][Range(1, 50)]
    public int scansPerFrame = 1;

#if UNITY_EDITOR
    [Header("Debug Options")]
#endif
    public bool useDebug = false;
#if UNITY_EDITOR
    [ConditionalHide("useDebug", true)]
#endif
    public bool showLines = false;
#if UNITY_EDITOR
    [ConditionalHide("useDebug", "showLines", true)]
#endif
    public Color fail = Color.red;
#if UNITY_EDITOR
    [ConditionalHide("useDebug", "showLines", true)]
#endif
    public Color hit = Color.green;

    private LineRenderer line;
    private GameObject overlappingbody;
    private float hits = 0f;


    private void Start()
    {
        if (showLines) // If the user wants to show the lines, this add a LineRender child for later use
        {
            line = this.gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
            line.widthMultiplier = 0.05f;
        }
    }

    /// <summary>
    /// OnPlayerFound is the event to call for when the player has been located successfully
    /// </summary>
    private void OnPlayerFound()
    {
        Debug.Log("Player Found!!");
    }

    /// <summary>
    /// Update is the per frame function
    /// Scanner's update function will call the overlapping body's IScannable's draw method and linecast. if enough casts are hit over time, the OnPlayerFound method is called on self
    /// </summary>
    private void Update()
    {
        if (overlappingbody != null)
        {
            IScannable iscan = overlappingbody.GetComponent<IScannable>(); // get the Iscan Interface. 
            if (iscan != null)
            {
                for (int i = 0; i < scansPerFrame; i++)
                {
                    if (iscan.trace(this.transform.position)) // if the scanner can draw an uninturrupted line from itself to a bone of the player
                    {
                        hits++;
                        if (showLines)
                        { // Draw lines, set their colour to the serialized hit colour
                            iscan.draw(this.transform.position, line);
                            line.material.color = hit;
                        }

                        if (hits >= requiredHits)
                        {
                            OnPlayerFound();
                        }
                    }
                    else
                    {
                        hits -= relaxspeed;

                        if (showLines)
                        { // Draw lines, set their colour to the serialized fail colour
                            line.material.color = fail;
                        }
                    }
                }
            }
        }
    }

 
    private void OnTriggerEnter(Collider collider)
    {
        if (useDebug) { Debug.Log(this.name + " has begun overlapping with " + collider.name); } // Debug log
        if (collider.GetComponent<IScannable>() != null)
        { // Only if the collider is scannable, set the overlapping body to the collider's game object, and reset the hits
            overlappingbody = collider.gameObject;
            hits = 0f; // Reset hits

            if (line != null) { line.positionCount = 2; } // Make the line renderable
        } 
    }

    private void OnTriggerExit(Collider collider)
    {
        if (useDebug) { Debug.Log(this.name + " has stopped overlapping with " + collider.name); } // Debug log
        if (collider.gameObject == overlappingbody)
        { // if the exiting 
            overlappingbody = null; // Nullify the overlapping body to prevent the linecasting reaching player after they leave trigger
            hits = 0f; // Reset hits

            if (line != null) { line.positionCount = 0; } // Make the line unrenderable
        }
    }

}
