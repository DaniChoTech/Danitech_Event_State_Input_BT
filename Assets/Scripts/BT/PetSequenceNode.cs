using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 완벽주의자 - 자식 노드를 왼쪽에서 오른쪽으로 쭉 실패할때까지 진행
public class PetSequenceNode : IBTNode
{
    List<IBTNode> _childNodeList;

    public PetSequenceNode(List<IBTNode> childNodeList)
    {
        _childNodeList = childNodeList;
    }
}
