
using UnityEngine;

public abstract class SteeringBehaviour : IEntityBehaviour {

    protected BaseEntity entity;

    public SteeringBehaviour(BaseEntity _entity) {
        entity = _entity;
    }

    public abstract void Init();
    public abstract ActionEnum Process();

    protected void ChangeFacingDirection() {
        Vector3 targetDir;
        if (entity.RandomWander) {
            targetDir = entity.target.position - entity.transform.position;
        } else {
            targetDir = entity.waypoints[entity.wayPointInd].transform.position - entity.transform.position;
        }
        float step = entity.WalkSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(entity.transform.forward, targetDir, step, 0.0F);
        entity.transform.rotation = Quaternion.LookRotation(newDir);
    }
}