using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ParasiteAttackBehaviour : AttackBehaviour {
    public ParasiteAttackBehaviour(BaseEntity _entity) : base(_entity) {

    }

    public override void Init() {
       
    }

    public override ActionEnum Process() {
        entity.animator.SetBool("Attack", true);
        Player player = entity.target.GetComponent<Player>();
        player.TakeDamage(25);
        return ActionEnum.STATUS_COMPLETED;
    }


}
