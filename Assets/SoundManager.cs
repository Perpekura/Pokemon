using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSourceStart;
    public AudioSource audioSource2Loop;
    public AudioClip clipStart,clip2Loop;
    void  Awake()
    {
        audioSourceStart.clip = clipStart;
        audioSource2Loop.clip = clip2Loop;
        audioSourceStart.Play();
        StartCoroutine(NextSounds());
    }

    void  Start()
    {
        
       
    }

    IEnumerator NextSounds() {

        
        yield return new WaitForSeconds(audioSourceStart.clip.length-0.6f);
        audioSource2Loop.Play();
    }
  
}
