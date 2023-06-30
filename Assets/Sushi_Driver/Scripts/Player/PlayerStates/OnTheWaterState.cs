using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTheWaterState : GenericState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("����� � ��������� �� ����");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("����� �� ��������� �� ����");
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("� ��������� �� ����");
    }
}
