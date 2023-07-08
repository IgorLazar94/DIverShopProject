using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericState
{
    protected GameObject playerSearchArea;

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
        playerSearchArea.SetActive(value);
    }

}
