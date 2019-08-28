using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WolfAttackBehaviour : AttackBehaviour {
    public WolfAttackBehaviour(BaseEntity _entity) : base(_entity) {

    }

    public override void Init() {
        throw new NotImplementedException();
    }

    public override ActionEnum Process() {
        entity.animator.SetBool("Attack", true);
        return ActionEnum.STATUS_COMPLETED;
    }
}
