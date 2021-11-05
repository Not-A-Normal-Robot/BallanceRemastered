using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void PlaySound(string name)
    {
        Sound sound = null;
        for(int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == name)
            {
                sound = sounds[i];
                break;
            }
        }
        if (sound == null)
        {
            Debug.LogWarning($"Sound named \"{name}\" not found!");
            return;
        }
        PlaySound(sound);
    }
    public void PlaySound(int id)
    {
        Sound sound = null;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].id == id)
            {
                sound = sounds[i];
                break;
            }
        }
        if (sound == null)
        {
            Debug.LogWarning($"Sound with ID \"{id}\" not found!");
            return;
        }
        PlaySound(sound);
    }
    public void PlaySound(Sound sound)
    {
        sound.source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        sound.source.clip = sound.audioClip;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.looped;
        sound.source.Play();
    }
    public void StopSound(string name)
    {
        Sound sound = null;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                sound = sounds[i];
                break;
            }
        }
        if (sound == null)
        {
            Debug.LogWarning($"Sound named \"{name}\" not found!");
            return;
        }
        StopSound(sound);
    }
    public void StopSound(int id)
    {
        Sound sound = null;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].id == id)
            {
                sound = sounds[i];
                break;
            }
        }
        if (sound == null)
        {
            Debug.LogWarning($"Sound with ID \"{id}\" not found!");
            return;
        }
        StopSound(sound);
    }
    public void StopSound(Sound sound)
    {
        sound.source.Stop();
        Destroy(sound.source);
        sound.source = null;
    }
}