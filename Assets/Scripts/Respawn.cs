using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //spawnPosition is the position that the player spawns at whenever they respawn
    public Vector3 spawnPosition = new Vector3(0f,0f,0f);

    /*checkpointTag is set to whatever the project uses to tag checkpoints
    enemyTag is used for when the player interacts with an enemy*/
    public string checkpointTag;
    public string enemyTag;
    public string objectiveTag;

    public WinCondition levelUI;

    /*When colliding with an object, checks to see what the tag is
    if it is the checkpointTag, sets the spawnPosition to the location of the object it collided with
    if it is enemyTag, calls the toRespawn function*/
    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == checkpointTag)
        {
            spawnPosition = collision.transform.position;
        }
        if(collision.tag == enemyTag)
        {
            toRespawn();
        }
        if (collision.tag == objectiveTag)
        {
            levelUI.toWin();
        }
    }

    //When called, sends itself to the spawnPosition
    public void toRespawn()
    {
        gameObject.transform.position = spawnPosition;
    }
}
