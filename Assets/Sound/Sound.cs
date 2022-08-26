using UnityEngine;

[CreateAssetMenu(fileName ="New Sound",menuName ="Sound")]
public class Sound : ScriptableObject
{
    [SerializeField] private AudioClip clip;
    [Range(0f, 1f)] 
    [SerializeField] private float volume = 1f;
    [Range(-3f, 3f)] 
    [SerializeField] private float pitch = 1f;
    [SerializeField] private bool loop;
    private AudioSource source;
    
    public AudioClip Clip { get { return clip; } private set { clip = value; } }
    public float Volume { get { return volume; } private set { volume = value; } }
    public float Pitch { get { return pitch; } private set { pitch = value; } }
    public bool Loop { get { return loop; } private set { loop = value; } }
    public void Play()
    {
        if( source == null)
        {
            AudioManager.Instance.Register(this);
        }
        source.Play();
    }
    public void Stop()
    {
        if (source == null)
        {
            AudioManager.Instance.Register(this);
        }
        source.Stop();
    }
    public void SetSource(AudioSource _source)
    {
        source = _source;
    }
    public void SetVolume(float vol)
    {
        if( source == null)
        {
            AudioManager.Instance.Register(this);
        }
        source.volume = vol;
    }
    public bool IsPlaying()
    {
        if(source == null)
        {
            AudioManager.Instance.Register(this);
        }
        return source.isPlaying;
    }
}
