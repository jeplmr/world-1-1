using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEmissionFader : MonoBehaviour
{

    public float min = 0f;
    public float max = 2f;
    public float scale = 1f;
    public Color glowColor; 

    private Renderer _rend;
    private Material _mat;
    //private Color _col;
    private float _intensity;

    //https://answers.unity.com/questions/914923/standard-shader-emission-control-via-script.html
    private const string EmissiveValue = "_EmissionScaleUI";
    private const string EmissiveColour = "_EmissionColor";

    // Start is called before the first frame update
    void Start()
    {
        _rend = GetComponent<Renderer>();
        _mat = _rend.material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        _intensity = Mathf.PingPong(Time.time * scale, max);
        Color finalColor = glowColor * _intensity; 
        _mat.SetColor("_EmissionColor", finalColor); 
    }
}
