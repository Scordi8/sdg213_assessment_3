using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    public bool active { get { return active; } set { active = value; this.enabled = active; } }

    private EngineBase engine;
    private Rigidbody body;

    private GameObject target;
    private Stack<Vector3> trackpoints;

    private Vector3 lastpos;

    private int mode = 1; // 1 is track, 0 is retreat

    // Start is called before the first frame update
    void Start()
    {
        engine = GetComponentInChildren<EngineBase>();
        body = engine.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 1) // If tracking
        {
            if (!Physics.Linecast(trackpoints.Peek(), body.transform.position))
            {
                trackpoints.Push(body.transform.position);
            }
        }
        Vector3 dest;
        switch (mode)
            {
            case 0: dest = trackpoints.Peek(); break;
            case 1: dest = target.transform.position; break;
            default: dest = body.transform.position; break;
        }
        Vector2 bpos = new Vector2(body.transform.position.x, body.transform.position.z);
        Vector2 ppos = new Vector2(dest.x, dest.z);
        float dist = Vector2.Distance(bpos, ppos);
        //Debug.Log(dist);
        if (dist < 0.1 && mode == 0)
        {
            trackpoints.Pop();
        }
        Vector2 diff = (ppos - bpos).normalized;
        engine.Move(diff);
    }
}
