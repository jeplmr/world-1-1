using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public float frameRate; 
    //The cloth simulation I did in Blender does not loop cleanly. I eyeballed the loop, the below frame selections work well. 
    public int beginningFrame = 119;
    public int endingFrame = 187;
    public Material[] mats;

    private float _nextFrame = 0f;
    private int _currentFrame;
    private int _blendShapeCount;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Mesh _skinnedMesh;
    private Renderer _renderer;

    void Awake()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        _skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        _renderer = GetComponent<Renderer>(); 
    }

    void Start()
    {
        _blendShapeCount = _skinnedMesh.blendShapeCount;
        _currentFrame = beginningFrame;
        SetFlagMaterial();  
    }

    //Animte the flag by stepping through blendShapes every fixed update
    void FixedUpdate()
    {
        if (Time.time > _nextFrame)
        {
            _nextFrame = Time.time + frameRate; 
            _skinnedMeshRenderer.GetBlendShapeWeight(_currentFrame);
            _skinnedMeshRenderer.SetBlendShapeWeight(_currentFrame, 0);
            _currentFrame++;
            if (_currentFrame >= endingFrame)
            {
                _currentFrame = beginningFrame;
            }
            _skinnedMeshRenderer.SetBlendShapeWeight(_currentFrame, 100);
        }
    }

    //Pick a random flag material when the level starts
    public void SetFlagMaterial()
    {
        int i = Random.Range(0, mats.Length);
        //TIL Random.Range() is exclusive; i.e., Range(0, 10) will return a value between 0 and 9. Source: 
        //https://docs.unity3d.com/ScriptReference/Random.Range.html
        _renderer.material = mats[i];
    }

}
