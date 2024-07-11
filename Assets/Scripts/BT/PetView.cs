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

}
