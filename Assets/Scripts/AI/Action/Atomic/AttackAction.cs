using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action
{

    // in all atomics, make use of strategy pattern
    public AttackAction() : base() { }
    public AttackAction(BaseEntity _entity) : base(_entity)
    {
        Description = "Attack (A)";
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
        Status = entity.entityBehaviours[BehaviourEnum.ATTACK_BEHAVIOUR].Process();
        return Status;
    }
}