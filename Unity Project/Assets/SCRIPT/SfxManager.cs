using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    //The audio source component that the sound will play through
    public AudioSource source;

    //The guarenteed audio source that the sound will play through
    public AudioSource sourceFaithfull;

    //A list of audio files, each file within this list must correlate with the same index on the SFX enum
    public List<AudioClip> clips;

    /// <summary>
    /// Plays a specific audio file
    /// </summary>
    /// <param name="sfx">The audio file that should be played (enum)</param>
    public void Play(SFX sfx){
        Play(sfx, 1f);
    }

    /// <summary>
    /// Plays a specific audio file but with a modified pitch
    /// </summary>
    /// <param name="sfx">The audio file that should be played (enum)</param>
    /// <param name="pitch">The amount the original audio files pitch will be changed</param>
    public void Play(SFX sfx, float pitch){
        source.pitch = pitch;
        source.PlayOneShot(clips[(int)sfx]);
    }

    /// <summary>
    /// Plays a specific audio file by passing in its specific audio clip
    /// </summary>
    /// <param name="clip">The audio clip that should be played</param>
    public void Play(AudioClip clip){
        source.pitch = 1;
        source.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays audio faithfully, preventing it from being stopped
    /// </summary>
    /// <param name="sfx">The audio file that should be played (enum)</param>
    public void PlayFaithfull(SFX sfx){
        sourceFaithfull.PlayOneShot(clips[(int)sfx]);
    }

    /// <summary>
    /// Modifies the volume of the audio source
    /// </summary>
    /// <param name="amount">The volume you wish to set the audio source to</param>
    public void SetVolume(float amount){
        source.volume = amount;
        sourceFaithfull.volume = amount;
        if(!source.isPlaying)
        Play(SFX.Talk);
    }
}

/// <summary>
/// The enum value that holds all of the names of the specific audio files that will be playable within the games code
/// </summary>
public enum SFX{
    Alert,
    Pause,
    Play,
    PlayFast,
    Bip,
    Bop,
    Tic,
    Tuc,
    Build,
    Talk,
    ShieldUp,
    SuperNova,
    PlaySuperFast
}
