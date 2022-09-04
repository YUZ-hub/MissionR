using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Setting",menuName ="Setting")]
public class Setting : ScriptableObject
{
    [Range(0f,1f)]
    [SerializeField] private float volume;
    public float Volume { get { return volume; } private set { volume = value; } }

    public void SetVolume(float vol)
    {
        volume = vol;
        AudioManager.Instance.SetVolume(vol);
    }
}
