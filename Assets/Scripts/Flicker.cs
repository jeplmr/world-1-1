using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 1.0f)]
    private float _flickerVariance = 0.1f;
    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float _travelVariance = 0.01f;
    private Light _light;
    private float _originalIntensity;
    private Vector3 _originalPosition;
    private bool _switched = false; 

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        _originalIntensity = _light.intensity;
        _originalPosition = transform.position; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_switched)
        {
            _light.intensity = _originalIntensity + (Random.Range(-1 * _flickerVariance, _flickerVariance));
            transform.position = _originalPosition + (Random.insideUnitSphere * _travelVariance);
            _switched = !_switched;
        }
        _switched = !_switched;

    }
}
