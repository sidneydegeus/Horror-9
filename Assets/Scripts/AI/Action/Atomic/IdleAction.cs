using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : Action
{

    // in all atomics, make use of strategy pattern
    public IdleAction() : base() { }
    public IdleAction(BaseEntity _entity) : base(_entity)
    {
        Description = "Idle (A)";
    }

    public override void Activate()
    {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    public override void Terminate()
    {
        throw new NotImplementedException();
    }

    public override ActionEnum Process()
    {
        Status = ActionEnum.STATUS_COMPLETED;
        return Status;
    }
}