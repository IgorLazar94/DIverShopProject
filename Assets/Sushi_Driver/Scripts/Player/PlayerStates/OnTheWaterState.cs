using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTheWaterState : GenericState
{
    private PlayerStateController playerStateController;
    public static bool isInWater { get; private set; }

    public OnTheWaterState(PlayerStateController playerStateController)
    {
        this.playerStateController = playerStateController;
    }

    public override void Enter()
    {
        base.Enter();
        playerSearchArea = playerStateController.ReturnSearchArea();
        ActivateSearchArea(true);
        isInWater = true;
    }

    public override void Exit()
    {
        base.Exit();
        isInWater = false;
    }

   

    public override void Update()
    {
        base.Update();
    }

}
