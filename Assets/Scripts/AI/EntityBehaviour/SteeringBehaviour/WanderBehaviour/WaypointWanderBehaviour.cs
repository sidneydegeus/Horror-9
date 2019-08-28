using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class WaypointWanderBehaviour : SteeringBehaviour {

    public WaypointWanderBehaviour(BaseEntity _entity) : base(_entity) {
    }

    public override void Init() {
        entity.wayPointInd = UnityEngine.Random.Range(0, entity.waypoints.Length);
        entity.NavAgent.updateRotation = false;
        entity.NavAgent.updatePosition = true;
    }

    public override ActionEnum Process() {
        WalkAround();
        ChangeFacingDirection();
        if (!entity.NavAgent.pathPending)
        {
            if (entity.NavAgent.remainingDistance <= entity.NavAgent.stoppingDistance)
            {
                if (!entity.NavAgent.hasPath || entity.NavAgent.velocity.sqrMagnitude == 0f)
                {
                    entity.animator.SetBool("Moving", false);
                    return ActionEnum.STATUS_COMPLETED;
                }
            }
        }

        if (entity.NavAgent.pathStatus == NavMeshPathStatus.PathPartial) {
            return ActionEnum.STATUS_FAILED;
        }
        return ActionEnum.STATUS_ACTIVE;
    }


    public void WalkAround() {
        if (Vector3.Distance(entity.transform.position, entity.waypoints[entity.wayPointInd].transform.position) >= 2) {
            entity.NavAgent.SetDestination(entity.waypoints[entity.wayPointInd].transform.position);
        }
        else if (Vector3.Distance(entity.transform.position, entity.waypoints[entity.wayPointInd].transform.position) <= 2) {
            entity.wayPointInd = UnityEngine.Random.Range(0, entity.waypoints.Length);
        }
    }
}

