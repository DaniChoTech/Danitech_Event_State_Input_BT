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
}
