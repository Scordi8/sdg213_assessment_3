using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseScanner
{
    void OnTargetFound(GameObject target);
    void OnTargetLost();
}
