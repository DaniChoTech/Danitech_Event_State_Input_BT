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

   
}
