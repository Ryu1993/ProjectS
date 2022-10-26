using Oculus.Platform.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class SoundManager : Singleton<SoundManager>
{
    private ObjectPool soundBoxPool;

    [SerializeField]
    private SoundBox soundBox;

    private void Awake()
    {
        soundBoxPool = ObjectPoolManager.Instance.PoolRequest(soundBox.gameObject, 10, 5);
    }

    public Transform CreateSoundBox(AudioClip clip,Vector3 position)
    {
        soundBoxPool.Call(position).TryGetComponent(out SoundBox soundBox);
        soundBox.PlayAudioClip(clip);
        return soundBox.transform;
    }
}
