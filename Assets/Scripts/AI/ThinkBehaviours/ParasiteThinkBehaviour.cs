using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ParasiteThinkBehaviour : IThinkBehaviour
{
    private Parasite parasite;
    
    public ParasiteThinkBehaviour(Parasite parasite)
    {
        this.parasite = parasite;
    }

    public Action Process()
    {

        if (parasite.target != null) {
                if (parasite.think.CurrentAction().GetType() != typeof(ChaseTarget)) {
                    //FollowpathAction followpathAction = new FollowpathAction(parasite);
                    ChaseTarget chaseTarget = new ChaseTarget(parasite);
                    return chaseTarget;
                }
        }
        else {
            if (parasite.think.CurrentAction().GetType() == typeof(ChaseTarget)) {
                parasite.think.RemoveAction();
                parasite.animator.SetBool("Moving", false);
            }
        }
        return null;
    }
}