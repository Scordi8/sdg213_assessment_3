using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //spawnPosition is the position that the player spawns at whenever they respawn
    public Vector3 spawnPosition = new Vector3(0f,0f,0f);
    float tempX = 0;
    float tempY = 0;
    float tempZ = 0;

    /*checkpointTag is set to whatever the project uses to tag checkpoints
    enemyTag is used for when the player interacts with an enemy*/
    public string checkpointTag;
    public string enemyTag;
    public string objectiveTag;

    public WinCondition levelUI;

    //at the start, runs the toRespawn script in case there was a spawning issue
    void Start()
    {
        toRespawn();
    }

    /*When colliding with an object, checks to see what the tag is
    if it is the checkpointTag, sets the spawnPosition to the location of the object it collided with
    if it is enemyTag, calls the toRespawn function*/
    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == checkpointTag)
        {
            spawnPosition = collision.transform.position;
            PlayerPrefs.SetFloat("checkpointX", spawnPosition.x);
            PlayerPrefs.SetFloat("checkpointY", spawnPosition.y);
            PlayerPrefs.SetFloat("checkpointZ", spawnPosition.z);
        }
        if(collision.tag == enemyTag)
        {
            levelUI.toLose();
        }
        if (collision.tag == objectiveTag)
        {
            levelUI.toWin();
        }
    }

    //When called, grabs global checkpoints positions, makes them into the spawn position, then sends itself to the spawnPosition
    public void toRespawn()
    {
        tempX = PlayerPrefs.GetFloat("checkpointX");
        tempY = PlayerPrefs.GetFloat("checkpointY");
        tempZ = PlayerPrefs.GetFloat("checkpointZ");
        spawnPosition = new Vector3 (tempX, tempY, tempZ);
        gameObject.transform.position = spawnPosition;
    }
}
