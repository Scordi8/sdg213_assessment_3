using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionHandler : MonoBehaviour, IUseScanner
{
    CameraMotion motion;

    public bool hasfound;

    bool IUseScanner.hasfound { get; set; }

    private void Start()
    {;
        motion = GetComponent<CameraMotion>();
        if (motion == null) {motion = GetComponentInChildren<CameraMotion>(); }
        
    }
    public void OnTargetFound(GameObject target)
    {
        if (hasfound == false)
        {
            hasfound = true;
            motion.setTracker(target);
        }
    }

    public void OnTargetLost()
    {
        if (hasfound == true)
        {
            hasfound = false;
            motion.setTracker(null);
            Debug.Log("setting tracker to null");
        }
    }
}