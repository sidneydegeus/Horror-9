using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : BaseEntity {

    protected override void Start() {
        base.Start();
        thinkBehaviour = new WolfThinkBehaviour(this);
        entityBehaviours[BehaviourEnum.WANDER_BEHAVIOUR] = new RandomWanderBehaviour(this);
        entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR] = new FollowpathBehaviour(this);
        entityBehaviours[BehaviourEnum.ATTACK_BEHAVIOUR] = new WolfAttackBehaviour(this);
    }

    protected override void Update() {
        base.Update();
    }

    void OnDestroy() {
        
    }
}
