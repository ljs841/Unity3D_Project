using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public float yOffset;
    Character _char;
    void Start()
    {
        _char = BattleManager._Instance.CurrentPlayer;
    }

    private void Update()
    {
        Vector3 pos = transform.position;

        pos.x = _char.gameObject.transform.position.x;
        pos.y = _char.gameObject.transform.position.y + yOffset;
        transform.position = pos;
    }
}
