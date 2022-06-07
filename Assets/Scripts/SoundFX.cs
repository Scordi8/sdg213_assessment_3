using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public AudioListener audioListener;

    public float Cooldown;
    public float Rate;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && Cooldown < 0f)
        {
            // Get random audio clip ID from array
            int index = Random.Range(0, audioClips.Length);
            // Set random audio pitch
            audioSource.pitch = 1f + Random.Range(-0.2f, 0.2f);
            // Set audio in AudioSource component
            audioSource.clip = audioClips[index];
            audioSource.Play();
            Cooldown = Rate;
        }
    }
}
