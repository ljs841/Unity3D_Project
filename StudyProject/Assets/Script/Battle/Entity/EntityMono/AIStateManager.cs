using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{

    [SerializeField]
    protected float _updateDealy;
    public float UpdateDealy
    {
        get
        {
            return _updateDealy;
        }
        
    }
    protected eAiState _currentAI;
    protected Character _unit;
    protected Character _target;
    protected WaitForSeconds _waitSec;
    protected AnimationStateManager _stateManager;

    private void Awake()
    {
        _updateDealy = 0.5f;
        _currentAI = eAiState.Idle;
        _waitSec = new WaitForSeconds(_updateDealy);
    }

    public void Init(Character unit)
    {
        _unit = unit;
        _stateManager = _unit.StateManager;
    }

    public void StartAi()
    {
        _target = BattleManager._Instance.CurrentPlayer;
        _currentAI = eAiState.Idle;
        StartCoroutine(coAIUpdate());
    }

    public void StopAI()
    {
        StopCoroutine(coAIUpdate());
    }


    IEnumerator coAIUpdate()
    {
        while(true)
        {
            eAnimationStateName curState = CurrentStateName();
            switch (_currentAI)
            {
                case eAiState.Idle:
                    //인식 범위
                    if (CalDistance() > _unit.Stat.AppreciateDistance)
                    {
                        if (HasState(eAnimationStateName.Hide))
                        {
                            ChangeState(eAnimationStateName.Hide);
                        }
                    }
                    else
                    {
                        CalDirToTarget();
                        switch (curState)
                        {
                            case eAnimationStateName.Idle:
                               if (HasState(eAnimationStateName.Run))
                                {
                                    ChangeState(eAnimationStateName.Run);
                                    _currentAI = eAiState.MoveToTarget;
                                }
                                else if (HasState(eAnimationStateName.Attack))
                                {
                                    ChangeState(eAnimationStateName.Attack);
                                    _currentAI = eAiState.Attack;
                                }
                                break;
                            case eAnimationStateName.Hide:
                                if (HasState(eAnimationStateName.Rise))
                                {
                                    ChangeState(eAnimationStateName.Rise);
                                    _currentAI = eAiState.MoveToTarget;
                                }
                                break;

                        }
                    }
                  
                    break;
                case eAiState.MoveToTarget:
                    if (CalDistance() > _unit.Stat.AttackRange)
                    {
                        if (HasState(eAnimationStateName.Run))
                        {
                            CalDirToTarget();
                            ChangeState(eAnimationStateName.Run);
                        }
                    }
                    else
                    {
                        CalDirToTarget();
                        switch (curState)
                        {
                            case eAnimationStateName.Run:
                                if (HasState(eAnimationStateName.Attack))
                                {
                                    ChangeState(eAnimationStateName.Attack);
                                    _currentAI = eAiState.Attack;
                                }
                                else
                                {
                                    ChangeState(eAnimationStateName.Idle);
                                    _currentAI = eAiState.Idle;
                                }
                                break;
                        }
                    }
                    
                    break;
                case eAiState.Attack:
                    if (CalDistance() > _unit.Stat.AttackRange)
                    {
                        ChangeState(eAnimationStateName.Idle);
                        _currentAI = eAiState.Idle;
                    }
                    else
                    {
                        switch (curState)
                        {
                            case eAnimationStateName.Idle:
                                if (HasState(eAnimationStateName.Attack) && CurrentStateName() != eAnimationStateName.Attack)
                                {
                                    CalDirToTarget();
                                    ChangeState(eAnimationStateName.Attack);
                                }
                                break;
                        }
                    }
                   
                    break;

            }
            yield return _waitSec;
        }
     
    }

    public bool HasChange(eAnimationStateName name)
    {
        if(HasState(name))
        {
            ChangeState(name);
            return true;
        }
        return false;
    }
    
    public float CalDistance()
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = _target.gameObject.transform.position;
        return Vector3.Distance(pos, targetPos);
    }

    public eAnimationStateName CurrentStateName()
    {
        return _stateManager.GetCurrentState();
    }

    public bool HasState(eAnimationStateName name)
    {
        if (_stateManager == null)
            return false;
        return _stateManager.HasState(name);
    }

    public void CalDirToTarget()
    {
        var forwardVec = _target.transform.position - _unit.transform.position;
        forwardVec.y = 0;
        _unit.SetForward(forwardVec);
        _unit.SetReverseVelocity(forwardVec);
    }

    public void ChangeState(eAnimationStateName name)
    {
        if (CurrentStateName() == name)
        {
            return;
        }
        _stateManager.ChangeState(name);
    }

    public void OnChangeState(eAnimationStateName changeName)
    {

    }

}