using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Player;

public class TrainingZone : GenericBuild
{
    public static Action OnHarpoonUpdateParameter;
    public static Action OnBackpackUpdateParameter;
    public static Action OnSpeedUpdateParameter;
    public static Action OnFOVUpdateParameter;

    [SerializeField] private UIController uIController;
    public void Enter()
    {
        uIController.ShowTrainingPanel();
        PlayerMovementControl.onPlayerStopped.Invoke();
    }

    public void Exit()
    {
        uIController.HideTrainingPanel();
    }
}
