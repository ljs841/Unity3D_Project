using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    protected float _logicIntevalSec;
    protected SpriteAnimationController _aniController;

    public SpriteAnimationController AniController
    {
        get
        {
            return _aniController;
        }
    }
    protected WaitForSeconds _delaySec;
    protected eEntityLookDir _currentLookDir;
    
    public virtual void Init()
    {
        if(_aniController == null)
        {
            _aniController = gameObject.GetComponent<SpriteAnimationController>();
        }
    }

    public virtual void SetEntityBehaviourData(EntityBehaviourData data) 
    {
        if(data == null)
        {
            return;
        }
        _logicIntevalSec = Util.LogicInterval(data.EntityType);
        _currentLookDir = data.BaseDir;
    }

    public virtual void ActiveBehavior(eEntityLookDir dir) { gameObject.SetActive(true); }

    public virtual void DeActiveBehavior() { gameObject.SetActive(false); }

    public virtual void SetLookDir(eEntityLookDir dir) { }

    public virtual void GetButtonEvent(eInputType inputEvenet) { }

}
