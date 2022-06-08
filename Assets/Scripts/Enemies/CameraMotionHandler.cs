using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionHandler : MonoBehaviour, IUseScanner
{
    CameraMotion motion;

    public bool hasfound;

    bool IUseScanner.hasfound { get => hasfound; set => hasfound = value; }

    private void Start()
    {;
        motion = GetComponent<CameraMotion>();
        if (motion == null) {motion = GetComponentInChildren<CameraMotion>(); }
        
    }
    public void OnTargetFound(GameObject target)
    {
        Debug.Log("Player was found woooo");
        if (hasfound == false)
        {
            hasfound = true;
            motion.setTracker(target);
        }
    }

    public void OnTargetLost()
    {
        Debug.Log("Player Lost Noooo");
        if (hasfound == true)
        {
            hasfound = false;
            motion.setTracker(null);
            Debug.Log("setting tracker to null");
        }
    }
}