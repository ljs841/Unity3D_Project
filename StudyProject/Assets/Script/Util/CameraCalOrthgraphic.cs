using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCalOrthgraphic : MonoBehaviour
{
    public Camera _camera;
    public int _pixelPerUnit;
    public float _scaleOffSet;
    // Start is called before the first frame update
    void Awake()
    {
        float x = (float)Screen.width;
        float y = (float)Screen.height;
        float Size = x / (((x / y) * 2) * _pixelPerUnit);
        Size *=  _scaleOffSet;
        _camera.orthographicSize = Size;
    }
}
