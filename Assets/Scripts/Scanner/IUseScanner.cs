using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseScanner
{
    bool hasfound { get; set; }  
    void OnTargetFound(GameObject target);
    void OnTargetLost();
}
