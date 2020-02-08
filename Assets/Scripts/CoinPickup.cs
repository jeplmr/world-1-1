using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class CoinPickup : MonoBehaviour
{
    public AudioClip clip;
    public AudioMixer mixer; 

    private bool _hit = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _hit == false)
        {
            _hit = true;
            AudioSource source = new GameObject().AddComponent<AudioSource>();
            source.outputAudioMixerGroup = mixer.outputAudioMixerGroup;
            source.clip = clip;
            source.transform.position = transform.position;
            source.PlayOneShot(clip);
            Destroy(gameObject);
        }
    }
}
