using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baker : MonoBehaviour
{
    private GameObject[] _probes; 

    // Start is called before the first frame update
    void Start()
    {
        _probes = GameObject.FindGameObjectsWithTag("probes");
        foreach(GameObject probe in _probes)
        {
            ReflectionProbe p = probe.GetComponent<ReflectionProbe>();
            //Debug.Log("Baking probe!"); 
            p.RenderProbe(); 
        }
    }

}
