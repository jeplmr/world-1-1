using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public GameObject cameraRig;
    public AudioSource music; 
    //public float padding = 0.1f; 

    public float lookAtOffset = 1f;
    public float scale = 1f; 

    public float paddingX = 0.5f;
    public float paddingY = 0.5f; 

    private Camera _camera; 
    private Vector3 _originalPostion;
    private float _ogFOV; 

    private float playerYaxisStartingPosition; 

    // Start is called before the first frame update
    void Start()
    {
        //_originalPostion = new Vector3(0, transform.position.y, transform.position.z);
        _originalPostion = transform.position; 
        _camera = GetComponent<Camera>();
        playerYaxisStartingPosition = player.transform.position.y;
        //Debug.Log(_originalPostion);
        _ogFOV = _camera.fieldOfView; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        NewFollow(); 
    }

    public void ResetCameraPosition()
    {
        transform.position = _originalPostion;// + new Vector3(-3, 0, 0);
    }

    private void OldFollow()
    {
        //Debug.Log(_camera.WorldToViewportPoint(player.transform.position).x);
        if (_camera.WorldToViewportPoint(player.transform.position).x >= 0.5f)
        {
            transform.position = new Vector3(player.transform.position.x, 0, 0) + _originalPostion;
        }
    }


    private void NewFollow()
    {
        /*
        if (_camera.WorldToViewportPoint(player.transform.position).x >= paddingX)
        {

        }
        */





        //transform.position = new Vector3(player.transform.position.x, 0, 0) + _originalPostion;
        /*
        if (player.transform.position.y > playerYaxisStartingPosition)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ((player.transform.position.y / playerYaxisStartingPosition) + _originalPostion.y) * scale);
        }
        */
        transform.position = new Vector3(player.transform.position.x, _originalPostion.y + (player.transform.position.y - playerYaxisStartingPosition), _originalPostion.z);
        _camera.fieldOfView = ((player.transform.position.y/playerYaxisStartingPosition) * scale) + _ogFOV;
        float difference = player.transform.position.y - playerYaxisStartingPosition;
        music.volume = 1.0f - (difference * 0.05f);


        
        if (cameraRig.transform.position.y <= 5.25f)
        {
            cameraRig.transform.position = new Vector3(cameraRig.transform.position.x, _originalPostion.y, cameraRig.transform.position.z);
            _camera.fieldOfView = 37.7f;
        }
        




        //transform.position = new Vector3(player.transform.position.x, _originalPostion.y, (_originalPostion.z - (player.transform.position.y / playerYaxisStartingPosition) * scale)); 
        //_cameraGO.transform.LookAt(player.transform.position + new Vector3(0, lookAtOffset, 0));

        /*
        if (_camera.WorldToViewportPoint(player.transform.position).y >= paddingY)
        {
            //transform.position = new Vector3(0, player.transform.position.y, 0) + _originalPostion;

        }
        */
    }

}
