using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IState
{
    void EnterState();
    void ExitState();
    void ExecuteOnUpdate();
    void OnInputCallback(InputAction.CallbackContext context);
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
    }

    public void ExitState() { }

    public void OnInputCallback(InputAction.CallbackContext context)
    {
        if(context.action.name == "Atk")
        {
            // 공격 Input 왔으니 여기서 상태 변경해주자
            _player.ChangeState(new AtkState(_player));
        }
    }

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
    public void OnInputCallback(InputAction.CallbackContext context) { }
}
