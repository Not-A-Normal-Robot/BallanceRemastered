using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject initParticles;
    public GameObject[] onParticles;
    public new AudioClip audio;
    private void Awake()
    {
        initParticles.SetActive(true);
        for(int i = 0; i < onParticles.Length; i++)
        {
            onParticles[i].SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(initParticles);
            for(int i = 0; i < onParticles.Length; i++)
            {
                onParticles[i].SetActive(true);
            }
            (other.GetComponent(typeof(PlayerController)) as PlayerController).respawnPos = transform.position;
            AudioSource.PlayClipAtPoint(audio, transform.position);
            Destroy(gameObject);
        }
    }
}
