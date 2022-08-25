using UnityEngine;

[CreateAssetMenu(fileName ="New Sound",menuName ="Sound")]
public class Sound : ScriptableObject
{
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(-3f, 3f)] public float pitch = 1f;
    public bool loop;

    public AudioSource source;
    public void Play()
    {
        if( source == null)
        {
            Debug.Log(clip.name + "not register yet");
            return;
        }
        source.Play();
    }
    public void Stop()
    {
        if (source == null)
        {
            Debug.Log(clip.name + "not register yet");
            return;
        }
        source.Stop();
    }
}
