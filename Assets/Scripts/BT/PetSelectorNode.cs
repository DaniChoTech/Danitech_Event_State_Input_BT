using System;
using System.Collections.Generic;

// 기회주의자 - 자식 중에 진행, 성공한 것 까지만 진행
public class PetSelectorNode : IBTNode
{
    List<IBTNode> _childNodeList;

    public PetSelectorNode(List<IBTNode> childNodeList)
    {
        _childNodeList = childNodeList;
    }

    // 자식 -> Running이면 Running 반환
    // 자식 -> Success이면 Success반환
    // 자식 -> Fail이면 다음 다식 실행
    public IBTNode.EBTNodeState Evaluate()
    {
        if(_childNodeList == null)
        {
            return IBTNode.EBTNodeState.Fail;
        }

        foreach(var child in _childNodeList)
        {
            // 자식 노드의 상태를 가져옴
            var childState = child.Evaluate();
            switch (childState)
            {
                case IBTNode.EBTNodeState.Running:
                    return IBTNode.EBTNodeState.Running;
                case IBTNode.EBTNodeState.Success:
                    return IBTNode.EBTNodeState.Success;
            }
        }

        return IBTNode.EBTNodeState.Fail;
    }
}
