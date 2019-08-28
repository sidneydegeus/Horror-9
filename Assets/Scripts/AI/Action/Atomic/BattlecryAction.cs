using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlecryAction : Action {

    // in all atomics, make use of strategy pattern
    public BattlecryAction() : base() { }
    public BattlecryAction(BaseEntity _entity) : base(_entity) {
        Description = "Battlecry (A)";
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    public override void Terminate() {
        Debug.Log("Terminating action");
    }

    public override ActionEnum Process() {        
        entity.animator.SetBool("Roar", true);
        Status = ActionEnum.STATUS_COMPLETED;
        return Status;
    }
}