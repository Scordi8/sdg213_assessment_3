using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Sound Effect Settings")]
#endif
    [SerializeField]
    [Tooltip("Audio Clips to use")]
    private AudioClip[] audioClips;
    [SerializeField]
    [Tooltip("Audio Source component")]
    private AudioSource audioSource;
    [SerializeField]
    [Tooltip("Audio Listener component")]
    private AudioListener audioListener;

    [SerializeField]
    [Tooltip("How far can the pitch be altered")]
    private float Volume = 1f;
    [SerializeField]
    [Tooltip("How far can the pitch be altered")]
    private float pitchVarience = 0.2f;
    [SerializeField]
    [Tooltip("Time between each sound effect")]
    private float Rate;

#if UNITY_EDITOR
    [Header("Debug Options")]
#endif
#pragma warning disable CS0414 // Suppress Unused warning
    private bool useDebug = false;
#pragma warning restore CS0414 // Revoke Suppression
#if UNITY_EDITOR
    [ConditionalHide("useDebug", true)]
#endif
    [SerializeField]
    [Tooltip("Editor visible cooldown number")]
    private float Cooldown;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = Volume;
    }

    void Update()
    {
        Cooldown -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && Cooldown < 0f)
        {
            // Get random audio clip ID from array
            int index = Random.Range(0, audioClips.Length);
            // Set random audio pitch
            audioSource.pitch = 1f + Random.Range(-pitchVarience, pitchVarience);
            // Set audio in AudioSource component
            audioSource.clip = audioClips[index];
            audioSource.Play();
            Cooldown = Rate;
        }
    }
}
