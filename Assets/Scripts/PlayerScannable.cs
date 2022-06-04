using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScannable : MonoBehaviour, IScannable
{
    // Start is called before the first frame update
    GameObject[] points;
    int index = 0;
    int cap;


    void Start()
    {
        enabled = false; // we don't need updates
        points = new GameObject[transform.childCount];
        for (int i = 0; i < points.Length; i++)
            points[i] = transform.GetChild(i).gameObject;
    }

    public bool trace(Vector3 from)
    {
        if (index >= cap) { index = 0; }
        else { index++; }
        return trace(from, index);
    }
    public bool trace(Vector3 from, int index)
    {
        return Physics.Linecast(from, points[index].transform.position);
    }
}
