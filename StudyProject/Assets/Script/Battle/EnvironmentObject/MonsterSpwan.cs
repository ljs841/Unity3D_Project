using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwan : MonoBehaviour
{
    [SerializeField]
    List<SpawnData> _list;
    bool isSpawned = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character player = BattleManager._Instance.GetPlayer();
        if(collision.gameObject.GetInstanceID() == player.gameObject.GetInstanceID())
        {
            foreach(var data in _list)
            {
                var table = TempData.GetCharacterTable(data.index);
                if(table != null)
                {
                    Character enemy1 = (Character)EntityFactory._Instance.CreateEntityForBattle(table._name, eEntityType.InGameCharacter, table._baseLook, table._tabelNumber);
                    enemy1.transform.position = data.Pos.position;
                    enemy1.StartEntity();
                    isSpawned = true;
                }


            }
        }

        if(isSpawned)
        {
            gameObject.SetActive(false);
        }
    }
}
[System.Serializable]
public class SpawnData
{
    [SerializeField]
    public Transform Pos;
    [SerializeField]
    public int index;

}

