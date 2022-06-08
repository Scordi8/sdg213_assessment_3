using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScannable
{
    int pointcount();
    bool trace(Vector3 from);
    bool trace(Vector3 from, int index);

    void draw(Vector3 from, LineRenderer target);
    void draw(Vector3 from, int index, LineRenderer target);
}
