using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void EnterState();
    void ExitState();
    void ExecuteOnUpdate();
}

public class IdleState : IState
{
    private readonly PlayerView _player;
    public IdleState(PlayerView player)
    {
        _player = player;
    }

    public void EnterState() { }
    public void ExecuteOnUpdate() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    public void ExitState() { }
}

public class CharacterState
{
}
