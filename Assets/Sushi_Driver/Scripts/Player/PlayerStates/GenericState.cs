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
            playerFOV.viewRadius = 0;
        }
        if (value)
        {
            playerFOV.viewRadius = 6.5f;
        }
    }

}
