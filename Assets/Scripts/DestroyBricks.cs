using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBricks : MonoBehaviour
{
    public GameObject brickDebris;
    //public AudioClip clip;

    private bool _hit = false;
    private Animator _animator;
    private AudioSource _source;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _source = GetComponentInChildren<AudioSource>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Head" && KBPlayerController.isSuper && _hit == false)
        {
            Instantiate(brickDebris, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GetComponentInChildren<MeshRenderer>().enabled = false; 
            Destroy(gameObject, 0.1f);
            _hit = true; 
        } else if (other.gameObject.tag == "Head" && KBPlayerController.isSuper == false)
        {
            _animator.Play("bricks");
            _source.Play(); 
        }
    }
}
