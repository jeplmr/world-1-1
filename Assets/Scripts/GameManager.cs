using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _timeInRound = 400;
    [SerializeField]
    private Text _timeDisplay; 
    private float _timeLeft;

    public float delayedLoad;
    public AudioSource music;
    public AudioClip diedClip;

    private bool _playerDead = false;
    private bool _hasFired = false; 

    // Start is called before the first frame update
    void Start()
    {
        _playerDead = false; 
        //_timeLeft = _timeInRound;
        //_timeDisplay = GameObject.FindGameObjectWithTag("timer text").GetComponent<Text>();
        //_timeDisplay.text = _timeLeft.ToString(); 
    }

    private void Update()
    {
        if (_playerDead == true && _hasFired == false)
        {
            PlayerHasDied(); 
            _hasFired = true; 
        }
    }


    private void PlayerHasDied()
    {
        music.Stop();
        music.PlayOneShot(diedClip); 
        StartCoroutine(DelayedLevelLoad(delayedLoad)); 
    }

    IEnumerator DelayedLevelLoad(float f)
    {
        yield return new WaitForSeconds(f);
        int level = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(level); 
    }

    public void SetPlayerStatus(bool b)
    {
        _playerDead = b; 
    }

}
