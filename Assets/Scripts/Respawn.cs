using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //spawnPosition is the position that the player spawns at whenever they respawn
    public Vector3 spawnPosition = new Vector3(0f,0f,0f);

    //checkpointTag is set to whatever the project uses to tag checkpoints
    public string checkpointTag;

    //During each update, if the F key is pressed, call toRespawn
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            toRespawn();
        }
    }

    /*When colliding with an object, checks to see if the tag is the same as the CheckpointTag
    if it is the same, sets the spawnPosition to the location of the object it collided with*/
    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == checkpointTag)
        {
            spawnPosition = collision.transform.position;
        }
    }

    //When called, sends itself to the spawnPosition
    void toRespawn()
    {
        gameObject.transform.position = spawnPosition;
    }
}
