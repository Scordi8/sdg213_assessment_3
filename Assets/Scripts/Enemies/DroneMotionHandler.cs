using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMotionHandler : MonoBehaviour, IUseScanner
{
    FollowCurve track_curve;
    FollowPlayer track_player;

    public bool hasfound;

    bool IUseScanner.hasfound { get; set; }

    private void Start()
    {
        track_curve = GetComponent<FollowCurve>();
        track_player = GetComponent<FollowPlayer>();
    }
    public void OnTargetFound(GameObject target)
    {
        if (hasfound == false)
        {
            hasfound = true;
            track_curve.enabled = false;
            track_player.enabled = true;
            track_player.SetTarget(target);
        }
    }

    public void OnTargetLost()
    {
        if (hasfound == true)
        {
            hasfound= false;
            track_player.EndTarget(this);
        }
    }

    public void ResumeCurve()
    {
        track_curve.enabled = true;
        track_player.enabled = false;
    }
}
