using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public float frameRate; 
    public int start = 119;
    public int end = 187;

    public Material[] mats;

    private float nextFrame = 0f;
    private int _currentFrame;
    private int _blendShapeCount;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Mesh _skinnedMesh;
    private Renderer _renderer;
    //private bool _skipFrame = true; 

    void Awake()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        _skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        _renderer = GetComponent<Renderer>(); 
    }

    void Start()
    {
        _blendShapeCount = _skinnedMesh.blendShapeCount;
        _currentFrame = start;
        SetFlagMaterial();  
    }

    void FixedUpdate()
    {
        if (Time.time > nextFrame)
        {
            nextFrame = Time.time + frameRate; 
            _skinnedMeshRenderer.GetBlendShapeWeight(_currentFrame);
            _skinnedMeshRenderer.SetBlendShapeWeight(_currentFrame, 0);
            _currentFrame++;
            if (_currentFrame >= end)
            {
                _currentFrame = start;
            }
            _skinnedMeshRenderer.SetBlendShapeWeight(_currentFrame, 100);
        }
    }

    public void SetFlagMaterial()
    {
        int i = Mathf.RoundToInt(Random.Range(0, mats.Length-1)); 
        //Debug.Log(i); 
        _renderer.material = mats[i];
    }

}
