using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField]
    private bool useDebug = false;

    MeshCollider area;
    private GameObject overlappingbody;

    private int index = 0;
    private int ccount = 0;
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
            int connected = 0;
            for (int i = 0; i < Mathf.Clamp(ccount, 0, 10); i++)
            {
                
                Vector3 to = overlappingbody.transform.GetChild(index).gameObject.transform.position;
                Vector3 from = this.transform.position;
                if (!Physics.Linecast(from, to))
                {
                    connected++;
                }
                index = loopover(index + 1, 0, ccount);
            }
            Debug.Log(connected);
        }
    }

 
    private void OnTriggerEnter(Collider collider)
    {
        if (useDebug) { Debug.Log(this.name + " has begun overlapping with " + collider.name); }
        if (collider.tag == "Player")
        {
            index = 0;
            ccount = collider.gameObject.transform.childCount-1;
            Debug.Log("count = " + ccount.ToString());
            overlappingbody = collider.gameObject;
        } 
    }

    private void OnTriggerExit(Collider collider)
    {
        if (useDebug) { Debug.Log(this.name + " has stopped overlapping with " + collider.name); }
        if (collider.tag == "Player")
        {
            index = 0;
            ccount = 0;
            overlappingbody = null;
        }
    }

}
