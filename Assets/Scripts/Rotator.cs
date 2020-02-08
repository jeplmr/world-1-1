using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField]
    [Range(0.1f, 5.0f)]
    private float _speed = 1.0f;
    [SerializeField]
    [Range(0f, 5.0f)]
    private float x = 0.0f;
    [SerializeField]
    [Range(0f, 5.0f)]
    private float y = 0.0f;
    [SerializeField]
    [Range(0f, 5.0f)]
    private float z = 0.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(x, y, z) * _speed * -1); 
    }
}
