using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (audioSource != null && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true; 
        }
    }
}
