using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioLookAround : MonoBehaviour
{

    public float min;
    public float max;
    public float scale; 
    public GameObject thingToLookAt;

    private Rigidbody _rb;
    private Quaternion _ogQ; 

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _ogQ = transform.rotation;
        StartCoroutine(ChangeLookAtTarget());
    }


    private IEnumerator ChangeLookAtTarget()
    {
        for (; ; )
        {

            Vector3 target = Random.insideUnitSphere;
            transform.LookAt(thingToLookAt.transform.position + (target * scale));
            yield return new WaitForSeconds(Random.Range(min, max));
        }
    }
    
    private void Update()
    {
        if (_rb.velocity.x > 0)
        {
            transform.rotation = _ogQ; 
        }
    }

}
