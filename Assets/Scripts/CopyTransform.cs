using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    // I can't believe I need to make this script.

    [SerializeField]
    GameObject source;

    [SerializeField] Vector3 PositionOffset;
    [SerializeField] Vector3 RotationOffset;

    private void FixedUpdate()
    {
        transform.position = source.transform.position + PositionOffset;
        transform.eulerAngles = source.transform.eulerAngles + RotationOffset;
    }
}
