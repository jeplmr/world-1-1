using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class DelayLevelLoad : MonoBehaviour
{

    public float loadDelay; 

    IEnumerator DelayedLoad(float f)
    {
        yield return new WaitForSeconds(f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    private void Start()
    {
        StartCoroutine(DelayedLoad(loadDelay)); 
    }

}
