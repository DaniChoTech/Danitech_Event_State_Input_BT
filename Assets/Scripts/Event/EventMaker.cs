using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaker : MonoBehaviour
{
    [SerializeField] SubsMemberOne Member_One;
    [SerializeField] SubsMemberTwo Member_Two;
    [SerializeField] Animator Animator_Player;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
    }

    private void InvokeEventTwo()
    {
        Animator_Player.SetTrigger("LevelUp");
    }

    private void InvokeEvent()
    {
        Animator_Player.SetTrigger("Atk");

        if (Member_One)
            Member_One.OnEventMakerInvoked();

        if (Member_Two)
            Member_Two.OnEventLalala();
    }
}
