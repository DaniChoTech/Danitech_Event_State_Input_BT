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
            _player.ChangeState(new AtkState(_player));
        }
    }

    public void ExitState() { }
}

public class AtkState : IState
{
    private readonly PlayerView _player;
    public AtkState(PlayerView player)
    {
        _player = player;
    }

    public void EnterState() 
    {
    
    }
    public void ExecuteOnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
    }

    public void ExitState() { }
}
