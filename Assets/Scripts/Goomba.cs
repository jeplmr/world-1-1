using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{

    public float speed;
    public float speedLimit;

    public float pushBackScale;

    private float _scale;
    private bool _dead = false; 
    private Rigidbody _rb;
    private AudioSource _source;
    private GameObject _player; 

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _scale = transform.localScale.x;
        _source = GetComponentInChildren<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player"); 
    }

    private void FixedUpdate()
    {
        _rb.AddForce(new Vector3(speed * Time.deltaTime, 0, 0));

        if (Mathf.Abs(_rb.velocity.x) > Mathf.Abs(speedLimit))
        {
            _rb.velocity = new Vector3(speedLimit, _rb.velocity.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Feet" && !_dead)
        {
            //Die();
            transform.localScale = new Vector3(_scale, _scale, 0.02f);
            speed = 0;
            //Debug.Log("Stomped!"); 
            //other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * pushBackScale, ForceMode.Impulse);
            _player.GetComponent<Rigidbody>().velocity = Vector3.zero; 
            _player.GetComponent<Rigidbody>().AddForce(Vector3.up * pushBackScale, ForceMode.Impulse);
            _dead = !_dead;
            _source.Play();
            Collider[] junk = GetComponents<Collider>();
            foreach(Collider j in junk)
            {
                j.enabled = false; 
            }
            Destroy(gameObject, 1.0f); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Player" && !_dead)
        {
            speed = speed * -1;
            speedLimit = speedLimit * -1;
            _rb.velocity = Vector3.zero;

        }

        if (collision.gameObject.tag == "Player" && !_dead)
        {
            //HurtPlayer(); 
            _player.GetComponent<KBPlayerController>().HurtMario(); 
            speed = speed * -1;
            speedLimit = speedLimit * -1;
            _rb.velocity = Vector3.zero;
        }

        if (collision.gameObject.tag == "Fireball" && !_dead)
        {
            transform.localScale = new Vector3(_scale, _scale, 0.02f);
            speed = 0;
            _dead = !_dead;
            _source.Play();
            Collider[] junk = GetComponents<Collider>();
            foreach (Collider j in junk)
            {
                j.enabled = false;
            }
            Destroy(gameObject, 1.0f);
        }


    }

}
