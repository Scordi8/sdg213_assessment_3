using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DEBUG: An enumator could be used to sort thru the types of items
[RequireComponent(typeof(Collision))]
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


    /// <summary>
    /// HandlePlayerPickup handles all of the actions after a player has been collided with
    /// </summary>
    /// <param name="player">GameObject to give the item</param> 
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
                //player.GetComponent<CurrentItem>() = ItemType; // you can't set a component that doesn't exist to an integer man
            player.AddComponent<CurrentItem>().setItem(ItemType);
        }
    }
}
