using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class Mushroom : MonoBehaviour
{

    public float speed;
    public int speedLimit;
    public int upgradeValue; 
    public AudioClip pickupSound; 

    private Rigidbody _rb;
    private bool goingRight = true;
    public AudioMixerGroup group;

    private GameObject _player; 

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //Debug.Log(group.ToString());
        _player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.AddForce(new Vector3(speed * Time.deltaTime, 0, 0));

        if (Mathf.Abs(_rb.velocity.x) > Mathf.Abs(speedLimit))
        {
            _rb.velocity = new Vector3(speedLimit, _rb.velocity.y, 0); 
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag); 

        if (collision.collider.gameObject.tag == "Wall")
        {
            speed = speed * -1;
            speedLimit = speedLimit * -1; 
            _rb.velocity = Vector3.zero;
        }

        if (collision.gameObject.tag == "Player")
        {
            AudioSource source = new GameObject().AddComponent<AudioSource>();
            source.outputAudioMixerGroup = group;
            source.PlayOneShot(pickupSound);
            _player.GetComponent<KBPlayerController>().UpgradeMario(upgradeValue); 
            Destroy(gameObject);
        }

    }
    
}
