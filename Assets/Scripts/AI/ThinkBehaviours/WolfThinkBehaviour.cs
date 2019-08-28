using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WolfThinkBehaviour : IThinkBehaviour {
    private Wolf wolf;

    public WolfThinkBehaviour(Wolf wolf) {
        this.wolf = wolf;
    }
    public Action Process() {
        float distance = Vector3.Distance(wolf.target.position, wolf.transform.position);

        if (distance <= 20) {
            FollowpathAction followpathAction = new FollowpathAction(wolf, true);
            return followpathAction;
        }
        if (wolf.think.ActionListCount() == 0) {
            WanderAction wanderAction = new WanderAction(wolf);
            return wanderAction;
        }
        return null;
    }
}