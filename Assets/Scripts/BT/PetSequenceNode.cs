using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 완벽주의자 - 자식 노드를 왼쪽에서 오른쪽으로 쭉 실패할때까지 진행
public class PetSequenceNode : IBTNode
{
    List<IBTNode> _childNodeList;
    int _currentChild;

    public PetSequenceNode(List<IBTNode> childNodeList)
    {
        _childNodeList = childNodeList;
        _currentChild = 0;
    }

    // 자식 -> Running -> Running 반환
    // 자식 -> Success -> 다음 자식 이동
    // 자식 -> Fail -> Fail 반환
    // 시퀀스 노드는 Running이면, 그 상태를 계속 유지해야함 (ex. 플레이어를 따라갈 때까지 이동 유지)
    // 자식으로 이동하면 안되고, 다음 프레임도 그 자식 평가를 진행해야함
    public IBTNode.EBTNodeState Evaluate()
    {
        if (_childNodeList == null || _childNodeList.Count == 0)
            return IBTNode.EBTNodeState.Fail;

       for(; _currentChild < _childNodeList.Count; _currentChild++)
        {
            var childState = _childNodeList[_currentChild].Evaluate();
            switch (childState)
            {
                case IBTNode.EBTNodeState.Running:
                    return IBTNode.EBTNodeState.Running;
                case IBTNode.EBTNodeState.Fail:
                    _currentChild = 0;
                    return IBTNode.EBTNodeState.Fail;
            }
        }

        _currentChild = 0;
        return IBTNode.EBTNodeState.Success;
    }

}
