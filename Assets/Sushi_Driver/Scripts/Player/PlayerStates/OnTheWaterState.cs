using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTheWaterState : GenericState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Вошёл в состояние на воде");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Вышел из состояния на воде");
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("В состоянии на воде");
    }
}
