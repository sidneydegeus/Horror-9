using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : BaseEntity
{
    Door door;

    protected override void Start()
    {
        base.Start();
        thinkBehaviour = new ParasiteThinkBehaviour(this);
        if(RandomWander == true)
        {
            entityBehaviours[BehaviourEnum.WANDER_BEHAVIOUR] = new RandomWanderBehaviour(this);
        }
        else
        {
            entityBehaviours[BehaviourEnum.WANDER_BEHAVIOUR] = new WaypointWanderBehaviour(this);
        }

        entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR] = new FollowpathBehaviour(this);
        entityBehaviours[BehaviourEnum.ATTACK_BEHAVIOUR] = new ParasiteAttackBehaviour(this);
    }

    protected override void Update()
    {
        base.Update();
    }

    void OnDestroy()
    {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Door")) {
            door = other.gameObject.GetComponent<Door>();
            if (target != null) {
                if (door.Open == false && target.CompareTag("Player")) {
                    animator.SetBool("Attack", true);
                    Invoke("DestroyDoor", 1.2f);
                }
            }
        }
    }

    private void DestroyDoor() {
        door.GetDestroyed(transform.forward);
    }
}
