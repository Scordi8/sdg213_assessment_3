using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentItem : MonoBehaviour, Iitem
{
    // Set the item type to the default of null
    private int ?ItemType = null;

    // Call this when you set an Item
    public void setItem(int _ItemType)
    {
        // Set the item type
        ItemType = _ItemType;
    }

    // Call this when you get an Item
    public int getItem()
    {
        return (int) ItemType;
    }

    // Call this when you use an Item
    public bool useItem()
    {
        // If their is no item type, return false
        if (ItemType == null) { return false; }
        
        // Return
        return true;
    }
}
