using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingZone : GenericBuild
{
    [SerializeField] private UIController uIController;
    public void Enter()
    {
        uIController.ShowTrainingPanel();
    }

    public void Exit()
    {
        uIController.HideTrainingPanel();
    }
}
