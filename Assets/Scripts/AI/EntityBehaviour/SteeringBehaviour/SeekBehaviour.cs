using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class SeekBehaviour : SteeringBehaviour {

    public SeekBehaviour(BaseEntity _entity) : base(_entity) {}

    public override void Init()
    {
        throw new NotImplementedException();
    }

    public override ActionEnum Process() {
        return ActionEnum.STATUS_COMPLETED;
    }

}

