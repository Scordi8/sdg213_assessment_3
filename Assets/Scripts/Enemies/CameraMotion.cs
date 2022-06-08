using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    [Header("Motion options")]
    [SerializeField]
    public bool _static = false;
    [SerializeField]
    public bool canTrack = true;

    [ConditionalHide("_static", true, true)]
    public float _turnAngle = 30f;
    [ConditionalHide("_static", true, true)]
    public float _turnspeed = 1f;

    [ConditionalHide("canTrack", true)]
    public GameObject track;
    [ConditionalHide("canTrack", true)]
    public float maxAngle = 50;


    [Header("Bones")]
    public GameObject yaw;
    public GameObject pitch;

    private int turndir = 1;

    private Vector3[] bounds;

    // Start is called before the first frame update
    void Start()
    {
        
        bounds = new Vector3[] {new Vector3(0, -_turnAngle, 0), new Vector3(0, _turnAngle, 0) };
    }

    void trackto(Vector3 pos)
    {
        Transform new_tform = yaw.transform;
        Vector3 target = new Vector3(pos.x, yaw.transform.position.y, pos.z);
        yaw.transform.LookAt(target);
        pitch.transform.LookAt(pos);
        
    }

    public void setTracker(GameObject? target)
    {
        if (target != null)
        {
            track = target;
            canTrack = true;
        }
        else
        {
            track = null;
            canTrack = false;
        }
    }


    // Fixed update responsible for checking whether to track an object or to oscillate
    void FixedUpdate()
    {
        if (canTrack && track != null)
        {
            trackto(track.transform.position);
        }
        else
        {
            if (!_static)
            {
                float angle = yaw.transform.eulerAngles.y;
                if (angle > 180) { angle = angle - 360; }

                if (angle > bounds[1].y) { turndir = -1; }
                if (angle < bounds[0].y) { turndir = 1; }
                yaw.transform.eulerAngles = new Vector3(0, Mathf.Clamp(angle, -_turnAngle, _turnAngle), 0);

                yaw.transform.eulerAngles = Vector3.Lerp(yaw.transform.eulerAngles, new Vector3(yaw.transform.eulerAngles.x, yaw.transform.eulerAngles.y + (_turnspeed*turndir), yaw.transform.eulerAngles.z), 0.5f);
            }
        }
    }
}
