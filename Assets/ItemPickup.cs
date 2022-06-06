using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DEBUG: An enumator could be used to sort thru the types of items
public class ItemPickup : MonoBehaviour
{
    // Allow the item to be changed by the level designer
    [SerializeField] private int ItemType = 0;

    // Try to pickup item when colliding with it
    void OnTriggerEnter(Collider2D pickupRadius)
    {
        if (pickupRadius.gameObject.tag == "Player")
        {
            GameObject player = pickupRadius.gameObject;
            PickupItem(player);
        }
    }

    // HandlePlayerPickup handles all of the actions after a player has been collided with
    private void PickupItem(GameObject player)
    {
        // Get the playerItem 
        if (player.GetComponent<CurrentItem>() != null)
        {
            // Player cannot have multiple items
            Debug.Log("Player already has an item");
            return;
        }
        else
        {
            // Set the players item to the item type
            player.GetComponent<CurrentItem>() = ItemType;
        }
    }
}
