using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    [Header("Motion options")]
    [SerializeField]
    [Tooltip("Is the camera stationry")]
    public bool _static = false;
    [SerializeField]
    [Tooltip("Can the Camera track an object")]
    public bool canTrack = true;

    [ConditionalHide("_static", true, true)]
    [Tooltip("Maximum angle to oscillate between")]
    public float _turnAngle = 30f;
    [ConditionalHide("_static", true, true)]
    [Tooltip("Oscillation turning speed")]
    public float _turnspeed = 1f;
    [ConditionalHide("_static", true, true)]
    [Tooltip("Pitch offset for oscillation")]
    public float PitchOffset = 0f;

    [ConditionalHide("canTrack", true)]
    [Tooltip("The GameObject to track")]
    public GameObject track;
    [ConditionalHide("canTrack", true)]
    [Tooltip("Maximum tracking angle")]
    public float maxAngle = 50;


    [Header("Bones")]
    [SerializeField]
    [Tooltip("GameObject/Bone controlling the Yaw")]
    public GameObject yaw;
    [SerializeField]
    [Tooltip("GameObject/Bone controlling the Pitch")]
    public GameObject pitch;


    private int turndir = 1;

    private Vector3[] bounds;

    // Start is called before the first frame update
    void Start()
    {
        bounds = new Vector3[] {new Vector3(0, yaw.transform.eulerAngles.y -_turnAngle, 0), new Vector3(0, yaw.transform.eulerAngles.y + _turnAngle, 0) };
    }
    
    void trackto(Vector3 pos)
    {
        Transform new_tform = yaw.transform;
        Vector3 target = new Vector3(pos.x, Mathf.Clamp(yaw.transform.position.y, -maxAngle, maxAngle), pos.z);
        yaw.transform.LookAt(target);
        pitch.transform.LookAt(pos);
    }

#nullable enable
    public void setTracker(GameObject? target)
#nullable restore
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
                pitch.transform.eulerAngles = new Vector3(PitchOffset, pitch.transform.eulerAngles.y, pitch.transform.eulerAngles.z);
            }
        }
    }
}
