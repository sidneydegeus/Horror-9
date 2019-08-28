using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;


public class RandomWanderBehaviour : SteeringBehaviour {

    Vector3 newPos;

    public RandomWanderBehaviour(BaseEntity _entity) : base(_entity) {
    }

    public override void Init() {
        entity.animator.SetBool("Moving", true);
        entity.Running = false;
        newPos = RandomNavSphere(entity.transform.position, entity.WanderRadius, -1);
    }

    public override ActionEnum Process() {
        entity.NavAgent.SetDestination(newPos);
        if (!entity.NavAgent.pathPending) {
            if (entity.NavAgent.remainingDistance <= entity.NavAgent.stoppingDistance) {
                if (!entity.NavAgent.hasPath || entity.NavAgent.velocity.sqrMagnitude == 0f) {
                    entity.animator.SetBool("Moving", false);
                    return ActionEnum.STATUS_COMPLETED;
                }
            }
        }
        // inaccessable path
        if (entity.NavAgent.pathStatus == NavMeshPathStatus.PathPartial) {
            return ActionEnum.STATUS_FAILED;
        }

        return ActionEnum.STATUS_ACTIVE;
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}

