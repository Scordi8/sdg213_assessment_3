using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField]
    private bool useDebug = false;

    MeshCollider area;
    private GameObject overlappingbody;


    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<MeshCollider>();
        
    }
    private int loopover(int a, int b, int c)
    {
        while (a < b) { a = c - a; }
        while (a > c) { a = a - c; }    
        return a;
    }

    private void FixedUpdate()
    {
        if (overlappingbody != null)
        {
            if (overlappingbody.GetComponent<IScannable>() != null)
            {
                if (overlappingbody.GetComponent<IScannable>().trace(this.transform.position)) { Debug.Log("Hit"); };
            }
        }
    }

 
    private void OnTriggerEnter(Collider collider)
    {
        if (useDebug) { Debug.Log(this.name + " has begun overlapping with " + collider.name); }
        if (collider.tag == "Player")
        {
            overlappingbody = collider.gameObject;
        } 
    }

    private void OnTriggerExit(Collider collider)
    {
        if (useDebug) { Debug.Log(this.name + " has stopped overlapping with " + collider.name); }
        if (collider.tag == "Player")
        {
            overlappingbody = null;
        }
    }

}
