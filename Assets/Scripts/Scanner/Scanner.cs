using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scanner : MonoBehaviour
{
#if UNITY_EDITOR 
    [Header("Scanner Settings")]
#endif
    [SerializeField, SerializeReference]
    public Component _master;
    private IUseScanner master;
    [SerializeField]
    [Tooltip("How many hits until the player is seen")]
    public int requiredHits = 100;
    [SerializeField]
    [Tooltip("Hit reduction if player is obscured/hiding")]
    public float relaxspeed = 0.25f;
    [SerializeField][Range(1, 50)]
    public int scansPerFrame = 1;
    [SerializeField]
    private Light spotlight;
    [SerializeField]
    [Tooltip("Scan colour when nothing is seen")]
    public Color calmColour = Color.cyan;
    [SerializeField]
    [Tooltip("Scan colour when player is seen")]
    public Color alertColour = Color.yellow;

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
        master = _master.GetComponent<IUseScanner>();
        if (master == null) { throw new System.Exception("Object " + _master.name + " has no IUseScanner Interface"); }
        if (showLines && useDebug) // If the user wants to show the lines, this add a LineRender child for later use
        {
            line = this.gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
            line.widthMultiplier = 0.05f;
            line.enabled = false;
        }
        GetComponent<MeshRenderer>().enabled = useDebug;
        if (spotlight != null)
        {
            spotlight.color = calmColour;
                }
    }

    /// <summary>
    /// OnPlayerFound is the event to call for when the player has been located successfully
    /// </summary>
    private void OnPlayerFound()
    {
        if (master.hasfound == false)
        {
            master.OnTargetFound(overlappingbody);
        }
    }

    /// <summary>
    /// OnPlayerFound is the event to call for when the player has been lost
    /// </summary>
    private void OnPlayerLost()
    {
        if (master.hasfound == true)
        {
            master.OnTargetLost();
        }
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
                        if (showLines && useDebug)
                        { // Draw lines, set their colour to the serialized hit colour
                            iscan.draw(this.transform.position, line);
                            line.material.color = hit;
                        }

                        if (hits*scansPerFrame >= requiredHits)
                        {
                            OnPlayerFound();
                        }
                        
                    }
                    else
                    {
                        hits -= relaxspeed * scansPerFrame;

                        if (showLines && useDebug)
                        { // Draw lines, set their colour to the serialized fail colour
                            line.material.color = fail;
                        }
                        if (hits >= 0)
                        {
                            OnPlayerLost();
                        }
                    }
                    if (spotlight != null) { spotlight.color = Color.Lerp(calmColour, alertColour, Mathf.Clamp(hits* scansPerFrame / requiredHits, 0, 1)); }
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

            if (line != null) { line.enabled = true; } // Make the line renderable
        } 
    }

    private void OnTriggerExit(Collider collider)
    {
        if (useDebug) { Debug.Log(this.name + " has stopped overlapping with " + collider.name); } // Debug log
        if (collider.gameObject == overlappingbody)
        { // if the exiting 
            overlappingbody = null; // Nullify the overlapping body to prevent the linecasting reaching player after they leave trigger
            hits = 0f; // Reset hits

            if (line != null) { line.enabled = false; } // Make the line unrenderable
            if (spotlight != null) {spotlight.color = calmColour; }

            OnPlayerLost();
        }
    }

}
