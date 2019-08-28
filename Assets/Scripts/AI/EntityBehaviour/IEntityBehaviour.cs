using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityBehaviour {
    ActionEnum Process();
    void Init();
}
