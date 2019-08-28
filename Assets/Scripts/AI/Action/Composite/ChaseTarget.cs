using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ChaseTarget : ActionGroup {

    public ChaseTarget() : base() { }
    public ChaseTarget(BaseEntity _entity) : base(_entity) {
        Description = "Chasing target (C)";
        List<Action> actions = new List<Action>();
        actions.Add(new FollowpathAction(entity, true));
        actions.Add(new BattlecryAction(entity));

        AddActions(actions);
    }

    override
    protected void AdditionalProcess() {
        float distance = Vector3.Distance(entity.target.position, entity.transform.position);

        if (distance < 2.0f && entity.animator.GetBool("Attack") == false) {
            AddAction(new AttackAction(entity));
        }
    }
}