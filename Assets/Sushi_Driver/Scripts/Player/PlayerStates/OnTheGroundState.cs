using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTheGroundState : GenericState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Вошёл в состояние на земле");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Вышел из состояния на земле");
    }
}
