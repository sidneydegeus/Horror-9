using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FollowpathBehaviour : SteeringBehaviour {

    public FollowpathBehaviour(BaseEntity _entity) : base(_entity) {}

    public override ActionEnum Process() {
        entity.NavAgent.SetDestination(entity.target.position);
        ChangeFacingDirection();

        if (!entity.NavAgent.pathPending) {
            if (entity.NavAgent.remainingDistance <= entity.NavAgent.stoppingDistance) {
                if (!entity.NavAgent.hasPath || entity.NavAgent.velocity.sqrMagnitude == 0f) {
                    return ActionEnum.STATUS_COMPLETED;
                }
            }
        }
        return ActionEnum.STATUS_ACTIVE;
    }

    public override void Init()
    {
    }

}
