using System.Collections.Generic;
using UnityEngine;

public class ScrollViewDataModel
{
    public int id;
    public string userName;
    public Color color;
    public ScrollViewDataModel()
    {
        id = (int)Random.Range(1, 60000);
        userName = (Random.Range(1, 60000)).ToString();
        color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0.6f, 1.0f));

    }
}

