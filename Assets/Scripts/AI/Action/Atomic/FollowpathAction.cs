
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class FollowpathAction : Action {

    bool running = false;

    public FollowpathAction() : base() { }
    public FollowpathAction(BaseEntity _entity) : base(_entity) {
        Description = "Following path (A)";
    }

    public FollowpathAction(BaseEntity _entity, bool _running) : base(_entity) {
        Description = "Following path (A)";
        running = _running;
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
        entity.entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR].Init();
        entity.Running = running;
    }

    public override ActionEnum Process() {
        Status = entity.entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR].Process();
        if (Status == ActionEnum.STATUS_COMPLETED) {
            entity.animator.SetBool("Moving", false);
            entity.Running = false;
        }
        return Status;
    }

    public override void Terminate() {
        //Status = ActionEnum.STATUS_FAILED;
    }
}

