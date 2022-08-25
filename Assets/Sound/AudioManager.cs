using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private Sound [] sounds;
    [SerializeField] private Setting setting;
    [SerializeField] private Sound bgm;

    private void Awake()
    {
        if( Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        foreach( Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = setting.Volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
        bgm.source.Play();
    }
    public void SetVolume(float vol)
    {
        foreach(Sound s in sounds)
        {
            s.source.volume = vol;
        }
    }
}
