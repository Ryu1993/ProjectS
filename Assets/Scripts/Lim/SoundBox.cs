using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour,IPoolingable
{
    private AudioSource audioSource;
    public ObjectPool home { get; set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        CheckState();
    }
    public void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void StopAudioClip()
    {
        audioSource.Stop();
    }
    private void CheckState()
    {
        if (audioSource.isPlaying)
            return;
        home.Return(this.gameObject);
    }
}
