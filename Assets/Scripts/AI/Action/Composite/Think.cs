using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Think : ActionGroup {

    public Think() : base() { }
    public Think(BaseEntity _entity) : base(_entity) {
        Activate();
        Description = "Thinking (C)";
    }

    override
    protected void AdditionalProcess() {
        if (ActionListCount() == 0) {
            AddAction(new WanderAction(entity));           
        }
        Action action = entity.thinkBehaviour.Process();
        if (action != null) {
            CurrentAction().Status = ActionEnum.STATUS_INACTIVE;
            AddAction(action);
        }
    }
}