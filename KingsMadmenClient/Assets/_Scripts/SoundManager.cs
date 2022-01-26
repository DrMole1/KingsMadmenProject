using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playAudioClip(int index)
    {
        if (clips[index] != null)
        {
            audioSource.pitch = 1;
            audioSource.PlayOneShot(clips[index]);
        }
        else
            Debug.Log("Attention, clip vide.");
    }

    public void playAudioClipWithPitch(int index, float pitch)
    {
        if (clips[index] != null)
        {
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clips[index]);
        }
        else
            Debug.Log("Attention, clip vide.");
    }

    public void playAudioClipWithStop(int index)
    {
        audioSource.Stop();

        if (clips[index] != null)
        {
            audioSource.pitch = 1;
            audioSource.PlayOneShot(clips[index]);
        }
        else
            Debug.Log("Attention, clip vide.");
    }

    public void playAudioClipJump(int index)
    {
        if (clips[index] != null)
        {
            audioSource.PlayOneShot(clips[index]);
        }
        else
            Debug.Log("Attention, clip vide.");
    }
}
