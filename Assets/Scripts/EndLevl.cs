using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EndLevl : MonoBehaviour
{

    public AudioSource music;
    public AudioClip winFanfare;
    public float delay; 

    private Animator _animator;
    private bool _triggered = false; 

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _triggered == false)
        {
            _animator.SetBool("hasWon", true);
            music.Stop();
            music.PlayOneShot(winFanfare);
            StartCoroutine(DelayedLevelLoad(delay));
            _triggered = true; 
        }
    }

    IEnumerator DelayedLevelLoad(float f)
    {
        yield return new WaitForSeconds(f);
        SceneManager.LoadScene(0);
    }

}
