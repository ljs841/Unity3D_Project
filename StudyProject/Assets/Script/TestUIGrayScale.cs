using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIGrayScale : MonoBehaviour
{
    public Image _test;
    bool _isShaderOn = true;

    public void On_Click()
    {
        if(_isShaderOn)
        {

            _isShaderOn = !_isShaderOn;
            _test.material = ShaderSeleceManager._Instance.FindMaterial(ConstValues.ConstValue._grayScaleShadeName);
        }
        else
        {
            _isShaderOn = !_isShaderOn;
            _test.material = Canvas.GetDefaultCanvasMaterial();
        }
    }

}
