using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class StateManager
{

    private BaseState _curState;
    private bool _initialized = false;

    public StateManager(BaseState initState)
    {

        Debug.Log("StateManager 최초로 생성자 실행됨 (" + initState + ")");
       _curState = initState;
        ChangeState(_curState);

        _initialized = true;
    }

    public void ChangeState(BaseState newState)
    {
        if (_initialized && newState == _curState) 
        {
            return;
        }

        if (_curState != null)
        {
            _curState.OnStateExit();
        }

        _curState = newState;
        _curState.OnStateEnter();
    }

    public void UpdateState()
    {
        if(_curState != null)
        {
            _curState.OnStateUpdate();
        }
    }
}
