using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class AttackBehaviour : IEntityBehaviour {

    protected BaseEntity entity;

    public AttackBehaviour(BaseEntity _entity) {
        entity = _entity;
    }

    public abstract void Init();

    public abstract ActionEnum Process();
}
