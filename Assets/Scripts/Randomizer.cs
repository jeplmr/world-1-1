using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public GameObject[] stuff;

    private void Start()
    {
        stuff[Random.Range(0, stuff.Length)].gameObject.SetActive(true); 
    }
}
