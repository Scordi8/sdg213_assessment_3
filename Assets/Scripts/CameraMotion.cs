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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void trackto(Vector3 pos)
    {
        Transform new_tform = yaw.transform;
        Vector3 target = new Vector3(pos.x, yaw.transform.position.y, pos.z);
        yaw.transform.LookAt(target);
        pitch.transform.LookAt(pos);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canTrack && track != null)
        {
            trackto(track.transform.position);
        }
    }
}
