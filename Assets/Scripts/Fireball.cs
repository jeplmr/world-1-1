using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private float timestamp; 
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        timestamp = Time.time; 
        if (KBPlayerController._facingRight)
        {
            _rb.velocity = new Vector3(speed, 0, 0);
        } else
        {
            _rb.velocity = new Vector3(speed * -1, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        if (Time.time - timestamp >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemey"||
            collision.gameObject.tag == "Wall" ||
            collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "Head" ||
            collision.gameObject.tag == "Feet")
        {
            Destroy(gameObject);
        }
    }


}
