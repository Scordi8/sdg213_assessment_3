using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScannable
{
    bool trace(Vector3 from);
    bool trace(Vector3 from, int index);
}
