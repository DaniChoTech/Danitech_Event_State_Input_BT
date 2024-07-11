using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBTRunner
{
    IBTNode _rootNode;

    public PetBTRunner(IBTNode rootNode)
    {
        _rootNode = rootNode;
    }

    public void Execute()
    {
        _rootNode.Evaluate();
    }
}

public class PetView : MonoBehaviour
{
    PetBTRunner _petBtRunner;
    GameObject _foundedMob; // 임시처리

    [SerializeField] GameObject _player;
    [SerializeField] Animator _animator;


    private void Awake()
    {
        _petBtRunner = new PetBTRunner(SetPetBT());
    }

    private void Update()
    {
        _petBtRunner.Execute();
    }

    IBTNode SetPetBT()
    {
        var followNodeList = new List<IBTNode>();
        followNodeList.Add(new PetActionNode(CheckFollowingOnUpdate));
        followNodeList.Add(new PetActionNode(CheckPetFollowingRangeOnUpdate));
        followNodeList.Add(new PetActionNode(CompleteFollowOnUpdate));

        var followSeqNode = new PetSequenceNode(followNodeList);

        var patrolNodeList = new List<IBTNode>();
        patrolNodeList.Add(new PetActionNode(PatrolEnemyOnUpdate));
        patrolNodeList.Add(new PetActionNode(MoveToEnemyOnUpdate));

        var pattrolSeqNode = new PetSequenceNode(patrolNodeList);

        List<IBTNode> firstSelectNode = new List<IBTNode>();
        firstSelectNode.Add(followSeqNode);
        firstSelectNode.Add(pattrolSeqNode);
        // firstSelectNode.Add(new PetActionNode(Test));

        var firstNode = new PetSelectorNode(firstSelectNode);
        return firstNode;
    }

   
    // 따라오기 ============================================
    public IBTNode.EBTNodeState CheckFollowingOnUpdate()
    {
        if (_foundedMob != null)
            return IBTNode.EBTNodeState.Fail;

        var distance = Vector3.Distance(_player.transform.position, this.transform.position);

        if (distance < 0.5f)
        {
            return IBTNode.EBTNodeState.Fail;
        }

        return IBTNode.EBTNodeState.Success;
    }

    private void MoveToTargetPosition(Transform originTransform, Transform targetTransform)
    {
        Vector3 direction = (targetTransform.position - originTransform.position).normalized;
        Vector3 move = direction * 1.0f * Time.deltaTime;
        originTransform.LookAt(targetTransform.transform);
        originTransform.position += move;
    }

    public IBTNode.EBTNodeState CheckPetFollowingRangeOnUpdate()
    {
        var distance = Vector3.Distance(_player.transform.position, this.transform.position);

        if (distance > 0.5f)
        {
            MoveToTargetPosition(this.transform, _player.transform);
            return IBTNode.EBTNodeState.Running;
        }

        _animator.SetTrigger("Atk");
        return IBTNode.EBTNodeState.Success;
    }

    public IBTNode.EBTNodeState CompleteFollowOnUpdate()
    {
        if (IsAnimationRunning("Atk"))
        {
            return IBTNode.EBTNodeState.Running;
        }

        _animator.CrossFade("Atk", 0.1f);
        // _animator.SetTrigger("LevelUp");
        return IBTNode.EBTNodeState.Success;
    }

    public IBTNode.EBTNodeState PatrolEnemyOnUpdate()
    {
        var overlapCollider = Physics.OverlapSphere(this.transform.position, 10.0f, LayerMask.GetMask("Monster"));
        if(overlapCollider != null && overlapCollider.Length > 0)
        {
            var firstCheckMob = overlapCollider[0].transform;
            _foundedMob = firstCheckMob.gameObject;
            
            var distance = Vector3.Distance(_foundedMob.transform.position, this.transform.position);
            if(distance < 0.5f)
            {
                _foundedMob = null;
                return IBTNode.EBTNodeState.Fail;
            }

            return IBTNode.EBTNodeState.Success;
        }
        return IBTNode.EBTNodeState.Fail;
    }

    public IBTNode.EBTNodeState MoveToEnemyOnUpdate()
    {
        if(_foundedMob == null)
        {
            return IBTNode.EBTNodeState.Fail;
        }

        var distance = Vector3.Distance(_foundedMob.transform.position, this.transform.position);

        if (distance > 0.5f)
        {
            MoveToTargetPosition(this.transform, _foundedMob.transform);
            return IBTNode.EBTNodeState.Running;
        }

        _foundedMob = null;
        return IBTNode.EBTNodeState.Success;
    }

    #region Util
    private bool IsAnimationRunning(string animName)
    {
        if (_animator == null)
            return false;

        bool isRunning = false;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            var normalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            isRunning = normalizedTime != 0 && normalizedTime < 1.0f;
        }

        return isRunning;
    }
    #endregion
}


