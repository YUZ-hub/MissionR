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
            Register(sound);
        }
        bgm.Play();
    }
    public void Register(Sound sound)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = sound.Clip;
        source.volume = sound.Volume*setting.Volume;
        source.pitch = sound.Pitch;
        source.loop = sound.Loop;
        sound.SetSource(source);
    }
    public void SetVolume(float vol)
    {
        foreach(Sound s in sounds)
        {
            s.SetVolume(vol);
        }
    }
}
