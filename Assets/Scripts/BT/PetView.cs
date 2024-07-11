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
