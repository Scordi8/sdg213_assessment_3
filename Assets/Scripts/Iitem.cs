using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iitem
{
    // Set up interface for the Iitem
    void setItem(int ItemType);
    int getItem();
    bool useItem();
}
