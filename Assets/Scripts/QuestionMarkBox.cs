using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMarkBox : MonoBehaviour
{
    public int numberOfPrizes;
    public GameObject prize;
    public GameObject prizeSpawnPoint; 
    public Material ready;
    public Material spent;
    public AudioClip coin;
    public AudioClip hit;
    public AudioClip powerUp; 

    private Renderer _renderer;
    private Animator _animator;
    private bool _isEmpty = false;
    private AudioSource _as;


    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _animator = GetComponent<Animator>();
        _as = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Head" && _isEmpty == false)
        {
            _renderer.material = ready; //use this for hidden blocks
            _animator.Play("box");
            numberOfPrizes--;
            _as.PlayOneShot(coin);
            _as.PlayOneShot(hit);
            Instantiate(prize, prizeSpawnPoint.transform.position, Quaternion.identity); 
        }
        if (numberOfPrizes <= 0)
        {
            _isEmpty = true;
            _renderer.material = spent;
            _as.PlayOneShot(hit);
        }
    }

}
