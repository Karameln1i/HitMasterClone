using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
   
    private Transform _path;
    private State _currentState;
    private Player _target;
    private Transform _transform;

    public State Current => _currentState;

    private void Start()
    {
        //_path = GetComponent<Player>().Path;
        //_target = GetComponent<Enemy>().Target;
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();

        if (nextState!=null)
        {
            Transit(nextState);
        }
    }
   
    private void Reset(State startState)
    {
        _currentState = startState;

        if (_currentState!=null)
        {
            _currentState.Enter(_target,_path);
        }
    }

    private void Transit(State nextState)
    {
        if (_currentState!=null)
        {
            _currentState.Exit();

            _currentState = nextState;

            if (_currentState!=null)
            {
                _currentState.Enter(_target,_path);
            }
        }
    }
}
