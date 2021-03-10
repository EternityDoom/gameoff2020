using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> songs;
    int index;
    public bool canPlay;

    /// <summary>
    /// Updates the status of the audio source based on whether the audio can play
    /// </summary>
    private void Update() {
        if(!audioSource.isPlaying && canPlay){
            audioSource.PlayOneShot(songs[index]);
            index ++;
            if(index == songs.Count){
                index = 0;
            }
        }
    }

    /// <summary>
    /// Updates the volume of the audio system
    /// </summary>
    /// <param name="amount">The amount to update the audio system by</param>
    public void SetVolume(float amount){
        audioSource.volume = amount;
        AudioDontDestroy.I.source.volume = amount;
    }
}
