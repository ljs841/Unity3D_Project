using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character player = BattleManager._Instance.GetPlayer();
        if (collision.gameObject.GetInstanceID() == player.gameObject.GetInstanceID())
        {

            BattleManager._Instance.StageEnd();
        }

    }
}
