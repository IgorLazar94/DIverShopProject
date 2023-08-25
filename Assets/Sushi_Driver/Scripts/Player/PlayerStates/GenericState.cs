using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericState
{
    protected PlayerFOV playerFOV;

    public virtual void Enter()
    {
    }
    public virtual void Exit()
    {
    }
    public virtual void Update()
    {
    }

    protected virtual void ActivateSearchArea(bool value)
    {
        if (!value)
        {
            playerFOV.currentViewRadius = 0;
        }
        if (value)
        {
            playerFOV.currentViewRadius = playerFOV.defaultViewRadius;
        }
    }
}
