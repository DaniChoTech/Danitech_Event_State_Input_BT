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

    public void EnterState() 
    {
        _player.TextMesh_Level.text = "기본";
    }

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
        _player.Animator_Player.SetTrigger("Atk");
        _player.TextMesh_Level.text = "공격";
    }

    public void ExecuteOnUpdate()
    {
        var animInfo = _player.Animator_Player.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime > 1)
        {
            _player.ChangeState(new IdleState(_player));
        }
    }

    public void ExitState() { }
}
