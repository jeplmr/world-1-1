using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCoinBox : MonoBehaviour
{

    public int numberOfCoins;
    public AudioClip coinSound;
    public GameObject spentBlock;
    public GameObject coinSpawnPoint;
    public GameObject coinPrize; 

    private AudioSource _source;
    private Animator _animator; 


    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Head" && numberOfCoins > 0)
        {
            _source.Play();
            _source.PlayOneShot(coinSound);
            _animator.Play("bricks");
            Instantiate(coinPrize, coinSpawnPoint.transform.position, Quaternion.identity);
            numberOfCoins--; 
        }
        if (numberOfCoins <= 0)
        {
            spentBlock.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
