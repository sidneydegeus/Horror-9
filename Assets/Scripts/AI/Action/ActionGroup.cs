using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionGroup : Action {

    public ActionGroup() : base() {}
    public ActionGroup(BaseEntity _entity) : base(_entity) { }

    override
    public void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    override
    public void Terminate() {
        actionLinkedList.Clear();
    }

    override
    public ActionEnum Process() {
        AdditionalProcess();
   //     Debug.Log("Pre process " + CurrentAction() + " " + CurrentAction().Status);
        if (Status == ActionEnum.STATUS_FAILED) {
            Terminate();
            return Status;
        }

        if (ActionListCount() > 0) {
            Action action = CurrentAction();
            if (action.Status == ActionEnum.STATUS_INACTIVE) {
                action.Activate();
            }
            action.Process();
            if (action.Status == ActionEnum.STATUS_ONHOLD) {
                Action nextAction = NextAction();
                if (nextAction.Status == ActionEnum.STATUS_INACTIVE) {
                    nextAction.Activate();
                }
                nextAction.Process();
            }
    //        Debug.Log("post process " + CurrentAction() + " " + CurrentAction().Status);
            if (action.Status == ActionEnum.STATUS_COMPLETED || action.Status == ActionEnum.STATUS_FAILED) {
                RemoveAction();
            }
        }
        else {
            this.Status = ActionEnum.STATUS_COMPLETED;
        }


        return Status;
    }

    protected abstract void AdditionalProcess();
}