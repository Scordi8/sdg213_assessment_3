using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentItem : MonoBehaviour, Iitem
{
    private int ?ItemType = null;

    /// <summary>
    /// Sets the Item type to the provided int
    /// </summary>
    /// <param name="_ItemType"></param>
    public void setItem(int _ItemType)
    {
        // this is where you'd do things -_- I'm watching you swhal
        ItemType = _ItemType;
    }

    /// <summary>
    /// Returns the Item type of the component
    /// </summary>
    /// <returns>int</returns>
    public int getItem()
    {
        return (int) ItemType;
    }

    /// <summary>
    /// Use the current item. returns: true, unless no item type is defined, then false
    /// </summary>
    /// <returns>bool</returns>
    public bool useItem()
    {
        if (ItemType == null) { return false; }
        return true;
        // Code for when item is used goes here :catyes:
    }
}
