using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBTNode
{
    public enum EBTNodeState
    {
        Success,
        Fail,
        Running
    }

    public EBTNodeState Evaluate();
}
