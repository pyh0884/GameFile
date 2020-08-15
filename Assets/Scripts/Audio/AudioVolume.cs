using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public float vol;
    public void setVolume(float volume)
    {
        mixer.SetFloat("Volume", volume);
        mixer.GetFloat("Volume", out vol);
    }
    public void VolumeUp()
    {
        mixer.GetFloat("Volume", out vol);
        vol += 10;
        mixer.SetFloat("Volume", vol);
    }
    public void VolumeDown()
    {
        mixer.GetFloat("Volume", out vol);
        vol -= 10;
        mixer.SetFloat("Volume", vol);
    }

}
