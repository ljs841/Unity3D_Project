using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDameage
{
    void OnDameage(Character sender, Character receiver, DameageInfo info);
}

public class DameageInfo
{
    public Vector3 _dameageDir;
    public float _dameageValue;
}
