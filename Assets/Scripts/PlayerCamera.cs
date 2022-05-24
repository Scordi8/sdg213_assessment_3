using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] GameObject objectToTrack;
    [SerializeField] float trackSpeed = 1f;
    [SerializeField] Vector3 CameraOffset;
    GameObject drag;
    EngineBase drag_EB;
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>().gameObject;
        drag = transform.Find("drag").gameObject;
        drag_EB = transform.Find("drag").gameObject.GetComponent<EngineBase>();

        cam.transform.position = drag.transform.position + CameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(drag.transform.position, objectToTrack.transform.position) > 0)
        {
            Vector3 difference = objectToTrack.transform.position - drag.transform.position;
            drag_EB.Move(difference * trackSpeed);

            cam.transform.position = drag.transform.position + CameraOffset;


            cam.transform.LookAt(objectToTrack.transform.position, Vector3.forward);

        }
    }
}
