using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [Header("Scanner Settings")]
    [SerializeField]
    public int requiredHits = 100;
    [SerializeField]
    public float relaxspeed = 0.25f;

    [Header("Debug Options")]
    public bool useDebug = false;
    [ConditionalHide("useDebug", true)]
    public bool showLines = false;
    [ConditionalHide("useDebug", "showLines", true)]
    public Color fail = Color.red;
    [ConditionalHide("useDebug", "showLines", true)]
    public Color hit = Color.green;

    private LineRenderer line;
    private GameObject overlappingbody;
    private int pointcount;
    private float hits = 0f;


    


    private void Start()
    {
        if (showLines)
        {
            line = this.gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
            line.positionCount = 2;
            line.widthMultiplier = 0.05f;
            
        }
    }


    private void FixedUpdate()
    {
        if (overlappingbody != null)
        {
            IScannable iscan = overlappingbody.GetComponent<IScannable>();
            if (iscan != null)
            {
                if (iscan.trace(this.transform.position))
                    {
                    hits++;
                    if (showLines)
                    {
                        iscan.draw(this.transform.position, line);
                        line.material.color = hit;
                    }

                    if (hits >= requiredHits)
                        {
                            Debug.Log("Player Found!!");
                        } 
                    }
                else {
                        hits -= relaxspeed;

                    if (showLines)
                    {
                        line.material.color = fail;
                    }
                }
            }
        }
    }

 
    private void OnTriggerEnter(Collider collider)
    {
        if (useDebug) { Debug.Log(this.name + " has begun overlapping with " + collider.name); }
        if (collider.GetComponent<IScannable>() != null)
        {
            overlappingbody = collider.gameObject;
            pointcount = overlappingbody.GetComponent<IScannable>().pointcount();
            hits = 0f;
        } 
    }

    private void OnTriggerExit(Collider collider)
    {
        if (useDebug) { Debug.Log(this.name + " has stopped overlapping with " + collider.name); }
        if (collider.GetComponent<IScannable>() != null)
        {
            overlappingbody = null;
            hits = 0f;
        }
    }

}
