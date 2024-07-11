using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 행동대장 - 실제로 어떤 행동을 하는 노드
public class PetActionNode : IBTNode
{
    Func<IBTNode.EBTNodeState> _onUpdate = null;

    public PetActionNode(Func<IBTNode.EBTNodeState> onUpdate)
    {
        _onUpdate = onUpdate;
    }
}
