using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScannable : MonoBehaviour, IScannable
{
    // Start is called before the first frame update
    GameObject[] points;
    int index = 0;
    int cap;


    private GameObject[] GetAllChildren(GameObject go)
    {
        List<GameObject> children = new List<GameObject>();
        Stack<GameObject> pile = new Stack<GameObject>();
        foreach (Transform t in go.transform)
        {
            pile.Push(t.gameObject);
        }
        while (pile.Count > 0)
        {
            GameObject child = pile.Pop();
            foreach (Transform t in child.transform)
            {
                pile.Push(t.gameObject);
            }
            children.Add(child);
        }
        return children.ToArray();
    }

    void Start()
    {
        enabled = false; // we don't need updates
        points = GetAllChildren(this.gameObject);
        cap = points.Length;
        Debug.Log(points.Length);
    }

    public int pointcount() { return points.Length; }

    public bool trace(Vector3 from)
    {
        if (index >= cap-1) { index = 0; }
        else { index++; }
        return trace(from, index);
    }
    public bool trace(Vector3 from, int index)
    {
        return Physics.Linecast(from, points[index].transform.position);
    }

    public void draw(Vector3 from, LineRenderer target)
    {
        draw(from, index, target);
    }

    public void draw(Vector3 from, int index, LineRenderer target)
    {
        target.SetPositions(new Vector3[2] { from, points[index].transform.position });
    }
}
