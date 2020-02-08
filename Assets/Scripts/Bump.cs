using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour
{
    public AudioClip coinClip; 

    private AudioSource _source; 

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.PlayOneShot(coinClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Head")
        {
            _source.Play(); 
        }
    }
}
