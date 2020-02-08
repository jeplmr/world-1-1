using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPrize : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private float _timestamp;

    private void Start()
    {
        _timestamp = Time.time; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Translate(Vector3.up * speed);
        if (Time.time - _timestamp > lifetime)
        {
            Destroy(gameObject); 
        }
    }
}
