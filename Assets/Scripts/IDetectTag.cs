using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IDetectObject defines how to speak to any Detect component
public interface IDetectTag
{
    void DetectTag(string tag);
}