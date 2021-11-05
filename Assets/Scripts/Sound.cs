using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public int id;

    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch = 1.0f;
    public bool looped = false;
    [HideInInspector]
    public AudioSource source = null;
    public void Play(float volume, float pitch, GameObject gameObject = null)
    {
        if(source == null)
        {
            if(gameObject == null)
            {
                Debug.LogError("gameObject not provided");
                return;
            }
            source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }
        source.clip = audioClip;
        source.volume = volume;
        source.pitch = pitch;
        source.time = 0f;
        source.loop = looped;
        source.Play();
    }
    public void Play(GameObject gameObject = null)
    {
        if (source == null)
        {
            if (gameObject == null)
            {
                Debug.LogError("gameObject not provided");
                return;
            }
            source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }
        source.clip = audioClip;
        source.volume = volume;
        source.pitch = pitch;
        source.time = 0f;
        source.loop = looped;
        source.Play();
    }
}