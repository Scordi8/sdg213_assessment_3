using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]



public class LoopedBGM : MonoBehaviour
{
    [SerializeField]
    private AudioClip songToPlay;
    [SerializeField]
    private float loopSeconds;
    [SerializeField]
    private float masterVolume;
    [SerializeField]
    private string t1;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<AudioSource>();
        this.SetSong(this.songToPlay, this.loopSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetSong(AudioClip song, float loopSeconds)
    {
        if (song != null)
        {
            this.songToPlay = song;
            this.loopSeconds = loopSeconds;
        }
    }
}
