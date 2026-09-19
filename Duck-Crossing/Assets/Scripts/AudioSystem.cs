using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    
    public static void PlaySound(string resourcePath, float pitch = 1.0f)
    {
        AudioClip clip = Resources.Load<AudioClip>(resourcePath);
        if (clip == null) return;

        GameObject obj = new GameObject("Sound: " + resourcePath);
        AudioSource ass = obj.AddComponent<AudioSource>();
        ass.clip = clip;
        ass.pitch = pitch;
        ass.Play();
        Destroy(obj, clip.length / ass.pitch);
        
    }

}
    