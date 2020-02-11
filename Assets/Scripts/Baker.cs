using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If memory serves, I'm not using this script at all. Look into and remove if unused. 
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
