using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BezierCurve))]
public class FollowCurve : MonoBehaviour
{
    [SerializeField] float MovementSpeedMultiplier = 1f;

    private EngineBase engine;
    private Rigidbody body;
    private BezierCurve curve;
    private int index = 1;
    BezierPoint[] points;
    // Start is called before the first frame update
    void Start()
    {
        engine = GetComponentInChildren<EngineBase>();
        body = engine.GetComponent<Rigidbody>();
        curve = GetComponent<BezierCurve>();

        points = curve.GetAnchorPoints();
        
    }

    

    // Update is called once per frame
    void Update()
    {
        Vector2 bpos = new Vector2(body.transform.position.x, body.transform.position.z);
        Vector2 ppos = new Vector2 (points[index].position.x, points[index].position.z);
        float dist = Vector2.Distance(bpos, ppos);
        //Debug.Log(dist);
        if (dist < 0.5)
        {
            index++;
            if (index >= points.Length) { index = 0; }
        }
        Vector2 diff = (ppos - bpos).normalized;
        engine.Move(diff * MovementSpeedMultiplier);
    }
}
