using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iitem
{
    void setItem(int ItemType);
    int getItem();
    bool useItem();
}
