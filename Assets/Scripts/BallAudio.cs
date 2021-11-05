using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(PlayerController))]
public class BallAudio : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;
    [SerializeField]
    Sound[] sounds;
    [SerializeField]
    private bool[] b;
    [SerializeField]
    public bool touchingWood 
    {
        get
        {
            return b[0];
        }
        set
        {
            if (value == b[0])
            {
                return;
            }
            WoodChanged();
            b[0] = value;
        }
    }
    [SerializeField]
    public bool touchingStone
    {
        get
        {
            return b[1];
        }
        set
        {
            if(value == b[1])
            {
                return;
            }
            StoneChanged();
            b[1] = value;
        }
    }
    [SerializeField]
    public bool touchingMetal
    {
        get
        {
            return b[2];
        }
        set
        {
            if (value == b[2])
            {
                return;
            }
            MetalChanged();
            b[2] = value;
        }
    }
    private void Awake()
    {
        b = new bool[3];
        controller = controller == null ? GetComponent(typeof(PlayerController)) as PlayerController : controller;
    }

    private void OnCollisionEnter(Collision collision)
    {
        touchingWood  = touchingWood  || collision.gameObject.tag.Contains("Wood");
        touchingStone = touchingStone || collision.gameObject.tag.Contains("Stone");
        touchingMetal = touchingMetal || collision.gameObject.tag.Contains("Metal");
        if (collision.gameObject.tag.Contains("WoodFlap") && collision.relativeVelocity.magnitude > 0.5)
        {
            Sound s = GetSound($"Hit_{controller._type.name}_WoodFlap");
            s.Play((float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, (float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, gameObject);
        }
        else if (collision.gameObject.tag.Contains("Wood") && collision.relativeVelocity.magnitude > 0.5)
        {
            Sound s = GetSound($"Hit_{controller._type.name}_Wood");           
            s.Play((float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, (float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, gameObject);
        }
        if (collision.gameObject.tag.Contains("MetalDome") && collision.relativeVelocity.magnitude > 0.5)
        {
            Sound s = GetSound($"Hit_{controller._type.name}_MetalDome");            
            s.Play((float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, (float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, gameObject);
        }
        else if (collision.gameObject.tag.Contains("Metal") && collision.relativeVelocity.magnitude > 0.5)
        {
            Sound s = GetSound($"Hit_{controller._type.name}_Metal");            
            s.Play((float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, (float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, gameObject);
        }
        if (collision.gameObject.tag.Contains("Stone") && collision.relativeVelocity.magnitude > 0.5)
        {
            Sound s = GetSound($"Hit_{controller._type.name}_Stone");            
            s.Play((float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, (float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, gameObject);
        }
        if (collision.gameObject.tag.Contains("Paper") && collision.relativeVelocity.magnitude > 0.5)
        {
            Sound s = GetSound($"Hit_{controller._type.name}_Paper");            
            s.Play((float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, (float)(-Math.Exp(-(collision.relativeVelocity.magnitude / 15f) + 4.605170186) + 200) / 100f, gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        touchingWood  = touchingWood  && !collision.gameObject.tag.Contains("Wood");
        touchingStone = touchingStone && !collision.gameObject.tag.Contains("Stone");
        touchingMetal = touchingMetal && !collision.gameObject.tag.Contains("Metal");
        if (!touchingWood)
        {
            Destroy(GetSound($"Roll_{controller._type.name}_Wood").source);
            GetSound($"Roll_{controller._type.name}_Wood").source = null;
        }
        if (!touchingStone)
        {
            Destroy(GetSound($"Roll_{controller._type.name}_Stone").source);
            GetSound($"Roll_{controller._type.name}_Stone").source = null;
        }
        if (!touchingMetal)
        {
            Destroy(GetSound($"Roll_{controller._type.name}_Metal").source);
            GetSound($"Roll_{controller._type.name}_Metal").source = null;
        }
    }
    private void WoodChanged()
    {
        Sound s = GetSound($"Roll_{controller._type.name}_Wood");
        if (touchingWood)
        {
            s.volume = (float)(-Math.Exp(-(controller.rigidbody.angularVelocity.magnitude / 15f) + 4.605170186) + 100) / 100f;
            s.pitch  = (float)(-Math.Exp(-(controller.rigidbody.angularVelocity.magnitude / 15f) + 4.605170186) + 100) / 100f;
            s.looped = true;
            s.Play(gameObject);
        }
        else
        {
            Destroy(s.source);
            s.source = null;
        }
    }
    private void StoneChanged()
    {
        Sound s = GetSound($"Roll_{controller._type.name}_Stone");
        if (touchingStone)
        {
            s.volume = (float)(-Math.Exp(-(controller.rigidbody.angularVelocity.magnitude / 15f) + 4.605170186) + 100) / 100f;
            s.pitch = (float)(-Math.Exp(-(controller.rigidbody.angularVelocity.magnitude / 15f) + 4.605170186) + 100) / 100f;
            s.looped = true;
            s.Play(gameObject);
        }
        else
        {
            Destroy(s.source);
            s.source = null;
        }
    }
    private void MetalChanged()
    {
        Sound s = GetSound($"Roll_{controller._type.name}_Metal");
        if (touchingMetal)
        {
            s.volume = (float)(-Math.Exp(-(controller.rigidbody.angularVelocity.magnitude / 15f) + 4.605170186) + 100) / 100f;
            s.pitch = (float)(-Math.Exp(-(controller.rigidbody.angularVelocity.magnitude / 15f) + 4.605170186) + 100) / 100f;
            s.looped = true;
            s.Play(gameObject);
        }
        else
        {
            Destroy(s.source);
            s.source = null;
        }
    }
    private void Update()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name.Contains("Roll"))
            {
                if(sounds[i].source == null)
                {
                    sounds[i].volume = (float)(-Math.Exp(-(controller.rigidbody.angularVelocity.magnitude / 15f) + 4.605170186) + 100) / 100f;
                    sounds[i].pitch  = (float)(-Math.Exp(-(controller.rigidbody.angularVelocity.magnitude / 15f) + 4.605170186) + 100) / 100f;
                }
                else
                {
                    sounds[i].source.volume = (float)(-Math.Exp(-(controller.rigidbody.angularVelocity.magnitude / 15f) + 4.605170186) + 100) / 100f;
                    sounds[i].source.pitch  = (float)(-Math.Exp(-(controller.rigidbody.angularVelocity.magnitude / 15f) + 4.605170186) + 100) / 100f;
                }
            }
        }
    }
    public void OnTransform()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].source != null)
            {
                Destroy(sounds[i].source);
                sounds[i].source = null;
            }
        }
    }
    private ref Sound GetSound(string name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == name)
            {
                return ref sounds[i];
            }
        }
        Debug.LogWarning($"Sound with name \"{name}\" not found!");
        return ref sounds[0];
    }
}