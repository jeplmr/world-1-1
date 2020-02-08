using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickForce : MonoBehaviour
{

    public float force;
    public float radius;
    public float upwardsModifier;
    public float debrisLifetime; 
    public Transform origin;

    private float timeStamp; 

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody[] bricks = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in bricks)
        {
            float randi = Random.Range(0.1f, 2f); 
            rb.AddExplosionForce(force, origin.position, radius, upwardsModifier, ForceMode.Impulse);
            rb.AddTorque(new Vector3(randi, randi, randi), ForceMode.Impulse); 
        }
        timeStamp = Time.time; 
    }

    private void Update()
    {
        if (Time.time - timeStamp >= debrisLifetime)
        {
            Destroy(gameObject); 
        }
    }


}
